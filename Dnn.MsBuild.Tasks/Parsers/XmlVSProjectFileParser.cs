// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlVSProjectFileParser.cs" company="XCESS expertise center b.v.">
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
using System.Xml;
using Dnn.MsBuild.Tasks.Entities.FileTypes;
using Dnn.MsBuild.Tasks.Entities.Internal;
using Dnn.MsBuild.Tasks.Extensions;

namespace Dnn.MsBuild.Tasks.Parsers
{
    internal class XmlVsProjectFileParser : IProjectFileParser
    {
        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlVsProjectFileParser"/> class.
        /// </summary>
        internal XmlVsProjectFileParser()
        {
            this.References = new List<AssemblyFileInfo>();
            this.ResourceFiles = new List<IFileInfo>();
        }

        #endregion

        private const string ExtensionSqlDataProvider = ".sqldataprovider";

        protected XmlNamespaceManager NamespaceManager { get; set; }

        protected IList<AssemblyFileInfo> References { get; }

        protected IList<IFileInfo> ResourceFiles { get; }

        #region Implementation of IProjectFileParser

        public IProjectFileData Parse(string fileName)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);

                // ReSharper disable once InvertIf
                if (xmlDocument.DocumentElement != null)
                {
                    this.NamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                    this.FindAndParseItemGroups(xmlDocument.DocumentElement);
                }

                return new ProjectFileData(Path.GetDirectoryName(fileName), this.References, this.ResourceFiles);
            }
            catch (Exception)
            {
                // TODO: Handle exceptions...
                throw;
            }
        }

        #endregion

        protected virtual void FindAndParseItemGroups(XmlElement node)
        {
            this.NamespaceManager.AddNamespace("prj", node.NamespaceURI);

            var itemGroups = node.SelectNodes("//prj:ItemGroup", this.NamespaceManager);
            itemGroups.OfType<XmlNode>()
                      .ForEach(this.ParseItemGroup);
        }

        private void ParseItemGroup(XmlNode node)
        {
            this.FindAndAddReferences(node);
            this.FindAndAddResources(node);
        }

        #region References

        private void AddReference(XmlNode node)
        {
            var attribute = node?.Attributes?
                                 .OfType<XmlAttribute>()?
                                 .FirstOrDefault(arg => arg.Name.Equals("include", StringComparison.InvariantCultureIgnoreCase));

            // ReSharper disable once InvertIf
            if (!string.IsNullOrWhiteSpace(attribute?.Value))
            {
                this.References.Add(new AssemblyFileInfo(attribute.Value, null));
            }
        }

        private void FindAndAddReferences(XmlNode node)
        {
            node.SelectNodes("prj:Reference", this.NamespaceManager)
                .OfType<XmlNode>()
                .ForEach(this.AddReference);
        }

        #endregion

        #region Resources

        private void AddResourceFile(XmlNode node)
        {
            var attribute = node?.Attributes?
                                 .OfType<XmlAttribute>()?
                                 .FirstOrDefault(arg => arg.Name.Equals("include", StringComparison.InvariantCultureIgnoreCase));

            // ReSharper disable once InvertIf
            // ReSharper disable once PossibleMultipleEnumeration
            if (!string.IsNullOrWhiteSpace(attribute?.Value))
            {
                var filePath = attribute.Value;

                this.ResourceFiles.Add(this.CreateFileInfo(filePath));
            }
        }

        private IFileInfo CreateFileInfo(string filePath)
        {
            var fileInfo = default(IFileInfo);

            var fileName = Path.GetFileName(filePath);
            var path = Path.GetDirectoryName(filePath);

            switch (Path.GetExtension(filePath).ToLowerInvariant())
            {
                case ExtensionSqlDataProvider:
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath).ToLowerInvariant();
                    Version version = null;

                    switch (fileNameWithoutExtension)
                    {
                        case ScriptFileInfo.InstallScriptFileName:
                        case ScriptFileInfo.UninstallScriptFileName:
                            break;
                        default:
                            var versionParts = fileNameWithoutExtension.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
                            version = new Version(Convert.ToInt32(versionParts[0]), // Major
                                                  Convert.ToInt32(versionParts[1]), // Minor
                                                  0, // Build
                                                  Convert.ToInt32(versionParts[2])); // Revision
                            break;
                    }

                    fileInfo = new ScriptFileInfo(fileName, path, version);
                    break;
                default:
                    fileInfo = new ResourceFileInfo(fileName, path);
                    break;
            }

            return fileInfo;
        }

        private void FindAndAddResources(XmlNode node)
        {
            node.SelectNodes("prj:Content", this.NamespaceManager)
                .OfType<XmlNode>()
                .ForEach(this.AddResourceFile);
        }

        #endregion
    }
}