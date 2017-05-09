// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 02-02-2017
//
// Last Modified By : David McCarter
// Last Modified On : 05-09-2017
// ***********************************************************************
// <copyright file="Encapsulation.cs" company="dotNetTips.Utility.Portable">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using dotNetTips.Utility.Portable.Extensions;
using dotNetTips.Utility.Portable.Properties;
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace dotNetTips.Utility.Portable.OOP
{
    /// <summary>
    /// Class Encapsulation.
    /// </summary>
    public static class Encapsulation
    {
        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="condition">The condition.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.InvalidCastException"></exception>
        public static void TryValidateParam<TException>(bool condition, string message = "") where TException : ArgumentException, new()
        {
            // Validate proper Exception type
            var t = typeof(TException);

            if (t.Name == nameof(Exception))
            {
                throw new InvalidCastException(String.Format(Resources.CannotBeOfTypeException, nameof(TException)));
            }

            var defaultMessage = Resources.ParameterIsInvalid;

            if (String.IsNullOrEmpty(message) == false)
            {
                defaultMessage = message;
            }

            if (condition == false)
            {
                var ex = Activator.CreateInstance(typeof(TException), message).As<TException>();
                throw ex;
            }
        }

        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void TryValidateParam(IEnumerable collection, string paramName, string message = "")
        {
            if (collection.IsValid() == false)
            {
                if (message.IsNull())
                {
                    message = Resources.CollectionIsNullOrHasNoItems;
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
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static void TryValidateParam(Enum value, string paramName, string message = "")
        {
            TryValidateParam(paramName, nameof(paramName));

            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                if (message.IsNull())
                {
                    message = "The value is not defined in the enumeration.";
                }

                throw new ArgumentOutOfRangeException(paramName, message);
            }
        }

        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(Guid value, string paramName, string message = "")
        {
            if (value.Equals(Guid.Empty))
            {
                if (message.IsNull())
                {
                    message = "Guid is empty.";
                }

                throw new ArgumentInvalidException(message, paramName);
            }
        }
        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(string value, string paramName, string message = "")
        {
            if (value.IsNull())
            {
                if (message.IsNull())
                {
                    message = "String is empty.";
                }

                throw new ArgumentInvalidException(message, paramName);
            }
        }

        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="size">The size.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static void TryValidateParam(IEnumerable collection, int size, string paramName, string message = "")
        {
            TryValidateParam(collection, paramName, message);

            if (collection.Count() != size)
            {
                if (message.IsNull())
                {
                    message = "Collection size is not valid.";
                }

                throw new ArgumentOutOfRangeException(paramName, message);
            }
        }

        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="match">The match.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentNullException">match</exception>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(string value, Regex match, string paramName, string message = "")
        {
            if (match.IsNull())
            {
                throw new ArgumentNullException(nameof(match));
            }

            TryValidateParam(value, paramName, message);

            if (match.IsMatch(value) == false)
            {
                if (message.IsNull())
                {
                    message = "String is not valid.";
                }

                throw new ArgumentInvalidException(message, paramName);
            }
        }

        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expected">The expected.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(string value, string expected, string paramName, string message = "")
        {
            TryValidateParam(value, paramName, message);

            if (value.Equals(expected) == false)
            {
                if (message.IsNull())
                {
                    message = "String does not match.";
                }

                throw new ArgumentInvalidException(message, paramName);
            }
        }

        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(Type value, Type expectedType, string paramName, string message = "")
        {
            if (value != expectedType)
            {
                if (message.IsNull())
                {
                    message = "Invalid type.";
                }

                throw new ArgumentInvalidException(message, paramName);
            }
        }

        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minimumLength">The minimum length.</param>
        /// <param name="maximumLength">The maximum length.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException">
        /// </exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(string value, int minimumLength, int maximumLength, string paramName, string message = "")
        {
            TryValidateParam(value, paramName, message);


            if (value.Length < minimumLength)
            {
                if (message.IsNull())
                {
                    message = "String does not match minimum length.";
                }

                throw new ArgumentInvalidException(message, paramName);
            }

            if (value.Length > maximumLength)
            {
                if (message.IsNull())
                {
                    message = Resources.StringDoesNotMatchMaximumLength;
                }

                throw new ArgumentInvalidException(message, paramName);
            }
        }
    }

}