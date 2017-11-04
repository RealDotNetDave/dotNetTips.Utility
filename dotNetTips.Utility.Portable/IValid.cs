// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 06-26-2017
// ***********************************************************************
// <copyright file="IValid.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace dotNetTips.Utility.Portable
{
    /// <summary>
    /// Interface IValid
    /// </summary>
    public interface IValid
    {
        /// <summary>
        /// Returns true if type is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        bool IsValid { get; }
    }
}