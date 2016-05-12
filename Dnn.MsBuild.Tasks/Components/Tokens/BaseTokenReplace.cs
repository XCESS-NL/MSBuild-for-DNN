// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseTokenReplace.cs" company="XCESS expertise center b.v.">
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

using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Dnn.MsBuild.Tasks.Components.Tokens.PropertyAccess;

namespace Dnn.MsBuild.Tasks.Components.Tokens
{
    internal abstract class BaseTokenReplace
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTokenReplace"/> class.
        /// </summary>
        protected BaseTokenReplace()
        {
            this.PropertySource[DateToken] = new DateTimePropertyAccess();
            this.PropertySource[DateTimeToken] = new DateTimePropertyAccess();
        }

        private const string ExpressionDefault =
            "(?:(?<text>\\[\\])|\\[(?:(?<object>[^\\]\\[:]+):(?<property>[^\\]\\[\\|]+))(?:\\|(?:(?<format>[^\\]\\[]+)\\|(?<ifEmpty>[^\\]\\[]+))|\\|(?:(?<format>[^\\|\\]\\[]+)))?\\])|(?<text>\\[[^\\]\\[]+\\])|(?<text>[^\\]\\[]+)";

        private const string ExpressionObjectLess =
            "(?:(?<text>\\[\\])|\\[(?:(?<object>[^\\]\\[:]+):(?<property>[^\\]\\[\\|]+))(?:\\|(?:(?<format>[^\\]\\[]+)\\|(?<ifEmpty>[^\\]\\[]+))|\\|(?:(?<format>[^\\|\\]\\[]+)))?\\])" +
            "|(?:(?<object>\\[)(?<property>[A-Z0-9._]+)(?:\\|(?:(?<format>[^\\]\\[]+)\\|(?<ifEmpty>[^\\]\\[]+))|\\|(?:(?<format>[^\\|\\]\\[]+)))?\\])" + "|(?<text>\\[[^\\]\\[]+\\])" +
            "|(?<text>[^\\]\\[]+)";

        protected const string DateToken = "date";

        protected const string DateTimeToken = "datetime";

        protected const string ObjectLessToken = "no_object";

        private CultureInfo _formatProvider;

        protected Dictionary<string, IPropertyAccess> PropertySource = new Dictionary<string, IPropertyAccess>();

        /// <summary>
        /// Gets the Format provider as Culture info from stored language or current culture
        /// </summary>
        /// <value>An CultureInfo</value>
        protected CultureInfo FormatProvider => this._formatProvider ?? (this._formatProvider = Thread.CurrentThread.CurrentUICulture);

        protected bool UseObjectLessExpression { get; set; }

        protected Regex TokenizerRegex => new Regex(this.UseObjectLessExpression ? ExpressionObjectLess : ExpressionDefault);

        protected virtual string ReplacedTokenValue(string objectName, string propertyName, string format)
        {
            var result = string.Empty;
            var propertyNotFound = false;
            if (this.PropertySource.ContainsKey(objectName.ToLower()))
            {
                result = this.PropertySource[objectName.ToLower()].GetProperty(propertyName, format, this.FormatProvider, ref propertyNotFound);
            }

            return result;
        }

        public string ReplaceTokens(string source)
        {
            if (source == null)
            {
                return string.Empty;
            }

            var result = new StringBuilder();
            foreach (Match currentMatch in this.TokenizerRegex.Matches(source))
            {
                var objectName = currentMatch.Result("${object}");
                if (!string.IsNullOrEmpty(objectName))
                {
                    if (objectName == "[")
                    {
                        objectName = ObjectLessToken;
                    }
                    var propertyName = currentMatch.Result("${property}");
                    var format = currentMatch.Result("${format}");
                    var ifEmptyReplacment = currentMatch.Result("${ifEmpty}");
                    var conversion = this.ReplacedTokenValue(objectName, propertyName, format);

                    if (!string.IsNullOrEmpty(ifEmptyReplacment) && string.IsNullOrEmpty(conversion))
                    {
                        conversion = ifEmptyReplacment;
                    }
                    result.Append(conversion);
                }
                else
                {
                    result.Append(currentMatch.Result("${text}"));
                }
            }
            return result.ToString();
        }
    }
}