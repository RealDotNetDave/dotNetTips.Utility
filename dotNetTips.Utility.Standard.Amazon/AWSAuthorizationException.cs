﻿// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 06-20-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-07-2018
// ***********************************************************************
// <copyright file="AWSAuthorizationException.cs" company="McCarter Consulting - dotNetTips.com">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace dotNetTips.Utility.Standard.Amazon
{
    /// <summary>
    /// Class AWSAuthorizationException.
    /// </summary>
    /// <seealso cref="dotNetTips.Utility.Standard.LoggableException" />
    /// <seealso cref="System.Exception" />
    public class AWSAuthorizationException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AWSAuthorizationException"></see> class.
        /// </summary>
        public AWSAuthorizationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AWSAuthorizationException"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        public AWSAuthorizationException(string message, string awsProfileName) : base(message)
        {
            this.ProfileName = awsProfileName;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AWSAuthorizationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <param name="innerException">The inner exception.</param>
        public AWSAuthorizationException(string message, string awsProfileName, Exception innerException) : base(message, innerException)
        {
            this.ProfileName = awsProfileName;
        }

        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        /// <value>The name of the profile.</value>
        public string ProfileName { get; private set; }
    }
}
