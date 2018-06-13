// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnAuthenticationControlAttribute.cs" company="XCESS expertise center b.v.">
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

    [AttributeUsage(AttributeTargets.Class)]
    public class DnnAuthenticationControlAttribute : DnnManifestAttribute
    {
        #region ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="DnnAuthenticationControlAttribute" /> class.
        /// </summary>
        /// <param name="controlType">Type of the control.</param>
        public DnnAuthenticationControlAttribute(DnnAuthenticationControlType controlType)
        {
            this.ControlType = controlType;
        }

        #endregion

        public DnnAuthenticationControlType ControlType { get; }
    }
}