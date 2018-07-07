// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 07-07-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-07-2018
// ***********************************************************************
// <copyright file="NotQueuedException.cs" company="McCarter Consulting - dotNetTips.com">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace dotNetTips.Utility.Standard.Amazon
{
    /// <summary>
    /// Class MessageNotQueuedException.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class NotQueuedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AWSAuthorizationException"></see> class.
        /// </summary>
        public NotQueuedException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AWSAuthorizationException"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotQueuedException(string message) : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AWSAuthorizationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NotQueuedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
