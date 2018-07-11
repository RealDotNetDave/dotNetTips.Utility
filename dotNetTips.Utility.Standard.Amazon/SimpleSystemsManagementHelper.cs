// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 07-07-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-07-2018
// ***********************************************************************
// <copyright file="SimpleSystemsManagementHelper.cs" company="McCarter Consulting - dotNetTips.com">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Net;
using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using dotNetTips.Utility.Standard.OOP;

namespace dotNetTips.Utility.Standard.Amazon
{
    /// <summary>
    /// Helper for Amazon SimpleSystemsManagement.
    /// </summary>
    public static class SimpleSystemsManagementHelper
    {

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="withDecryption">if set to <c>true</c> [with decryption].</param>
        /// <returns>System.String.</returns>
        public static (HttpStatusCode statusCode, string paramater) GetParameter(RegionEndpoint region, string parameterName, bool withDecryption)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(region != null, nameof(region));
            Encapsulation.TryValidateParam(parameterName, nameof(parameterName));

            using (var client = new AmazonSimpleSystemsManagementClient(region))
            {
                var request = new GetParameterRequest
                {
                    Name = parameterName,
                    WithDecryption = withDecryption
                };

                var response = client.GetParameterAsync(request).Result;

                return (response.HttpStatusCode, response.Parameter.Value);
            }
        }
    }
}
