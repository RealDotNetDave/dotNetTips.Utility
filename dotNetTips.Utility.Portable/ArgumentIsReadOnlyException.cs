// *********************************************************************** Assembly :
// dotNetTips.Utility.Portable Author : David McCarter Created : 06-01-2016
//
// Last Modified By : David McCarter Last Modified On : 06-12-2016 ***********************************************************************
// <copyright file="ArgumentIsReadOnlyException.cs" company="dotNetTips.com">
//     Copyright Â© 2015
// </copyright>
// <summary>
// </summary>
// ***********************************************************************

using System;

namespace dotNetTips.Utility.Portable
{
    /// <summary>
    /// Class ArgumentIsReadOnlyException.
    /// </summary>
    /// <seealso cref="System.Exception"/>

    public class ArgumentIsReadOnlyException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentIsReadOnlyException"/> class.
        /// </summary>
        public ArgumentIsReadOnlyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ArgumentIsReadOnlyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentIsReadOnlyException"/> class.
        /// </summary>
        /// <param name="message">       The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in
        /// Visual Basic) if no inner exception is specified.
        /// </param>
        public ArgumentIsReadOnlyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}