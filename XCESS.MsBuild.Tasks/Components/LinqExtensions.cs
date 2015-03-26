// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinqExtensions.cs" company="XCESS expertise center b.v.">
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
//   Defines the LinqExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Tasks.Components
{
    using System;
    using System.Collections.Generic;

    public static class LinqExtensions
    {
        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="act">The act.</param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> act)
        {
            foreach (T element in source)
            {
                act(element);
            }

            return source;
        }
    }
}
