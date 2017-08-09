// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 08-09-2017
//
// Last Modified By : David McCarter
// Last Modified On : 08-09-2017
// ***********************************************************************
// <copyright file="TypeHelper.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
using System.Reflection;

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
        /// Gets the default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        private static T GetDefault<T>()
        {
            T result = default(T);

            //TODO: NEED TO FIGURE THIS OUT
            //if (typeof(System.Collections.IList).IsAssignableFrom(typeof(T)))
            //{
            //    if (typeof(T).GetGenericArguments().Length > 0)
            //    {
            //        result = (T)Activator.CreateInstance(typeof(T));
            //    }
            //}

            return result;
        }

        /// <summary>
        /// Does the object equal instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="instance">The instance.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool DoesObjectEqualInstance(Object value, object instance)
        {
            var result = object.ReferenceEquals(value, instance);

            return result;

        }

        /// <summary>
        /// Gets the instance hash code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>Int32.</returns>
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