// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModuleControlAttribute.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnModuleControlAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Attributes
{
    using System;

    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    [Serializable]
    public sealed class DnnModuleControlAttribute : Attribute
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleControlAttribute" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="title">The title.</param>
        /// <param name="type">The type.</param>
        public DnnModuleControlAttribute(string key, string title, DnnControlType controlType)
            : this(key, title, controlType, true, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleControlAttribute" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="title">The title.</param>
        /// <param name="controlType">Type of the control.</param>
        /// <param name="supportsPartialRendering">If set to <c>true</c> [supports partial rendering].</param>
        /// <param name="supportsPopups">If set to <c>true</c> [supports popups].</param>
        public DnnModuleControlAttribute(string key, string title, DnnControlType controlType, bool supportsPartialRendering, bool supportsPopups)
        {
            this.ControlTitle = title;
            this.ControlType = controlType;
            this.Key = key;
            this.SupportsPartialRendering = supportsPartialRendering;
            this.SupportsPopups = supportsPopups;
        }
        
        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the sub folder.
        /// </summary>
        /// <value>
        /// The sub folder.
        /// </value>
        public string SubFolder { get; set; }

        /// <summary>
        /// Gets or sets the control title.
        /// </summary>
        /// <value>
        /// The control title.
        /// </value>
        public string ControlTitle { get; set; }

        /// <summary>
        /// Gets or sets the type of the control.
        /// </summary>
        /// <value>
        /// The type of the control.
        /// </value>
        public DnnControlType ControlType { get; set; }

        /// <summary>
        /// Gets or sets the help URL.
        /// </summary>
        /// <value>
        /// The help URL.
        /// </value>
        public string HelpUrl { get; set; }

        /// <summary>
        /// Gets or sets the key used by DNN to uniquely identify the module control (.ascx) within the DesktopModule. The key is used in functions like NavigateUrl(key).
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the module definition.
        /// </summary>
        /// <value>
        /// The module definition.
        /// </value>
        public string ModuleDefinition { get; set; }

        /// <summary>
        /// Gets or sets the name of the package to which this control belongs.
        /// </summary>
        /// <value>
        /// The name of the package.
        /// </value>
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the module control (.ascx) supports partial rendering. Partial rendering in DNN is accomplished by wrapping the module control in an AJAX Update Panel. This property is enabled (<c>true</c>) by default.
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports partial rendering]; otherwise, <c>false</c>.
        /// </value>
        public bool SupportsPartialRendering { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the module control (.ascx) supports (DNN) popups. This property is enabled (<c>true</c>) by default.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [supports popups]; otherwise, <c>false</c>.
        /// </value>
        public bool SupportsPopups { get; set; }

        #endregion
    }
}
