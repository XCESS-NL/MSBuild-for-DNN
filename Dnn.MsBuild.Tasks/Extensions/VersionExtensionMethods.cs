// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionExtensionMethods.cs" company="XCESS expertise center bv">
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
using System.Globalization;

namespace Dnn.MsBuild.Tasks.Extensions
{
    public static class VersionExtensionMethods
    {
        public static string ToDnnVersionString(this Version source)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:D2}.{1:D2}.{2:D2}", source.Major, source.Minor, source.Revision);
        }
    }
}