// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageData.cs" company="XCESS expertise center b.v.">
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
using System.Reflection;
using Dnn.MsBuild.Tasks.Composition;

namespace Dnn.MsBuild.Tasks.Entities.Internal
{
    internal class TaskData : ITaskData
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskData" /> class.
        /// </summary>
        /// <param name="projectFileData">The project file data.</param>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="dnnAssemblyPath">The DNN assembly path.</param>
        public TaskData(IProjectFileData projectFileData, string assemblyPath, string dnnAssemblyPath)
            : this(projectFileData, Assembly.LoadFrom(assemblyPath), dnnAssemblyPath)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskData" /> class.
        /// </summary>
        /// <param name="projectFileData">The project file data.</param>
        /// <param name="assembly">The assembly.</param>
        /// <param name="dnnAssemblyPath">The DNN assembly path.</param>
        public TaskData(IProjectFileData projectFileData, Assembly assembly, string dnnAssemblyPath)
        {
            this.Assembly = assembly;
            this.DnnAssemblyPath = dnnAssemblyPath;
            this.ExportedTypes = this.Assembly.GetExportedTypes();
            this.ProjectFileData = projectFileData;
        }

        #endregion

        #region Implementation of ITaskData

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <value>
        /// The assembly.
        /// </value>
        public Assembly Assembly { get; }

        /// <summary>
        /// Gets the DNN assembly path.
        /// </summary>
        /// <value>
        /// The DNN assembly path.
        /// </value>
        public string DnnAssemblyPath { get; }

        /// <summary>
        /// Gets the DNN manifest extension.
        /// </summary>
        /// <value>
        /// The DNN manifest extension.
        /// </value>
        public string DnnManifestExtension { get; set; }

        /// <summary>
        /// Gets the exported types.
        /// </summary>
        /// <value>
        /// The exported types.
        /// </value>
        public IEnumerable<Type> ExportedTypes { get; }

        /// <summary>
        /// Gets or sets the package.
        /// </summary>
        /// <value>
        /// The package.
        /// </value>
        public IPackageData Package { get; set; }

        /// <summary>
        /// Gets or sets the project file data.
        /// </summary>
        /// <value>
        /// The project file data.
        /// </value>
        public IProjectFileData ProjectFileData { get; }

        #endregion
    }
}