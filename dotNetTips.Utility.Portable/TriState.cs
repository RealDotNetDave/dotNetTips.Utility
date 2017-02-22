// *********************************************************************** Assembly :
// dotNetTips.Utility.Portable Author : David McCarter Created : 06-01-2016 Created : 06-01-2016
//
// Last Modified By : David McCarter Last Modified On : 06-02-2016
// *********************************************************************** Last Modified On :
// 06-02-2016 ***********************************************************************
// <copyright file="Tristate.cs" company="dotNetTips.com">
//     Copyright Â© 2015
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
namespace dotNetTips.Utility.Portable {
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

        ,
    }
}