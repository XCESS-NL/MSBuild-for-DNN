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

using Dnn.MsBuild.Tasks.Components;
using Dnn.MsBuild.Tasks.Entities;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Dnn.MsBuild.Tasks
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Microsoft.Build.Utilities.Task" />
    public class BuildDnnManifest : Task
    {
        private const string InstallZip = "_Install.zip";

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
            using (var buildTask = new BuildManifestTask<DnnManifest>(this.ProjectFile, this.ProjectTargetAssembly, this.DnnAssemblyPath))
            {
                var manifest = buildTask.Build();

                // ReSharper disable once AssignmentInConditionalExpression
                // ReSharper disable once InvertIf
                if (taskResult = (manifest != null))
                {
                    manifest.Extension = this.DnnManifestExtension ?? DnnManifest.DefaultManifestExtension;

                    var serializeTask = new SerializeManifestTask<DnnManifest>();
                    serializeTask.Execute(manifest);

                    var fullExtension = "." + manifest.Extension;
                    this.InstallFileName = manifest.FileName.Replace(fullExtension, InstallZip);
                }
            }

            return taskResult;
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
        public string InstallFileName { get; set; }

        #endregion
    }
}