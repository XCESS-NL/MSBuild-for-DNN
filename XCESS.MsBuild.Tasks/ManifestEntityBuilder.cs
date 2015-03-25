// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManifestEntityBuilder.cs" company="XCESS expertise center b.v.">
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
//   Defines the ManifestEntityBuilder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Serialization;
    using XCESS.MsBuild.Tasks.Entities;
    using XCESS.MsBuild.Tasks.Reflection;

    /// <summary>
    /// </summary>
    /// <typeparam name="TManifest">The type of the manifest.</typeparam>
    public class ManifestEntityBuilder<TManifest>
        where TManifest : class, IManifest
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestEntityBuilder{TManifest}"/> class.
        /// </summary>
        /// <param name="manifestAssembly">The manifest assembly.</param>
        public ManifestEntityBuilder(string manifestAssembly)
        {
            var assembly = Assembly.LoadFrom(manifestAssembly);

            var dnnManifest = new DnnManifest
                                  {
                                      Content = new DnnPackages(
                                          ReflectPackages.Parse(assembly)
                                                         .ToList())
                                  };

            var serializer = new XmlSerializer(typeof(DnnPackages));
            using (var stream = new StreamWriter(dnnManifest.FileName))
            {
                serializer.Serialize(stream, dnnManifest.Content);
            }

            var moduleControls = ReflectModuleControls.Parse(assembly, dnnManifest.Content.Packages);

            var exportedTypes = assembly.GetExportedTypes()
                                        .OfType<Type>()
                                        .ToList();

            this.Manifest = dnnManifest as TManifest;
        }
        
        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the manifest.
        /// </summary>
        /// <value>
        /// The manifest.
        /// </value>
        public TManifest Manifest { get; private set; }

        #endregion
    }
}
