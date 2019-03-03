﻿// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 09-15-2017
//
// Last Modified By : David McCarter
// Last Modified On : 03-03-2019
// ***********************************************************************
// <copyright file="ObjectExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using dotNetTips.Utility.Standard.Extensions.Properties;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class ObjectExtensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentNullException">value - Value cannot be null.</exception>
        public static T As<T>(this object value) => (T)value;

        /// <summary>
        /// Clones the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>T.</returns>
        public static T Clone<T>(this object obj)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);

                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Computes the m d5 hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
        public static string ComputeMD5Hash(this object data)
        {
            // Create a MD5   
            using (var md5Hash = MD5.Create())
            {
                // ComputeHash - returns byte array  
                var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(data.ToJson()));

                // Convert byte array to a string   
                var builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2", CultureInfo.InvariantCulture));
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Computes the sha256 hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
        public static string ComputeSha256Hash(this object data)
        {
            // Create a SHA256   
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data.ToJson()));

                // Convert byte array to a string   
                var builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2", CultureInfo.InvariantCulture));
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Disposes the fields.
        /// </summary>
        /// <param name="obj">The object.</param>
        public static void DisposeFields(this IDisposable obj)
        {
            var fieldInfos = obj.GetType().GetRuntimeFields();

            foreach (var fieldInfo in fieldInfos.Where(p => p.IsStatic == false).AsParallel())
            {
                var value = fieldInfo.GetValue(obj);

                if (value != null)
                {
                    if (value is IDisposable disposableItem)
                    {
                        disposableItem.TryDispose();
                    }
                    else if (value is IEnumerable collection)
                    {
                        collection.DisposeCollection();
                    }
                }
            }
        }

        /// <summary>
        /// Creates object from Json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The json.</param>
        /// <returns>T.</returns>
        public static T FromJson<T>(this string input) where T : class => JsonSerializer.Deserialize<T>(input);

        /// <summary>
        /// Creates object from a Json file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>T.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        public static T FromJsonFile<T>(string fileName)
            where T : class
        {
            if (File.Exists(fileName) == false)
            {
                throw new FileNotFoundException(Resources.FileNotFound, fileName);
            }

            var json = File.ReadAllText(fileName, Encoding.UTF8);

            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Determines whether the specified object has the property.
        /// </summary>
        /// <param name="obj">The instance.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the specified property name has property; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">propertyName - Source cannot be null.</exception>
        public static bool HasProperty(this object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetRuntimeProperties().FirstOrDefault(p => p.Name == propertyName);

            return (propertyInfo != null);
        }

        /// <summary>
        /// Ins the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="list">The list.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">source - Source cannot be null.
        /// or
        /// list - List cannot be null or have a 0 length.</exception>
        /// <remarks>Original code by: Rory Becker</remarks>
        public static bool In<T>(this T source, params T[] list) => list.FastAny(value => value.Equals(source));

        /// <summary>
        /// Initializes the fields.
        /// </summary>
        /// <param name="obj">The object.</param>
        public static void InitializeFields(this object obj)
        {
            var fieldInfos = obj.GetType().GetRuntimeFields();

            foreach (var fieldInfo in fieldInfos.AsParallel())
            {
                var objectValue = fieldInfo.GetValue(obj);
                var runtimeField = obj.GetType().GetRuntimeField(fieldInfo.Name);

                if (runtimeField != null)
                {
                    var t = Nullable.GetUnderlyingType(runtimeField.FieldType) ?? runtimeField.FieldType;
                    var safeValue = (objectValue == null)
                        ? null
                        : Convert.ChangeType(objectValue, t, CultureInfo.InvariantCulture);
                    runtimeField.SetValue(obj, safeValue);
                }
            }
        }

        /// <summary>
        /// Determines whether [is not null] [the specified object].
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns><count>true</count> if [is not null] [the specified object]; otherwise, <count>false</count>.</returns>
        public static bool IsNotNull(this object obj) => obj != null;

        /// <summary>
        /// Determines whether the specified object is null.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns><count>true</count> if the specified object is null; otherwise, <count>false</count>.</returns>
        public static bool IsNull(this object obj) => obj is null;

        /// <summary>
        /// Strips the null.
        /// </summary>
        /// <param name="input">The field.</param>
        /// <returns>System.String.</returns>
        public static string StripNull(this object input) => input == null ? (string.Empty) : (input.ToString());

        /// <summary>
        /// Serializes object to Json.
        /// </summary>
        /// <param name="obj">The instance.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this object obj) => JsonSerializer.Serialize(obj);

        /// <summary>
        /// Saves object to Json file.
        /// </summary>
        /// <param name="obj">The instance.</param>
        /// <param name="fileName">The file.</param>
        public static void ToJsonFile(this object obj, string fileName)
        {
            var json = JsonSerializer.Serialize(obj);

            File.WriteAllText(fileName, json, Encoding.UTF8);
        }

        /// <summary>
        /// Tries the to call Dispose.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public static void TryDispose(this IDisposable obj) => ObjectExtensions.TryDispose(obj, false);

        /// <summary>
        /// Tries to Dispose the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="throwException">if set to <count>true</count> [throw exception].</param>
        public static void TryDispose(this IDisposable obj, bool throwException)
        {
            try
            {
                if (IsNotNull(obj))
                {
                    obj.Dispose();
                    obj = null;
                }
            }
            catch //Swallow exception on purpose.
            {
                if (throwException)
                {
                    throw;
                }
            }
        }
    }
}