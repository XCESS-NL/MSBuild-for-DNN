// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseBuilder.cs" company="XCESS expertise center b.v.">
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
using Dnn.MsBuild.Tasks.Entities.Internal;
using Dnn.MsBuild.Tasks.Extensions;

namespace Dnn.MsBuild.Tasks.Composition
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <seealso cref="Dnn.MsBuild.Tasks.Composition.IBuilder{TElement}" />
    internal abstract class BaseBuilder<TElement> : IBuilder<TElement>
        where TElement : IManifestElement
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBuilder{TElement}" /> class.
        /// </summary>
        /// <param name="element">The element.</param>
        protected BaseBuilder(TElement element)
        {
            this.ComponentBuilders = new List<IBuilder>();
            this.Element = element;
        } 

        #endregion

        protected IList<IBuilder> ComponentBuilders { get; }

        protected TElement Element { get; private set; }

        #region Implementation of IBuilder<out TElement>

        public TElement Build(ITaskData data)
        {
            this.BuildElement(data);

            this.ComponentBuilders.ForEach(builder => this.OnComponentCreated(builder.Build(data)));

            return this.Element;
        }

        #endregion

        protected abstract void BuildElement(ITaskData data);

        protected virtual void OnComponentCreated(IManifestElement component)
        {}
    }
}