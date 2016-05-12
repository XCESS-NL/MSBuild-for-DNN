// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnPackageAttribute.cs" company="XCESS expertise center b.v.">
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
    [AttributeUsage(AttributeTargets.Assembly)] // Current version of XCESS MsBuild only supports a single package
    public sealed class DnnPackageAttribute : DnnManifestAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnPackageAttribute" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="packageFolder">The package folder.</param>
        /// <param name="packageType">Type of the package.</param>
        /// <exception cref="System.ArgumentException">The package name cannot be null or an empty string.;name.</exception>
        public DnnPackageAttribute(string name, string packageFolder, DnnPackageType packageType = DnnPackageType.Module)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                // TODO: Localize...
                throw new ArgumentException("The package name cannot be null or an empty string.", nameof(name));
            }

            this.Name = name;
            this.PackageFolder = packageFolder;
            this.PackageType = packageType;
        }

        #endregion

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the icon file.
        /// </summary>
        /// <value>
        /// The name of the icon file.
        /// </value>
        public string IconFileName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the package folder.
        /// </summary>
        /// <value>
        /// The package folder.
        /// </value>
        public string PackageFolder { get; set; }

        /// <summary>
        /// Gets or sets the name of the package.
        /// </summary>
        /// <value>
        /// The name of the package.
        /// </value>
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets the type of the package.
        /// </summary>
        /// <value>
        /// The type of the package.
        /// </value>
        public DnnPackageType PackageType { get; set; }
    }
}