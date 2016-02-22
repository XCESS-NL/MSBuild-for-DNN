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
using System.Text.RegularExpressions;
using Dnn.MsBuild.Tasks;

namespace Dnn.MsBuild.Generator
{
    internal class Program
    {
        private const string DesktopModulesFolderName = "DesktopModules";

        private static void Main(string[] args)
        {
            var setup = AppDomain.CurrentDomain.SetupInformation;

            // Determine the website root
            var regex = new Regex($@"(?i)\b{DesktopModulesFolderName}\b");
            var match = regex.Match(setup.ApplicationBase);

            // ReSharper disable once InvertIf
            if (match.Success)
            {
                var websiteBase = setup.ApplicationBase.Substring(0, match.Index);

                // Build DNN module
                BuildManifest(Path.Combine(websiteBase, DesktopModulesFolderName, "XCESS.DNN.Module", "XCESS.DNN.Module.csproj"),
                              Path.Combine(websiteBase, DesktopModulesFolderName, "XCESS.DNN.Module", "bin", "XCESS.DNN.Module.dll"));

                // Build DNN Authentication Provider
                BuildManifest(Path.Combine(websiteBase, DesktopModulesFolderName, "XCESS.DNN.AuthenticationProvider", "XCESS.DNN.AuthenticationProvider.csproj"),
                              Path.Combine(websiteBase, DesktopModulesFolderName, "XCESS.DNN.AuthenticationProvider", "bin", "XCESS.DNN.AuthenticationProvider.dll"));
            }
        }

        /// <summary>
        /// Builds the manifest.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        private static void BuildManifest(string projectFile, string assemblyName)
        {
            var entityBuilder = new BuildDnnManifest
                                {
                                    ProjectFile = projectFile,
                                    ProjectTargetAssembly = assemblyName
                                };
            entityBuilder.Execute();
        }
    }
}