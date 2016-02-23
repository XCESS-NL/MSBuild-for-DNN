// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModuleControl.cs" company="XCESS expertise center b.v.">
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
using System.Xml.Serialization;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// http://www.dnnsoftware.com/wiki/module-component
    /// </remarks>
    [Serializable]
    public class DnnModuleControl
    {
        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnModuleControl"/> class from being created.
        /// </summary>
        private DnnModuleControl()
        {}

        #endregion

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

        /// <summary>
        /// Creates a DnnModuleControl from a DnnModuleControl attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="userControlFilePath">The user control file path.</param>
        /// <returns></returns>
        public static DnnModuleControl CreateFromAttribute(DnnModuleControlAttribute attribute, string userControlFilePath)
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