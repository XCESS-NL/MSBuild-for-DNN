// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnManifest.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnManifest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Dnn.MsBuild.Tasks.Composition;

namespace Dnn.MsBuild.Tasks.Entities
{
    // http://www.dnnsoftware.com/wiki/page/manifests

    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// </summary>
    [XmlRoot("dotnetnuke")]
    public class DnnManifest : IManifest, IManifestElement
    {
        public const string DnnManifestFileNameFormat = "{0}_{1}.dnn5";

        public const string DnnManifestPackageVersion = "5.0";

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnManifest"/> class.
        /// </summary>
        internal DnnManifest()
        {
            this.Packages = new List<DnnPackage>();
            this.Type = DnnManifestType.Package;
            this.Version = DnnManifestPackageVersion;
        }
        
        #endregion

        private string _fileName = null;
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [XmlIgnore]
        public string FileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._fileName))
                {
                    var package = this.Packages.FirstOrDefault();
                    if (package != null)
                    {
                        this._fileName = string.Format(DnnManifestFileNameFormat, package.Name, package.VersionString);
                    }
                }

                return this._fileName;
            }
            set
            {
                this._fileName = value;
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [XmlArray("packages")]
        [XmlArrayItem("package")]
        public List<DnnPackage> Packages { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [XmlAttribute("type")]
        public DnnManifestType Type { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [XmlAttribute("version")]
        public string Version { get; set; }
    }
}
