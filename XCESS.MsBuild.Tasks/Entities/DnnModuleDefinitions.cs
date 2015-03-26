// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModuleDefinitions.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnModuleDefinitions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// </summary>
    [Serializable]
    public class DnnModuleDefinitions
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleDefinitions"/> class.
        /// </summary>
        internal DnnModuleDefinitions()
        {
            this.moduleDefinitions = new List<DnnModuleDefinition>() { new DnnModuleDefinition() };
        }
        
        #endregion

        #region [ Properties ]

        /// <summary>
        /// The module definitions.
        /// </summary>
        private readonly List<DnnModuleDefinition> moduleDefinitions;

        /// <summary>
        /// Gets or sets the definitions.
        /// </summary>
        /// <value>
        /// The definitions.
        /// </value>
        [XmlElement("moduleDefinition")]
        public List<DnnModuleDefinition> Definitions
        {
            get
            {
                return this.moduleDefinitions;
            }
            // ReSharper disable once ValueParameterNotUsed
            set
            {
                // Ignored...
            }
        }

        #endregion
    }
}
