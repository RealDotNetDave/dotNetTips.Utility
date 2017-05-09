// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 12-07-2016
//
// Last Modified By : David McCarter
// Last Modified On : 03-15-2017
// ***********************************************************************
// <copyright file="ArgumentIsReadOnlyException.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace dotNetTips.Utility.Portable
{
    /// <summary>
    /// Class ArgumentIsInvalidException.
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
    public class ArgumentInvalidException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidException" /> class.
        /// </summary>
        public ArgumentInvalidException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ArgumentInvalidException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentReadOnlyException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in
        /// Visual Basic) if no inner exception is specified.</param>
        public ArgumentInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        public ArgumentInvalidException(string message, string paramName)
: base(message, paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentInvalidException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public ArgumentInvalidException(string message, string paramName, Exception innerException)
    : base(message, paramName, innerException)
        {
        }
    }
}