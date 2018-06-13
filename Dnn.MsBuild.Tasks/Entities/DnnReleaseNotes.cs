﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnReleaseNotes.cs" company="XCESS expertise center b.v.">
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
    using System.Xml.Serialization;

    public class DnnReleaseNotes
    {
        /// <summary>
        ///     The default package release notes file name
        /// </summary>
        public const string DefaultFilePath = "releaseNotes.txt";

        /// <summary>
        ///     Gets or sets the file path.
        /// </summary>
        /// <value>
        ///     The file path.
        /// </value>
        [XmlAttribute("src")]
        public string FilePath { get; set; }

        #region ctor

        /// <summary>
        ///     Prevents a default instance of the <see cref="DnnReleaseNotes" /> class from being created.
        /// </summary>
        private DnnReleaseNotes()
        {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="DnnReleaseNotes" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        internal DnnReleaseNotes(string filePath)
        {
            this.FilePath = filePath;
        }

        #endregion
    }
}