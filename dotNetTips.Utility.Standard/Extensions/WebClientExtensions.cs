using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class WebClientExtensions.
    /// </summary>
    public static class WebClientExtensions
    {
        /// <summary>
        /// Gets the json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client">The client.</param>
        /// <param name="url">The URL.</param>
        /// <returns>T.</returns>
        public static T GetJson<T>(this WebClient client, string url) where T : class
        {
            var data = client.DownloadString(url);
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            var serializer = new DataContractJsonSerializer(typeof(T));
            var obj = (T)serializer.ReadObject(stream);
            stream.Close();

            return obj;
        }
    }
}