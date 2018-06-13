// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnPackageMetaAttribute.cs" company="XCESS expertise center b.v.">
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
    public class DnnPackageMetaAttribute : DnnManifestAttribute
    {
        #region ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="DnnPackageMetaAttribute" /> class.
        /// </summary>
        /// <param name="licensePath">The license path.</param>
        /// <param name="releaseNotesPath">The release notes path.</param>
        /// <param name="versionSpecificReleaseNotes">if set to <c>true</c> [version specific release notes].</param>
        public DnnPackageMetaAttribute(string licensePath, string releaseNotesPath, bool versionSpecificReleaseNotes)
        {
            this.LicensePath = licensePath;
            this.ReleaseNotesPath = releaseNotesPath;
            this.VersionSpecificReleaseNotes = versionSpecificReleaseNotes;
        }

        #endregion

        public string LicensePath { get; set; }

        public string ReleaseNotesPath { get; set; }

        public bool VersionSpecificReleaseNotes { get; set; }
    }
}