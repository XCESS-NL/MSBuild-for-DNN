// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimePropertyAccess.cs" company="XCESS expertise center b.v.">
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
using System.Globalization;

namespace Dnn.MsBuild.Tasks.Components.Tokens.PropertyAccess
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Dnn.MsBuild.Tasks.Components.Tokens.IPropertyAccess" />
    internal class DateTimePropertyAccess : IPropertyAccess
    {
        #region Implementation of IPropertyAccess

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="propertyNotFound">if set to <c>true</c> [property not found].</param>
        /// <returns></returns>
        public string GetProperty(string propertyName, string format, CultureInfo formatProvider, ref bool propertyNotFound)
        {
            propertyNotFound = false;

            var propertyValue = default(string);
            var outputFormat = string.IsNullOrWhiteSpace(format) ? "g" : format;

            switch (propertyName.ToLowerInvariant())
            {
                case "system":
                    propertyValue = DateTime.Now.ToString(outputFormat, formatProvider);
                    break;
                case "utc":
                    propertyValue = DateTime.Now.ToUniversalTime().ToString(outputFormat, formatProvider);
                    break;
                default:
                    propertyNotFound = true;
                    break;
            }
            return propertyValue ?? string.Empty;
        }

        #endregion
    }
}