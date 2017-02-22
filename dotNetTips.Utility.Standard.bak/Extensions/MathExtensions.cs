// *********************************************************************** Assembly :
// dotNetTips.Utility.Standard Author : David McCarter Created : 05-31-2016
//
// Last Modified By : David McCarter Last Modified On : 05-31-2016 ***********************************************************************
// <copyright file="MathExtensions.cs" company="McCarter Consulting">
//     Copyright (c) McCarter Consulting. All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************

using System;
using System.Diagnostics.Contracts;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class MathExtensions.
    /// </summary>
    public static class MathExtensions
    {
        #region Public Methods

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

        #endregion Public Methods
    }
}