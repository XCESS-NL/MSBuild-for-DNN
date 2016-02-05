// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="XCESS expertise center bv">
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

using Dnn.MsBuild.Tasks;
using Dnn.MsBuild.Tasks.Entities;

namespace XCESS.MsBuild.Generator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sourceFiles = new FileTaskItem[]
                              {
                                  new FileTaskItem(@"D:\Projects\MsBuild\Website\DesktopModules\XCESS.DNN.Module\view.ascx"),
                                  new FileTaskItem(@"D:\Projects\MsBuild\Website\DesktopModules\XCESS.DNN.Module\settings.ascx")
                              };



            var fileBuilder = new ManifestFileBuilder<DnnManifest>();
            var entityBuilder = new ManifestEntityBuilder<DnnManifest>(@"D:\Projects\MsBuild\Website\DesktopModules\XCESS.DNN.Module\bin\XCESS.DNN.Module.dll", sourceFiles);
            // fileBuilder.Build(entityBuilder.Manifest);
        }
    }
}