// *********************************************************************** Assembly :
// dotNetTips.Utility.Portable Author : David McCarter Created : 04-15-2016
//
// Last Modified By : David McCarter Last Modified On : 06-02-2016 ***********************************************************************
// <copyright file="ExceptionExtension.cs" company="dotNetTips.com">
//     Copyright Â© 2015
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using dotNetTips.Utility.Portable.OOP;
using System;


namespace dotNetTips.Utility.Portable.Extensions
{
    /// <summary>
    /// Class ExceptionExtension.
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// Traverses Exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex">The ex.</param>
        /// <returns>T.</returns>
        public static T TraverseFor<T>(this Exception ex)
            where T : class
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(ex != null);

            if (ReferenceEquals(ex.GetType(), typeof(T)))
            {
                return ex as T;
            }

            return ex.InnerException.TraverseFor<T>() as T;
        }
    }
}