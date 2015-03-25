// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnPackage.cs" company="XCESS expertise center b.v.">
//   Copyright (c) 2014 XCESS expertise center b.v. 
//   
//   The software is owned by XCESS expertise center b.v. and is protected by 
//   the Dutch copyright laws and international treaty provisions. 
//   You are allowed to make copies of the software solely for backup or archival purposes. 
//   You may not lease, rent, export or sublicense the software. 
//   You may not reverse engineer, decompile, disassemble or create derivative works from the software.
//   
//   XCESS expertise center b.v., Storkstraat 19, 3833 LB Leusden, The Netherlands
//   T. +31-33-4335151, I. http://www.xcess.nl
// </copyright>
// <summary>
//   Defines the DnnPackage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks.Entities
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using XCESS.MsBuild.Attributes;

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
    public class DnnPackage
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnPackage" /> class.
        /// </summary>
        public DnnPackage(AssemblyName assemblyName)
            : this(assemblyName.Name, assemblyName)
        {           
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnPackage"/> class from being created.
        /// </summary>
        private DnnPackage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnPackage"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        private DnnPackage(string name, AssemblyName assemblyName)
        {
            this.Name = name;
            this.Components = new DnnComponents();
            this.License = new DnnLicense();
            this.Owner = new DnnOwner();
            this.PackageType = DnnPackageType.Module;

            this.version = assemblyName.Version;
            this.versionString = string.Format(CultureInfo.InvariantCulture, "{0:D2}.{1:D2}.{2:D2}", this.Version.Major, this.Version.Minor, this.Version.Revision); // Only use Major, Minor and Revision
        }

        #endregion

        #region [ Properties ]

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
        /// Gets or sets the type of the package.
        /// </summary>
        /// <value>
        /// The type of the package.
        /// </value>
        [XmlAttribute("type")]
        public DnnPackageType PackageType { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        [XmlElement("owner")]
        public DnnOwner Owner { get; set; }

        /// <summary>
        /// The version string
        /// </summary>
        private readonly string versionString;

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
                return this.versionString;
            }
            set
            {
            }
        }

        /// <summary>
        /// The version
        /// </summary>
        private readonly Version version;

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [XmlIgnore]
        public Version Version
        {
            get
            {
                return this.version;
            }
            set
            {
            }
        }


        /// <summary>
        /// Gets or sets the components.
        /// </summary>
        /// <value>
        /// The components.
        /// </value>
        [XmlElement("components")]
        public DnnComponents Components { get; set; }

        #endregion

        /// <summary>
        /// Creates a DnnPackage entity from the DnnPackage attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static DnnPackage FromAttribute(DnnPackageAttribute attribute, AssemblyName assemblyName)
        {
            return new DnnPackage(attribute.Name, assemblyName)
                       {
                           AzureCompatible = attribute.AzureCompatible,
                           Description = attribute.Description,
                           FriendlyName = attribute.FriendlyName,
                           IconFileName = attribute.IconFileName,
                           PackageType = attribute.PackageType
                       };
        }
    }
}
