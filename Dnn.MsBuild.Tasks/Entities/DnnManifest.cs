// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnManifest.cs" company="XCESS expertise center b.v.">
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

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Dnn.MsBuild.Tasks.Composition;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// http://www.dnnsoftware.com/wiki/page/manifests
    /// </remarks>
    [XmlRoot("dotnetnuke")]
    public class DnnManifest : IManifest
    {
        public const string DefaultManifestExtension = "dnn";

        public const string DnnManifestFileNameFormat = "{0}_{1}";

        public const string DnnManifestPackageVersion = "5.0";

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnManifest"/> class.
        /// </summary>
        public DnnManifest()
        {
            this.Extension = DefaultManifestExtension;
            this.Packages = new List<DnnPackage>();
            this.Type = DnnManifestType.Package;
            this.Version = DnnManifestPackageVersion;
        }

        #endregion

        private string _extension = DefaultManifestExtension;

        private string _fileName = null;

        /// <summary>
        /// Gets or sets the manifest extension.
        /// </summary>
        /// <value>
        /// The manifest extension.
        /// </value>
        [XmlIgnore]
        public string Extension
        {
            get { return this._extension;  }
            set
            {
                this._extension = value;
                this._fileName = null;
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [XmlArray("packages")]
        [XmlArrayItem("package")]
        public List<DnnPackage> Packages { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [XmlAttribute("type")]
        public DnnManifestType Type { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [XmlAttribute("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [XmlIgnore]
        public string FileName
        {
            get
            {
                // ReSharper disable once InvertIf
                if (string.IsNullOrWhiteSpace(this._fileName))
                {
                    var package = this.Packages.FirstOrDefault();
                    if (package != null)
                    {
                        this._fileName = string.Format(DnnManifestFileNameFormat, package.Name, package.VersionString);
                    }
                }

                return this._fileName;
            }
            set { this._fileName = value; }
        }
    }
}