// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildManifestTask.cs" company="XCESS expertise center b.v.">
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
using System.Reflection;
using System.Xml.Serialization;
using Dnn.MsBuild.Tasks.Composition;
using Dnn.MsBuild.Tasks.Composition.Manifest;
using Dnn.MsBuild.Tasks.Entities.Internal;
using Dnn.MsBuild.Tasks.Parsers;
using Microsoft.Build.Framework;

namespace Dnn.MsBuild.Tasks
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TManifest">The type of the manifest.</typeparam>
    public class BuildManifestTask<TManifest>
        where TManifest : IManifest, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildManifestTask{TManifest}" /> class.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="packageAssembly">The manifest assembly.</param>
        /// <param name="dnnAssemblyPath">The DNN assembly path.</param>
        public BuildManifestTask(string projectFile, string packageAssembly, string dnnAssemblyPath)
        {
            // Gather the package data needed for building the manifest
            this.TaskData = new TaskData(packageAssembly, dnnAssemblyPath);

            // Before assigning the UserControls to the TaksData, ensure that the Assembly locators are correctly setup.
            this.SetupDnnAssemblyLocator();

            this.TaskData.ProjectFileData = this.GetProjectPackageData(projectFile);
            this.TaskData.ProjectFileData.UserControls = this.GetUserControls(this.TaskData.ProjectFileData);
        }

        #endregion

        private ITaskData TaskData { get; }

        public IManifest Build()
        {
            var manifest = this.BuildManifest();
            if (manifest != null)
            {
                this.SerializeManifest(manifest);
            }

            return manifest;
        }

        private IManifest BuildManifest()
        {
            var builder = new ManifestBuilder<TManifest>();
            return builder.Build(this.TaskData);
        }

        private void SerializeManifest(IManifest manifest)
        {
            var serializer = new XmlSerializer(manifest.GetType());
            using (var stream = new StreamWriter(manifest.FileName))
            {
                serializer.Serialize(stream, manifest);
            }
        }

        private IProjectFileData GetProjectPackageData(string projectFile)
        {
            var parser = new XmlVsProjectFileParser();
            return parser.Parse(projectFile);
        }

        private IDictionary<string, string> GetUserControls(ITaskItem[] sourceFiles)
        {
            var parser = new UserControlParser();
            var userControlFiles = sourceFiles.Select(arg => arg.ItemSpec)
                                              .ToList();

            return parser.Parse(userControlFiles);
        }

        private IDictionary<string, string> GetUserControls(IProjectFileData projectFileData)
        {
            var basePath = projectFileData.BasePath;
            var parser = new UserControlParser();
            var userControlFiles = projectFileData.ResourceFiles
                                                  .Where(arg => arg.Name.EndsWith(UserControlParser.UserControlFileExtension))
                                                  .Select(arg => Path.Combine(basePath, arg.Name))
                                                  .ToList();

            return parser.Parse(userControlFiles);
        }

        /// <summary>
        /// Setups the DNN assembly locator.
        /// </summary>
        private void SetupDnnAssemblyLocator()
        {
            AppDomain.CurrentDomain.AssemblyResolve += this.CurrentDomainOnAssemblyResolve;
        }

        /// <summary>
        /// Assembly resolve handler for the current AppDomain.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        private Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var fileNameParts = args.Name.Split(',');

            // ReSharper disable once InvertIf
            if (fileNameParts.First().StartsWith("dotnetnuke", StringComparison.InvariantCultureIgnoreCase))
            {
                var assemblyToLoad = Path.Combine(this.TaskData.DnnAssemblyPath, fileNameParts.First() + ".dll");
                return Assembly.LoadFrom(assemblyToLoad);
            }

            // TODO: Extent exception
            throw new FileNotFoundException();
        }
    }
}