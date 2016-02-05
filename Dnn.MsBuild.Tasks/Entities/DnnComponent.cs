// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnComponent.cs" company="XCESS expertise center bv">
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

using System.Xml.Serialization;
using Dnn.MsBuild.Tasks.Composition;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    [XmlInclude(typeof(DnnComponentAssembly))]
    [XmlInclude(typeof(DnnComponentCleanup))]
    [XmlInclude(typeof(DnnComponentModule))]
    [XmlInclude(typeof(DnnComponentResourceFile))]
    [XmlInclude(typeof(DnnComponentScript))]
    [XmlRoot("component")]
    public abstract class DnnComponent : IManifestElement
    {
        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnComponent"/> class from being created.
        /// </summary>
        private DnnComponent()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnComponent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        protected DnnComponent(DnnComponentType type)
        {
            this._componentType = type;
        }

        #endregion

        private readonly DnnComponentType _componentType;

        /// <summary>
        /// Gets or sets the type of the component.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        [XmlAttribute("type")]
        public DnnComponentType ComponentType
        {
            get { return this._componentType; }
            // ReSharper disable once ValueParameterNotUsed
            set { }
        }
    }
}