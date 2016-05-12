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

[assembly: AssemblyProduct("XCESS.DNN.AuthenticationProvider")]
[assembly: AssemblyTitle("MsBuild Test Authentication Provider")]
[assembly: AssemblyDescription("A DNN AuthenticationProvider for demonstration purposes of the XCESS DNN MsBuild.")] // Description in DNN manifest
#if (DEBUG)

[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: DnnPackage("XCESS.DNN.AuthenticationProvider", "XCESS.DNN.AuthenticationProvider", DnnPackageType.Auth_System)]
[assembly: DnnAzureCompliant(true)]
[assembly: AssemblyCompany("XCESS expertise center b.v.")] // Organisation in DNN manifest
[assembly: AssemblyOwnerInfo("XCESS", "info@xcess.nl", "http://www.xcess.nl")] // Email and url in DNN manifest

[assembly: AssemblyCopyright("Copyright © 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("391D4CDA-1C14-4B66-A146-D54E7D578D49")]

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

[assembly: AssemblyVersion("2.1.0.4")]
[assembly: AssemblyFileVersion("2.1.0.4")]