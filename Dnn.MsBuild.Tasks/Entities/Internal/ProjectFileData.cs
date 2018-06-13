﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectFileData.cs" company="XCESS expertise center b.v.">
//     Copyright (c) 2017-2018 XCESS expertise center b.v.
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

namespace Dnn.MsBuild.Tasks.Entities.Internal
{
    using System.Collections.Generic;
    using Dnn.MsBuild.Tasks.Entities.FileTypes;

    internal class ProjectFileData : IProjectFileData
    {
        #region ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProjectFileData" /> class.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="references">The references.</param>
        /// <param name="resourceFiles">The resource files.</param>
        internal ProjectFileData(string basePath, IEnumerable<AssemblyFileInfo> references,
                                 IEnumerable<IFileInfo> resourceFiles)
        {
            this.BasePath = basePath;
            this.References = references;
            this.ResourceFiles = resourceFiles;
        }

        #endregion

        #region Implementation of IProjectFileData

        public string BasePath { get; }

        public IEnumerable<AssemblyFileInfo> References { get; }

        public IEnumerable<IFileInfo> ResourceFiles { get; }

        public IDictionary<string, string> UserControls { get; set; }

        #endregion
    }
}