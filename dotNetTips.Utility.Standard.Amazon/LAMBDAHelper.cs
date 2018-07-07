// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 07-07-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-07-2018
// ***********************************************************************
// <copyright file="LAMBDAHelper.cs" company="McCarter Consulting - dotNetTips.com">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
namespace dotNetTips.Utility.Standard.Amazon
{
    /// <summary>
    /// Helper for Amazon LAMBDA functions.
    /// </summary>
    public static class LAMBDAHelper
    {
        /// <summary>
        /// Gets the environment variable.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static string GetEnvironmentVariable(string key)
        {
            OOP.Encapsulation.TryValidateParam(key, nameof(key));

            var returnValue = string.Empty;

            if (Environment.GetEnvironmentVariables().Contains(key))
            {
                returnValue = Environment.GetEnvironmentVariable(key);
            }

            return returnValue;
        }
    }
}
