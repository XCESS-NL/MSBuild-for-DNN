// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserControlParser.cs" company="XCESS expertise center b.v.">
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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Dnn.MsBuild.Tasks.Composition.Component;
using Dnn.MsBuild.Tasks.Extensions;

namespace Dnn.MsBuild.Tasks.Parsers
{
    public class UserControlParser
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlParser"/> class.
        /// </summary>
        public UserControlParser()
        {
            this.UserControls = new Dictionary<string, string>();
            this.UserControlBaseClassPattern = new Regex(@"\s(?i)inherits(?-i)=""[a-zA-Z0-9\.]+""\s", RegexOptions.IgnoreCase);
        }

        #endregion

        public const string UserControlFileExtension = ".ascx";

        public IDictionary<string, string> UserControls { get; }

        protected Regex UserControlBaseClassPattern { get; }

        public IDictionary<string, string> Parse(IEnumerable<string> sourceFiles)
        {
            sourceFiles.ForEach(
                fileName =>
                {
                    if (fileName.EndsWith(UserControlFileExtension, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Only for user controls...
                        using (var reader = new StreamReader(fileName))
                        {
                            // TODO: optimize. Only read until found <> or EOF.
                            var content = reader.ReadToEnd();

                            var match = this.UserControlBaseClassPattern.Match(content);
                            if (match.Success)
                            {
                                // We know that the match.Value includes a '=' character, because it is part of the Regular Expression.
                                var baseClass = match.Value.Split('=')
                                                     .Last()
                                                     .Trim(new[] {' ', '"'});

                                var startOfDesktopModuleFolder = fileName.IndexOf(ModuleComponentBuilder.DesktopModuleFolderName);
                                var relativeUserControlPath = fileName.Substring(startOfDesktopModuleFolder)
                                                                      .Replace(@"/", @"\"); // Replace the forward slashes by backslashes in line with relative URIs.
                                this.UserControls.Add(baseClass, relativeUserControlPath);
                            }
                        }
                    }
                });

            return this.UserControls;
        }
    }
}