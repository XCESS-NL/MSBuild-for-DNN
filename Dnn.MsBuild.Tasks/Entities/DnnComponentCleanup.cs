// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnComponentCleanup.cs" company="XCESS expertise center bv">
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
using System.Xml.Serialization;
using Dnn.MsBuild.Tasks.Extensions;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// <component type="Cleanup" version="05.00.00">
    ///   <files>
    ///     <file>
    ///       <path />
    ///       <name />
    ///     </file>
    ///   </files>
    /// </component>
    /// ]]>
    /// </remarks>
    [Serializable]
    public class DnnComponentCleanup : DnnComponent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnComponentCleanup"/> class.
        /// </summary>
        internal DnnComponentCleanup()
            : base(DnnComponentType.Cleanup)
        {
            this.Files = new List<FileInfo>();
        }

        #endregion

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        [XmlArray("files")]
        [XmlArrayItem("file")]
        public List<FileInfo> Files { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [XmlIgnore]
        public Version Version { get; set; }

        [XmlElement("version")]
        public string VersionString
        {
            get { return this.Version.ToDnnVersionString(); }
            set { }
        }
    }
}