// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnComponentModule.cs" company="XCESS expertise center bv">
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

using System;
using System.Xml.Serialization;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// <component type="Module">
    ///   <desktopModule>
    ///     <moduleName/>
    ///     <foldername/>
    ///     <businessControllerClass/>
    ///     <codeSubdirectory />
    ///     <isAdmin />
    ///     <isPremium/>
    ///     <supportedFeatures>
    ///       <supportedFeature type="" />
    ///     </supportedFeatures>
    ///     <moduleDefinitions /> 
    ///   </desktopModule>
    ///   <eventMessage /> 
    ///   </component>
    /// ]]>
    /// </remarks>
    [Serializable]
    public class DnnComponentModule : DnnComponent
    {
        [XmlAttribute("type")]
        public override DnnComponentType ComponentType
        {
            get { return DnnComponentType.Module; }
            set { }
        }

        [XmlElement("desktopModule")]
        public DnnDesktopModule DesktopModule { get; set; }

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnComponentModule"/> class from being created.
        /// </summary>
        internal DnnComponentModule()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnComponentModule"/> class.
        /// </summary>
        /// <param name="desktopModule">The desktop module.</param>
        public DnnComponentModule(DnnDesktopModule desktopModule)
        {
            this.DesktopModule = desktopModule;
        }

        #endregion
    }
}