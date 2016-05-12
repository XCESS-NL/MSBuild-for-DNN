// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildManifestTask.cs" company="XCESS expertise center b.v.">
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
using System.Threading;
using Dnn.MsBuild.Tasks.Composition;
using Dnn.MsBuild.Tasks.Composition.Manifest;
using Dnn.MsBuild.Tasks.Entities.Internal;

namespace Dnn.MsBuild.Tasks.Components
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TManifest">The type of the manifest.</typeparam>
    internal class BuildManifestTask<TManifest> : IDisposable
        where TManifest : IManifest, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildManifestTask{TManifest}" /> class.
        /// </summary>
        /// <param name="taskData">The task data.</param>
        public BuildManifestTask(ITaskData taskData)
        {
            this.TaskData = taskData;
        }

        #endregion

        /// <summary>
        /// A value which indicates the disposable state. 0 indicates undisposed, 1 indicates disposing
        /// or disposed.
        /// </summary>
        private int _disposableState = 0;

        private ITaskData TaskData { get; }

        /// <summary>
        /// Gets a value indicating whether the object is undisposed.
        /// </summary>
        public bool IsUndisposed => Thread.VolatileRead(ref this._disposableState) == 0;

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Attempt to move the disposable state from 0 to 1. If successful, we can be assured that
            // this thread is the first thread to do so, and can safely dispose of the object.
            if (Interlocked.CompareExchange(ref this._disposableState, 1, 0) == 0)
            {
                // Call the Dispose method with the disposing flag set to true, indicating
                // that derived classes may release unmanaged resources and dispose of managed resources.
                this.Dispose(true);

                // Suppress finalization of this object (remove it from the finalization queue and
                // prevent the destructor from being called).
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        /// <summary>
        /// Finalizes an instance of the DisposableBase class.
        /// </summary>
        ~BuildManifestTask()
        {
            // The destructor has been called as a result of finalization, indicating that the object
            // was not disposed of using the Dispose() method. In this case, call the Dispose
            // method with the dispoing flag set to false, indicating that derived classes
            // may only release unmanaged resources.
            this.Dispose(false);
        }

        public IManifest Build()
        {
            var manifest = this.BuildManifest();
            return manifest;
        }

        private IManifest BuildManifest()
        {
            var builder = new ManifestBuilder<TManifest>();
            var manifest = builder.Build(this.TaskData);
            return manifest;
        }

        #region Dispose Pattern

        // For information about the Dispose Pattern see: https://msdn.microsoft.com/en-us/library/b1yfkh5e(v=vs.110).aspx

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {}

        #endregion
    }
}