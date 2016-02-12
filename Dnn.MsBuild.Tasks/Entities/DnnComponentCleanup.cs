// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnComponentCleanup.cs" company="XCESS expertise center b.v.">
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
using System.Collections.Generic;
using System.Xml.Serialization;
using Dnn.MsBuild.Tasks.Entities.FileTypes;
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