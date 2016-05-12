// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureController.cs" company="XCESS expertise center bv">
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

using System;
using System.Collections.Generic;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Installer.MsBuild;
using DotNetNuke.Services.Search;
using DotNetNuke.Services.Search.Entities;

namespace XCESS.DNN.Module.Components
{
    /// <summary>
    /// </summary>
    [DnnBusinessController]
    [DnnPackageDependency(DnnPackageDependencyType.CoreVersion, "8.0.0")]
    public class FeatureController : ModuleSearchBase, ISearchable, IUpgradeable
    {
        #region Implementation of ISearchable

        public SearchItemInfoCollection GetSearchItems(ModuleInfo ModInfo)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Implementation of IUpgradeable

        public string UpgradeModule(string Version)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Overrides of ModuleSearchBase

        public override IList<SearchDocument> GetModifiedSearchDocuments(ModuleInfo moduleInfo, DateTime beginDateUtc)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}