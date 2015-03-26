// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnPackageAttribute.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnPackageAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Attributes
{
    using System;

    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]   // Current version of XCESS MsBuild only supports a single package
    public sealed class DnnPackageAttribute : Attribute
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnPackageAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="System.ArgumentException">The package name cannot be null or an empty string.;name.</exception>
        public DnnPackageAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                // TODO: Localize...
                throw new ArgumentException("The package name cannot be null or an empty string.", "name");
            }

            this.Name = name;
            this.PackageType = DnnPackageType.Module;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets a value indicating whether [azure compatible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [azure compatible]; otherwise, <c>false</c>.
        /// </value>
        public bool AzureCompatible { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the icon file.
        /// </summary>
        /// <value>
        /// The name of the icon file.
        /// </value>
        public string IconFileName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the package.
        /// </summary>
        /// <value>
        /// The name of the package.
        /// </value>
        public string PackageName { get; set; }

        /// <summary>
        /// Gets or sets the type of the package.
        /// </summary>
        /// <value>
        /// The type of the package.
        /// </value>
        public DnnPackageType PackageType { get; set; }

        #endregion
    }
}
