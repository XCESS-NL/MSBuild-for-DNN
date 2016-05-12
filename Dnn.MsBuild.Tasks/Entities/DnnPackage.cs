// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnPackage.cs" company="XCESS expertise center b.v.">
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
using System.Globalization;
using System.Xml.Serialization;
using Dnn.MsBuild.Tasks.Components.Tokens;
using Dnn.MsBuild.Tasks.Composition;
using Dnn.MsBuild.Tasks.Extensions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// <package name="" type="" version="">
    ///   <friendlyName />
    ///   <description />
    ///   <owner>
    ///     <name />
    ///     <organization />
    ///     <url />
    ///     <email />
    ///   </owner>
    ///   <license src="" />
    ///   <releaseNotes src="" />
    ///   <dependencies />
    /// </package>
    /// ]]>
    /// </remarks>
    [Serializable]
    public class DnnPackage : IManifestElement, IPackageData, IPropertyAccess
    {
        public static readonly Version DefaultVersion = new Version(0, 0, 0);

        /// <summary>
        /// Gets or sets a value indicating whether [azure compatible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [azure compatible]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("azureCompatible")]
        public bool AzureCompatible { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [XmlElement("friendlyName")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the icon file.
        /// </summary>
        /// <value>
        /// The name of the icon file.
        /// </value>
        [XmlElement("iconFile")]
        public string IconFilePath { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        [XmlElement("owner")]
        public DnnOwner Owner { get; set; }

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        /// <value>
        /// The license.
        /// </value>
        [XmlElement("license")]
        public DnnLicense License { get; set; }

        /// <summary>
        /// Gets or sets the release notes.
        /// </summary>
        /// <value>
        /// The release notes.
        /// </value>
        [XmlElement("releaseNotes")]
        public DnnReleaseNotes ReleaseNotes { get; set; }

        /// <summary>
        /// Gets or sets the dependencies.
        /// </summary>
        /// <value>
        /// The dependencies.
        /// </value>
        [XmlArray("dependencies")]
        [XmlArrayItem("dependency")]
        public List<DnnPackageDependency> Dependencies { get; set; }

        /// <summary>
        /// Gets or sets the components.
        /// </summary>
        /// <value>
        /// The components.
        /// </value>
        [XmlArray("components")]
        [XmlArrayItem("component")]
        public List<DnnComponent> Components { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [XmlAttribute("version")]
        public string VersionString
        {
            get
            {
                var version = this.Version ?? DefaultVersion;
                return version.ToDnnVersionString();
            }
            // ReSharper disable once ValueParameterNotUsed
            set { }
        }


        /// <summary>
        /// Gets or sets the folder.
        /// </summary>
        /// <value>
        /// The folder.
        /// </value>
        [XmlIgnore]
        public string Folder { get; set; }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the package.
        /// </summary>
        /// <value>
        /// The type of the package.
        /// </value>
        [XmlAttribute("type")]
        public DnnPackageType PackageType { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [XmlIgnore]
        public Version Version { get; set; }

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnPackage" /> class from being created.
        /// </summary>
        internal DnnPackage()
        {
            this.Components = new List<DnnComponent>();
            this.Owner = new DnnOwner();
            this.Dependencies = new List<DnnPackageDependency>();
            this.PackageType = DnnPackageType.Module;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnPackage"/> class.
        /// </summary>
        /// <param name="packageType">Type of the package.</param>
        internal DnnPackage(DnnPackageType packageType)
            : this()
        {
            this.PackageType = packageType;
        }

        #endregion

        #region Implementation of IPropertyAccess

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="propertyNotFound">if set to <c>true</c> [property not found].</param>
        /// <returns></returns>
        public string GetProperty(string propertyName, string format, CultureInfo formatProvider, ref bool propertyNotFound)
        {
            propertyNotFound = false;

            var propertyValue = default(string);
            var outputFormat = string.IsNullOrWhiteSpace(format) ? "G" : format;

            switch (propertyName.ToLowerInvariant())
            {
                case "description":
                    propertyValue = FormatString(this.Description, format);
                    break;
                case "email":
                    propertyValue = FormatString(this.Owner?.Email, format);
                    break;
                case "name":
                    propertyValue = FormatString(this.Name, format);
                    break;
                case "compagny":
                case "organisation":
                    propertyValue = FormatString(this.Owner?.Organisation, format);
                    break;
                case "url":
                case "website":
                    propertyValue = FormatString(this.Owner?.Url, format);
                    break;
                default:
                    propertyNotFound = true;
                    break;
            }

            return propertyValue ?? string.Empty;
        }

        #endregion

        private static string FormatString(string value, string format)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return value;
            }

            return !string.IsNullOrWhiteSpace(value) ? string.Format(format, value) : string.Empty;
        }

    }
}