// *********************************************************************** Assembly :
// dotNetTips.Utility.Portable Author : David McCarter Created : 04-15-2016
//
// Last Modified By : David McCarter Last Modified On : 06-02-2016 ***********************************************************************
// <copyright file="MathExtensions.cs" company="dotNetTips.com">
//     Copyright Â© 2015
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;
using System.Diagnostics.Contracts;

namespace dotNetTips.Utility.Portable.Extensions
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
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static int Round(this double value)
        {
            Contract.Requires<ArgumentOutOfRangeException>(value >= double.MinValue && value <= double.MaxValue);

            return Convert.ToInt32(System.Math.Round(value));
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value"> The value.</param>
        /// <param name="digits">The digits.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static int Round(this double value, int digits)
        {
            Contract.Requires<ArgumentOutOfRangeException>(value >= double.MinValue && value <= double.MaxValue);

            return Convert.ToInt32(System.Math.Round(value, digits));
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="mode"> The mode.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static int Round(this double value, MidpointRounding mode)
        {
            Contract.Requires<ArgumentOutOfRangeException>(value >= double.MinValue && value <= double.MaxValue);

            return Convert.ToInt32(System.Math.Round(value, mode));
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value"> The value.</param>
        /// <param name="digits">The digits.</param>
        /// <param name="mode">  The mode.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static int Round(this double value, int digits, MidpointRounding mode)
        {
            Contract.Requires<ArgumentOutOfRangeException>(value >= double.MinValue && value <= double.MaxValue);

            return Convert.ToInt32(System.Math.Round(value, digits, mode));
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static int Round(this decimal value)
        {
            Contract.Requires<ArgumentOutOfRangeException>(value >= decimal.MinValue && value <= decimal.MaxValue);

            return Convert.ToInt32(System.Math.Round(value));
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value"> The value.</param>
        /// <param name="digits">The digits.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static int Round(this decimal value, int digits)
        {
            Contract.Requires<ArgumentOutOfRangeException>(value >= decimal.MinValue && value <= decimal.MaxValue);

            return Convert.ToInt32(System.Math.Round(value, digits));
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="mode"> The mode.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static int Round(this decimal value, MidpointRounding mode)
        {
            Contract.Requires<ArgumentOutOfRangeException>(value >= decimal.MinValue && value <= decimal.MaxValue);

            return Convert.ToInt32(System.Math.Round(value, mode));
        }

        /// <summary>
        /// Rounds the specified value.
        /// </summary>
        /// <param name="value"> The value.</param>
        /// <param name="digits">The digits.</param>
        /// <param name="mode">  The mode.</param>
        /// <returns>System.Int32.</returns>
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static int Round(this decimal value, int digits, MidpointRounding mode)
        {
            Contract.Requires<ArgumentOutOfRangeException>(value >= decimal.MinValue && value <= decimal.MaxValue);

            return Convert.ToInt32(System.Math.Round(value, digits, mode));
        }
    }
}