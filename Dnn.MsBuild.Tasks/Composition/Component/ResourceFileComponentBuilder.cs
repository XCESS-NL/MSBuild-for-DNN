// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceFileComponentBuilder.cs" company="XCESS expertise center b.v.">
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
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Entities.FileTypes;

namespace Dnn.MsBuild.Tasks.Composition.Component
{
    internal class ResourceFileComponentBuilder : ComponentBuilder<DnnComponentResourceFile>
    {
        #region Overrides of ComponentBuilder<DnnComponentResourceFile>

        protected override DnnComponentResourceFile BuildElement()
        {
            var resourceFiles = this.Input
                                    .ProjectFileData
                                    .ResourceFiles
                                    .OfType<ResourceFileInfo>()
                                    .ToList();

            // TODO: use switch to determine whether to create a single resources zip or multiple. Or even specify each individual resource :)
            // ReSharper disable once InvertIf
            if (resourceFiles.Any())
            {
                // Create a single resources ZIP file.
                var defaultResourceFile = new ResourceFileInfo()
                                          {
                                              Name = "resources.zip",
                                              ResourceSourceFileName = "resources.zip"
                                          };
                return new DnnComponentResourceFile(this.GetBasePath(), new List<ResourceFileInfo>()
                                                                    {
                                                                        defaultResourceFile
                                                                    });
            }

            return new DnnComponentResourceFile();
        }

        #endregion
    }
}