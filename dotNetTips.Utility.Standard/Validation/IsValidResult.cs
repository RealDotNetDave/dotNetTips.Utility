// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : dotnetdave
// Created          : 06-26-2017
//
// Last Modified By : dotnetdave
// Last Modified On : 06-26-2017
// ***********************************************************************
// <copyright file="IsValidResult.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetTips.Utility.Portable.Validation
{
    /// <summary>
    /// Class IsValidResult.
    /// </summary>
    public class IsValidResult
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid { get; private set; }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IEnumerable<string> Errors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IsValidResult"/> class.
        /// </summary>
        /// <param name="valid">if set to <c>true</c> [valid].</param>
        /// <param name="errors">The errors.</param>
        public IsValidResult(bool valid, IEnumerable<string> errors)
        {
            this.IsValid = valid;
            this.Errors = errors;
        }
    }
}
