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
using System.Globalization;
using System.Text.RegularExpressions;
using dotNetTips.Utility.Standard.Extensions;
using dotNetTips.Utility.Standard.Properties;
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
        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="condition">The condition.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.InvalidCastException"></exception>
        public static void TryValidateParam<TException>(bool condition, string paramName = "", string message = "") where TException : ArgumentException, new()
        {
            // Confirm proper Exception type
            var t = typeof(TException);

            if (t.Name == nameof(Exception))
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentUICulture, Resources.CannotBeOfTypeException, nameof(TException)));
            }
            Argument
            var defaultMessage = Resources.ParameterIsInvalid;

            if (string.IsNullOrEmpty(message) == false)
            {
                defaultMessage = message;
            }

            if (condition == false)
            {
                var ex = Activator.CreateInstance(typeof(TException), paramName, defaultMessage).As<TException>();
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
        public static void TryValidateParam(Enum value, string paramName, string message = "")
        {
            TryValidateParam(paramName, nameof(paramName));

            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                if (message.IsNull())
                {
                    message = Properties.Resources.TheValueIsNotDefinedInTheEnumeration;
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
        /// <exception cref="ArgumentInvalidException"></exception>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <remarks>TEST Created</remarks>
        public static void TryValidateParam(Guid value, string paramName, string message = "")
        {
            if (value.Equals(Guid.Empty))
            {
                if (message.IsNull())
                {
                    message = Properties.Resources.GuidIsEmpty;
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
        /// <exception cref="ArgumentInvalidException"></exception>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(string value, string paramName, string message = "")
        {
            if (value.IsNull())
            {
                if (message.IsNull())
                {
                    message = Properties.Resources.StringIsEmpty;
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
                    message = Properties.Resources.CollectionSizeIsNotValid;
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
        /// <exception cref="ArgumentInvalidException"></exception>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException">match</exception>
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
                    message = Properties.Resources.StringIsNotValid;
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
        /// <exception cref="ArgumentInvalidException"></exception>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(string value, string expected, string paramName, string message = "")
        {
            TryValidateParam(value, paramName, message);

            if (value.Equals(expected, StringComparison.CurrentCultureIgnoreCase) == false)
            {
                if (message.IsNull())
                {
                    message = Properties.Resources.StringDoesNotMatch;
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
        /// <exception cref="ArgumentInvalidException"></exception>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(Type value, Type expectedType, string paramName, string message = "")
        {
            if (value != expectedType)
            {
                if (message.IsNull())
                {
                    message = Properties.Resources.InvalidType;
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
        /// <exception cref="ArgumentInvalidException"></exception>
        /// <exception cref="dotNetTips.Utility.Portable.ArgumentInvalidException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static void TryValidateParam(string value, int minimumLength, int maximumLength, string paramName, string message = "")
        {
            TryValidateParam(value, paramName, message);


            if (value.Length < minimumLength)
            {
                if (message.IsNull())
                {
                    message = Properties.Resources.StringDoesNotMatchMinimumLength;
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