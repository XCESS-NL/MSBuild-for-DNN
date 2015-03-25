// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectPackages.cs" company="XCESS expertise center b.v.">
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
//   Defines the ReflectPackages type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using XCESS.MsBuild.Attributes;
    using XCESS.MsBuild.Tasks.Entities;

    /// <summary>
    /// </summary>
    internal class ReflectPackages
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectPackages"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        private ReflectPackages(Assembly assembly)
        {
            this.Assembly = assembly;
        }
        
        #endregion

        private Assembly Assembly { get; set; }

        /// <summary>
        /// Creates the package from attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        private DnnPackage CreatePackageFromAttribute(DnnPackageAttribute attribute)
        {
            var assemblyName = this.Assembly.GetName();
            return DnnPackage.FromAttribute(attribute, assemblyName).EnrichPackageFromAssembly(this.Assembly);
        }

        /// <summary>
        /// Creates the package from assembly.
        /// </summary>
        /// <returns></returns>
        private DnnPackage CreatePackageFromAssembly()
        {
            var assemblyName = this.Assembly.GetName();
            return new DnnPackage(assemblyName).EnrichPackageFromAssembly(this.Assembly);
        }

        public IEnumerable<DnnPackage> GetPackages()
        {
            var packages = default(IEnumerable<DnnPackage>);
            var packageAttributes = this.Assembly.GetCustomAttributes(typeof(DnnPackageAttribute), false)
                                        .OfType<DnnPackageAttribute>();

            if (packageAttributes.Any())
            {
                // At least one DnnPackage attribute found...
                packages = packageAttributes.Select(this.CreatePackageFromAttribute)
                                            .ToList();
            }
            else
            {
                // No DnnPackage attribute found, create the Package from the assembly info
                packages = new List<DnnPackage>() { this.CreatePackageFromAssembly() };
            }

            return packages;
        }

        public static IEnumerable<DnnPackage> Parse(Assembly assembly)
        {
            var packageReflector = new ReflectPackages(assembly);
            var packages = packageReflector.GetPackages();

            var moduleControls = ReflectModuleControls.Parse(assembly, packages);

            return packages;
        }
    }
}
