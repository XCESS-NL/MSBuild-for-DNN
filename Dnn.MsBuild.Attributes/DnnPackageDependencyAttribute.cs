// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnPackageDependencyAttribute.cs" company="XCESS expertise center bv">
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

// ReSharper disable once CheckNamespace
namespace DotNetNuke.Services.Installer.MsBuild
{
    public class DnnPackageDependencyAttribute : DnnManifestAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DnnPackageDependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public DnnPackageDependencyAttribute(DnnPackageDependencyType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public DnnPackageDependencyType Type { get; }

        public string Value { get; }
    }
}