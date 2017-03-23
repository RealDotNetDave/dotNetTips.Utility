// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 02-28-2017
//
// Last Modified By : David McCarter
// Last Modified On : 03-20-2017
// ***********************************************************************
// <copyright file="DateTimeExtensions.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using dotNetTips.Utility.Portable.OOP;
using System;

using System.Globalization;

namespace dotNetTips.Utility.Portable.Extensions
{
    /// <summary>
    /// Extensions for DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the last.
        /// </summary>
        /// <param name="input">The date/ time.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GetLast(this DateTime input, DayOfWeek dayOfWeek)
        {
            Encapsulation.TryValidateParam<ArgumentOutOfRangeException>(Enum.IsDefined(typeof(DayOfWeek), input));

            var daysToSubtract = input.DayOfWeek > dayOfWeek ? input.DayOfWeek - dayOfWeek : (7 - (int)dayOfWeek) + (int)input.DayOfWeek;
            return input.AddDays(daysToSubtract * -1);
        }

        /// <summary>
        /// Gets the next.
        /// </summary>
        /// <param name="input">The date/ time.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GetNext(this DateTime input, DayOfWeek dayOfWeek)
        {
            Encapsulation.TryValidateParam<ArgumentOutOfRangeException>(Enum.IsDefined(typeof(DayOfWeek), input));

            var daysToAdd = 0;
            daysToAdd = input.DayOfWeek < dayOfWeek ? dayOfWeek - input.DayOfWeek : (7 - (int)input.DayOfWeek) + (int)dayOfWeek;
            return input.AddDays(daysToAdd);
        }

        /// <summary>
        /// Intersects the specified start date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="intersectingStartDate">The intersecting start date.</param>
        /// <param name="intersectingEndDate">The intersecting end date.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <remarks>Code by: Walter Quesada</remarks>
        public static bool Intersects(this DateTime startDate, DateTime endDate, DateTime intersectingStartDate, DateTime intersectingEndDate)
        {
            return intersectingEndDate >= startDate && intersectingStartDate <= endDate;
        }

        /// <summary>
        /// To the friendly date string.
        /// </summary>
        /// <param name="input">The date.</param>
        /// <returns>System.String.</returns>
        public static string ToFriendlyDateString(this DateTime input)
        {
            var formattedDate = string.Empty;

            if (input.Date == DateTime.Today)
            {
                formattedDate = nameof(DateTime.Today);
            }
            else
            {
                formattedDate = input.Date == DateTime.Today.AddDays(-1) ? nameof(Yesterday) : input.Date > DateTime.Today.AddDays(-6) ? input.ToString("dddd").ToString() : input.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern, CultureInfo.CurrentCulture);
            }

            formattedDate += " @ " + input.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern, CultureInfo.CurrentCulture).ToLower();

            return formattedDate;
        }

        /// <summary>
        /// Yesterdays the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>DateTime.</returns>
        public static DateTime Yesterday(this DateTime input)
        {
            return input.Subtract(new TimeSpan(1, 0, 0, 0));
        }

        /// <summary>
        /// Tomorrows the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>DateTime.</returns>
        public static DateTime Tomorrow(this DateTime input)
        {
            return input.AddDays(1);
        }
    }
}