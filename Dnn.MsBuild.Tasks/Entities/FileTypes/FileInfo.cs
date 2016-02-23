// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileInfo.cs" company="XCESS expertise center b.v.">
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

using System.Xml.Serialization;

namespace Dnn.MsBuild.Tasks.Entities.FileTypes
{
    /// <summary>
    /// </summary>
    public abstract class FileInfo : IFileInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfo"/> class.
        /// </summary>
        protected FileInfo()
            : this(null)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfo"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected FileInfo(string name)
            : this(name, null)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfo"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        protected FileInfo(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }

        #endregion

        #region Implementation of IFileInfo

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        [XmlElement("path")]
        public string Path { get; set; }

        #endregion
    }
}