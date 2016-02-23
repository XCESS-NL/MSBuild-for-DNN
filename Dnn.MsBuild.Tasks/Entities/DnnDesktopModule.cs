// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnDesktopModule.cs" company="XCESS expertise center b.v.">
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
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Dnn.MsBuild.Tasks.Entities
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// http://www.dnnsoftware.com/wiki/module-component
    /// <![CDATA[
    /// <desktopModule>
    ///   <moduleName/>
    ///   <foldername/>
    ///   <businessControllerClass/>
    ///   <codeSubdirectory />
    ///   <isAdmin />
    ///   <isPremium/>
    ///   <supportedFeatures>
    ///     <supportedFeature type="" />
    ///   </supportedFeatures>
    ///   <moduleDefinitions /> 
    /// </desktopModule>
    /// ]]>
    /// </remarks>
    [Serializable]
    public class DnnDesktopModule
    {
        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnDesktopModule"/> class from being created.
        /// </summary>
        internal DnnDesktopModule()
        {
            this.ModuleDefinitions = new List<DnnModuleDefinition>();
            this.SupportedFeatures = new List<DnnSupportedFeature>();
        }

        #endregion

        /// <summary>
        /// Gets or sets the fully qualified name of the class that includes the extension methods (search, import/export, ...) as required by DNN.
        /// </summary>
        /// <value>
        /// The fully qualified name of the class.
        /// </value>
        [XmlElement("businessControllerClass")]
        public string BusinessControllerClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder where the module will be installed (relative to DesktopModules)
        /// </summary>
        /// <value>
        /// The name of the folder.
        /// </value>
        [XmlElement("folderName")]
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this module is an Admin module.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("isAdmin")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this module is a Premium module. Premium modules must be explicitely 
        /// assigned to portals in order to be available for users to install in the portal. 
        /// By default, isPremium is set to false.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is premium; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("isPremium")]
        public bool IsPremium { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        [XmlElement("moduleName")]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets whether this module is shareable.
        /// </summary>
        /// <value>
        /// The shareable.
        /// </value>
        // public ModuleSharing Shareable { get; set; }
        /// <summary>
        /// Gets or sets the module definitions.
        /// </summary>
        /// <value>
        /// The module definitions.
        /// </value>
        [XmlArray("moduleDefinitions")]
        [XmlArrayItem("moduleDefinition")]
        public List<DnnModuleDefinition> ModuleDefinitions { get; set; }

        /// <summary>
        /// Gets or sets the supported features.
        /// </summary>
        /// <value>
        /// The supported features.
        /// </value>
        [XmlArray("supportedFeatures")]
        [XmlArrayItem("supportedFeature")]
        public List<DnnSupportedFeature> SupportedFeatures { get; set; }

        public void AssignSupportedFeature<TFeatureInterface>(IEnumerable<Type> interfaces, DnnSupportedFeatureType featureType)
        {
            if (interfaces.Any(arg => arg.FullName == typeof(TFeatureInterface).FullName))
            {
                this.SupportedFeatures.Add(new DnnSupportedFeature(featureType));
            }
        }
    }
}