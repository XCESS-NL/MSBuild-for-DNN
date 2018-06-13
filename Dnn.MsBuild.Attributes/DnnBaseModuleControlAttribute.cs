// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnBaseModuleControlAttribute.cs" company="XCESS expertise center b.v.">
//     Copyright (c) 2017-2018 XCESS expertise center b.v.
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

// ReSharper disable once CheckNamespace

namespace DotNetNuke.Services.Installer.MsBuild
{
    using System;

    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class DnnBaseModuleControlAttribute : DnnManifestAttribute
    {
        /// <summary>
        ///     Gets or sets the control title.
        /// </summary>
        /// <value>
        ///     The control title.
        /// </value>
        public string ControlTitle { get; set; }

        /// <summary>
        ///     Gets or sets the type of the control.
        /// </summary>
        /// <value>
        ///     The type of the control.
        /// </value>
        public DnnControlType ControlType { get; set; }

        /// <summary>
        ///     Gets or sets the help URL.
        /// </summary>
        /// <value>
        ///     The help URL.
        /// </value>
        public string HelpUrl { get; set; }

        /// <summary>
        ///     Gets or sets the key used by DNN to uniquely identify the module control (.ascx) within the DesktopModule. The key
        ///     is used in functions like NavigateUrl(key).
        /// </summary>
        /// <value>
        ///     The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        ///     Gets or sets the module definition.
        /// </summary>
        /// <value>
        ///     The module definition.
        /// </value>
        public string ModuleDefinition { get; set; }
    }
}