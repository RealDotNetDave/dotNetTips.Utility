// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 02-02-2017
//
// Last Modified By : David McCarter
// Last Modified On : 02-02-2017
// ***********************************************************************
// <copyright file="Encapsulation.cs" company="dotNetTips.Utility.Portable">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Diagnostics.Contracts;
using dotNetTips.Utility.Portable.Extensions;

namespace dotNetTips.Utility.Portable.OOP {
    /// <summary>
    /// Class Encapsulation.
    /// </summary>
    public static class Encapsulation {
        /// <summary>
        /// Tries the validate parameter.
        /// </summary>
        /// <typeparam name="TException">The type of the t exception.</typeparam>
        /// <param name="condition">The condition.</param>
        /// <param name="message">The message.</param>
        public static void TryValidateParam<TException>(bool condition, string message = "") where TException : ArgumentException, new() {
            //Validate proper Exception type
            var t = typeof(TException);

            if (t.Name == nameof(Exception)) {
                throw new InvalidCastException("TException cannot be of type Exception. Use a more specific exception from the framework or a custom Exception inheriting type Exception (only)");
            }

            var defaultMessage = "Parameter is invalid.";

            if (string.IsNullOrEmpty(message)) {
                defaultMessage = message;
            }

            if (condition == false) {
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
        /// <exception cref="ArgumentNullException"></exception>
        public static void TryValidateParam(IEnumerable collection, string paramName, string message = "") {
            if (collection.IsValid() == false) {
                if (message.IsNull()) {
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
        public static void TryValidateParam(Enum value, string paramName, string message = "The value is not defined in the enum.") {
            Contract.Requires<ArgumentNullException>(string.IsNullOrEmpty(paramName) == false, "paramName cannot be null or empty.");
            Contract.Requires<ArgumentNullException>(string.IsNullOrEmpty(message) == false, "message cannot be null or empty.");

            if (Enum.IsDefined(value.GetType(), value) == false) {
                throw new ArgumentOutOfRangeException(paramName, message);
            }
        }
    }

}