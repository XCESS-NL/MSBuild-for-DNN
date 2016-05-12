// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModuleComponentBuilder.cs" company="XCESS expertise center b.v.">
//     Copyright (c) 2016-2016 XCESS expertise center b.v.
// 
//     Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//     documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//     the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
//     to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
//     The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//     of the Software.
// 
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//     TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//     THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//     CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//     DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Extensions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Composition.Component
{
    internal class ModuleComponentBuilder : ComponentBuilder<DnnComponentModule>
    {
        public const string DesktopModuleFolderName = "DesktopModules";

        #region Overrides of ComponentBuilder<DnnComponentModule>

        protected override DnnComponentModule BuildElement()
        {
            // Only a single DesktopModule per module component
            var component = new DnnComponentModule(this.GetOrCreateDesktopModule());
            component.DesktopModule.ModuleDefinitions.AddRange(this.GetModuleDefinitions());

            return component;
        }

        #endregion

        private DnnDesktopModule GetOrCreateDesktopModule()
        {
            var desktopModule = new DnnDesktopModule();

            // Only a single desktop module is allowed per package.
            var desktopModuleAttribute = this.Input.ExportedTypes.GetCustomAttribute<DnnDesktopModuleAttribute>();
            desktopModule.FolderName = desktopModule.FolderName
                                                    .FirstNotEmpty(desktopModuleAttribute?.FolderName, this.Input.Package?.Folder);
            desktopModule.ModuleName = desktopModule.ModuleName
                                                    .FirstNotEmpty(desktopModuleAttribute?.ModuleName, this.Input.Package?.Name);

            if (desktopModuleAttribute != null)
            {
                desktopModule.IsAdmin = desktopModuleAttribute.IsAdmin;
                desktopModule.IsPremium = desktopModuleAttribute.IsPremium;
            }

            var type = this.Input.ExportedTypes.FirstOrDefault(arg => arg.HasAttribute<DnnBusinessControllerAttribute>());
            // ReSharper disable once InvertIf
            if (type != null)
            {
                var assemblyName = type.Assembly.GetName();
                desktopModule.BusinessControllerClass = $"{type.FullName}, {assemblyName.Name}";

                var supportedInterfaces = type.GetInterfaces();
                desktopModule.AssignSupportedFeature<IPortable>(supportedInterfaces, DnnSupportedFeatureType.Portable);
                desktopModule.AssignSupportedFeature<ISearchable>(supportedInterfaces, DnnSupportedFeatureType.Searchable);
                desktopModule.AssignSupportedFeature<IUpgradeable>(supportedInterfaces, DnnSupportedFeatureType.Upgradeable);
                if (type.IsSubclassOf(typeof(ModuleSearchBase)) && desktopModule.SupportedFeatures.All(arg => arg.Type != DnnSupportedFeatureType.Searchable))
                {
                    desktopModule.SupportedFeatures.Add(new DnnSupportedFeature(DnnSupportedFeatureType.Searchable));
                }
            }

            return desktopModule;
        }

        private IEnumerable<DnnModuleDefinition> GetModuleDefinitions()
        {
            var moduleDefinitions = new Dictionary<string, DnnModuleDefinition>();

            var userControls = this.Input.ProjectFileData?.UserControls;
            // ReSharper disable once InvertIf
            if (userControls != null)
            {

                var moduleControlTypes = this.Input.ExportedTypes.Where(arg => arg.HasAttribute<DnnModuleControlAttribute>());
                moduleControlTypes.ForEach(arg =>
                                           {
                                               var moduleControlAttribtute = arg.GetCustomAttribute<DnnModuleControlAttribute>();

                                               // Add the module definition for the module control, if it does not already exist.
                                               var moduleDefinitionName = moduleControlAttribtute.ModuleDefinition ?? DnnModuleDefinition.DefaultModuleDefinitionName;
                                               if (!moduleDefinitions.ContainsKey(moduleDefinitionName))
                                               {
                                                   moduleDefinitions.Add(moduleDefinitionName, new DnnModuleDefinition(moduleDefinitionName));
                                               }

                                               var moduleDefinition = moduleDefinitions[moduleDefinitionName];

                                               var userControlFilePath = string.Empty;
                                               var moduleControl = DnnModuleControl.CreateFromAttribute(moduleControlAttribtute,
                                                                                                        userControls.TryGetValue(arg.FullName, out userControlFilePath)
                                                                                                            ? userControlFilePath
                                                                                                            : arg.FullName);
                                               moduleDefinition.ModuleControls.Add(moduleControl);

                                               var permissionAttribute = arg.GetCustomAttribute<DnnModulePermissionAttribute>();
                                               if (permissionAttribute != null)
                                               {
                                                   // Create and add the permission.
                                                   moduleDefinition.Permissions.Add(DnnModulePermission.CreateFromAttribute(permissionAttribute));
                                               }
                                           });
            }

            return moduleDefinitions.Values;
        }
    }
}