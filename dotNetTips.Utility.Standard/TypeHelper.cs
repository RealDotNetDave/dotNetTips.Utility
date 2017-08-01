// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-23-2017
//
// Last Modified By : David McCarter
// Last Modified On : 01-23-2017
// ***********************************************************************
// <copyright file="TypeHelper.cs" company="dotNetTips.Utility.Standard">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Reflection;

/// <summary>
/// The dotNetTips.Utility.Standard namespace.
/// </summary>
namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Class TypeHelper.
    /// </summary>
    public static class TypeHelper
    {
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

            var result = instance is T ? (T)instance : null;

            return result;
        }

        /// <summary>
        /// Does the object equal instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool DoesObjectEqualInstance(Object value, object instance)
        {
            var result = object.ReferenceEquals(value, instance);

            return result;

        }

        /// <summary>
        /// Gets the instance hash code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Int32 GetInstanceHashCode(object instance)
        {
            var hash = -1;

            foreach (var prop in instance.GetType().GetRuntimeProperties().Where(p => p != null))
            {
                var value = prop.GetValue(instance);

                if (value != null)
                {
                    hash = hash ^ value.GetHashCode();
                }
            }

            return hash;
        }
    }
}