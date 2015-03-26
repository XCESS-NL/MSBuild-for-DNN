// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManifestFileBuilder.cs" company="XCESS expertise center b.v.">
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
//   Defines the ManifestFileBuilder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks
{
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// </summary>
    /// <typeparam name="TManifest">The type of the manifest.</typeparam>
    public class ManifestFileBuilder<TManifest>
        where TManifest : IManifest
    {
        /// <summary>
        /// Builds the specified manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        public void Build(TManifest manifest)
        {
            var serializer = new XmlSerializer(typeof(TManifest));
            using (TextWriter writer = new StreamWriter(manifest.FileName))
            {
                serializer.Serialize(writer, manifest);
            }
        }
    }
}
