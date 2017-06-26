// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 12-07-2016
//
// Last Modified By : David McCarter
// Last Modified On : 03-16-2017
// ***********************************************************************
// <copyright file="ICloneable.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace dotNetTips.Utility.Portable
{
    /// <summary>
    /// Interface ICloneable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICloneable<T> where T : new()
    {
        /// <summary>
        /// Cones this instance.
        /// </summary>
        /// <returns>T.</returns>
        T Clone();
    }
}