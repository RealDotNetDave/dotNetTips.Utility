// *********************************************************************** Assembly :
// dotNetTips.Utility.Portable Author : David McCarter Created : 04-15-2016
//
// Last Modified By : David McCarter Last Modified On : 06-02-2016 ***********************************************************************
// <copyright file="TypeExtensions.cs" company="dotNetTips.com">
//     Copyright Â© 2015
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using dotNetTips.Utility.Portable.OOP;
using System;


namespace dotNetTips.Utility.Portable.Extensions
{
    /// <summary>
    /// Class TypeExtensions.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Return maximum type. Works with value and reference types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>T.</returns>
        /// <remarks>Original code by: Jeremy Clark</remarks>
        public static T Max<T>(this T obj1, T obj2)
            where T : IComparable
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(obj1 != null);
            Encapsulation.TryValidateParam<ArgumentNullException>(obj2 != null);

            return obj1.CompareTo(obj2) >= 0 ? obj1 : obj2;
        }
    }
}