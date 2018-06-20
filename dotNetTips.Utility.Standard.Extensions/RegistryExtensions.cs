// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 06-16-2018
//
// Last Modified By : David McCarter
// Last Modified On : 06-16-2018
// ***********************************************************************
// <copyright file="RegistryExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class RegistryExtensions.
    /// </summary>
    public static class RegistryExtensions
    {
        /// <summary>
        /// Gets the registry key sub key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="name">The name.</param>
        /// <returns>RegistryKey.</returns>
        public static RegistryKey GetSubKey(this RegistryKey key, string name)
        {
            return key.OpenSubKey(name);
        }

        /// <summary>
        /// Gets the registry key value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="name">The name.</param>
        /// <returns>T.</returns>
        public static T GetValue<T>(this RegistryKey key, string name)
        {
            var returnValue = default(T);

            var keyValue = key.GetValue(name);

            if (keyValue != null)
            {
                returnValue = (T)keyValue;
            }

            return returnValue;
        }
    }
}
