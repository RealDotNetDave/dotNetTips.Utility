// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="PerformanceStopwatch.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
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
    public class PerformanceStopwatch : Stopwatch
    {
        /// <summary>
        /// Starts the new.
        /// </summary>
        /// <returns>PerformanceStopwatch.</returns>
        public static new PerformanceStopwatch StartNew()
        {
            var sw = TypeHelper.Create<PerformanceStopwatch>();
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