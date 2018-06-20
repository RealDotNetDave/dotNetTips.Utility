// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-15-2018
//
// Last Modified By : David McCarter
// Last Modified On : 06-15-2018
// ***********************************************************************
// <copyright file="RegistryHelper.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace dotNetTips.Utility.Standard.Win32
{

    /// <summary>
    /// Class RegistryHelper.
    /// </summary>
    public static class RegistryHelper
    {
        /// <summary>
        /// The key current user one drive
        /// </summary>
        public const string KeyCurrentUserOneDrive = @"Software\Microsoft\OneDrive";

        /// <summary>
        /// Gets the registry key.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>RegistryKey.</returns>
        public static RegistryKey GetCurrentUserRegistryKey(string name)
        {
            return Registry.CurrentUser.OpenSubKey(name);
        }
    }
}