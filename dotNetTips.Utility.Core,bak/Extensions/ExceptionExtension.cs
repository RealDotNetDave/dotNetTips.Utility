// *********************************************************************** Assembly :
// dotNetTips.Utility.Core Author : David McCarter Created : 04-15-2016 Created : 04-15-2016
//
// Last Modified By : David McCarter Last Modified On : 04-18-2016
// *********************************************************************** Last Modified On :
// 05-31-2016 ***********************************************************************
// <copyright file="ExceptionExtension.cs" company="dotNetTips.com">
//     Copyright © 2015
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;
using System.Diagnostics.Contracts;

namespace dotNetTips.Utility.Core.Extensions
{
    /// <summary>
    /// Class ExceptionExtension.
    /// </summary>
    public static class ExceptionExtension
    {
        #region Public Methods

        /// <summary>
        /// Traverses Exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex">The ex.</param>
        /// <returns>T.</returns>
        public static T TraverseFor<T>(this Exception ex) where T : class
        {
            Contract.Requires<ArgumentNullException>(ex != null);

            if (ReferenceEquals(ex.GetType(), typeof(T)))
            {
                return ex as T;
            }

            return ex.InnerException.TraverseFor<T>() as T;
        }

        #endregion Public Methods
    }
}