// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 02-02-2017
//
// Last Modified By : David McCarter
// Last Modified On : 03-15-2017
// ***********************************************************************
// <copyright file="PerformanceStopwatch.cs" company="dotNetTips.com">
//     Copyright © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Diagnostics;

namespace dotNetTips.Utility.Standard.Diagnostics
{
    /// <summary>
    /// Class PerformanceStopwatch.
    /// </summary>
    /// <seealso cref="System.Diagnostics.Stopwatch" />
    [Obsolete("Use PerformanceStopwatch from dotNetTips.Utility.Standard.")]
    public class PerformanceStopwatch : Stopwatch
    {

        /// <summary>
        /// Starts the new.
        /// </summary>
        /// <returns>PerformanceStopwatch.</returns>
        public static new PerformanceStopwatch StartNew()
        {
            var sw = new PerformanceStopwatch();
            sw.Start();
            return sw;
        }
        /// <summary>
        /// Stops the reset.
        /// </summary>
        /// <returns>TimeSpan.</returns>
        public TimeSpan StopReset()
        {
            this.Stop();
            var result = this.Elapsed;
            base.Reset();

            return result;
        }
    }
}