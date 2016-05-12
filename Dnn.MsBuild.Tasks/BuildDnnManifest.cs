// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildDnnManifest.cs" company="XCESS expertise center b.v.">
//     Copyright (c) 2016-2016 XCESS expertise center b.v.
// 
//     Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//     documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//     the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
//     to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
//     The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//     of the Software.
// 
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//     TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//     THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//     CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//     DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Dnn.MsBuild.Tasks.Components;
using Dnn.MsBuild.Tasks.Composition;
using Dnn.MsBuild.Tasks.Composition.Component;
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Entities.FileTypes;
using Dnn.MsBuild.Tasks.Entities.Internal;
using Dnn.MsBuild.Tasks.Parsers;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Dnn.MsBuild.Tasks
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Microsoft.Build.Utilities.Task" />
    public class BuildDnnManifest : Task
    {
        public const string BuildFolder = @".build\";

        public const string NuGetPackagesFile = "packages.config";

        #region Overrides of Task

        /// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            var taskResult = false;
            var taskData = this.CreateTaskData();

            using (var buildTask = new BuildManifestTask<DnnManifest>(taskData))
            {
                var manifest = buildTask.Build();
                var package = manifest.Packages.FirstOrDefault();

                // ReSharper disable once AssignmentInConditionalExpression
                // ReSharper disable once InvertIf
                if (taskResult = (package != null))
                {
                    manifest.Extension = this.DnnManifestExtension ?? DnnManifest.DefaultManifestExtension;

                    var serializeTask = new SerializeManifestTask<DnnManifest>();
                    serializeTask.Execute(manifest);

                    // Fill the output parameters
                    this.ManifestFileName = manifest.FileName;

                    this.Assemblies = GetOutputParameterAssemblies(package);
                    this.License = package.License.FilePath;
                    this.ReleaseNotes = package.ReleaseNotes.FilePath;
                    this.ResourceFiles = GetOutputParameterResourceFiles(taskData, package);
                }
            }

            return taskResult;
        }

        #endregion

        #region Helpers

        private IProjectFileData CreateProjectPackageData()
        {
            var parser = new XmlVsProjectFileParser();
            var projectDataFile = parser.Parse(this.ProjectFile);
            projectDataFile.UserControls = GetUserControls(projectDataFile.BasePath, projectDataFile.ResourceFiles);

            return projectDataFile;
        }

        private ITaskData CreateTaskData()
        {
            var dnnAssemblyPath = this.DnnAssemblyPath;
            if (string.IsNullOrWhiteSpace(dnnAssemblyPath))
            {
                dnnAssemblyPath = GetDnnAssemblyPathFromProjectFile(this.ProjectFile);
            }

            var projectFileData = this.CreateProjectPackageData();
            return new TaskData(projectFileData, this.ProjectTargetAssembly, dnnAssemblyPath);
        }

        private static string GetDnnAssemblyPathFromProjectFile(string projectFile)
        {
            var dnnAssemblyPath = string.Empty;
            var desktopModuleRegex = new Regex($@"(?i)\b{ModuleComponentBuilder.DesktopModuleFolderName}\b");
            var match = desktopModuleRegex.Match(projectFile);

            if (match.Success)
            {
                dnnAssemblyPath = Path.Combine(projectFile.Substring(0, match.Index), "bin");
            }

            return dnnAssemblyPath;
        }

        private static string[] GetOutputParameterAssemblies(DnnPackage package)
        {
            // Find the assembly component in the manifest
            var assemblyComponent = package?.Components
                                            .OfType<DnnComponentAssembly>()
                                            .FirstOrDefault();

            // Return all registered assemblies in the component.
            return assemblyComponent?.Assemblies
                                     .Select(arg => Path.Combine(arg.Path, arg.Name))
                                     .ToArray() ?? new string[0];
        }

        private static string[] GetOutputParameterResourceFiles(ITaskData taskData, DnnPackage package)
        {
            // Gather all relevant resource files
            return taskData.ProjectFileData
                           .ResourceFiles
                           .Select(arg => Path.Combine(arg.Path, arg.Name))
                           .Where(arg => ResourceFilePredicate(arg, package))
                           .ToArray();
        }

        private static IDictionary<string, string> GetUserControls(string basePath, IEnumerable<IFileInfo> resourceFiles)
        {
            var parser = new UserControlParser();
            var userControlFiles = resourceFiles.Where(arg => arg.Name.EndsWith(UserControlParser.UserControlFileExtension))
                                                .Select(arg => Path.Combine(basePath, arg.Path ?? string.Empty, arg.Name))
                                                .ToList();

            return parser.Parse(userControlFiles);
        }

        private static bool ResourceFilePredicate(string filePath, DnnPackage package)
        {
            // TODO: Use some kind of config/xml file like .GITIGNORE to exclude files instead of a hardcoded list...
            var includeResource = !filePath.Equals(package.License.FilePath) && // Exclude the license 
                                  !filePath.Equals(package.ReleaseNotes.FilePath) && // Exclude the releasenotes
                                  !filePath.EndsWith(NuGetPackagesFile, StringComparison.InvariantCultureIgnoreCase) && // Exclude the NuGet packages.config files
                                  !filePath.StartsWith(BuildFolder, StringComparison.InvariantCultureIgnoreCase); // Exclude any files in the .build folder
            return includeResource;
        }

        #endregion

        #region Task Properties

        /// <summary>
        /// Gets or sets the DNN assembly path.
        /// </summary>
        /// <value>
        /// The DNN assembly path.
        /// </value>
        public string DnnAssemblyPath { get; set; }

        /// <summary>
        /// Gets or sets the DNN manifest extension.
        /// </summary>
        /// <value>
        /// The DNN manifest extension.
        /// </value>
        public string DnnManifestExtension { get; set; }

        /// <summary>
        /// Gets or sets the project file.
        /// </summary>
        /// <value>
        /// The project file.
        /// </value>
        [Required]
        public string ProjectFile { get; set; }

        /// <summary>
        /// Gets or sets the project target assembly.
        /// </summary>
        /// <value>
        /// The project target assembly.
        /// </value>
        [Required]
        public string ProjectTargetAssembly { get; set; }

        /// <summary>
        /// Gets or sets the name of the install file.
        /// </summary>
        /// <value>
        /// The name of the install file.
        /// </value>
        [Output]
        public string ManifestFileName { get; protected set; }

        /// <summary>
        /// Gets or sets a list of filenames with a relative path of assemblies to should be included in the installation package.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        [Output]
        public string[] Assemblies { get; protected set; }

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        /// <value>
        /// The license.
        /// </value>
        [Output]
        public string License { get; protected set; }

        /// <summary>
        /// Gets or sets the release notes.
        /// </summary>
        /// <value>
        /// The release notes.
        /// </value>
        [Output]
        public string ReleaseNotes { get; protected set; }

        /// <summary>
        /// Gets or sets a list of filename with a relative path of all resource files that should be included in the installation package.
        /// </summary>
        /// <value>
        /// The resource files.
        /// </value>
        [Output]
        public string[] ResourceFiles { get; protected set; }

        #endregion
    }
}