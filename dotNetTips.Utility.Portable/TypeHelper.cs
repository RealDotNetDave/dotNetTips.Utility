// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 12-07-2016
//
// Last Modified By : David McCarter
// Last Modified On : 05-04-2017
// ***********************************************************************
// <copyright file="TypeHelper.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq.Expressions;

namespace dotNetTips.Utility.Portable
{
    /// <summary>
    /// Class TypeHelper.
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Creates type instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        public static T Create<T>()
            where T : class
        {
            var t = typeof(T);
            var result = Expression.Lambda<Func<T>>(Expression.Block(t, new Expression[] { Expression.New(t) })).Compile();

            return result();
        }
    }
}