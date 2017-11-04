using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class EnumExtensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Ases the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">The value.</param>
        /// <returns>T.</returns>
        public static T As<T>(this Enum val)
        {
            var enumType = val.GetType();
            var enumValue = Enum.Parse(enumType, val.ToString());
            return (T)enumValue;
        }
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetDescription(this Enum val)
        {
            var field = val.GetType().GetField(val.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : val.ToString();
        }
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration">The enumeration.</param>
        /// <returns>IList&lt;EnumItem&lt;T&gt;&gt;.</returns>
        public static IList<EnumItem<T>> GetItems<T>(this Enum enumeration)
        {
            var enumType = enumeration.GetType();
            var values = Enum.GetValues(enumType);
            var items = new List<EnumItem<T>>();
            foreach (var v in values)
            {
                var enumValue = Enum.Parse(enumType, v.ToString());
                items.Add(GetDescriptionInternal<T>(enumValue));
            }
            return items;
        }
        /// <summary>
        /// Parses the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns>T.</returns>
        public static T Parse<T>(this String name) where T : struct => (T)Enum.Parse(typeof(T), name);
        /// <summary>
        /// Gets the description internal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">The value.</param>
        /// <returns>EnumItem&lt;T&gt;.</returns>
        private static EnumItem<T> GetDescriptionInternal<T>(object val)
        {
            var field = val.GetType().GetField(val.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            var enumItem = new EnumItem<T>
            {
                Description = attributes.Length > 0 ? attributes[0].Description : val.ToString(),
                Value = (T)val
            };
            return enumItem;
        }
    }
}
