// *********************************************************************** Assembly :
// dotNetTips.Utility.Core Author : David McCarter Created : 05-16-2014
//
// Last Modified By : David McCarter Last Modified On : 05-16-2014 ***********************************************************************
// <copyright file="TriState.cs" company="David McCarter Consulting">
//     David McCarter Consulting. All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************

namespace dotNetTips.Utility.Core
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