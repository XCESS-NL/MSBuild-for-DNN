// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModulePermissionAttribute.cs" company="XCESS expertise center b.v.">
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
//   Defines the DnnModulePermissionAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Attributes
{
    using System;

    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [Serializable]
    public sealed class DnnModulePermissionAttribute : Attribute
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModulePermissionAttribute"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="key">The key.</param>
        /// <param name="name">The name.</param>
        public DnnModulePermissionAttribute(string code, string key, string name)
        {
            this.Code = code;
            this.Key = key;
            this.Name = name;
        }
        
        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
 
        #endregion    
    }
}
