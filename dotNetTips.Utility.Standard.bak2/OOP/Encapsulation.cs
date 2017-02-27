// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 02-02-2017
//
// Last Modified By : David McCarter
// Last Modified On : 02-02-2017
// ***********************************************************************
// <copyright file="Encapsulation.cs" company="dotNetTips.Utility.Standard">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

/// <summary>
/// The OOP namespace.
/// </summary>
namespace dotNetTips.Utility.Standard.OOP
{
    /// <summary>
    /// Class Encapsulation.
    /// </summary>
    public static  class Encapsulation
    {
        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="condition">The condition.</param>
        /// <param name="message">The message.</param>
        public static void TryValidateParam<TException>(bool condition, string message) where TException : Exception
        {
            var defaultMessage = "One or more parameters are invalid.";

            if (string.IsNullOrEmpty(message))
            {
                defaultMessage = message;
            }

            if (condition == false)
            {
                //TODO: NEED TO FIGURE THIS OUT
                //var test = new TException();
                //var ex = Activator.CreateInstance<TException>(){ message = defaultMessage};
                //throw ex;
            }
        }
    }
}
