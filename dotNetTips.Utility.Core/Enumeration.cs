// *********************************************************************** Assembly :
// dotNetTips.Utility.Portable Author : David McCarter Created : 07-27-2016
//
// Last Modified By : David McCarter Last Modified On : 07-27-2016 ***********************************************************************
// <copyright file="Enumeration.cs" company="dotNetTips.com">
//     Copyright Â© 2015
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;

namespace dotNetTips.Utility.Portable
{
    /// <summary>
    /// Class Enumeration.
    /// </summary>
    /// <remarks>Original code from: https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/</remarks>
    /// <example>
    /// public class EmployeeType : Enumeration { public static readonly EmployeeType Manager
    /// = new EmployeeType(0, "Manager"); public static readonly EmployeeType Servant
    /// = new EmployeeType(1, "Servant"); public static readonly EmployeeType AssistantToTheRegionalManager
    /// = new EmployeeType(2, "Assistant to the Regional Manager"); private EmployeeType() { }
    ///   private EmployeeType(int value, string displayName) : base(value, displayName) { } }
    /// </example>
    /// <seealso cref="System.IComparable"/>
    public abstract class Enumeration : IComparable
    {
        /// <summary>
        /// Value
        /// </summary>
        private readonly int _value;

        /// <summary>
        /// isplay name
        /// </summary>
        private readonly string _displayName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Enumeration"/> class.
        /// </summary>
        protected Enumeration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enumeration"/> class.
        /// </summary>
        /// <param name="value">      The value.</param>
        /// <param name="displayName">The display name.</param>
        protected Enumeration(int value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get { return _displayName; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        //public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        //{
        //    var type = typeof(T);
        //    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        // foreach (var info in fields) { var instance = new T(); var locatedValue =
        // info.GetValue(instance) as T;

        //        if (locatedValue != null)
        //        {
        //            yield return locatedValue;
        //        }
        //    }
        //}

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = _value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures
        /// like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Absolutes the difference.
        /// </summary>
        /// <param name="firstValue"> The first value.</param>
        /// <param name="secondValue">The second value.</param>
        /// <returns>System.Int32.</returns>
        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
            return absoluteDifference;
        }

        /// <summary>
        /// Froms the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        //public static T FromValue<T>(int value) where T : Enumeration, new()
        //{
        //    var matchingItem = Parse<T, int>(value, nameof(value), item => item.Value == value);
        //    return matchingItem;
        //}

        /// <summary>
        /// Froms the display name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="displayName">The display name.</param>
        /// <returns>T.</returns>
        //public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
        //{
        //    var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
        //    return matchingItem;
        //}

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="value">      The value.</param>
        /// <param name="description">The description.</param>
        /// <param name="predicate">  The predicate.</param>
        /// <returns>T.</returns>
        /// <exception cref="ApplicationException"></exception>
        //private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        //{
        //    var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        // if (matchingItem == null) { var message = string.Format("'{0}' is not a valid {1} in {2}",
        // value, description, typeof(T)); throw new ApplicationException(message); }

        //    return matchingItem;
        //}

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        public int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration)other).Value);
        }
    }
}