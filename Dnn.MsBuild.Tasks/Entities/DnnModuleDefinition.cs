// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnModuleDefinition.cs" company="XCESS expertise center b.v.">
//     Copyright (c) 2016-2016 XCESS expertise center b.v.
// 
//     Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//     documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//     the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
//     to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
//     The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//     of the Software.
// 
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//     TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//     THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//     CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//     DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// <moduleDefinition>
    ///   <friendlyName/>
    ///   <defaultCacheTime/>
    ///   <moduleControls/>
    ///   <permissions/>
    /// </moduleDefinition>
    /// ]]>
    /// </remarks>
    [Serializable]
    public class DnnModuleDefinition
    {
        public const string DefaultModuleDefinitionName = "default";

        /// <summary>
        /// Gets or sets the default cache time.
        /// </summary>
        /// <value>
        /// The default cache time.
        /// </value>
        [XmlElement("defaultCacheTime")]
        public int DefaultCacheTime { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        [XmlElement("friendlyName")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the module controls.
        /// </summary>
        /// <value>
        /// The module controls.
        /// </value>
        [XmlArray("moduleControls")]
        [XmlArrayItem("moduleControl")]
        public List<DnnModuleControl> ModuleControls { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlIgnore]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        [XmlArray("permissions")]
        [XmlArrayItem("permission")]
        public List<DnnModulePermission> Permissions { get; set; }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleDefinition"/> class.
        /// </summary>
        internal DnnModuleDefinition()
            : this(DefaultModuleDefinitionName)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnModuleDefinition"/> class.
        /// </summary>
        internal DnnModuleDefinition(string name)
        {
            this.DefaultCacheTime = -1;
            this.ModuleControls = new List<DnnModuleControl>();
            this.Permissions = new List<DnnModulePermission>();
            this.Name = name;
        }

        #endregion
    }
}