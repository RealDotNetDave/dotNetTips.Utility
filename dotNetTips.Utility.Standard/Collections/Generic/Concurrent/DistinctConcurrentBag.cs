// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 12-14-2017
//
// Last Modified By : David McCarter
// Last Modified On : 05-28-2018
// ***********************************************************************
// <copyright file="DistinctConcurrentBag.cs" company="dotNetTips.com - David McCarter">
//      McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
using dotNetTips.Utility.Standard.OOP;
// ***********************************************************************

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace dotNetTips.Utility.Standard.Collections.Generic.Concurrent
{
    /// <summary>
    /// Class DistinctConcurrentBag.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Concurrent.ConcurrentBag{T}" />
    public class DistinctConcurrentBag<T> : ConcurrentBag<T>
    {
        /// <summary>
        /// The hash codes
        /// </summary>
        HashSet<int> _hashCodes = new HashSet<int>();

        /// <summary>
        /// The lock
        /// </summary>
        object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctConcurrentBag{T}" /> class.
        /// </summary>
        public DistinctConcurrentBag()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctConcurrentBag{T}" /> class.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentBag`1"></see>.</param>
        public DistinctConcurrentBag(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Adds an object to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1"></see>.
        /// </summary>
        /// <param name="item">The object to be added to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1"></see>. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
        public new void Add(T item)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(item != null, nameof(item));

            var hashCode = item.GetHashCode();

            lock (this._lock)
            {
                if (this._hashCodes.Contains(hashCode) == false)
                {
                    base.Add(item);
                    this._hashCodes.Add(hashCode);
                }
            }
        }

        /// <summary>
        /// Attempts to remove and return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1"></see>.
        /// </summary>
        /// <param name="result">When this method returns, result contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1"></see> or the default value of T if the bag is empty.</param>
        /// <returns>true if an object was removed successfully; otherwise, false.</returns>
        public new bool TryTake(out T result)
        {
            lock (this._lock)
            {
                if (base.TryTake(out result))
                {
                    this._hashCodes.Remove(result.GetHashCode());
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}