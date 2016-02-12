// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnDesktopModuleAttribute.cs" company="XCESS expertise center b.v.">
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

// ReSharper disable once CheckNamespace

namespace DotNetNuke.Services.Installer.MsBuild
{
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DnnDesktopModuleAttribute : DnnManifestAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnDesktopModuleAttribute"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public DnnDesktopModuleAttribute(string moduleName)
        {
            this.ModuleName = moduleName;
        }

        #endregion

        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        /// <value>
        /// The name of the folder.
        /// </value>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is admin.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is premium.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is premium; otherwise, <c>false</c>.
        /// </value>
        public bool IsPremium { get; set; }

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName { get; private set; }
    }
}