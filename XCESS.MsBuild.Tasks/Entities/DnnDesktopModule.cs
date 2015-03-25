// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnDesktopModule.cs" company="XCESS expertise center b.v.">
//   Copyright (c) 2014 XCESS expertise center b.v. 
//   
//   The software is owned by XCESS expertise center b.v. and is protected by 
//   the Dutch copyright laws and international treaty provisions. 
//   You are allowed to make copies of the software solely for backup or archival purposes. 
//   You may not lease, rent, export or sublicense the software. 
//   You may not reverse engineer, decompile, disassemble or create derivative works from the software.
//   
//   XCESS expertise center b.v., Storkstraat 19, 3833 LB Leusden, The Netherlands
//   T. +31-33-4335151, I. http://www.xcess.nl
// </copyright>
// <summary>
//   Defines the DnnDesktopModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks.Entities
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// <desktopModule>
    ///   <moduleName/>
    ///   <foldername/>
    ///   <businessControllerClass/>
    ///   <codeSubdirectory />
    ///   <isAdmin />
    ///   <isPremium/>
    ///   <supportedFeatures>
    ///     <supportedFeature type="" />
    ///   </supportedFeatures>
    ///   <moduleDefinitions /> 
    /// </desktopModule>
    /// ]]>
    /// </remarks>
    [Serializable]
    public class DnnDesktopModule
    {
        /// <summary>
        /// Gets or sets the fully qualified name of the class that includes the extension methods (search, import/export, ...) as required by DNN.
        /// </summary>
        /// <value>
        /// The fully qualified name of the class.
        /// </value>
        [XmlElement("businessControllerClass")]
        public string BusinessControllerClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder where the module will be installed (relative to DesktopModules)
        /// </summary>
        /// <value>
        /// The name of the folder.
        /// </value>
        [XmlElement("folderName")]
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this module is an Admin module.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("isAdmin")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this module is a Premium module. Premium modules must be explicitely 
        /// assigned to portals in order to be available for users to install in the portal. 
        /// By default, isPremium is set to false.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is premium; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("isPremium")]
        public bool IsPremium { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        [XmlElement("moduleName")]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets whether this module is shareable.
        /// </summary>
        /// <value>
        /// The shareable.
        /// </value>
        // public ModuleSharing Shareable { get; set; }

        /// <summary>
        /// Gets or sets the module definitions.
        /// </summary>
        /// <value>
        /// The module definitions.
        /// </value>
        public DnnModuleDefinitions ModuleDefinitions { get; set; }
    }
}
