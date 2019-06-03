// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-06-2018
//
// Last Modified By : David McCarter
// Last Modified On : 03-03-2019
// ***********************************************************************
// <copyright file="LoggingHelper.cs" company="dotNetTips.com - David McCarter">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNetTips.Utility.Standard.Logging
{
    /// <summary>
    /// Class LoggingHelper.
    /// </summary>
    public static class LoggingHelper
    {
        /// <summary>
        /// Retrieves all exceptions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>IEnumerable&lt;Exception&gt;.</returns>
        public static IEnumerable<Exception> RetrieveAllExceptions(Exception ex)
        {
            List<Exception> collection = null;

            if (ex != null)
            {
                collection = new List<Exception> { ex };

                if (ex.InnerException != null)
                {
                    collection.AddRange(RetrieveAllExceptions(ex.InnerException));
                }
            }

            return collection.AsEnumerable();
        }

    }
}
