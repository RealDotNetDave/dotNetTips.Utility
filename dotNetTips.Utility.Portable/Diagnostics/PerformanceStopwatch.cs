
using System;
using System.Diagnostics;

namespace dotNetTips.Utility.Standard.Diagnostics {
    /// <summary>
    /// Class PerformanceStopwatch.
    /// </summary>
    /// <seealso cref="System.Diagnostics.Stopwatch"/>
    [Obsolete("Use PerformanceStopwatch from dotNetTips.Utility.Standard.")]
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