// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnPackage.cs" company="XCESS expertise center bv">
//   Copyright (c) 2016 XCESS expertise center bv
//   
//   The software is owned by XCESS and is protected by 
//   the Dutch copyright laws and international treaty provisions.
//   You are allowed to make copies of the software solely for backup or archival purposes.
//   You may not lease, rent, export or sublicense the software.
//   You may not reverse engineer, decompile, disassemble or create derivative works from the software.
//   
//   Owned by XCESS expertise center b.v., Storkstraat 19, 3833 LB Leusden, The Netherlands
//   T. +31-33-4335151, E. info@xcess.nl, I. http://www.xcess.nl
// </copyright>
// <summary>
//   
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Xml.Serialization;
using Dnn.MsBuild.Tasks.Composition;
using Dnn.MsBuild.Tasks.Extensions;
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
    public class DnnPackage : IManifestElement, IPackageData
    {
        public static readonly Version DefaultVersion = new Version(0, 0, 0);

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
        /// Gets or sets the folder.
        /// </summary>
        /// <value>
        /// The folder.
        /// </value>
        [XmlIgnore]
        public string Folder { get; set; }

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
        public string IconFileName { get; set; }

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        /// <value>
        /// The license.
        /// </value>
        [XmlElement("license")]
        public DnnLicense License { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        [XmlElement("owner")]
        public DnnOwner Owner { get; set; }

        /// <summary>
        /// Gets or sets the type of the package.
        /// </summary>
        /// <value>
        /// The type of the package.
        /// </value>
        [XmlAttribute("type")]
        public DnnPackageType PackageType { get; set; }

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
        /// Gets or sets the dependencies.
        /// </summary>
        /// <value>
        /// The dependencies.
        /// </value>
        [XmlArray("dependencies")]
        [XmlArrayItem("dependency")]
        public List<DnnPackageDependency> Dependencies { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [XmlIgnore]
        public Version Version { get; set; }

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
    }
}