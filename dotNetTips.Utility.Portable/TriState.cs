// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 12-07-2016
//
// Last Modified By : David McCarter
// Last Modified On : 03-13-2017
// ***********************************************************************
// <copyright file="Tristate.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace dotNetTips.Utility.Portable
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