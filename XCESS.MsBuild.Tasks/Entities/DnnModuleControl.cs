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
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using XCESS.MsBuild.Attributes;
    using XCESS.MsBuild.Tasks.Entities;

    /// <summary>
    /// </summary>
    [Serializable]
    public class DnnModuleControl
    {
        public const string DefaultControlExtension = ".ascx";

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleControl"/> class.
        /// </summary>
        public DnnModuleControl()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleControl"/> class.
        /// </summary>
        /// <param name="ownerType">Type of the owner.</param>
        /// <param name="moduleControl">The module control.</param>
        /// <param name="desktopModule">The desktop module.</param>
        /// <param name="packages">The packages.</param>
        public DnnModuleControl(Type ownerType, DnnModuleControlAttribute moduleControl, DnnDesktopModuleAttribute desktopModule, IEnumerable<DnnPackage> packages)
        {
            if (packages == null || !packages.Any())
            {
                // TODO: Localize
                throw new NotSupportedException("No packages found!");
            }

            if ((packages.Count() > 1) && (desktopModule == null))
            {
                // TODO: Localize
                throw new NotSupportedException("When more than one (1) package is defined, each control should be assigned to a desktopmodule with the DesktopModule attribute.");
            }

            // NOTE: use the package name (with dots replaced by underscores) as default folder name for the module control(s).
            var package = packages.FirstOrDefault();
            var moduleControlsFolder = (desktopModule != null) ? desktopModule.FolderName : package.Name.Replace('.', '_');

            this.ControlSource = Path.Combine(DnnGlobals.DnnDesktopModuleFolder, moduleControlsFolder, moduleControl.SubFolder ?? string.Empty, ownerType.Name + DnnGlobals.WebControlExtension);
            this.ControlTitle = moduleControl.ControlTitle;
            this.ControlType = moduleControl.ControlType;
            this.HelpUrl = moduleControl.HelpUrl;
            this.Key = moduleControl.Key;
            this.SupportsPartialRendering = moduleControl.SupportsPartialRendering;
            this.SupportsPopups = moduleControl.SupportsPopups;
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

        public static DnnModuleControl FromAttribute(DnnModuleControlAttribute attribute, Type controlType, IEnumerable<DnnPackage> packages)
        {
            var moduleControl = new DnnModuleControl() { ControlType = attribute.ControlType, ControlTitle = attribute.ControlTitle, Key = attribute.Key };

            return moduleControl;
        }
    }
}
