// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentBuilder.cs" company="XCESS expertise center bv">
//   Copyright (c) 2016 XCESS expertise center bv
//   
//   The software is owned by XCESS and is protected by 
//   the Dutch copyright laws and international treaty provisions.
//   You are allowed to make copies of the software solely for backup or archival purposes.
//   You may not lease, rent, export or sublicense the software.
//   You may not reverse engineer, decompile, disassemble or create derivative works from the software.
//   
//   Owned by XCESS expertise center b.v., Storkstraat 19, 3833 LB Leusden, The Netherlands
//   T. +31-33-4335151, E. info@xcess.nl, I. http://www.xcess.nl
// </copyright>
// <summary>
//   
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace Dnn.MsBuild.Tasks.Composition.Components
{
    internal abstract class ComponentBuilder<TOutput> : IBuilder, IBuilder<TOutput>
        where TOutput : IManifestElement
    {
        protected TOutput Output { get; set; }

        protected IManifestData Input { get; set; }

        #region Implementation of IBuilder

        IManifestElement IBuilder.Build(IManifestData data)
        {
            return this.Build(data);
        }

        #endregion

        #region Implementation of IBuilder<TOutput>

        public TOutput Build(IManifestData inputData)
        {
            this.Input = inputData;
            this.Output = this.BuildElement();
            return this.Output;
        }

        #endregion

        protected abstract TOutput BuildElement();
    }
}