// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationSystemComponentBuilder.cs" company="XCESS expertise center b.v.">
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
using System.Linq;
using System.Reflection;
using Dnn.MsBuild.Tasks.Entities;
using Dnn.MsBuild.Tasks.Extensions;
using DotNetNuke.Services.Installer.MsBuild;

namespace Dnn.MsBuild.Tasks.Composition.Component
{
    internal class AuthenticationSystemComponentBuilder : ComponentBuilder<DnnComponentAuthenticationSystem>
    {
        #region Overrides of ComponentBuilder<DnnComponentAuthenticationSystem>

        protected override DnnComponentAuthenticationSystem BuildElement()
        {
            var component = new DnnComponentAuthenticationSystem();

            var authenticationControls = this.GetAuthenticationControls();
            if (authenticationControls.Any())
            {
                component.AuthenticationService = new DnnAuthenticationService(
                    authenticationControls[DnnAuthenticationControlType.Login],
                    authenticationControls[DnnAuthenticationControlType.Logoff],
                    authenticationControls[DnnAuthenticationControlType.Settings]);
            }

            return component;
        }

        #endregion

        private IDictionary<DnnAuthenticationControlType, string> GetAuthenticationControls()
        {
            var authenticationControls = new Dictionary<DnnAuthenticationControlType, string>()
                                         {
                                             {DnnAuthenticationControlType.Login, null},
                                             {DnnAuthenticationControlType.Logoff, null},
                                             {DnnAuthenticationControlType.Settings, null}
                                         };

            var userControls = this.Input.ProjectFileData?.UserControls;
            var authenticationControlTypes = this.Input.ExportedTypes.Where(arg => arg.HasAttribute<DnnAuthenticationControlAttribute>());
            authenticationControlTypes.ForEach(arg =>
                                               {
                                                   var attribute = arg.GetCustomAttribute<DnnAuthenticationControlAttribute>();

                                                   // ReSharper disable once InvertIf
                                                   if (authenticationControls.ContainsKey(attribute.ControlType))
                                                   {
                                                       var userControlFilePath = string.Empty;
                                                       userControls.TryGetValue(arg.FullName, out userControlFilePath);

                                                       authenticationControls[attribute.ControlType] = userControlFilePath;
                                                   }
                                               });

            return authenticationControls;
        }
    }
}