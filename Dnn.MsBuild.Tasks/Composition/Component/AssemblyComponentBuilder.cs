// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyComponentBuilder.cs" company="XCESS expertise center b.v.">
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

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Entities.FileTypes;

namespace Dnn.MsBuild.Tasks.Composition.Component
{
    internal class AssemblyComponentBuilder : ComponentBuilder<DnnComponentAssembly>
    {
        public string AssemblyExtension => ".dll";

        public string DefaultAssemblyPath => "bin";

        protected string[] DnnAssemblies { get; set; }

        #region Overrides of ComponentBuilder<DnnComponentAssembly>

        protected override DnnComponentAssembly BuildElement()
        {
            this.DnnAssemblies = Directory.GetFiles(this.Input.DnnAssemblyPath, $"*{this.AssemblyExtension}");

            var assembly = this.Input.Assembly;
            var references = assembly.GetReferencedAssemblies()
                                     .Where(this.IncludeAssemblies)
                                     .ToList();

            var component = new DnnComponentAssembly();
            references.ForEach(arg => component.Assemblies.Add(new AssemblyFileInfo()
                                                               {
                                                                   Name = arg.Name + this.AssemblyExtension,
                                                                   Path = this.DefaultAssemblyPath
                                                               }));
            return component;
        }

        #endregion

        private bool IncludeAssemblies(AssemblyName assemblyName)
        {
            var name = assemblyName.Name + this.AssemblyExtension;
            var excludeAssembly = name.StartsWith("mscorlib") ||
                                  name.StartsWith("System.") ||
                                  this.DnnAssemblies.Any(arg => arg.EndsWith(name, StringComparison.InvariantCultureIgnoreCase));

            return !excludeAssembly;
        }
    }
}