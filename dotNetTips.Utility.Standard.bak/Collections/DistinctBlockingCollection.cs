using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace dotNetTips.Utility.Standard.Collections
{
    /// <summary>
    /// Class DistinctBlockingCollection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Concurrent.BlockingCollection{T}"/>
    public class DistinctBlockingCollection<T> : BlockingCollection<T>, ICloneable<T>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctBlockingCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public DistinctBlockingCollection(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctBlockingCollection{T}"/> class.
        /// </summary>
        public DistinctBlockingCollection()
        { }

        /// <summary>
        /// Adds the item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The item to be added to the collection. The value can be a null reference.
        /// </param>
        public new void Add(T item)
        {
            if ((ItemNotInCollection(item)))
            {
                base.Add(item);
            }
        }

        /// <summary>
        /// Adds the item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1"/>.
        /// </summary>
        /// <param name="item">             
        /// The item to be added to the collection. The value can be a null reference.
        /// </param>
        /// <param name="cancellationToken">A cancellation token to observe.</param>
        public new void Add(T item, CancellationToken cancellationToken)
        {
            if ((ItemNotInCollection(item)))
            {
                base.Add(item, cancellationToken);
            }
        }

        /// <summary>
        /// Cones this instance.
        /// </summary>
        /// <returns>T.</returns>
        public T Cone()
        {
            return (T)this.MemberwiseClone();
        }

        /// <summary>
        /// Tries to add the specified item to the <see
        /// cref="T:System.Collections.Concurrent.BlockingCollection`1"/> within the specified time period.
        /// </summary>
        /// <param name="item">               The item to be added to the collection.</param>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite"/>
        /// (-1) to wait indefinitely.
        /// </param>
        /// <returns>
        /// true if the <paramref name="item"/> could be added to the collection within the specified
        /// time; otherwise, false. If the item is a duplicate, and the underlying collection does
        /// not accept duplicate items, then an <see cref="T:System.InvalidOperationException"/> is thrown.
        /// </returns>
        public new bool TryAdd(T item, int millisecondsTimeout)
        {
            return (ItemNotInCollection(item)) ? base.TryAdd(item, millisecondsTimeout) : false;
        }

        /// <summary>
        /// Tries to add the specified item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1"/>.
        /// </summary>
        /// <param name="item">The item to be added to the collection.</param>
        /// <returns>
        /// true if <paramref name="item"/> could be added; otherwise false. If the item is a
        /// duplicate, and the underlying collection does not accept duplicate items, then an <see
        /// cref="T:System.InvalidOperationException"/> is thrown.
        /// </returns>
        public new bool TryAdd(T item)
        {
            return (ItemNotInCollection(item)) ? base.TryAdd(item) : false;
        }

        /// <summary>
        /// Tries to add the specified item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1"/>.
        /// </summary>
        /// <param name="item">   The item to be added to the collection.</param>
        /// <param name="timeout">
        /// A <see cref="T:System.TimeSpan"/> that represents the number of milliseconds to wait, or
        /// a <see cref="T:System.TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
        /// </param>
        /// <returns>
        /// true if the <paramref name="item"/> could be added to the collection within the specified
        /// time span; otherwise, false.
        /// </returns>
        public new bool TryAdd(T item, TimeSpan timeout)
        {
            return (ItemNotInCollection(item)) ? base.TryAdd(item, timeout) : false;
        }

        /// <summary>
        /// Tries to add the specified item to the <see
        /// cref="T:System.Collections.Concurrent.BlockingCollection`1"/> within the specified time
        /// period, while observing a cancellation token.
        /// </summary>
        /// <param name="item">               The item to be added to the collection.</param>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite"/>
        /// (-1) to wait indefinitely.
        /// </param>
        /// <param name="cancellationToken">  A cancellation token to observe.</param>
        /// <returns>
        /// true if the <paramref name="item"/> could be added to the collection within the specified
        /// time; otherwise, false. If the item is a duplicate, and the underlying collection does
        /// not accept duplicate items, then an <see cref="T:System.InvalidOperationException"/> is thrown.
        /// </returns>
        public new bool TryAdd(T item, int millisecondsTimeout, CancellationToken cancellationToken)
        {
            return (ItemNotInCollection(item)) ? base.TryAdd(item, millisecondsTimeout, cancellationToken) : false;
        }

        /// <summary>
        /// Items the not in collection.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool ItemNotInCollection(T item)
        {
            return (item == null && this.Contains(item) == false);
        }

        /// <summary>
        /// Removes all.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns>System.Int32.</returns>
        public int RemoveAll(Predicate<T> match)
        {
            var itemsToRemove = new List<T>();

            foreach (var item in base.ToArray())
            {
                if (match?.Invoke(item) ?? default(bool))
                {
                    itemsToRemove.Add(item);
                }
            }

            //foreach (var item in itemsToRemove.AsParallel())
            //{   //TODO: FIX TRYTAKE
            //    //base.TryTake(item);
            //}

            return 0;// itemsToRemove.Count;
        }
    }
}