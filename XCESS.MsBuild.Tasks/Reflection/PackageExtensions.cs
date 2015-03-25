// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageExtensions.cs" company="XCESS expertise center b.v.">
//   Copyright (c) 2014 XCESS expertise center b.v. 
//   
//   The software is owned by XCESS expertise center b.v. and is protected by 
//   the Dutch copyright laws and international treaty provisions. 
//   You are allowed to make copies of the software solely for backup or archival purposes. 
//   You may not lease, rent, export or sublicense the software. 
//   You may not reverse engineer, decompile, disassemble or create derivative works from the software.
//   
//   XCESS expertise center b.v., Storkstraat 19, 3833 LB Leusden, The Netherlands
//   T. +31-33-4335151, I. http://www.xcess.nl
// </copyright>
// <summary>
//   Defines the PackageExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks.Reflection
{
    using System.Globalization;
    using System.Reflection;
    using XCESS.MsBuild.Attributes;
    using XCESS.MsBuild.Tasks.Entities;

    internal static class PackageExtensions
    {
        /// <summary>
        /// Enriches the package from assembly.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static DnnPackage EnrichPackageFromAssembly(this DnnPackage source, Assembly assembly)
        {
            // Package friendly name: if empty set from the AssemblyTitle attribute or defaults to the package name.
            if (string.IsNullOrWhiteSpace(source.FriendlyName))
            {
                var titleAttribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
                source.FriendlyName = titleAttribute != null && !string.IsNullOrWhiteSpace(titleAttribute.Title) ? titleAttribute.Title : source.Name;
            }

            // Package description: if empty set from AssemblyDescription attribute or defaults to the package name.
            if (string.IsNullOrWhiteSpace(source.Description))
            {
                var descriptionAttribute = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
                source.Description = descriptionAttribute != null && !string.IsNullOrWhiteSpace(descriptionAttribute.Description) ? descriptionAttribute.Description : source.Name;
            }

            // Package owner
            var owner = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            if (owner != null)
            {
                source.Owner.Organisation = owner.Company;
            }

            var companyInfo = assembly.GetCustomAttribute<AssemblyCompanyInfoAttribute>();
            if (companyInfo != null)
            {
                source.Owner.Email = companyInfo.EmailAddress;
                source.Owner.Url = companyInfo.Url;
            }

            return source;
        }
    }
}
