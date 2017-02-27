// *********************************************************************** Assembly :
// dotNetTips.Utility.Standard Author : David McCarter Created : 05-16-2014
//
// Last Modified By : David McCarter Last Modified On : 05-16-2014 ***********************************************************************
// <copyright file="FileAttribute.cs" company="David McCarter Consulting">
//     David McCarter Consulting. All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
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