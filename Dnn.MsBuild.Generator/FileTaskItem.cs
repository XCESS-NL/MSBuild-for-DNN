// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileTaskItem.cs" company="XCESS expertise center b.v.">
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

using System.Collections;
using System.Collections.Specialized;
using Microsoft.Build.Framework;

namespace Dnn.MsBuild.Generator
{
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