// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DnnBusinessControllerAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DnnBusinessControllerAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XCESS.MsBuild.Attributes
{
    using System;

    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited =false)]
    [Serializable]
    public sealed class DnnBusinessControllerAttribute : Attribute
    {
    }
}
