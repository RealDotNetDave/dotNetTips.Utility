using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using dotNetTips.Utility.Standard.OOP;
using dotNetTips.Utility.Standard.Serialization;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class ObjectExtensions.
    /// </summary>
    public static class ObjectExtensions
    {
        #region Public Methods

        /// <summary>
        /// Returns the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentNullException">value - Value cannot be null.</exception>
        public static T As<T>(this object value) => (T)value;

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
        public static bool In<T>(this T source, params T[] list)
        {
            Encapsulation.TryValidateParam(list, nameof(list));

            foreach (T value in list)
            {
                if (value.Equals(source))
                {
                    return true;
                }
            }
            return false;
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
                }
            }
            catch
            {
                if (throwException)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Tries the to call Dispose.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public static void TryDispose(this IDisposable obj) => ObjectExtensions.TryDispose(obj, false);

        /// <summary>
        /// Determines whether the specified object has the property.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the specified property name has property; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">propertyName - Source cannot be null.</exception>
        public static bool HasProperty(this object instance, string propertyName)
        {
            Encapsulation.TryValidateParam(propertyName, nameof(propertyName));

            var propertyInfo = instance.GetType().GetRuntimeProperties().FirstOrDefault(p => p.Name == propertyName);

            return (propertyInfo != null);
        }

        /// <summary>
        /// Serializes object to Json.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>System.String.</returns>
        public static string ToJson(this object instance) => JsonSerializer.Serialize(instance);

        /// <summary>
        /// Disposes the fields.
        /// </summary>
        /// <param name="obj">The object.</param>
        public static void DisposeFields(this IDisposable obj)
        {
            var fieldInfos = obj.GetType().GetRuntimeFields();

            foreach (var fieldInfo in fieldInfos.AsParallel())
            {
                var value = fieldInfo.GetValue(null) as IDisposable;

                if (value == null)
                {
                    continue;
                }

                value.Dispose();
                fieldInfo.SetValue(obj, null);
            }
        }

        /// <summary>
        /// Strips the null.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>System.String.</returns>
        public static string StripNull(this object field) => field == null ? (string.Empty) : (field.ToString());

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
                    var safeValue = (objectValue == null) ? null : Convert.ChangeType(objectValue, t, CultureInfo.InvariantCulture);
                    runtimeField.SetValue(obj, safeValue);
                }
            }
        }

        /// <summary>
        /// Saves object to Json file.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="file">The file.</param>
        public static void ToJsonFile(this object instance, string file)
        {
            var json = JsonSerializer.Serialize(instance);

            File.WriteAllText(file, json, Encoding.UTF8);
        }

        /// <summary>
        /// Creates object from Json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <returns>T.</returns>
        public static T FromJson<T>(this string json) where T : class => JsonSerializer.Deserialize<T>(json);

        /// <summary>
        /// Creates object from a Json file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file">The file.</param>
        /// <returns>T.</returns>
        /// <exception cref="FileNotFoundException">File not found.</exception>
        public static T FromJsonFile<T>(string file) where T : class
        {
            Encapsulation.TryValidateParam(file, nameof(file));

            if (File.Exists(file) == false)
            {
                throw new FileNotFoundException("File not found.", file);
            }

            var json = File.ReadAllText(file, Encoding.UTF8);

            return JsonSerializer.Deserialize<T>(json);
        }
        #endregion Public Methods
    }
}