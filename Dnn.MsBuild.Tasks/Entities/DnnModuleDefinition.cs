// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModuleDefinition.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnModuleDefinition type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnn.MsBuild.Tasks.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// <moduleDefinition>
    ///   <friendlyName/>
    ///   <defaultCacheTime/>
    ///   <moduleControls/>
    ///   <permissions/>
    /// </moduleDefinition>
    /// ]]>
    /// </remarks>
    [Serializable]
    public class DnnModuleDefinition
    {
        public const string DefaultModuleDefinitionName = "default";

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleDefinition"/> class.
        /// </summary>
        internal DnnModuleDefinition()
            : this(DefaultModuleDefinitionName)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleDefinition"/> class.
        /// </summary>
        internal DnnModuleDefinition(string name)
        {
            this.DefaultCacheTime = -1;
            this.ModuleControls = new List<DnnModuleControl>();
            this.Permissions = new List<DnnModulePermission>();
            this.Name = name;
        }

        #endregion

        /// <summary>
        /// Gets or sets the default cache time.
        /// </summary>
        /// <value>
        /// The default cache time.
        /// </value>
        [XmlElement("defaultCacheTime")]
        public int DefaultCacheTime { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [XmlElement("friendlyName")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the module controls.
        /// </summary>
        /// <value>
        /// The module controls.
        /// </value>
        [XmlArray("moduleControls")]
        [XmlArrayItem("moduleControl")]
        public List<DnnModuleControl> ModuleControls { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlIgnore]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        [XmlArray("permissions")]
        [XmlArrayItem("permission")]
        public List<DnnModulePermission> Permissions { get; set; }
    }
}
