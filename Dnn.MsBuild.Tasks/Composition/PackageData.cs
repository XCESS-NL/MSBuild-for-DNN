// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageData.cs" company="XCESS expertise center bv">
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
using System.Collections.Generic;
using System.Reflection;

namespace Dnn.MsBuild.Tasks.Composition
{
    public class PackageData : IManifestData
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageData"/> class.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        public PackageData(string assemblyPath)
            : this(Assembly.LoadFrom(assemblyPath))
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageData"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public PackageData(Assembly assembly)
        {
            this.Assembly = assembly;
            this.ExportedTypes = this.Assembly.GetExportedTypes();
        }

        #endregion

        #region Implementation of IManifestData

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <value>
        /// The assembly.
        /// </value>
        public Assembly Assembly { get; }

        /// <summary>
        /// Gets the exported types.
        /// </summary>
        /// <value>
        /// The exported types.
        /// </value>
        public IEnumerable<Type> ExportedTypes { get; }

        /// <summary>
        /// Gets or sets the package.
        /// </summary>
        /// <value>
        /// The package.
        /// </value>
        public IPackageData Package { get; set; }

        public IDictionary<string, string> UserControls { get; set; }

        #endregion
    }
}