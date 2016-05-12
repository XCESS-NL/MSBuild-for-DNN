// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModulePackageBuilder.cs" company="XCESS expertise center b.v.">
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

using System.Linq;
using Dnn.MsBuild.Tasks.Composition.Component;
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Entities.Internal;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Composition.Package
{
    internal class ModulePackageBuilder : PackageBuilder
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulePackageBuilder"/> class.
        /// </summary>
        public ModulePackageBuilder()
            : base(DnnPackageType.Module)
        {
            // TODO: Move always present component builders to the base class

            // A DNN module package typically contains the following components:
            // 1. Component Module          (required)
            // 2. Component Assembly        (required)
            // 3. Component Script          (optional)
            // 4. Component ResourceFile    (required)
            this.ComponentBuilders.Add(new ModuleComponentBuilder());
            this.ComponentBuilders.Add(new AssemblyComponentBuilder());
            this.ComponentBuilders.Add(new ScriptComponentBuilder());
            this.ComponentBuilders.Add(new ResourceFileComponentBuilder());
        }

        #endregion

        #region Overrides of PackageBuilder

        public override DnnPackage Build(ITaskData data)
        {
            var package = base.Build(data);

            // Gets the module component.
            var moduleComponent = package.Components.OfType<DnnComponentModule>().FirstOrDefault();

            return package;
        }

        #endregion
    }
}