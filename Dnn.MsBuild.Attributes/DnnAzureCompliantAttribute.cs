// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnAzureCompliantAttribute.cs" company="XCESS expertise center bv">
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
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class DnnAzureCompliantAttribute : DnnManifestAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnAzureCompliantAttribute"/> class.
        /// </summary>
        /// <param name="isCompliant">if set to <c>true</c> [is compliant].</param>
        public DnnAzureCompliantAttribute(bool isCompliant)
        {
            this.IsCompliant = isCompliant;
        }

        #endregion

        /// <summary>
        /// Gets a value indicating whether this instance is compliant.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is compliant; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompliant { get; private set; }
    }
}