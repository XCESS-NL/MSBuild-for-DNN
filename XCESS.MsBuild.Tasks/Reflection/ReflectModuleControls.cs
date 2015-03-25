// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectModuleControls.cs" company="XCESS expertise center b.v.">
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
//   Defines the ReflectModuleControls type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using XCESS.MsBuild.Attributes;
    using XCESS.MsBuild.Tasks.Entities;

    /// <summary>
    /// </summary>
    internal class ReflectModuleControls
    {
        private ReflectModuleControls(Assembly assembly, IEnumerable<DnnPackage> packages)
        {
            this.ExportedTypes = assembly.ExportedTypes;
            this.Packages = packages;
        }

        private IEnumerable<Type> ExportedTypes { get; set; }

        private IEnumerable<DnnPackage> Packages { get; set; }

        public IEnumerable<DnnModuleControl> GetModuleControls()
        {
            var dekstopModules = new List<DnnDesktopModule>();
            var moduleControls = new Dictionary<string, DnnModuleControl>();

            var packageFolder = (this.Packages.Count() == 1)
                                    ? this.Packages.First()
                                          .Name.Replace('.', '_')
                                    : string.Empty;
            var requiredDesktopModuleAttribute = string.IsNullOrWhiteSpace(packageFolder);

            foreach (var type in this.ExportedTypes)
            {
                var moduleControlAttribute = type.GetCustomAttribute<DnnModuleControlAttribute>(false);
                if (moduleControlAttribute != null)
                {
                    var moduleControl = DnnModuleControl.FromAttribute(moduleControlAttribute, type, this.Packages);
                    moduleControls.Add(moduleControl.Key, moduleControl);
                }
            }

            return moduleControls.Values;
        }

        public static IEnumerable<DnnModuleControl> Parse(Assembly assembly, IEnumerable<DnnPackage> packages)
        {
            var parser = new ReflectModuleControls(assembly, packages);
            return parser.GetModuleControls();
        }
    }
}
