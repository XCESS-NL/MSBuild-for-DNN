// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnDesktopModuleAttribute.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnDesktopModuleAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Attributes
{
    using System;

    /// <summary>
    /// </summary>
    [Serializable]
    public sealed class DnnDesktopModuleAttribute : Attribute
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnDesktopModuleAttribute"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public DnnDesktopModuleAttribute(string moduleName)
            : this(moduleName, moduleName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnDesktopModuleAttribute"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        public DnnDesktopModuleAttribute(string moduleName, string friendlyName)
        {
            this.FriendlyName = friendlyName;
            this.ModuleName = moduleName;
        }
        
        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        /// <value>
        /// The name of the folder.
        /// </value>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        public string FriendlyName { get; private set; }

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName { get; private set; }
 
        #endregion    
    }
}
