﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LicenseInfo.cs" company="XCESS expertise center b.v.">
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

namespace Dnn.MsBuild.Tasks.Entities.Internal
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Services.Tokens;

    internal class LicenseInfo : IPropertyAccess
    {
        public string CompanyName { get; set; }

        public int YearFrom { get; set; }

        public int YearTill { get; set; }

        #region Implementation of IPropertyAccess

        /// <summary>
        ///     Gets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="accessingUser">The accessing user.</param>
        /// <param name="accessLevel">The access level.</param>
        /// <param name="propertyNotFound">if set to <c>true</c> [property not found].</param>
        /// <returns></returns>
        public string GetProperty(string propertyName, string format, CultureInfo formatProvider, UserInfo accessingUser,
                                  Scope accessLevel, ref bool propertyNotFound)
        {
            var result = string.Empty;
            var requestedProperty = this.GetType()
                                        .GetProperties(BindingFlags.Public | BindingFlags.GetProperty)
                                        .FirstOrDefault(
                                            arg =>
                                                arg.Name.Equals(propertyName,
                                                                StringComparison.InvariantCultureIgnoreCase));

            propertyNotFound = requestedProperty == null;
            if (!propertyNotFound)
            {
                if (string.IsNullOrWhiteSpace(format))
                {
                    format = "{0:d}";
                }

                switch (requestedProperty.Name)
                {
                    case nameof(this.YearFrom):
                        result = string.Format(formatProvider, format, this.YearFrom);
                        break;
                    case nameof(this.YearTill):
                        result = string.Format(formatProvider, format, this.YearFrom);
                        break;
                    case nameof(this.CompanyName):
                        result = this.CompanyName;
                        break;
                    default:
                        propertyNotFound = true;
                        break;
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets the cacheability.
        /// </summary>
        /// <value>
        ///     The cacheability.
        /// </value>
        public CacheLevel Cacheability => CacheLevel.fullyCacheable;

        #endregion
    }
}