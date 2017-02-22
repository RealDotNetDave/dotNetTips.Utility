using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace dotNetTips.Utility.Standard.Xml
{
    public class XmlHelper
    {
        /// <summary>
        /// Deserializes from XML file.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public T DeserializeFromXmlFile<T>(string fileName)
        {
            return Deserialize<T>(File.ReadAllText(fileName));

        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            using (var reader = new StringReader(xml))
            {
                using (var xmlReader = new XmlTextReader(reader))
                {
                    return (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
                }
            }
        }

        /// <summary>
        /// Serializes obj to XML file.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="fileName">Name of the file.</param>

        public void SerializeToXmlFile(object obj, string fileName)
        {
            My.Computer.FileSystem.WriteAllText(fileName, Serialize(obj), false);

        }

        /// <summary>
        /// Serializes the specified obj to xml.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public string Serialize(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            using (var writer = new StringWriter())
            {
                //TODO: BLOG POST
                using (var xmlWriter = XmlWriter.Create(writer))
                {
                    System.Xml.XmlDictionaryWriter
           XmlSerializer serilizer = new XmlSerializer(obj.GetType());
                    serilizer.Serialize(xmlWriter, obj);
                    return writer.ToString();
                }
            }

        }
    }
}
