// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModulePackageBuilder.cs" company="XCESS expertise center bv">
//   Copyright (c) 2016 XCESS expertise center bv
//   
//   The software is owned by XCESS and is protected by 
//   the Dutch copyright laws and international treaty provisions.
//   You are allowed to make copies of the software solely for backup or archival purposes.
//   You may not lease, rent, export or sublicense the software.
//   You may not reverse engineer, decompile, disassemble or create derivative works from the software.
//   
//   Owned by XCESS expertise center b.v., Storkstraat 19, 3833 LB Leusden, The Netherlands
//   T. +31-33-4335151, E. info@xcess.nl, I. http://www.xcess.nl
// </copyright>
// <summary>
//   
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using Dnn.MsBuild.Tasks.Components;
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Extensions;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Composition
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
            this.ComponentBuilders.Add(new ModuleComponentBuilder());
        }

        #endregion

        protected override void BuildElement(IManifestData data)
        {
            this.SetPackageCoreAttributes(data);
            this.SetPackageOwner(data);
            this.SetPackageDependencies(data);
        }

        /// <summary>
        /// Sets the package core attributes.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void SetPackageCoreAttributes(IManifestData data)
        {
            var assemblyName = data.Assembly.GetName();

            var packageAttribute = data.Assembly.GetCustomAttribute<DnnPackageAttribute>();
            var assemblyTitle = data.Assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            var assemblyDescription = data.Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();

            this.Package.Name = this.Package
                                    .Name
                                    .FirstNotEmpty(packageAttribute?.Name,
                                                   assemblyTitle?.Title,
                                                   assemblyName.Name);
            this.Package.FriendlyName = this.Package
                                            .FriendlyName
                                            .FirstNotEmpty(packageAttribute?.FriendlyName,
                                                           packageAttribute?.Name,
                                                           assemblyTitle?.Title,
                                                           assemblyName.Name);

            this.Package.Description = this.Package
                                           .Description
                                           .FirstNotEmpty(packageAttribute?.Description,
                                                          assemblyDescription?.Description,
                                                          assemblyTitle?.Title,
                                                          assemblyName.Name);

            this.Package.IconFileName = packageAttribute?.IconFileName;

            this.Package.Version = assemblyName.Version;
        }

        /// <summary>
        /// Sets the package dependencies.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void SetPackageDependencies(IManifestData data)
        {
            data.ExportedTypes.ForEach(this.TestPackageDependencyAttribteAndAddMatching);
        }

        /// <summary>
        /// Sets the package owner.
        /// </summary>
        /// <param name="data">The data.</param>
        protected virtual void SetPackageOwner(IManifestData data)
        {
            var companyAttribute = data.Assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            this.Package.Owner.Organisation = this.Package
                                                  .Owner
                                                  .Organisation
                                                  .FirstNotEmpty(companyAttribute?.Company);

            var companyInfoAttribute = data.Assembly.GetCustomAttribute<AssemblyOwnerInfoAttribute>();
            // ReSharper disable once InvertIf
            if (companyInfoAttribute != null)
            {
                this.Package.Owner.Email = companyInfoAttribute?.EmailAddress;
                this.Package.Owner.Name = companyInfoAttribute?.Name;
                this.Package.Owner.Url = companyInfoAttribute?.Url;
            }
        }

        private void AddPackageDependency(DnnPackageDependencyAttribute attribute)
        {
            this.Package.Dependencies.Add(DnnPackageDependency.FromAttribute(attribute));
        }

        private void TestPackageDependencyAttribteAndAddMatching(Type type)
        {
            var attributes = type.GetCustomAttributes<DnnPackageDependencyAttribute>();
            attributes.ForEach(this.AddPackageDependency);
        }
    }
}