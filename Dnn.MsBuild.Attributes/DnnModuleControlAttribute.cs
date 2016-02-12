// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModuleControlAttribute.cs" company="XCESS expertise center b.v.">
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