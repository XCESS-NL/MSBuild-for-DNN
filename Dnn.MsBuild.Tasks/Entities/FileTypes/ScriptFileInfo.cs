// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScriptFileInfo.cs" company="XCESS expertise center b.v.">
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
using Dnn.MsBuild.Tasks.Extensions;

namespace Dnn.MsBuild.Tasks.Entities.FileTypes
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Dnn.MsBuild.Tasks.Entities.FileTypes.FileInfo" />
    public class ScriptFileInfo : FileInfo
    {
        public const string InstallScriptFileName = "Install";

        public const string UninstallScriptFileName = "uninstall";

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ScriptFileInfo"/> class from being created.
        /// </summary>
        private ScriptFileInfo()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptFileInfo"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        /// <param name="version">The version.</param>
        public ScriptFileInfo(string name, string path, Version version)
            : base(name, path)
        {
            this.Version = version;

            this.ScriptType = name.StartsWith(UninstallScriptFileName, StringComparison.InvariantCultureIgnoreCase)
                                  ? ScriptType.Uninstall
                                  : ScriptType.Install;
        }

        #endregion

        [XmlAttribute("type")]
        public ScriptType ScriptType { get; set; }

        [XmlIgnore]
        public Version Version { get; set; }

        [XmlElement("version")]
        public string VersionString
        {
            get
            {
                return this.Version?.ToDnnVersionString();
            }
            // ReSharper disable once ValueParameterNotUsed
            set { }
        }
    }
}