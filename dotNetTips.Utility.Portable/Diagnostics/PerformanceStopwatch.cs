// *********************************************************************** Assembly :
// dotNetTips.Utility.Portable Author : David McCarter Created : 04-15-2016 Created : 04-15-2016
//
// Last Modified By : David McCarter Last Modified On : 06-02-2016
// *********************************************************************** Last Modified On :
// 07-22-2016 ***********************************************************************
// <copyright file="PerformanceStopwatch.cs" company="dotNetTips.com">
//     Copyright Â© 2015
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;
using System.Diagnostics;

namespace dotNetTips.Utility.Portable.Diagnostics
{
    /// <summary>
    /// Class PerformanceStopwatch.
    /// </summary>
    /// <seealso cref="System.Diagnostics.Stopwatch"/>
    public class PerformanceStopwatch : Stopwatch
    {
        /// <summary>
        /// Stops the reset.
        /// </summary>
        /// <returns>TimeSpan.</returns>
        public TimeSpan StopReset()
        {
            this.Stop();
            var result = Elapsed;
            base.Reset();

            return result;
        }

        /// <summary>
        /// Starts the new.
        /// </summary>
        /// <returns>PerformanceStopwatch.</returns>
        public new static PerformanceStopwatch StartNew()
        {
            var sw = new PerformanceStopwatch();
            sw.Start();
            return sw;
        }
    }
}