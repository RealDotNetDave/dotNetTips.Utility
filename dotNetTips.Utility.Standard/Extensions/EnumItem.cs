using System;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class EnumItem.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumItem<T>
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public String Description { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Value { get; set; }
    }
}
