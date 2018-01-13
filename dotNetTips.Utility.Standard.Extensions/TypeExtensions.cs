// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 09-15-2017
//
// Last Modified By : David McCarter
// Last Modified On : 12-20-2017
// ***********************************************************************
// <copyright file="TypeExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

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
        /// <exception cref="ArgumentNullException">obj1 - Object 1 cannot be null.
        /// or
        /// obj2 - Object 1 cannot be null.</exception>
        /// <remarks>Original code by: Jeremy Clark</remarks>
        public static T Max<T>(this T obj1, T obj2) where T : IComparable
        {
            if (obj2 == null)
            {
                throw new ArgumentNullException(nameof(obj2), $"{nameof(obj1)} cannot be null.");
            }

            return obj1.CompareTo(obj2) >= 0 ? obj1 : obj2;
        }

        /// <summary>
        /// Creates type instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        /// <remarks>Original code by: Jeremy Clark</remarks>
        public static T Create<T>()
            where T : class
        {
            var instance = Activator.CreateInstance<T>();

            var result = instance is T ? instance : null;

            return result;
        }

        #endregion Public Methods
    }
}