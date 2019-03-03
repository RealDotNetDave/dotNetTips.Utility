﻿// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 06-16-2018
//
// Last Modified By : David McCarter
// Last Modified On : 03-03-2019
// ***********************************************************************
// <copyright file="RegistryExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Runtime.InteropServices;
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
        /// <exception cref="PlatformNotSupportedException"></exception>
        /// <exception cref="System.PlatformNotSupportedException"></exception>
        public static RegistryKey GetSubKey(this RegistryKey key, string name)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return key.OpenSubKey(name);
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Gets the registry key value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="name">The name.</param>
        /// <returns>T.</returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        /// <exception cref="System.PlatformNotSupportedException"></exception>
        public static T GetValue<T>(this RegistryKey key, string name)
        {
            var returnValue = default(T);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var keyValue = key.GetValue(name);

                if (keyValue != null)
                {
                    returnValue = (T)keyValue;
                }

                return returnValue;
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }
    }
}
