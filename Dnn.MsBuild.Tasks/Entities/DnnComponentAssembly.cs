// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnComponentAssembly.cs" company="XCESS expertise center b.v.">
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
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Dnn.MsBuild.Tasks.Entities.FileTypes;
    using DotNetNuke.Services.Installer.MsBuild;

    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://www.dnnsoftware.com/wiki/manifest-assembly-component
    ///     http://www.dnnsoftware.com/community-blog/cid/134857/the-new-extension-installer-manifest-ndash-part-4-the-assembly-component
    ///     <![CDATA[
    /// <component type="Assembly">
    ///     <assemblies>
    ///         <assembly [Action="UnRegister"]>
    ///             <path></path>
    ///             <name></name>
    ///             <version></version>
    ///         </assembly>
    ///     </assemblies>
    /// </component>
    /// ]]>
    /// </remarks>
    /// <seealso cref="Dnn.MsBuild.Tasks.Entities.DnnComponent" />
    public class DnnComponentAssembly : DnnComponent
    {
        #region ctor

        /// <summary>
        ///     Prevents a default instance of the <see cref="DnnComponentAssembly" /> class from being created.
        /// </summary>
        internal DnnComponentAssembly()
            : base(DnnComponentType.Assembly)
        {
            this.Assemblies = new List<AssemblyFileInfo>();
        }

        #endregion

        [XmlArray("assemblies")]
        [XmlArrayItem("assembly")]
        public List<AssemblyFileInfo> Assemblies { get; set; }
    }
}