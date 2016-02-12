// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModulePackageBuilder.cs" company="XCESS expertise center b.v.">
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
using System.Reflection;
using Dnn.MsBuild.Tasks.Composition.Component;
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Entities.Internal;
using Dnn.MsBuild.Tasks.Extensions;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Composition.Package
{
    internal class ModulePackageBuilder : PackageBuilder
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulePackageBuilder"/> class.
        /// </summary>
        public ModulePackageBuilder()
            : base(DnnPackageType.Module)
        {
            // A DNN module package typically contains the following components:
            // 1. Component Module          (required)
            // 2. Component Assembly        (required)
            // 3. Component Script          (optional)
            // 4. Component ResourceFile    (required)
            this.ComponentBuilders.Add(new ModuleComponentBuilder());
            this.ComponentBuilders.Add(new AssemblyComponentBuilder());
            this.ComponentBuilders.Add(new ScriptComponentBuilder());
            this.ComponentBuilders.Add(new ResourceFileComponentBuilder());
        }

        #endregion

        protected override void BuildElement(ITaskData data)
        {
            this.SetPackageCoreAttributes(data);
            this.SetPackageLicense(data);
            this.SetPackageOwner(data);
            this.SetPackageDependencies(data);
            this.SetPackageReleaseNotes(data);
        }

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

            this.Element.IconFileName = packageAttribute?.IconFileName;

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

        protected virtual void SetPackageLicense(ITaskData data)
        {
            // TODO: Provide Implementation
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

        protected virtual void SetPackageReleaseNotes(ITaskData data)
        {
            // TODO: Provide Implementation
        }

        private void AddPackageDependency(DnnPackageDependencyAttribute attribute)
        {
            this.Element.Dependencies.Add(DnnPackageDependency.FromAttribute(attribute));
        }

        private void TestPackageDependencyAttribteAndAddMatching(Type type)
        {
            var attributes = type.GetCustomAttributes<DnnPackageDependencyAttribute>();
            attributes.ForEach(this.AddPackageDependency);
        }
    }
}