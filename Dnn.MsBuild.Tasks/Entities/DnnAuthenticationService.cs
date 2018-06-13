﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnAuthenticationService.cs" company="XCESS expertise center b.v.">
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

    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://www.dnnsoftware.com/wiki/manifest-authenticationsystem-component
    /// </remarks>
    public class DnnAuthenticationService
    {
        /// <summary>
        ///     Gets or sets the login control source.
        /// </summary>
        /// <value>
        ///     The login control source.
        /// </value>
        [XmlElement("loginControlSrc")]
        public string LoginControlSource { get; set; }

        /// <summary>
        ///     Gets or sets the logoff control source.
        /// </summary>
        /// <value>
        ///     The logoff control source.
        /// </value>
        [XmlElement("logoffControlSrc")]
        public string LogoffControlSource { get; set; }

        /// <summary>
        ///     Gets or sets the settings control source.
        /// </summary>
        /// <value>
        ///     The settings control source.
        /// </value>
        [XmlElement("settingsControlSrc")]
        public string SettingsControlSource { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        [XmlElement("type")]
        public string Type { get; set; }

        #region ctor

        /// <summary>
        ///     Prevents a default instance of the <see cref="DnnAuthenticationService" /> class from being created.
        /// </summary>
        private DnnAuthenticationService()
        {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="DnnAuthenticationService" /> class.
        /// </summary>
        /// <param name="loginControlSource">The login control source.</param>
        /// <param name="logoffControlSource">The logoff control source.</param>
        /// <param name="settingsControlSource">The settings control source.</param>
        public DnnAuthenticationService(string loginControlSource, string logoffControlSource,
                                        string settingsControlSource)
        {
            this.LoginControlSource = loginControlSource;
            this.LogoffControlSource = logoffControlSource;
            this.SettingsControlSource = settingsControlSource;
        }

        #endregion
    }
}