// *********************************************************************** Assembly :
// dotNetTips.Utility.Portable Author : David McCarter Created : 06-01-2016
//
// Last Modified By : David McCarter Last Modified On : 06-02-2016 ***********************************************************************
// <copyright file="FileAttributes.cs" company="dotNetTips.com">
//     Copyright Â© 2015
// </copyright>
// <summary>
// </summary>
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