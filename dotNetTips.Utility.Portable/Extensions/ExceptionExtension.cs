// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 02-28-2017
//
// Last Modified By : David McCarter
// Last Modified On : 05-17-2017
// ***********************************************************************
// <copyright file="ExceptionExtension.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace dotNetTips.Utility.Portable.Extensions
{
    /// <summary>
    /// Class ExceptionExtension.
    /// </summary>
    public static class ExceptionExtension
    {

        /// <summary>
        /// Froms the hierarchy.
        /// </summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="nextItem">The next item.</param>
        /// <returns>IEnumerable&lt;TSource&gt;.</returns>
        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem) where TSource : class => FromHierarchy(source, nextItem, s => s != null);

        /// <summary>
        /// Froms the hierarchy.
        /// </summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="nextItem">The next item.</param>
        /// <param name="canContinue">The can continue.</param>
        /// <returns>IEnumerable&lt;TSource&gt;.</returns>
        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem, Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        /// <summary>
        /// Gets all messages.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>System.String.</returns>
        public static string GetAllMessages(this Exception exception) => GetAllMessages(exception, Environment.NewLine);

        /// <summary>
        /// Gets all messages.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>System.String.</returns>
        public static string GetAllMessages(this Exception exception, string separator = " ")
        {
            var messages = exception.FromHierarchy(ex => ex.InnerException).Select(ex => ex.Message);


            return string.Join(separator, messages);
        }


        /// <summary>
        /// Traverses Exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex">The ex.</param>
        /// <returns>T.</returns>
        public static T TraverseFor<T>(this Exception ex)
            where T : class
        {
            if (ReferenceEquals(ex.GetType(), typeof(T)))
            {
                return ex as T;
            }

            return ex.InnerException.TraverseFor<T>();
        }
    }
}