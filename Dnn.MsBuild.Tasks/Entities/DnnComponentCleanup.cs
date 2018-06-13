// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnComponentCleanup.cs" company="XCESS expertise center b.v.">
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

namespace Dnn.MsBuild.Tasks.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Dnn.MsBuild.Tasks.Entities.FileTypes;
    using Dnn.MsBuild.Tasks.Extensions;
    using DotNetNuke.Services.Installer.MsBuild;

    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://www.dnnsoftware.com/wiki/cleanup-component
    ///     http://www.dnnsoftware.com/community-blog/cid/135286/the-new-extension-installer-manifest-part-3-the-cleanup-component
    ///     <![CDATA[
    /// <component type="Cleanup" version="" fileName="">
    ///   <files>
    ///     <file>
    ///       <path />
    ///       <name />
    ///     </file>
    ///   </files>
    /// </component>
    /// ]]>
    /// </remarks>
    /// <seealso cref="Dnn.MsBuild.Tasks.Entities.DnnComponent" />
    public class DnnComponentCleanup : DnnComponent
    {
        /// <summary>
        ///     Gets or sets the files.
        /// </summary>
        /// <value>
        ///     The files.
        /// </value>
        [XmlArray("files")]
        [XmlArrayItem("file")]
        public List<FileInfo> Files { get; set; }

        /// <summary>
        ///     Gets or sets the name of the file.
        /// </summary>
        /// <value>
        ///     The name of the file.
        /// </value>
        [XmlAttribute("fileName")]
        public string FileName { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        [XmlIgnore]
        public Version Version { get; set; }

        /// <summary>
        ///     Gets or sets the version string.
        /// </summary>
        /// <value>
        ///     The version string.
        /// </value>
        [XmlAttribute("version")]
        public string VersionString
        {
            get { return this.Version.ToDnnVersionString(); }
            // ReSharper disable once ValueParameterNotUsed
            set { }
        }

        #region ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="DnnComponentCleanup" /> class.
        /// </summary>
        internal DnnComponentCleanup()
            : base(DnnComponentType.Cleanup)
        {
            this.Files = new List<FileInfo>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DnnComponentCleanup" /> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="version">The version.</param>
        internal DnnComponentCleanup(string fileName, Version version)
            : base(DnnComponentType.Cleanup)
        {
            this.FileName = fileName;
            this.Version = version;

            // Notice: this.Files is indeed null
        }

        #endregion
    }
}