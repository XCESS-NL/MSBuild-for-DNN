// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnPackageDependency.cs" company="XCESS expertise center bv">
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
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    [XmlRoot("dependency")]
    public class DnnPackageDependency
    {
        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnPackageDependency"/> class from being created.
        /// </summary>
        private DnnPackageDependency()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnPackageDependency"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        internal DnnPackageDependency(DnnPackageDependencyType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        #endregion

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [XmlAttribute("type")]
        public DnnPackageDependencyType Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [XmlText]
        public string Value { get; set; }

        public static DnnPackageDependency FromAttribute(DnnPackageDependencyAttribute attribute)
        {
            return new DnnPackageDependency(attribute.Type, attribute.Value);
        }
    }
}