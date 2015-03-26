// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModuleControls.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnModuleControls type.
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
    public class DnnModuleControls
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleControls"/> class.
        /// </summary>
        internal DnnModuleControls()
        {
            this.moduleControlItems = new List<DnnModuleControl>();
        }
        
        #endregion

        #region [ Properties ]

        /// <summary>
        /// The module control items.
        /// </summary>
        private readonly List<DnnModuleControl> moduleControlItems;

        /// <summary>
        /// Gets or sets the module controls.
        /// </summary>
        /// <value>
        /// The module controls.
        /// </value>
        [XmlElement("moduleControl")]
        public List<DnnModuleControl> Items
        {
            get
            {
                return this.moduleControlItems;
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
