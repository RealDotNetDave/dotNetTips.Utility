// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-06-2018
//
// Last Modified By : David McCarter
// Last Modified On : 06-03-2019
// ***********************************************************************
// <copyright file="LoggingHelper.cs" company="dotNetTips.com - David McCarter">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using dotNetTips.Utility.Standard.Extensions;
using dotNetTips.Utility.Standard.OOP;

namespace dotNetTips.Utility.Standard.Logging
{
    /// <summary>
    /// Class LoggingHelper.
    /// </summary>
    /// TODO Edit XML Comment Template for LoggingHelper
    public static class LoggingHelper
    {
        /// <summary>
        /// Retrieves all exceptions (including inner exceptions).
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>IEnumerable&lt;Exception&gt;.</returns>
        /// TODO Edit XML Comment Template for RetrieveAllExceptions
        public static ImmutableList<Exception> RetrieveAllExceptions(Exception ex)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(ex != null, nameof(ex));

            var collection = new List<Exception>();

            if (ex != null)
            {
                collection = new List<Exception> { ex };

                if (ex.InnerException.IsNotNull())
                {
                    collection.AddRange(RetrieveAllExceptions(ex.InnerException));
                }
            }

            return collection.ToImmutable();
        }

        /// <summary>
        /// Retrieves all exception messages.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <remarks>NEW: Added July 2019</remarks>
        public static ImmutableList<string> RetrieveAllExceptionMessages(Exception ex)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(ex != null, nameof(ex));

            var collection = RetrieveAllExceptions(ex);
            var messages = new List<string>();

            foreach (var exception in collection)
            {
                messages.Add(ex.Message);
            }

            return messages.ToImmutableList();
        }

    }
}
