// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnPackages.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnPackages type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// </summary>
    [Serializable]
    [XmlRoot("packages")]
    public class DnnPackages
    {
        public DnnPackages()
        {
            this.Packages = new List<DnnPackage>();
        }

        public DnnPackages(List<DnnPackage> packages)
        {
            this.Packages = packages;
        }

        [XmlElement("package")]
        public List<DnnPackage> Packages { get; private set; }
    }
}
