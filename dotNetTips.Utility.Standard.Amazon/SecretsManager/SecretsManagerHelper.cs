// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 06-20-2018
//
// Last Modified By : David McCarter
// Last Modified On : 06-20-2018
// ***********************************************************************
// <copyright file="SecretsManagerHelper.cs" company="McCarter Consulting - dotNetTips.com">
//     David McCarter - dotNetTips.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using dotNetTips.Utility.Standard.OOP;

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
        private static GetSecretValueResponse GetSecretValue(AmazonSecretsManagerClient client, string secretKey)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(client != null, nameof(client));
            Encapsulation.TryValidateParam(secretKey, nameof(secretKey));

            var request = new GetSecretValueRequest
            {
                SecretId = secretKey
            };

            return client.GetSecretValueAsync(request).Result;
        }
    }
}
