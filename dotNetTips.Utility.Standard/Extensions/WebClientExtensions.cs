// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 08-06-2017
//
// Last Modified By : David McCarter
// Last Modified On : 06-26-2017
// ***********************************************************************
// <copyright file="WebClientExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class WebClientExtensions.
    /// </summary>
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