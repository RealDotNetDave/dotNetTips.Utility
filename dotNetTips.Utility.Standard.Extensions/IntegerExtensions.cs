// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 09-15-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="IntegerExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class IntegerExtensions.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Indicate whether the number falls in the specified range.
        /// </summary>
        /// <param name="theNumber">Number to process</param>
        /// <param name="lower">Lower bound</param>
        /// <param name="upper">Upper bound</param>
        /// <returns>True/False</returns>
        public static bool Between(this int theNumber, int lower, int upper) => theNumber >= lower && theNumber <= upper;
        /// <summary>
        /// Decrement a number ensuring it never passes a given lower-bound.
        /// </summary>
        /// <param name="number">Number to process</param>
        /// <param name="lowerBound">Lower bound</param>
        /// <param name="step">Step of the decrement</param>
        /// <returns>Integer</returns>
        public static int Decrement(this int number, int lowerBound = 0, int step = 1)
        {
            var n = number - step;
            return n < lowerBound ? lowerBound : n;
        }
        /// <summary>
        /// Increment a number ensuring it never passes a given upper-bound.
        /// </summary>
        /// <param name="number">Number to process</param>
        /// <param name="upperBound">Upper bound</param>
        /// <param name="step">Step of the increment</param>
        /// <returns>Integer</returns>
        public static int Increment(this int number, int upperBound = 100, int step = 1)
        {
            var n = number + step;
            return n > upperBound ? upperBound : n;
        }
        /// <summary>
        /// Indicate whether the number is even.
        /// </summary>
        /// <param name="theNumber">Number to process</param>
        /// <returns>True/False</returns>
        public static bool IsEven(this int theNumber) => (theNumber % 2) == 0;

        /// <summary>
        /// Determines if the Integer is of the specified interval. E.g. if the interval is 100 and
        /// the integer is 400, it would return true. This function uses the Mod operator, for the
        /// above example: (300 Mod 100 = 0)
        /// </summary>
        /// <param name="num">The number.</param>
        /// <param name="interval">The interval.</param>
        /// <returns><c>true</c> if the specified number is interval; otherwise, <c>false</c>.</returns>
        public static bool IsInterval(this int num, int interval) => num % interval == 0 ? true : false;
       
        /// <summary>
        /// Noes the duplicates.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.Int32().</returns>
        /// <remarks>Code by: Kevin S Gallagher</remarks>
        public static IEnumerable<int> RemoveDuplicates(this int[] values) => values.Distinct().AsEnumerable();
       
        /// <summary>
        /// Returns the nearest power of 2 that is bigger than the number.
        /// </summary>
        /// <param name="theNumber">Number to process</param>
        /// <returns>Integer</returns>
        public static int RoundToPowerOf2(this int theNumber)
        {
            var exponent = 1;
            while (true)
            {
                var powerOf2 = (uint)Math.Pow(2, exponent++);
                if (powerOf2 >= theNumber)
                {
                    return (int)powerOf2;
                }
            }
        }
        /// <summary>
        /// Parse the number to a string or a default string if outside given range.
        /// </summary>
        /// <param name="theNumber">Number to process</param>
        /// <param name="lowerLimit">Lower bound</param>
        /// <param name="upperLimit">Upper bound</param>
        /// <param name="defaultText">Default text</param>
        /// <returns>String</returns>
        public static string ToStringOrEmpty(this int theNumber, int lowerLimit = 0, int upperLimit = 9000, string defaultText = "")
        {
            return theNumber <= lowerLimit || theNumber > upperLimit ? defaultText : theNumber.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Translate the number in words (English)
        /// </summary>
        /// <param name="number">Number to translate</param>
        /// <returns>String</returns>
        public static string ToWords(this int number)
        {
            if (number == 0)
            {
                return "Zero";
            }

            if (number < 0)
            {
                return "Minus " + ToWords(Math.Abs(number));
            }

            var words = string.Empty;

            if ((number / 1000000) > 0)
            {
                words += ToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (string.IsNullOrEmpty(words) == false)
                {
                    words += "and ";
                }

                var units = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tens = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                {
                    words += units[number];
                }
                else
                {
                    words += tens[number / 10];
                    if ((number % 10) > 0)
                    {
                        words += "-" + units[number % 10];
                    }
                }
            }

            return words;
        }
    }
}