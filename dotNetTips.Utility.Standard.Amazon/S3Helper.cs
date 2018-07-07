// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 07-07-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-07-2018
// ***********************************************************************
// <copyright file="S3Helper.cs" company="McCarter Consulting - dotNetTips.com">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Net;

namespace dotNetTips.Utility.Standard.Amazon
{
    /// <summary>
    /// Helper class for Amazon S3.
    /// </summary>
    public static class S3Helper
    {



        /// <summary>
        /// Retrieves as string.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static (HttpStatusCode statusCode, string data)  RetrieveAsString(RegionEndpoint region, string bucketName, string key)
        {
            OOP.Encapsulation.TryValidateParam<ArgumentNullException>(region != null, nameof(region));
            OOP.Encapsulation.TryValidateParam(bucketName, nameof(bucketName));
            OOP.Encapsulation.TryValidateParam(key, nameof(key));

            using (var client = new AmazonS3Client(region))
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName.Trim(),
                    Key = key.Trim()
                };

                using (var response = client.GetObjectAsync(request).Result)
                {
                    using (var responseStream = response.ResponseStream)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            var responseBody = reader.ReadToEnd();

                            return (response.HttpStatusCode, responseBody);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sends data to S3.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="bucketName">Name of the bucket.</param>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <returns>HttpStatusCode.</returns>
        public static HttpStatusCode Store(RegionEndpoint region, string bucketName, string key, string data)
        {
            OOP.Encapsulation.TryValidateParam<ArgumentNullException>(region != null, nameof(region));
            OOP.Encapsulation.TryValidateParam(bucketName, nameof(bucketName));
            OOP.Encapsulation.TryValidateParam(key, nameof(key));

            using (var client = new AmazonS3Client(region))
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName.Trim(),
                    Key = key.Trim(),
                    ContentBody = data
                };

                var putObjectResponse = client.PutObjectAsync(putRequest).Result;

                return putObjectResponse.HttpStatusCode;
            }
        }
    }
}
