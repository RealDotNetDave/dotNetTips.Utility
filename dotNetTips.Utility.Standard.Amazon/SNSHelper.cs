// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 07-07-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-07-2018
// ***********************************************************************
// <copyright file="SNSHelper.cs" company="McCarter Consulting - dotNetTips.com">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************

using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;
using System.Net;

namespace dotNetTips.Utility.Standard.Amazon
{
    /// <summary>
    /// Class SNSHelper.
    /// </summary>
    public static class SNSHelper
    {
        /// <summary>
        /// Sends to queue.
        /// </summary>
        /// <param name="region">The region endpoint.</param>
        /// <param name="topicArn">The topic arn.</param>
        /// <param name="data">The message.</param>
        /// <returns>Tuple&lt;HttpStatusCode, System.String&gt;.</returns>
        /// <exception cref="dotNetTips.Utility.Standard.Amazon.NotQueuedException"></exception>
        /// <exception cref="NotQueuedException"></exception>
        public static (HttpStatusCode statusCode, string messageId) SendToQueue(RegionEndpoint region, string topicArn, string data)
        {
            OOP.Encapsulation.TryValidateParam<ArgumentNullException>(region != null, nameof(region));
            OOP.Encapsulation.TryValidateParam(topicArn, nameof(topicArn));
            OOP.Encapsulation.TryValidateParam(data, nameof(data));

            try
            {
                using (var snsClient = new AmazonSimpleNotificationServiceClient(region))
                {
                    // Sending a message
                    var sendMessageRequest = new PublishRequest
                    {
                        TopicArn = topicArn,
                        Message = data
                    };

                    var result = snsClient.PublishAsync(sendMessageRequest).Result;

                    return (result.HttpStatusCode, result.MessageId);
                }
            }
            catch (Exception ex)
            {

                throw new NotQueuedException($"Unable to queue message into AWS SNS: {topicArn}.", ex);
            }
        }
    }
}
