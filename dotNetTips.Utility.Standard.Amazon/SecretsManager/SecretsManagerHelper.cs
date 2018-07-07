// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 06-20-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-07-2018
// ***********************************************************************
// <copyright file="SecretsManagerHelper.cs" company="McCarter Consulting - dotNetTips.com">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace dotNetTips.Utility.Standard.Amazon.SecretsManager
{
    /// <summary>
    /// Class SecretsManagerHelper.
    /// </summary>
    public static class SecretsManagerHelper
    {
        /// <summary>
        /// Gets the secret value.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="secretKey">The secret key.</param>
        /// <returns>GetSecretValueResponse.</returns>
        /// <exception cref="ArgumentNullException">client - client</exception>
        /// <exception cref="ArgumentException">secretKey - secretKey</exception>
        /// <exception cref="System.ArgumentNullException">client - client</exception>
        /// <exception cref="System.ArgumentException">secretKey - secretKey</exception>
        private static GetSecretValueResponse GetSecretValue(AmazonSecretsManagerClient client, string secretKey)
        {
            OOP.Encapsulation.TryValidateParam<ArgumentNullException>(client != null, nameof(client));
            OOP.Encapsulation.TryValidateParam(secretKey, nameof(secretKey));

            var request = new GetSecretValueRequest
            {
                SecretId = secretKey
            };

            return client.GetSecretValueAsync(request).Result;
        }
    }
}
