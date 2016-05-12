// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LicenseBuilder.cs" company="XCESS expertise center b.v.">
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
using System.IO;
using System.Linq;
using Dnn.MsBuild.Tasks.Components;
using Dnn.MsBuild.Tasks.Components.Tokens;
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Entities.FileTypes;
using Dnn.MsBuild.Tasks.Entities.Internal;
using Dnn.MsBuild.Tasks.Extensions;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Composition.Package
{
    internal class LicenseBuilder : IBuilder<DnnLicense>
    {
        public static string DefaultLicenseFilePath = DefaultLicenseFileName + DefaultTextExtension;

        public static string DefaultLicenseFileName = "license";

        public static string DefaultTemplateExtension = ".template";

        public static string DefaultTextExtension = ".txt";

        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseBuilder"/> class.
        /// </summary>
        /// <param name="package">The package.</param>
        public LicenseBuilder(DnnPackage package)
        {
            this.Package = package;
        }

        private DnnPackage Package { get; }

        #region Implementation of IBuilder<out DnnLicense>

        public DnnLicense Build(ITaskData taskData)
        {
            var licenseFile = FindLicenseFile(taskData);
            if (licenseFile == null)
            {
                return null;
            }

            var fileName = licenseFile.Name;

            // Check if the specified license is a template. If so, than any tokens should be replaced and a new license file should be created.
            // ReSharper disable once InvertIf
            if (fileName.EndsWith(DefaultTemplateExtension, StringComparison.InvariantCultureIgnoreCase))
            {
                var licenseContent = string.Empty;
                using (var stream = new StreamReader(Path.Combine(taskData.ProjectFileData.BasePath, licenseFile.Path, fileName)))
                {
                    licenseContent = stream.ReadToEnd();
                }

                // Template - so replace variables
                var tokenReplace = new DnnLicenseTokenReplace(this.Package);
                var actualLicense = tokenReplace.ReplaceTokens(licenseContent);

                // ReSharper disable once InvertIf
                if (!string.IsNullOrWhiteSpace(actualLicense))
                {
                    fileName = fileName.Replace(DefaultTemplateExtension, DefaultTextExtension);
                    var targetLicenseFilePath = Path.Combine(taskData.ProjectFileData.BasePath, licenseFile.Path, fileName);
                    File.WriteAllText(targetLicenseFilePath, actualLicense);
                }
            }

            return new DnnLicense(Path.Combine(licenseFile.Path, fileName));
        }

        #endregion

        private static IFileInfo FindLicenseFile(ITaskData taskData)
        {
            var attribute = taskData.ExportedTypes.GetCustomAttribute<DnnPackageMetaAttribute>();
            var resourceFiles = taskData.ProjectFileData
                                        .ResourceFiles;

            var licenseFile = default(IFileInfo);
            if (attribute != null)
            {
                // Try to look up the specified license file
                licenseFile = resourceFiles.FirstOrDefault(arg => arg.Name.EndsWith(attribute.LicensePath, StringComparison.InvariantCultureIgnoreCase));
            }

            if (licenseFile == null)
            {
                // Try to look up the default license template file
                var licenseFileName = LicenseBuilder.DefaultLicenseFileName + LicenseBuilder.DefaultTemplateExtension;
                licenseFile = resourceFiles.FirstOrDefault(arg => arg.Name.EndsWith(licenseFileName, StringComparison.InvariantCultureIgnoreCase));
            }

            // ReSharper disable once InvertIf
            if (licenseFile == null)
            {
                var licenseFileName = LicenseBuilder.DefaultLicenseFileName + LicenseBuilder.DefaultTextExtension;
                licenseFile = resourceFiles.FirstOrDefault(arg => arg.Name.EndsWith(licenseFileName, StringComparison.InvariantCultureIgnoreCase));
            }

            return licenseFile;
        }
    }
}