// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 07-07-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-07-2018
// ***********************************************************************
// <copyright file="RedisHelper.cs" company="McCarter Consulting - dotNetTips.com">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using ServiceStack.Redis;

namespace dotNetTips.Utility.Standard.Amazon
{
    /// <summary>
    /// Helper for Amazon Redis cache.
    /// </summary>
    public static class RedisHelper
    {
        /// <summary>
        /// Caches the message.
        /// </summary>
        /// <param name="host">The redis connection.</param>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CacheMessage(string host, string key, string data)
        {
            OOP.Encapsulation.TryValidateParam(host, nameof(host));
            OOP.Encapsulation.TryValidateParam(key, nameof(key));
            OOP.Encapsulation.TryValidateParam(data, nameof(data));

            using (var manager = new RedisManagerPool(host.Trim()))
            {
                using (var cache = manager.GetClient())
                {
                    return cache.Set(key.Trim(), data);
                }
            }
        }

        /// <summary>
        /// Loads from cache.
        /// </summary>
        /// <param name="host">The redis host.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static string LoadFromCache(string host, string key)
        {
            OOP.Encapsulation.TryValidateParam(host, nameof(host));
            OOP.Encapsulation.TryValidateParam(key, nameof(key));

            using (var manager = new RedisManagerPool(host.Trim()))
            {
                using (var redisClient = manager.GetClient())
                {
                    var messageBody = redisClient.Get<string>(key.Trim());
                    return messageBody;
                }
            }
        }
    }
}
