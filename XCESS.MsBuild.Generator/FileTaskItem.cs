using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCESS.MsBuild.Generator
{
    using System.Collections;
    using System.Collections.Specialized;
    using Microsoft.Build.Framework;

    public class FileTaskItem : ITaskItem
    {
        public FileTaskItem(string itemSpec)
        {
            this.ItemSpec = itemSpec;
        }

        #region ITaskItem Members

        public IDictionary CloneCustomMetadata()
        {
            // Ignore...
            return new ListDictionary();
        }

        public void CopyMetadataTo(ITaskItem destinationItem)
        {
            // Ignore...
        }

        public string GetMetadata(string metadataName)
        {
            // Ignore...
            return metadataName;
        }

        public string ItemSpec { get; set; }

        public int MetadataCount
        {
            get
            {
                // Ignore...
                return 0;
            }
        }

        public ICollection MetadataNames
        {
            // Ignore...
            get { return new ListDictionary().Values; }
        }

        public void RemoveMetadata(string metadataName)
        {
            // Ignore...
        }

        public void SetMetadata(string metadataName, string metadataValue)
        {
            // Ignore...
        }

        #endregion
    }
}
