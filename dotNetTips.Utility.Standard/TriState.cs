// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-23-2017
//
// Last Modified By : David McCarter
// Last Modified On : 01-23-2017
// ***********************************************************************
// <copyright file="TriState.cs" company="dotNetTips.Utility.Standard">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The dotNetTips.Utility.Standard namespace.
/// </summary>
namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Enum TriState
    /// </summary>
    public enum Tristate
    {
        /// <summary>
        /// False
        /// </summary>
        False = 0,

        /// <summary>
        /// True
        /// </summary>
        True = -1,

        /// <summary>
        /// Use default
        /// </summary>
        UseDefault = -2
    }
}