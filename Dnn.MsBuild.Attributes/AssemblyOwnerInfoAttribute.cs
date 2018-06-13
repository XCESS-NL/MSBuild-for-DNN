// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyOwnerInfoAttribute.cs" company="XCESS expertise center b.v.">
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

// ReSharper disable once CheckNamespace

namespace DotNetNuke.Services.Installer.MsBuild
{
    using System;

    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class AssemblyOwnerInfoAttribute : Attribute
    {
        #region ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssemblyOwnerInfoAttribute" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="url">The URL.</param>
        public AssemblyOwnerInfoAttribute(string name, string emailAddress, string url)
        {
            this.EmailAddress = emailAddress;
            this.Name = name;
            this.Url = url;
        }

        #endregion

        /// <summary>
        ///     Gets the owner email address.
        /// </summary>
        /// <value>
        ///     The email address.
        /// </value>
        public string EmailAddress { get; }

        /// <summary>
        ///     Gets the owner name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        ///     Gets the owner (usually the company) URL.
        /// </summary>
        /// <value>
        ///     The URL.
        /// </value>
        public string Url { get; }
    }
}