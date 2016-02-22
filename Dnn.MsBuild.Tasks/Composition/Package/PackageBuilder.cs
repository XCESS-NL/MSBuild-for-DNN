﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageBuilder.cs" company="XCESS expertise center b.v.">
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
using System.Reflection;
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Entities.Internal;
using Dnn.MsBuild.Tasks.Extensions;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Composition.Package
{
    internal abstract class PackageBuilder : BaseBuilder<DnnPackage>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageBuilder"/> class.
        /// </summary>
        /// <param name="packageType">Type of the package.</param>
        protected PackageBuilder(DnnPackageType packageType)
            : base(new DnnPackage(packageType))
        {}

        #endregion

        #region Overrides of BaseBuilder<DnnPackage>

        protected override void BuildElement(ITaskData data)
        {
            this.SetPackageCoreAttributes(data);
            this.SetPackageLicense(data);
            this.SetPackageOwner(data);
            this.SetPackageDependencies(data);
            this.SetPackageReleaseNotes(data);
        }

        protected override void OnComponentCreated(IManifestElement component)
        {
            var componentToAdd = component as DnnComponent;
            if (componentToAdd != null)
            {
                this.Element.Components.Add(componentToAdd);
            }
        }

        #endregion

        /// <summary>
        /// Sets the package core attributes.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void SetPackageCoreAttributes(ITaskData data)
        {
            var assemblyName = data.Assembly.GetName();

            var packageAttribute = data.Assembly.GetCustomAttribute<DnnPackageAttribute>();
            var assemblyTitle = data.Assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            var assemblyDescription = data.Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();

            this.Element.Name = this.Element
                                    .Name
                                    .FirstNotEmpty(packageAttribute?.Name,
                                                   assemblyTitle?.Title,
                                                   assemblyName.Name);
            this.Element.FriendlyName = this.Element
                                            .FriendlyName
                                            .FirstNotEmpty(packageAttribute?.FriendlyName,
                                                           packageAttribute?.Name,
                                                           assemblyTitle?.Title,
                                                           assemblyName.Name);

            this.Element.Description = this.Element
                                           .Description
                                           .FirstNotEmpty(packageAttribute?.Description,
                                                          assemblyDescription?.Description,
                                                          assemblyTitle?.Title,
                                                          assemblyName.Name);

            this.SetPackageIcon(data, packageAttribute);

            this.Element.Version = assemblyName.Version;
        }

        /// <summary>
        /// Sets the package dependencies.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void SetPackageDependencies(ITaskData data)
        {
            data.ExportedTypes.ForEach(this.TestPackageDependencyAttribteAndAddMatching);
        }

        protected virtual void SetPackageIcon(ITaskData data, DnnPackageAttribute packageAttribute)
        {
            var iconFilePath = packageAttribute?.IconFileName;

            if (string.IsNullOrWhiteSpace(iconFilePath))
            {
                iconFilePath = $"{data.Assembly.GetName().Name}32.png";
            }

            var iconFileFullPath = Path.Combine(data.ProjectFileData.BasePath, iconFilePath);
            if (File.Exists(iconFileFullPath))
            {
                this.Element.IconFilePath = iconFilePath;
            }
        }

        /// <summary>
        /// Sets the package license.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void SetPackageLicense(ITaskData data)
        {
            var licenseFilePath = DnnLicense.DefaultFilePath;

            var metaAttributeType = data.ExportedTypes.FirstOrDefault(arg => arg.HasAttribute<DnnPackageMetaAttribute>());
            if (metaAttributeType != null)
            {
                var attribute = metaAttributeType.GetCustomAttribute<DnnPackageMetaAttribute>();
                licenseFilePath = attribute.LicensePath;
            }

            var licenseFile = data.ProjectFileData
                                  .ResourceFiles
                                  .FirstOrDefault(arg => arg.Name.EndsWith(licenseFilePath, StringComparison.InvariantCultureIgnoreCase));

            if (licenseFile != null)
            {
                // TODO: verify existance of license file
                this.Element.License = new DnnLicense()
                                       {
                                           FilePath = licenseFile.Name
                                       };
            }
        }

        /// <summary>
        /// Sets the package owner.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void SetPackageOwner(ITaskData data)
        {
            var companyAttribute = data.Assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            this.Element.Owner.Organisation = this.Element
                                                  .Owner
                                                  .Organisation
                                                  .FirstNotEmpty(companyAttribute?.Company);

            var companyInfoAttribute = data.Assembly.GetCustomAttribute<AssemblyOwnerInfoAttribute>();
            // ReSharper disable once InvertIf
            if (companyInfoAttribute != null)
            {
                this.Element.Owner.Email = companyInfoAttribute?.EmailAddress;
                this.Element.Owner.Name = companyInfoAttribute?.Name;
                this.Element.Owner.Url = companyInfoAttribute?.Url;
            }
        }

        /// <summary>
        /// Sets the package release notes.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void SetPackageReleaseNotes(ITaskData data)
        {
            var releaseNoteFilePath = DnnReleaseNotes.DefaultFilePath;

            var metaAttributeType = data.ExportedTypes.FirstOrDefault(arg => arg.HasAttribute<DnnPackageMetaAttribute>());
            if (metaAttributeType != null)
            {
                var attribute = metaAttributeType.GetCustomAttribute<DnnPackageMetaAttribute>();
                releaseNoteFilePath = attribute.ReleaseNotesPath;
            }

            var licenseFile = data.ProjectFileData
                                  .ResourceFiles
                                  .FirstOrDefault(arg => arg.Name.EndsWith(releaseNoteFilePath, StringComparison.InvariantCultureIgnoreCase));

            if (licenseFile != null)
            {
                // TODO: verify existance of license file
                // TODO: Provide a mechanisme for multiple release notes (version dependant).
                this.Element.ReleaseNotes = new DnnReleaseNotes()
                                            {
                                                FilePath = licenseFile.Name
                                            };
            }

        }

        /// <summary>
        /// Adds the package dependency.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        private void AddPackageDependency(DnnPackageDependencyAttribute attribute)
        {
            this.Element.Dependencies.Add(DnnPackageDependency.FromAttribute(attribute));
        }

        /// <summary>
        /// Tests the package dependency attribte and add matching.
        /// </summary>
        /// <param name="type">The type.</param>
        private void TestPackageDependencyAttribteAndAddMatching(Type type)
        {
            var attributes = type.GetCustomAttributes<DnnPackageDependencyAttribute>();
            attributes.ForEach(this.AddPackageDependency);
        }

        #region PackageBuilder Creator

        /// <summary>
        /// Creates a concrete DnnPackage builder from the assembly. By default a ModulePackageBuilder is created, unless the assembly defines the DnnPackage attribute.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static IBuilder<DnnPackage> CreateFromAssembly(Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<DnnPackageAttribute>();
            switch (attribute?.PackageType)
            {
                case DnnPackageType.Auth_System:
                    return new AuthenticationProviderPackageBuilder();

                // ReSharper disable once RedundantCaseLabel
                case DnnPackageType.Module:
                default:
                    return new ModulePackageBuilder();
            }
        }

        #endregion
    }
}