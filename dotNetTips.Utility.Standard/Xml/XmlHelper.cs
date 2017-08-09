// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 02-04-2017
//
// Last Modified By : David McCarter
// Last Modified On : 08-06-2017
// ***********************************************************************
// <copyright file="XmlHelper.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using dotNetTips.Utility.Standard.OOP;
using System;
using System.IO;
using System.Xml;

namespace dotNetTips.Utility.Standard.Xml
{
    /// <summary>
    /// Class XmlHelper.
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// Deserializes from XML file.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>T.</returns>
        public T DeserializeFromXmlFile<T>(string fileName)
        {
            return Deserialize<T>(File.ReadAllText(fileName));

        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentNullException">xml</exception>
        public T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            using (var reader = new StringReader(xml))
            {
                //TODO: NEED TO FIGURE THIS OUT
                //using (var xmlReader = new XmlTextReader(reader))
                //{
                //    return (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
                //}
                return default(T);
            }
        }

        /// <summary>
        /// Serializes obj to XML file.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="NotImplementedException"></exception>

        public void SerializeToXmlFile(object obj, string fileName)
        {
            //TODO: NEED TO FIGURE THIS OUT
            //  My.Computer.FileSystem.WriteAllText(fileName, Serialize(obj), false);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serializes the specified obj to xml.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">obj</exception>
        public string Serialize(object obj)
        {

            Encapsulation.TryValidateParam<ArgumentNullException>(obj != null);

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
         
            using (var writer = new StringWriter())
            {
                //TODO: BLOG POST
                using (var xmlWriter = XmlWriter.Create(writer))
                {
                    //TODO: NEED TO FIGURE THIS OUT
                    //XmlSerializer serilizer = new XmlSerializer(obj.GetType());
                    //serilizer.Serialize(xmlWriter, obj);
                    //return writer.ToString();
                    return null;
                }
            }

        }
    }
}
