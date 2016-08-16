﻿// *********************************************************************** Assembly : Author : David
// McCarter Created : 05-31-2016
//
// Last Modified By : David McCarter Last Modified On : 05-31-2016 ***********************************************************************
// <copyright file="ArgumentIsReadOnlyException.cs" company="McCarter Consulting">
//     Copyright (c) McCarter Consulting. All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;

namespace dotNetTips.Utility.Core
{
    /// <summary>
    /// Class ArgumentIsReadOnlyException.
    /// </summary>
    /// <seealso cref="System.Exception"/>
    public class ArgumentIsReadOnlyException : Exception
    {
        #region Public Constructors

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

        #endregion Public Constructors
    }
}