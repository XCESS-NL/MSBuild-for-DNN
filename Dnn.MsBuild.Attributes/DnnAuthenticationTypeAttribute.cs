// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnAuthenticationTypeAttribute.cs" company="XCESS expertise center b.v.">
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

// ReSharper disable once CheckNamespace
namespace DotNetNuke.Services.Installer.MsBuild
{
    /// <summary>
    /// </summary>
    /// <seealso cref="DotNetNuke.Services.Installer.MsBuild.DnnManifestAttribute" />
    [AttributeUsage(AttributeTargets.Assembly)]
    public class DnnAuthenticationTypeAttribute : DnnManifestAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnAuthenticationTypeAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DnnAuthenticationTypeAttribute(string type)
        {
            this.Type = type;
        }

        #endregion

        /// <summary>
        /// Gets the authentication type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; }
    }
}