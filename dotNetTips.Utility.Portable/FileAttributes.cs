// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 12-07-2016
//
// Last Modified By : David McCarter
// Last Modified On : 03-13-2017
// ***********************************************************************
// <copyright file="FileAttributes.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace dotNetTips.Utility.Portable
{
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