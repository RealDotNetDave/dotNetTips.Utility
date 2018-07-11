// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 07-07-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-07-2018
// ***********************************************************************
// <copyright file="SQSHelper.cs" company="McCarter Consulting - dotNetTips.com">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Net;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using dotNetTips.Utility.Standard.OOP;

namespace dotNetTips.Utility.Standard.Amazon
{
    /// <summary>
    /// Amazon SQS helper.
    /// </summary>
    public static class SQSHelper
    {
        /// <summary>
        /// Deletes the SQS message.
        /// </summary>
        /// <param name="sqsUri">The SQS URI.</param>
        /// <param name="messageReceiptHandle">The message.</param>
        /// <returns>HttpStatusCode.</returns>
        public static HttpStatusCode Delete(string sqsUri, string messageReceiptHandle)
        {
            Encapsulation.TryValidateParam(sqsUri, nameof(sqsUri));
            Encapsulation.TryValidateParam(messageReceiptHandle, nameof(messageReceiptHandle));

            using (var sqs = new AmazonSQSClient(RegionEndpoint.USWest2))
            {
                var deleteRequest = new DeleteMessageRequest(sqsUri, messageReceiptHandle);

                var deleteResponse = sqs.DeleteMessageAsync(deleteRequest).Result;

                return deleteResponse.HttpStatusCode;
            }
        }
    }
}
