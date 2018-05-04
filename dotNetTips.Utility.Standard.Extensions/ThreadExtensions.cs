// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 09-15-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-18-2017
// ***********************************************************************
// <copyright file="ObjectExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class ThreadExtensions.
    /// </summary>
    public static class ThreadExtensions
    {
        /// <summary>
        /// Waits for a certian amount of time. Does not use timer (no need to call Dispose).
        /// </summary>
        /// <param name="thread">The thread.</param>
        /// <param name="interval">The interval.</param>
        public static void WaitUntil(this Thread thread, TimeSpan interval)
        {
            var stopAt = DateTime.Now.Add(interval);

            do
            {
                Thread.SpinWait(10);
            }
            while (thread.IsAlive && DateTime.Now < stopAt);

        }
    }
}