// *********************************************************************** Assembly :
// dotNetTips.Utility.Portable Author : David McCarter Created : 07-08-2016
//
// Last Modified By : David McCarter Last Modified On : 07-08-2016 ***********************************************************************
// <copyright file="ICloneable.cs" company="dotNetTips.com">
//     Copyright Â© 2015
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
namespace dotNetTips.Utility.Portable
{
    /// <summary>
    /// Interface ICloneable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICloneable<T>
    {
        /// <summary>
        /// Cones this instance.
        /// </summary>
        /// <returns>T.</returns>
        T Cone();
    }
}