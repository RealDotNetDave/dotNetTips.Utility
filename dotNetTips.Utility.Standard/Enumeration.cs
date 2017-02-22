// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-22-2017
//
// Last Modified By : David McCarter
// Last Modified On : 01-22-2017
// ***********************************************************************
// <copyright file="Enumeration.cs" company="dotNetTips.Utility.Standard">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

/// <summary>
/// The Standard namespace.
/// </summary>
namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Class Enumeration.
    /// </summary>
    /// <example>
    /// public class EmployeeType : Enumeration { public static readonly EmployeeType Manager
    /// = new EmployeeType(0, "Manager"); public static readonly EmployeeType Servant
    /// = new EmployeeType(1, "Servant"); public static readonly EmployeeType AssistantToTheRegionalManager
    /// = new EmployeeType(2, "Assistant to the Regional Manager"); private EmployeeType() { }
    /// private EmployeeType(int value, string displayName) : base(value, displayName) { } }
    /// </example>
    /// <seealso cref="System.IComparable" />
    /// <remarks>Original code from: https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/</remarks>
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
        /// Initializes a new instance of the <see cref="Enumeration" /> class.
        /// </summary>
        protected Enumeration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enumeration" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
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
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures
        /// like a hash table.</returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Absolutes the difference.
        /// </summary>
        /// <param name="firstValue">The first value.</param>
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
        /// <param name="other">The other.</param>
        /// <returns>T.</returns>
        /// <exception cref="ApplicationException"></exception>
        public int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration)other).Value);
        }
    }
}