// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 05-04-2017
//
// Last Modified By : David McCarter
// Last Modified On : 05-04-2017
// ***********************************************************************
// <copyright file="HttpExtensions.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Net.Http;

namespace dotNetTips.Utility.Portable.Extensions
{
    /// <summary>
    /// Class HttpExtensions.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Determines whether [is XML or json] [the specified content].
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns><c>true</c> if [is XML or json] [the specified content]; otherwise, <c>false</c>.</returns>
        private static bool IsXmlOrJson(this HttpContent content)
        {
            string type = content.Headers.ContentType?.MediaType;
            return type != null && (type.Contains("/xml") || type.Contains("/json"));
        }
    }
}
