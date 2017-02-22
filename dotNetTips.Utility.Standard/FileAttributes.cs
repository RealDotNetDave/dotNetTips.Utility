// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-22-2017
//
// Last Modified By : David McCarter
// Last Modified On : 01-22-2017
// ***********************************************************************
// <copyright file="FileAttributes.cs" company="dotNetTips.Utility.Standard">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The Standard namespace.
/// </summary>
namespace dotNetTips.Utility.Standard
{
    using System;

    /// <summary>
    /// Enum FileAttributes
    /// </summary>
    [Flags]
    public enum FileAttributes
    {
        /// <summary>
        /// Archive
        /// </summary>
        Archive = 0x20,

        /// <summary>
        /// Directory
        /// </summary>
        Directory = 0x10,

        /// <summary>
        /// Hidden
        /// </summary>
        Hidden = 2,

        /// <summary>
        /// Normal
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Read only
        /// </summary>
        ReadOnly = 1,

        /// <summary>
        /// System
        /// </summary>
        System = 4,

        /// <summary>
        /// Volume
        /// </summary>
        Volume = 8
    }
}