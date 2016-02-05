using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dnn.MsBuild.Tasks.Components;
using Dnn.MsBuild.Tasks.Entities;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Composition
{
    internal abstract class PackageBuilder : IBuilder<DnnPackage>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageBuilder"/> class.
        /// </summary>
        /// <param name="packageType">Type of the package.</param>
        protected PackageBuilder(DnnPackageType packageType)
        {
            this.ComponentBuilders = new List<IBuilder>();
            this.Package = new DnnPackage(packageType);
        }

        #endregion

        protected IList<IBuilder> ComponentBuilders { get; }

        protected DnnPackage Package { get; }

        #region Implementation of IBuilder

        /// <summary>
        /// Builds a package from the given data
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public DnnPackage Build(IManifestData data)
        {
            this.BuildElement(data);
            data.Package = this.Package;
            this.ComponentBuilders.ForEach(builder =>
                                           {
                                               var component = builder.Build(data) as DnnComponent;
                                               if (component != null)
                                               {
                                                   this.Package.Components.Add(component);
                                               }
                                           });

            return this.Package;
        }

        #endregion

        protected abstract void BuildElement(IManifestData data);

        /// <summary>
        /// Creates a concrete DnnPackage builder from the assembly. By default a ModulePackageBuilder is created, unless the assembly defines the DnnPackage attribute.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static IBuilder<DnnPackage> CreateFromAssembly(Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<DnnPackageAttribute>();
            switch (attribute?.PackageType)
            {
                // ReSharper disable once RedundantCaseLabel
                case DnnPackageType.Module:
                default:
                    return new ModulePackageBuilder();
            }
        }
    }
}
