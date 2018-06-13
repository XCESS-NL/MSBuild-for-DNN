// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceFileInfo.cs" company="XCESS expertise center b.v.">
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

namespace Dnn.MsBuild.Tasks.Entities.FileTypes
{
    using System.Xml.Serialization;

    /// <summary>
    /// </summary>
    /// <seealso cref="Dnn.MsBuild.Tasks.Entities.FileTypes.FileInfo" />
    public class ResourceFileInfo : FileInfo
    {
        /// <summary>
        ///     Gets or sets the name of the resource source file.
        /// </summary>
        /// <value>
        ///     The name of the resource source file.
        /// </value>
        [XmlElement("sourceFileName")]
        public string ResourceSourceFileName { get; set; }

        #region ctor

        /// <summary>
        ///     Prevents a default instance of the <see cref="ResourceFileInfo" /> class from being created.
        /// </summary>
        private ResourceFileInfo()
        {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceFileInfo" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        /// <param name="resourceFileName">Name of the resource file.</param>
        public ResourceFileInfo(string name, string path, string resourceFileName = null)
            : base(name, path)
        {
            this.ResourceSourceFileName = resourceFileName;
        }

        #endregion
    }
}