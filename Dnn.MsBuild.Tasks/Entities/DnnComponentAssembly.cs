// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnComponentAssembly.cs" company="XCESS expertise center bv">
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

using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// <component type="Assembly">
    ///     <assemblies>
    ///         <assembly [Action="UnRegister"]>
    ///             <path></path>
    ///             <name></name>
    ///             <version></version>
    ///         </assembly>
    ///     </assemblies>
    /// </component>
    /// ]]>
    /// </remarks>
    public class DnnComponentAssembly : DnnComponent
    {
        /// <summary>
        /// Gets or sets the type of the component.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        public override DnnComponentType ComponentType { get; set; }
    }
}