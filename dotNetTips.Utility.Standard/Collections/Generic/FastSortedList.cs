// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-22-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-22-2018
// ***********************************************************************
// <copyright file="SortedList.cs" company="dotNetTips.com - David McCarter">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using dotNetTips.Utility.Standard.Extensions;
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace dotNetTips.Utility.Standard.Collections.Generic
{
    /// <summary>
    /// Class SortedList.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="dotNetTips.Utility.Standard.ICloneable{T}" />
    /// <seealso cref="System.Collections.Generic.List{T}" />
    [DebuggerDisplay("Count = {Count}"), Serializable]
    public class FastSortedList<T> : List<T>, ICloneable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortedList{TKey, TValue}" /> class.
        /// </summary>
        public FastSortedList()
        {
        }

        /// <summary>
        /// Cones this instance.
        /// </summary>
        /// <returns>T.</returns>
        public T Cone() => this.Clone<T>();

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.List`1"></see>.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.List`1.Enumerator"></see> for the <see cref="T:System.Collections.Generic.List`1"></see>.</returns>
        public new Enumerator GetEnumerator()
        {
            base.Sort();
            return base.GetEnumerator();
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.List`1"></see> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="T:System.Collections.Generic.List`1"></see>.</returns>
        public new T[] ToArray()
        {
            base.Sort();
            return base.ToArray();
        }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> ToList()
        {
            base.Sort();
            return new List<T>(base.ToArray());
        }
    }
}
