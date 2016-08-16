﻿// *********************************************************************** Assembly :
// dotNetTips.Utility.Core Author : David McCarter Created : 10-27-2014
//
// Last Modified By : David McCarter Last Modified On : 10-27-2014 ***********************************************************************
// <copyright file="TypeHelper.cs" company="David McCarter Consulting">
//     David McCarter Consulting. All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;

namespace dotNetTips.Utility.Core
{
    /// <summary>
    /// Class TypeHelper.
    /// </summary>
    public static class TypeHelper
    {
        #region Public Methods

        /// <summary>
        /// Creates type instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        /// <remarks>Original code by: Jeremy Clark</remarks>
        public static T Create<T>() where T : class
        {
            object instance = Activator.CreateInstance<T>();

            var result = instance is T ? (T)instance : null;

            return result;
        }

        #endregion Public Methods
    }
}