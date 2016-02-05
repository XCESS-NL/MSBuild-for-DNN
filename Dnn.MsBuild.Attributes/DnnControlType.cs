// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnControlType.cs" company="XCESS expertise center b.v.">
//   Copyright (c) 2014 XCESS expertise center b.v. 
//   
//   The software is owned by XCESS expertise center b.v. and is protected by 
//   the Dutch copyright laws and international treaty provisions. 
//   You are allowed to make copies of the software solely for backup or archival purposes. 
//   You may not lease, rent, export or sublicense the software. 
//   You may not reverse engineer, decompile, disassemble or create derivative works from the software.
//   
//   XCESS expertise center b.v., Storkstraat 19, 3833 LB Leusden, The Netherlands
//   T. +31-33-4335151, I. http://www.xcess.nl
// </copyright>
// <summary>
//   The type of controls DNN is using.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace DotNetNuke.Services.Installer.MsBuild
{
    /// <summary>
    /// The type of controls DNN is using. 
    /// </summary>
    public enum DnnControlType
    {
        /// <summary>
        /// The control is a skin object.
        /// </summary>
        SkinObject,

        /// <summary>
        /// The control is an anonymous control.
        /// </summary>
        Anonymous,

        /// <summary>
        /// The control is a view (the default) control.
        /// </summary>
        View,

        /// <summary>
        /// The control is an edit control.
        /// </summary>
        Edit,

        /// <summary>
        /// The control is an admin control.
        /// </summary>
        Admin,

        /// <summary>
        /// The control is a host control.
        /// </summary>
        Host
    }
}
