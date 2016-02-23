// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnLicense.cs" company="XCESS expertise center b.v.">
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

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    public class DnnLicense
    {
        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnLicense"/> class from being created.
        /// </summary>
        private DnnLicense()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnLicense"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        internal DnnLicense(string filePath)
        {
            this.FilePath = filePath;
        }

        #endregion

        /// <summary>
        /// The default package license file name
        /// </summary>
        public const string DefaultFilePath = "license.txt";

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        [XmlAttribute("src")]
        public string FilePath { get; set; }
    }
}