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
using System.Text.RegularExpressions;
using System.Threading;
using Dnn.MsBuild.Tasks.Composition;
using Dnn.MsBuild.Tasks.Composition.Manifest;
using Dnn.MsBuild.Tasks.Entities.Internal;
using Dnn.MsBuild.Tasks.Parsers;

namespace Dnn.MsBuild.Tasks.Components
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TManifest">The type of the manifest.</typeparam>
    internal class BuildManifestTask<TManifest> : IDisposable
        where TManifest : IManifest, new()
    {
        /// <summary>
        /// A value which indicates the disposable state. 0 indicates undisposed, 1 indicates disposing
        /// or disposed.
        /// </summary>
        private int _disposableState = 0;

        private ITaskData TaskData { get; }

        /// <summary>
        /// Gets a value indicating whether the object is undisposed.
        /// </summary>
        public bool IsUndisposed => Thread.VolatileRead(ref this._disposableState) == 0;

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Attempt to move the disposable state from 0 to 1. If successful, we can be assured that
            // this thread is the first thread to do so, and can safely dispose of the object.
            if (Interlocked.CompareExchange(ref this._disposableState, 1, 0) == 0)
            {
                // Call the Dispose method with the disposing flag set to true, indicating
                // that derived classes may release unmanaged resources and dispose of managed resources.
                this.Dispose(true);

                // Suppress finalization of this object (remove it from the finalization queue and
                // prevent the destructor from being called).
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        /// <summary>
        /// Finalizes an instance of the DisposableBase class.
        /// </summary>
        ~BuildManifestTask()
        {
            // The destructor has been called as a result of finalization, indicating that the object
            // was not disposed of using the Dispose() method. In this case, call the Dispose
            // method with the dispoing flag set to false, indicating that derived classes
            // may only release unmanaged resources.
            this.Dispose(false);
        }

        public IManifest Build()
        {
            var manifest = this.BuildManifest();
            return manifest;
        }

        private IManifest BuildManifest()
        {
            var builder = new ManifestBuilder<TManifest>();
            return builder.Build(this.TaskData);
        }

        private string GetDnnAssemblyPathFromProjectFile(string projectFile)
        {
            var dnnAssemblyPath = string.Empty;
            var desktopModuleRegex = new Regex(@"(?i)\bdesktopmodules\b");
            var match = desktopModuleRegex.Match(projectFile);

            if (match.Success)
            {
                dnnAssemblyPath = Path.Combine(projectFile.Substring(0, match.Index), "bin");
            }

            return dnnAssemblyPath;
        }

        private IProjectFileData GetProjectPackageData(string projectFile)
        {
            var parser = new XmlVsProjectFileParser();
            return parser.Parse(projectFile);
        }

        private IDictionary<string, string> GetUserControls(IProjectFileData projectFileData)
        {
            var basePath = projectFileData.BasePath;
            var parser = new UserControlParser();
            var userControlFiles = projectFileData.ResourceFiles
                                                  .Where(arg => arg.Name.EndsWith(UserControlParser.UserControlFileExtension))
                                                  .Select(arg =>
                                                          {
                                                              var path = Path.Combine(basePath, arg.Path ?? string.Empty, arg.Name);
                                                              return path;
                                                          })
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

        #region Dispose Pattern

        // For information about the Dispose Pattern see: https://msdn.microsoft.com/en-us/library/b1yfkh5e(v=vs.110).aspx

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {}

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildManifestTask{TManifest}"/> class.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="packageAssembly">The package assembly.</param>
        public BuildManifestTask(string projectFile, string packageAssembly)
            : this(projectFile, packageAssembly, null)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildManifestTask{TManifest}" /> class.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="packageAssembly">The manifest assembly.</param>
        /// <param name="dnnAssemblyPath">The DNN assembly path.</param>
        public BuildManifestTask(string projectFile, string packageAssembly, string dnnAssemblyPath)
        {
            if (string.IsNullOrWhiteSpace(dnnAssemblyPath))
            {
                dnnAssemblyPath = this.GetDnnAssemblyPathFromProjectFile(projectFile);
            }

            // Gather the package data needed for building the manifest
            this.TaskData = new TaskData(packageAssembly, dnnAssemblyPath);

            // Before assigning the UserControls to the TaksData, ensure that the Assembly locators are correctly setup.
            this.SetupDnnAssemblyLocator();

            this.TaskData.ProjectFileData = this.GetProjectPackageData(projectFile);
            this.TaskData.ProjectFileData.UserControls = this.GetUserControls(this.TaskData.ProjectFileData);
        }

        #endregion
    }
}