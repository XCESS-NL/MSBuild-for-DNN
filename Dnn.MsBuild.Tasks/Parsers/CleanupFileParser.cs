// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CleanupFileParser.cs" company="XCESS expertise center b.v.">
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
using System.Linq;
using System.Text.RegularExpressions;
using Dnn.MsBuild.Tasks.Entities.FileTypes;

namespace Dnn.MsBuild.Tasks.Parsers
{
    internal class CleanupFileParser
    {
        private const string CleanupFilePattern = @"(?i)\d+\.\d+.\d+\{0}\b";

        private const string DefaultCleanupFileExtension = ".txt";

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanupComponentBuilder" /> class.
        /// </summary>
        /// <param name="cleanupFileExtension">The cleanup file extension.</param>
        internal CleanupFileParser(string cleanupFileExtension = DefaultCleanupFileExtension)
        {
            this.CleanupFileExtension = cleanupFileExtension;
        }

        #endregion

        /// <summary>
        /// Gets the cleanup file extension.
        /// </summary>
        /// <value>
        /// The cleanup file extension.
        /// </value>
        protected string CleanupFileExtension { get; }

        /// <summary>
        /// Gets the cleanup files.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        public IEnumerable<CleanupFileInfo> GetCleanupFiles(IEnumerable<IFileInfo> files)
        {
            var pattern = string.Format(CleanupFilePattern, this.CleanupFileExtension);
            var regex = new Regex(pattern);

            var cleanupFiles = files.Where(arg => regex.IsMatch(arg.Name))
                                    .Select(arg => new CleanupFileInfo(arg.Name, this.GetVersionFromFileName(arg.Name)))
                                    .OrderBy(arg => arg.Version)
                                    .ToList();

            return cleanupFiles;
        }

        /// <summary>
        /// Gets the name of the version from file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private Version GetVersionFromFileName(string fileName)
        {
            var fileNameWithoutExtension = fileName.Replace(this.CleanupFileExtension, string.Empty);
            var versionNumbers = fileNameWithoutExtension.Split('.')
                                                         .Select(arg => Convert.ToInt32(arg))
                                                         .ToList();
            return new Version(versionNumbers[0], versionNumbers[1], 0, versionNumbers[2]);
        }
    }
}