// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModuleControl.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnModuleControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using XCESS.MsBuild.Attributes;
    using XCESS.MsBuild.Tasks.Entities;

    /// <summary>
    /// </summary>
    [Serializable]
    public class DnnModuleControl
    {
        #region [ Constructors ]

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnModuleControl"/> class from being created.
        /// </summary>
        private DnnModuleControl()
        {
        }
     
        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the control source.
        /// </summary>
        /// <value>
        /// The control source.
        /// </value>
        [XmlElement("controlSrc")]
        public string ControlSource { get; set; }

        /// <summary>
        /// Gets or sets the control title.
        /// </summary>
        /// <value>
        /// The control title.
        /// </value>
        [XmlElement("controlTitle")]
        public string ControlTitle { get; set; }

        /// <summary>
        /// Gets or sets the type of the control.
        /// </summary>
        /// <value>
        /// The type of the control.
        /// </value>
        [XmlElement("controlType")]
        public DnnControlType ControlType { get; set; }

        /// <summary>
        /// Gets or sets the help URL.
        /// </summary>
        /// <value>
        /// The help URL.
        /// </value>
        [XmlElement("helpUrl")]
        public string HelpUrl { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [XmlElement("controlKey")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [supports partial rendering].
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports partial rendering]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("supportsPartialRendering")]
        public bool SupportsPartialRendering { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [supports popups].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [supports popups]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("supportsPopUps")]
        public bool SupportsPopups { get; set; }

        #endregion

        public static DnnModuleControl FromAttribute(DnnModuleControlAttribute attribute, Type controlType, string userControlFilePath, IEnumerable<DnnPackage> packages)
        {
            return new DnnModuleControl()
                       {
                           ControlSource = userControlFilePath,
                           ControlTitle = attribute.ControlTitle,
                           ControlType = attribute.ControlType,
                           Key = attribute.Key,
                           SupportsPartialRendering = attribute.SupportsPartialRendering,
                           SupportsPopups = attribute.SupportsPopups
                       };
        }
    }
}
