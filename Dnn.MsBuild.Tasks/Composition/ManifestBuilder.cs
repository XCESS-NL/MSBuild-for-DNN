// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManifestBuilder.cs" company="XCESS expertise center bv">
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

using Dnn.MsBuild.Tasks.Entities;

namespace Dnn.MsBuild.Tasks.Composition
{
    internal class ManifestBuilder : IBuilder<DnnManifest>
    {
        #region Implementation of IBuilder<out DnnManifest>

        public DnnManifest Build(IManifestData data)
        {
            var builder = PackageBuilder.CreateFromAssembly(data.Assembly);
            var package = builder.Build(data) as DnnPackage;

            var manifest = new DnnManifest();
            manifest.Packages.Add(package);

            return manifest;
        }

        #endregion
    }
}