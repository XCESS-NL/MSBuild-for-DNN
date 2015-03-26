namespace XCESS.MsBuild.Tasks.Entities
{
    using System;
    using System.Xml.Serialization;
    using XCESS.MsBuild.Attributes;

    /// <summary>
    /// </summary>
    /// <remarks>
    /// <![CDATA[
    /// <component type="Module">
    ///   <desktopModule>
    ///     <moduleName/>
    ///     <foldername/>
    ///     <businessControllerClass/>
    ///     <codeSubdirectory />
    ///     <isAdmin />
    ///     <isPremium/>
    ///     <supportedFeatures>
    ///       <supportedFeature type="" />
    ///     </supportedFeatures>
    ///     <moduleDefinitions /> 
    ///   </desktopModule>
    ///   <eventMessage /> 
    ///   </component>
    /// ]]>
    /// </remarks>
    [Serializable]
    public class DnnComponentModule : DnnComponent
    {
        #region [ Constructors ]

        /// <summary>
        /// Prevents a default instance of the <see cref="DnnComponentModule"/> class from being created.
        /// </summary>
        private DnnComponentModule()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DnnComponentModule"/> class.
        /// </summary>
        /// <param name="desktopModule">The desktop module.</param>
        public DnnComponentModule(DnnDesktopModule desktopModule)
        {
            this.DesktopModule = desktopModule;
        }
        
        #endregion

        [XmlAttribute("type")]
        public override DnnComponentType ComponentType
        {
            get
            {
                return DnnComponentType.Module;
            }
            set
            {
            }
        }

        [XmlElement("desktopModule")]
        public DnnDesktopModule DesktopModule { get; set; }
    }
}
