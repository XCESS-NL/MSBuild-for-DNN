﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModulePermission.cs" company="XCESS expertise center bv">
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
    public class DnnModulePermission
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModulePermission"/> class.
        /// </summary>
        internal DnnModulePermission()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModulePermission"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="key">The key.</param>
        /// <param name="name">The name.</param>
        internal DnnModulePermission(string code, string key, string name)
        {
            this.Code = code;
            this.Key = key;
            this.Name = name;
        }

        #endregion

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [XmlAttribute("code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [XmlAttribute("key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Froms the attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static DnnModulePermission CreateFromAttribute(DnnModulePermissionAttribute attribute)
        {
            return new DnnModulePermission(attribute.Code, attribute.Key, attribute.Name);
        }
    }
}