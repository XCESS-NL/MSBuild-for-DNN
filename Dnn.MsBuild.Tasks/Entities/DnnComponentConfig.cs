// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnComponentConfig.cs" company="XCESS expertise center b.v.">
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
    using DotNetNuke.Services.Installer.MsBuild;

    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://www.dnnsoftware.com/wiki/manifest-config-component
    ///     <![CDATA[
    /// <component type="Config">
    ///   <config>
    ///     <configFile>web.config</configFile>
    ///     <install>
    ///       <configuration>
    ///         <nodes>
    ///           <node path="/configuration/system.web/httpHandlers" action="update" key="path" collision="overwrite">
    ///             <add verb="*" path="Telerik.Web.UI.WebResource.axd" validate="false" type="Telerik.Web.UI.WebResource, Telerik.Web.UI" />
    ///           </node>
    ///         </nodes>
    ///       </configuration>
    ///     </install>
    ///     <uninstall>
    ///       <configuration>
    ///         <nodes />
    ///       </configuration>
    ///     </uninstall>
    ///   </config>
    /// </component>
    /// ]]>
    /// </remarks>
    /// <seealso cref="Dnn.MsBuild.Tasks.Entities.DnnComponent" />
    public class DnnComponentConfig : DnnComponent
    {
        #region ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="DnnComponent" /> class.
        /// </summary>
        internal DnnComponentConfig()
            : base(DnnComponentType.Config)
        {}

        #endregion
    }
}