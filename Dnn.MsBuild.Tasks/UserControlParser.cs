using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dnn.MsBuild.Tasks.Components;
using DotNetNuke.Common;
using Microsoft.Build.Framework;

namespace Dnn.MsBuild.Tasks
{
    public class UserControlParser
    {
        public const string UserControlFileExtension = ".ascx";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlParser"/> class.
        /// </summary>
        public UserControlParser()
        {
            this.UserControls = new Dictionary<string, string>();
            this.UserControlBaseClassPattern = new Regex(@"\s(?i)inherits(?-i)=""[a-zA-Z0-9\.]+""\s", RegexOptions.IgnoreCase);
        }

        public IDictionary<string, string> UserControls { get; }

        protected Regex UserControlBaseClassPattern { get; }

        public IDictionary<string, string> Parse(ITaskItem[] sourceFiles)
        {
            var desktopModuleFolder = Globals.DesktopModulePath.Replace(@"/", @"\");
            sourceFiles.ForEach(
                item =>
                {
                    if (item.ItemSpec.EndsWith(UserControlFileExtension, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Only for user controls...
                        using (var reader = new StreamReader(item.ItemSpec))
                        {
                            // TODO: optimize. Only read until found <> or EOF.
                            var content = reader.ReadToEnd();

                            var match = this.UserControlBaseClassPattern.Match(content);
                            if (match.Success)
                            {
                                // We know that the match.Value includes a '=' character, because it is part of the Regular Expression.
                                var baseClass = match.Value.Split('=')
                                                     .Last()
                                                     .Trim(new[] { ' ', '"' });

                                var startOfDesktopModuleFolder = item.ItemSpec.IndexOf(desktopModuleFolder);
                                var relativeUserControlPath = item.ItemSpec.Substring(startOfDesktopModuleFolder + 1) // Add 1 so the first backslash won't be included.
                                                                  .Replace(@"/", @"\"); // Replace the forward slashes by backslashes in line with relative URIs.
                                this.UserControls.Add(baseClass, relativeUserControlPath);
                            }
                        }
                    }
                });

            return this.UserControls;
        }
    }
}
