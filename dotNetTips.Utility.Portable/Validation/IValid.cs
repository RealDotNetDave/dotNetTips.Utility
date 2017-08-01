// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 07-21-2017
// ***********************************************************************
// <copyright file="IValid.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace dotNetTips.Utility.Standard.Validation
{
    /// <summary>
    /// Class IValid.
    /// </summary>
    interface IValid
    {
        /// <summary>
        /// Returns true if data for the object is valid.
        /// </summary>
        /// <value>The is valid.</value>
        IsValidResult IsValid { get; }
    }
}
