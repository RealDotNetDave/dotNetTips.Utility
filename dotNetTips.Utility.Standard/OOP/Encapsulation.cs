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
using System.Collections;
using dotNetTips.Utility.Standard.Extensions;
/// <summary>
/// The OOP namespace.
/// </summary>
namespace dotNetTips.Utility.Standard.OOP
{
    /// <summary>
    /// Class Encapsulation.
    /// </summary>
    public static class Encapsulation
    {
        public static void TryValidateParam(string input, string message = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "String is null or empty.";
            }

            TryValidateParam<ArgumentNullException>(string.IsNullOrWhiteSpace(input) == false, message);
        }
        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="condition">The condition.</param>
        /// <param name="message">The message.</param>
        public static void TryValidateParam<TException>(bool condition, string message = "") where TException : ArgumentException, new()
        {
            if (condition)
            {
                return;
            }

            //Validate proper Exception type
            var t = typeof(TException);

            if (t.Name == nameof(Exception))
            {
                throw new InvalidCastException("TException cannot be of type Exception. Use a more specific ArgumentException from the framework or a custom Exception inheriting type ArgumentException (only)");
            }

            var defaultMessage = "Parameter is invalid.";

            if (string.IsNullOrEmpty(message))
            {
                defaultMessage = message;
            }

            var ex = Activator.CreateInstance(typeof(TException), message).As<TException>();
            throw ex;
        }

        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void TryValidateParam(IEnumerable collection, string paramName, string message = "")
        {
            if (collection.IsValid() == false)
            {
                if (message.IsNull())
                {
                    message = "Collection is null or has no items.";
                }

                throw new ArgumentNullException(paramName, message);
            }
        }

        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public static void TryValidateParam(Enum value, string paramName, string message = "")
        {
            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                if (message.IsNull())
                {
                    message = "The value is not defined in the enum.";
                }

                throw new ArgumentOutOfRangeException(paramName, message);
            }
        }
    }

}