// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModuleControlAttribute.cs" company="XCESS expertise center bv">
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

// ReSharper disable once CheckNamespace
namespace DotNetNuke.Services.Installer.MsBuild
{
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class DnnModuleControlAttribute : DnnBaseModuleControlAttribute
    {
        /// <summary>
        /// Gets or sets the sub folder.
        /// </summary>
        /// <value>
        /// The sub folder.
        /// </value>
        public string SubFolder { get; set; }

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

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleControlAttribute" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="title">The title.</param>
        /// <param name="controlType">Type of the control.</param>
        public DnnModuleControlAttribute(string key, string title, DnnControlType controlType)
            : this(key, title, controlType, true, true)
        {}

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
    }
}