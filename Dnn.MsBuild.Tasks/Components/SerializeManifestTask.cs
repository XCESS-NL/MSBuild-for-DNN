// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializeManifestTask.cs" company="XCESS expertise center b.v.">
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

using System.IO;
using System.Xml.Serialization;
using Dnn.MsBuild.Tasks.Composition;

namespace Dnn.MsBuild.Tasks.Components
{
    internal class SerializeManifestTask<TManifest>
        where TManifest : IManifest, new()
    {
        public void Execute(IManifest manifest)
        {
            var fileName = manifest.FileName + (manifest.Extension.StartsWith(".") ? manifest.Extension : "." + manifest.Extension);
            var serializer = new XmlSerializer(manifest.GetType());
            using (var stream = new StreamWriter(fileName))
            {
                serializer.Serialize(stream, manifest);
            }
        }
    }
}