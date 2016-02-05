// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManifestEntityBuilder.cs" company="XCESS expertise center bv">
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Dnn.MsBuild.Tasks.Composition;
using Dnn.MsBuild.Tasks.Entities;
using Microsoft.Build.Framework;

namespace Dnn.MsBuild.Tasks
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TManifest">The type of the manifest.</typeparam>
    public class ManifestEntityBuilder<TManifest>
        where TManifest : class, IManifest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestEntityBuilder{TManifest}" /> class.
        /// </summary>
        /// <param name="packageAssembly">The manifest assembly.</param>
        /// <param name="sourceFiles">The source files.</param>
        public ManifestEntityBuilder(string packageAssembly, ITaskItem[] sourceFiles)
        {
            this.SetupDnnAssemblyLocator(packageAssembly);

            // Gather the package data needed for building the manifest
            var packageData = new PackageData(packageAssembly)
                              {
                                  UserControls = this.GetUserControls(sourceFiles)
                              };

            // Build the manifest!
            var builder = new ManifestBuilder();
            var manifest = builder.Build(packageData);

            // Serialize manifest
            var serializer = new XmlSerializer(typeof(DnnManifest));
            using (var stream = new StreamWriter(manifest.FileName))
            {
                serializer.Serialize(stream, manifest);
            }
        }

        protected string DnnAssemblyPath { get; set; }

        private IDictionary<string, string> GetUserControls(ITaskItem[] sourceFiles)
        {
            var parser = new UserControlParser();
            return parser.Parse(sourceFiles);
        }

        private void SetupDnnAssemblyLocator(string packageAssembly)
        {
            // Determine the DNN bin folder
            var index = packageAssembly.IndexOf("DesktopModules", StringComparison.InvariantCultureIgnoreCase);
            this.DnnAssemblyPath = Path.Combine(packageAssembly.Substring(0, index), "bin");

            AppDomain.CurrentDomain.AssemblyResolve += this.CurrentDomainOnAssemblyResolve;
        }

        private Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var fileNameParts = args.Name.Split(',');

            // ReSharper disable once InvertIf
            if (fileNameParts.First().StartsWith("dotnetnuke", StringComparison.InvariantCultureIgnoreCase))
            {
                var assemblyToLoad = Path.Combine(this.DnnAssemblyPath, fileNameParts.First() + ".dll");
                return Assembly.LoadFrom(assemblyToLoad);
            }

            // TODO: Extent exception
            throw new FileNotFoundException();
        }

        #endregion
    }
}