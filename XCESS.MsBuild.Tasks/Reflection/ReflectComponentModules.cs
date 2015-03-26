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
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using Microsoft.Build.Framework;
    using XCESS.MsBuild.Attributes;
    using XCESS.MsBuild.Tasks.Components;
    using XCESS.MsBuild.Tasks.Entities;

    /// <summary>
    /// </summary>
    internal class ReflectComponentModules
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectComponentModules"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="packages">The packages.</param>
        /// <param name="sourceFiles">The source files.</param>
        private ReflectComponentModules(Assembly assembly, IEnumerable<DnnPackage> packages, ITaskItem[] sourceFiles)
        {
            this.DesktopModules = new SortedDictionary<string, DnnDesktopModule>();
            this.ExportedTypes = assembly.ExportedTypes;
            this.Packages = packages;

            this.CreateDnnUserControlDictionary(sourceFiles);
        }
        
        #endregion

        #region [ Properties ]

        protected IEnumerable<Type> ExportedTypes { get; set; }

        protected IEnumerable<DnnPackage> Packages { get; set; }

        protected IDictionary<string, string> UserControls { get; set; }

        protected IDictionary<string, DnnDesktopModule> DesktopModules { get; private set; }

        #endregion

        /// <summary>
        /// Gets the module controls.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DnnDesktopModule> GetDesktopModules()
        {
            // Retrieve all exported types
            this.ExportedTypes.ForEach(
                type =>
                    {
                        var businessControllerAttribute = type.GetCustomAttribute<DnnBusinessControllerAttribute>(false);
                        if (businessControllerAttribute != null)
                        {
                        }
                        else
                        {
                            var moduleControlAttribute = type.GetCustomAttribute<DnnModuleControlAttribute>(false);
                            if (moduleControlAttribute != null && this.UserControls.ContainsKey(type.FullName))
                            {
                                var moduleControl = DnnModuleControl.FromAttribute(moduleControlAttribute, type, this.UserControls[type.FullName], this.Packages);

                                var desktopModule = default(DnnDesktopModule);
                                var desktopModuleAttribute = type.GetCustomAttribute<DnnDesktopModuleAttribute>(false);
                                desktopModule = desktopModuleAttribute == null ? this.EnsureDefaultDesktopModule() : this.CreateDesktopModuleFromAttribute(desktopModuleAttribute);

                                // In this version of XCESS MsBuild a desktopModule supports only a single ModuleDefinition.
                                var moduleDefinition = desktopModule.ModuleDefinitions.Definitions.First();
                                moduleDefinition.ModuleControls.Items.Add(moduleControl);
                            }
                        }
                    });

            return this.DesktopModules.Values;
        }

        /// <summary>
        /// Creates the DNN user control dictionary.
        /// </summary>
        /// <param name="sourceFiles">The source files.</param>
        private void CreateDnnUserControlDictionary(ITaskItem[] sourceFiles)
        {
            var desktopModuleFolder = string.Format(@"\{0}\", DnnGlobals.DnnDesktopModuleFolder);
            var userControlBaseClassPattern = new Regex(@"\s(?i)inherits(?-i)=""[a-zA-Z0-9\.]+""\s", RegexOptions.IgnoreCase);

            this.UserControls = new Dictionary<string, string>();
            sourceFiles.ForEach(
                item =>
                    {
                        if (item.ItemSpec.EndsWith(DnnGlobals.UserControlFileExtension, StringComparison.InvariantCultureIgnoreCase))
                        {
                            // Only for user controls...
                            using (var reader = new StreamReader(item.ItemSpec))
                            {
                                // TODO: optimize. Only read until found <> or EOF.
                                var content = reader.ReadToEnd();

                                var match = userControlBaseClassPattern.Match(content);
                                if (match.Success)
                                {
                                    // We know that the match.Value includes a '=' character, because it is part of the Regular Expression.
                                    var baseClass = match.Value.Split('=')
                                                         .Last()
                                                         .Trim(new[] { ' ', '"' });

                                    var startOfDesktopModuleFolder = item.ItemSpec.IndexOf(desktopModuleFolder);
                                    var relativeUserControlPath = item.ItemSpec.Substring(startOfDesktopModuleFolder + 1) // Add 1 so the first backslash won't be included.
                                                                      .Replace(@"\", @"/"); // Replace the backslashes by forward slashes in line with relative URIs.
                                    this.UserControls.Add(baseClass, relativeUserControlPath);
                                }
                            }
                        }
                    });
        }

        /// <summary>
        /// Creates the desktop module from attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        private DnnDesktopModule CreateDesktopModuleFromAttribute(DnnDesktopModuleAttribute attribute)
        {
            if (!this.DesktopModules.ContainsKey(attribute.ModuleName))
            {
                this.DesktopModules.Add(attribute.ModuleName, DnnDesktopModule.FromAttribute(attribute));
            }

            return this.DesktopModules[attribute.ModuleName];
        }

        /// <summary>
        /// Ensures the default desktop module.
        /// </summary>
        /// <returns></returns>
        private DnnDesktopModule EnsureDefaultDesktopModule()
        {
            var defaultPackage = this.Packages.FirstOrDefault();
            var moduleName = defaultPackage.Name;
            if (!this.DesktopModules.ContainsKey(moduleName))
            {
                var desktopModule = new DnnDesktopModule(moduleName);
                this.DesktopModules.Add(moduleName, desktopModule);
            }

            return this.DesktopModules[moduleName];
        }

        /// <summary>
        /// Parses the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="packages">The packages.</param>
        /// <param name="sourceFiles">The source files.</param>
        /// <returns></returns>
        public static void AssignComponents(Assembly assembly, IEnumerable<DnnPackage> packages, ITaskItem[] sourceFiles)
        {
            var parser = new ReflectComponentModules(assembly, packages, sourceFiles);
            var desktopModules = parser.GetDesktopModules();

            desktopModules.ForEach(
                desktopModule =>
                    {
                        var moduleComponent = new DnnComponentModule(desktopModule);

                        // Find the package to which this module component belongs.
                        var packageName = moduleComponent.DesktopModule.ModuleName;
                        var package = packages.FirstOrDefault(arg => arg.Name.Equals(packageName, StringComparison.InvariantCultureIgnoreCase));

                        // Update the module component with package details.
                        moduleComponent.DesktopModule.ModuleDefinitions.Definitions.ForEach(definition => definition.FriendlyName = package.FriendlyName);

                        // Add the module component
                        package.Components.Components.Add(moduleComponent);
                    });
        }
    }
}
