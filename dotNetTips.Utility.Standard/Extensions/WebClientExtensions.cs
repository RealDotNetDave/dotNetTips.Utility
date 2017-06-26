using System;
using System.IO;
using System.Net;
using System.Text;

namespace dotNetTips.Utility.Standard.Extensions
{
    public static class WebClientExtensions
    {
        //public static T GetJson<T>(this WebClient client, String url) where T : class
        //{
        //    var data = client.DownloadString(url);
        //    if (data.IsNullOrWhitespace())
        //        return null;

        //    var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
        //    var serializer = new DataContractJsonSerializer(typeof(T));
        //    var obj = (T)serializer.ReadObject(stream);
        //    stream.Close();
        //    return obj;
        //}
    }
}