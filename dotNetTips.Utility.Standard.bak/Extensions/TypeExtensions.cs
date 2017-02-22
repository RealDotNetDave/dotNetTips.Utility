// *********************************************************************** Assembly :
// dotNetTips.Utility.Standard Author : David McCarter Created : 05-31-2016
//
// Last Modified By : David McCarter Last Modified On : 05-31-2016 ***********************************************************************
// <copyright file="TypeExtensions.cs" company="McCarter Consulting">
//     Copyright (c) McCarter Consulting. All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************

using System;
using System.Diagnostics.Contracts;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class TypeExtensions.
    /// </summary>
    public static class TypeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Return maximum type. Works with value and reference types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>T.</returns>
        /// <remarks>Original code by: Jeremy Clark</remarks>
        public static T Max<T>(this T obj1, T obj2) where T : IComparable
        {
            Contract.Requires<ArgumentNullException>(obj1 != null);
            Contract.Requires<ArgumentNullException>(obj2 != null);

            return obj1.CompareTo(obj2) >= 0 ? obj1 : obj2;
        }

        #endregion Public Methods
    }
}