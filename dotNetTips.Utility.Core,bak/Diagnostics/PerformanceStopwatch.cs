// *********************************************************************** Assembly :
// dotNetTips.Utility.Core Author : David McCarter Created : 05-31-2016
//
// Last Modified By : David McCarter Last Modified On : 07-29-2016 ***********************************************************************
// <copyright file="PerformanceStopwatch.cs" company="McCarter Consulting">
//     Copyright (c) McCarter Consulting. All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;

namespace dotNetTips.Utility.Core.Diagnostics
{
    /// <summary>
    /// Class PerformanceStopwatch.
    /// </summary>
    /// <seealso cref="System.Diagnostics.Stopwatch"/>
    public class PerformanceStopwatch : System.Diagnostics.Stopwatch
    {
        #region Public Methods

        /// <summary>
        /// Stops the reset.
        /// </summary>
        /// <returns>TimeSpan.</returns>
        public TimeSpan StopReset()
        {
            this.Stop();
            var result = this.Elapsed;
            this.Reset();
            return result;
        }

        #endregion Public Methods
    }
}