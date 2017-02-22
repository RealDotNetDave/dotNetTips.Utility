// *********************************************************************** Assembly :
// dotNetTips.Utility.Standard Author : David McCarter Created : 07-29-2016
//
// Last Modified By : David McCarter Last Modified On : 07-29-2016 ***********************************************************************
// <copyright file="IntegerExtensions.cs" company="McCarter Consulting">
//     Copyright (c) McCarter Consulting. All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class IntegerExtensions.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Noes the duplicates.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.Int32().</returns>
        /// <remarks>Code by: Kevin S Gallagher</remarks>
        public static IEnumerable<int> RemoveDuplicates(this int[] values)
        {
            return values.Distinct().AsEnumerable();
        }

        /// <summary>
        /// Determines if the Integer is of the specified interval. E.g. if the interval is 100 and
        /// the integer is 400, it would return true. This function uses the Mod operator, for the
        /// above example: (300 Mod 100 = 0)
        /// </summary>
        /// <param name="num">     The number.</param>
        /// <param name="interval">The interval.</param>
        /// <returns><c>true</c> if the specified number is interval; otherwise, <c>false</c>.</returns>
        /// <remarks>Code by: Blake Pell</remarks>
        public static bool IsInterval(this int num, int interval)
        {
            return num % interval == 0 ? true : false;
        }
    }
}