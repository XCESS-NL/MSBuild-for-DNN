﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnComponent.cs" company="XCESS expertise center b.v.">
//     Copyright (c) 2017-2018 XCESS expertise center b.v.
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

namespace Dnn.MsBuild.Tasks.Entities
{
    using System.Xml.Serialization;
    using Dnn.MsBuild.Tasks.Composition;
    using DotNetNuke.Services.Installer.MsBuild;

    /// <summary>
    /// </summary>
    [XmlInclude(typeof(DnnComponentAssembly))]
    [XmlInclude(typeof(DnnComponentAuthenticationSystem))]
    [XmlInclude(typeof(DnnComponentCleanup))]
    [XmlInclude(typeof(DnnComponentConfig))]
    [XmlInclude(typeof(DnnComponentModule))]
    [XmlInclude(typeof(DnnComponentResourceFile))]
    [XmlInclude(typeof(DnnComponentScript))]
    [XmlRoot("component")]
    public abstract class DnnComponent : IManifestElement
    {
        private readonly DnnComponentType _componentType;

        /// <summary>
        ///     Gets or sets the type of the component.
        /// </summary>
        /// <value>
        ///     The type of the component.
        /// </value>
        [XmlAttribute("type")]
        public DnnComponentType ComponentType
        {
            get { return this._componentType; }
            // ReSharper disable once ValueParameterNotUsed
            set { }
        }

        #region ctor

        /// <summary>
        ///     Prevents a default instance of the <see cref="DnnComponent" /> class from being created.
        /// </summary>
        private DnnComponent()
        {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="DnnComponent" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        protected DnnComponent(DnnComponentType type)
        {
            this._componentType = type;
        }

        #endregion
    }
}