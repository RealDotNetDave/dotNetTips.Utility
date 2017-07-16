// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : dotnetdave
// Created          : 06-26-2017
//
// Last Modified By : dotnetdave
// Last Modified On : 06-26-2017
// ***********************************************************************
// <copyright file="IValid.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;

namespace dotNetTips.Utility.Portable.Validation
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
