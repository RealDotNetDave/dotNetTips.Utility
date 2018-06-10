// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 09-15-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="MathExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using dotNetTips.Utility.Standard.Extensions.Properties;
using System;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class MathExtensions.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">value - Value is invalid.</exception>
        public static int Round(this double value) => Convert.ToInt32(Math.Round(value));

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="digits">The digits.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">value - Value is invalid.</exception>
        public static int Round(this double value, int digits) => Convert.ToInt32(Math.Round(value, digits));

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">value - Value is invalid.</exception>
        public static int Round(this double value, MidpointRounding mode) => Convert.ToInt32(Math.Round(value,
                                                                                                               mode));

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="digits">The digits.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">value - Value is invalid.</exception>
        public static int Round(this double value, int digits, MidpointRounding mode) => Convert.ToInt32(Math.Round(value,
                                                                                                                           digits,
                                                                                                                           mode));

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">value - Value is invalid.</exception>
        public static int Round(this decimal value) => Convert.ToInt32(Math.Round(value));

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="digits">The digits.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">value - Value is invalid.</exception>
        public static int Round(this decimal value, int digits) => Convert.ToInt32(Math.Round(value, digits));

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">value - Value is invalid.</exception>
        public static int Round(this decimal value, MidpointRounding mode)
        {
            if(value <= decimal.MinValue || value >= decimal.MaxValue)
            {
                throw new ArgumentNullException(nameof(value), Resources.ValueIsInvalid);
            }

            return Convert.ToInt32(Math.Round(value, mode));
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="digits">The digits.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">value - Value is invalid.</exception>
        public static int Round(this decimal value, int digits, MidpointRounding mode)
        {
            if(value <= decimal.MinValue || value >= decimal.MaxValue)
            {
                throw new ArgumentNullException(nameof(value), Resources.ValueIsInvalid);
            }

            return Convert.ToInt32(Math.Round(value, digits, mode));
        }
    }
}