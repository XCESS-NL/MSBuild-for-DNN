// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="XCESS expertise center b.v.">
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
using Dnn.MsBuild.Tasks;
using Dnn.MsBuild.Tasks.Entities;

namespace Dnn.MsBuild.Generator
{
    internal class Program
    {
        private const string DesktopModulesFolderName = "DesktopModules";

        private static void Main(string[] args)
        {
            var setup = AppDomain.CurrentDomain.SetupInformation;

            // Determine the website root
            var index = setup.ApplicationBase.IndexOf($"\\{DesktopModulesFolderName}\\");
            var websiteBase = setup.ApplicationBase.Substring(0, index);
            var dnnAssemblyPath = Path.Combine(websiteBase, "bin");

            // Build DNN module
            BuildManifest(dnnAssemblyPath,
                          Path.Combine(websiteBase, DesktopModulesFolderName, @"XCESS.DNN.Module\bin\XCESS.DNN.Module.dll"),
                          Path.Combine(websiteBase, DesktopModulesFolderName, @"XCESS.DNN.Module\XCESS.DNN.Module.csproj"));

            // Build DNN Authentication Provider
            BuildManifest(dnnAssemblyPath,
                          Path.Combine(websiteBase, DesktopModulesFolderName, @"XCESS.DNN.AuthenticationProvider\bin\XCESS.DNN.AuthenticationProvider.dll"),
                          Path.Combine(websiteBase, DesktopModulesFolderName, @"XCESS.DNN.AuthenticationProvider\XCESS.DNN.AuthenticationProvider.csproj"));
        }

        private static void BuildManifest(string dnnAssemblyPath, string assemblyName, string projectFile)
        {
            var entityBuilder = new BuildManifestTask<DnnManifest>(projectFile, assemblyName, dnnAssemblyPath);
            entityBuilder.Build();
        }
    }
}