// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="XCESS expertise center bv">
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

using System.Reflection;
using System.Runtime.InteropServices;
using DotNetNuke.Services.Installer.MsBuild;

[assembly: AssemblyProduct("XCESS.DNN.Module")]
[assembly: AssemblyTitle("MsBuild Test Module")]
[assembly: AssemblyDescription("A DNN module for demonstration purposes of the XCESS DNN MsBuild.")] // Description in DNN manifest
#if (DEBUG)

[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: DnnPackage("XCESS.DNN.Module", @"XCESS\XCESS.DNN.Module", DnnPackageType.Module, IconFileName = @"install\MyIcon32.png")]
[assembly: DnnAzureCompliant(true)]
[assembly: AssemblyCompany("XCESS expertise center b.v.")] // Organisation in DNN manifest
[assembly: AssemblyOwnerInfo("XCESS", "info@xcess.nl", "http://www.xcess.nl")] // Email and url in DNN manifest

[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("d9553526-3106-4d95-8b6b-fe2e561bc0a7")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]