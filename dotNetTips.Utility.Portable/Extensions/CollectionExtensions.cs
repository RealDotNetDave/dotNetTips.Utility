// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 02-28-2017
//
// Last Modified By : David McCarter
// Last Modified On : 05-11-2017
// ***********************************************************************
// <copyright file="CollectionExtensions.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using dotNetTips.Utility.Portable.OOP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetTips.Utility.Portable.Extensions
{
    /// <summary>
    /// Class CollectionExtensions.
    /// </summary>
    [Obsolete("Use CollectionExtensions from dotNetTips.Utility.Standard.")]
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds if not exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The list.</param>
        /// <param name="value">The value.</param>
        public static void AddIfNotExists<T>(this ICollection<T> source, T value)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(value != null);
            Encapsulation.TryValidateParam<ArgumentReadOnlyException>(source.IsReadOnly == false);

            if (source.Contains(value) == false)
            {
                source.Add(value);
            }
        }

        /// <summary>
        /// Adds if not exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The list.</param>
        /// <param name="values">The values.</param>
        public static void AddIfNotExists<T>(this ICollection<T> source, params T[] values)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(values != null);
            Encapsulation.TryValidateParam<ArgumentReadOnlyException>(source.IsReadOnly == false);

            foreach (var value in values)
            {
                source.AddIfNotExists(value);
            }
        }

        /// <summary>
        /// Makes copy of the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>System.Collections.Generic.List&lt;T&gt;.</returns>
        public static List<T> CopyToList<T>(this List<T> source)
        {

            return new List<T>(source);
        }

        /// <summary>
        /// Counts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.Int32.</returns>
        public static int Count(this IEnumerable source)
        {

            var collection = source as ICollection;

            if (collection != null)
            {
                return collection.Count;
            }

            var count = 0;

            while (source.GetEnumerator().MoveNext())
            {
                count++;
            }

            return count;
        }

        /// <summary>
        /// Finds first item or returns null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="match">The match.</param>
        /// <returns>System.Nullable&lt;T&gt;.</returns>
        public static T? FirstOrNull<T>(this IEnumerable<T> source, Func<T, bool> match)
            where T : struct
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(match != null);

            foreach (var local in source)
            {
                if (match.Invoke(local) && default(bool))
                {
                    return new T?(local);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns><c>true</c> if the specified list is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValid<T>(this ObservableCollection<T> list) => (list != null) && (list.Count > 0);

        /// <summary>
        /// Determines whether the specified source is valid.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><count>true</count> if the specified source is valid; otherwise, <count>false</count>.</returns>
        public static bool IsValid(this IEnumerable source) => source != null && source.Count() > 0;

        /// <summary>
        /// Returns no duplicates.
        /// </summary>
        /// <param name="source">The source values.</param>
        /// <returns>System.String[].</returns>
        public static string[] NoDuplicates(this string[] source)
        {

            return source.Distinct().ToArray();
        }

        /// <summary>
        /// Pages the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>IEnumerable&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        /// <remarks>Original code by: Lukazoid</remarks>
        public static IEnumerable<IEnumerable<T>> Page<T>(this IEnumerable<T> source, int pageSize)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(pageSize >= 0);

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var currentPage = new List<T>(pageSize)
                    {
                    enumerator.Current
                    };

                    while (currentPage.Count < pageSize && enumerator.MoveNext())
                    {
                        currentPage.Add(enumerator.Current);
                    }

                    yield return currentPage.AsEnumerable();
                }
            }
        }

        /// <summary>
        /// Picks random items out of the collection.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="count">The selection count.</param>
        /// <returns>IEnumerable collection.</returns>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(count >= 0);

            return source.Randomize().Take(count);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified firstKey selector function and
        /// rotates the unique values from the secondKey selector function into multiple values in
        /// the output, and performs aggregations.
        /// </summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <typeparam name="TFirstKey">The type of the t first key.</typeparam>
        /// <typeparam name="TSecondKey">The type of the t second key.</typeparam>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="firstKeySelector">The first key selector.</param>
        /// <param name="secondKeySelector">The second key selector.</param>
        /// <param name="aggregate">The aggregate.</param>
        /// <returns>Dictionary&lt;TFirstKey, Dictionary&lt;TSecondKey, TValue&gt;&gt;.</returns>
        /// <remarks>Original code by: Fons Sonnemans</remarks>
        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(aggregate != null);
            Encapsulation.TryValidateParam<ArgumentNullException>(firstKeySelector != null);
            Encapsulation.TryValidateParam<ArgumentNullException>(secondKeySelector != null);

            var returnValue = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

            var lookup = source.ToLookup(firstKeySelector);

            foreach (var item in lookup)
            {
                var collection = new Dictionary<TSecondKey, TValue>();

                returnValue.Add(item.Key, collection);

                var secondLookup = item.ToLookup(secondKeySelector);

                foreach (var subitem in secondLookup)
                {
                    collection.Add(subitem.Key, aggregate(subitem));
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Randomizes the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        /// <remarks>Original code by: Phil Campbell</remarks>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => new Random().Next());
        }

        /// <summary>
        /// Converts collection to a delimited string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="delimiter">The delimiter (default is comma if not supplied).</param>
        /// <returns>System.String.</returns>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, char delimiter = ControlChars.Comma)
        {
            var sb = new StringBuilder();

            foreach (var item in source)
            {
                if (sb.Length > 0)
                {
                    sb.Append(delimiter.ToString());
                }

                sb.Append(item.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts an enumeration of groupings into a Dictionary of those groupings.
        /// </summary>
        /// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
        /// <typeparam name="TValue">Element type of the grouping and dictionary list.</typeparam>
        /// <param name="source">The enumeration of groupings from a GroupBy() clause.</param>
        /// <returns>A dictionary of groupings such that the key of the dictionary is TKey type and the value
        /// is List of TValue type.</returns>
        /// <remarks>Original code by: James Michael Hare</remarks>
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> source)
        {

            return source.ToDictionary(group => group.Key, group => group.ToList());
        }

        /// <summary>
        /// Creates a Generic.List.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The list.</param>
        /// <returns>Task&lt;List&lt;T&gt;&gt;.</returns>
        /// <remarks>Original code by: Fons Sonnemans</remarks>
        public static Task<List<T>> ToListAsync<T>(this IEnumerable<T> source)
        {

            return Task.Run(() => source.ToList());
        }

        /// <summary>
        /// To the observable collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>ObservableCollection&lt;T&gt;.</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {

            return new ObservableCollection<T>(source);
        }

        /// <summary>
        /// Creates a read only collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>ReadOnlyCollection&lt;T&gt;.</returns>
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IList<T> source)
        {

            return new ReadOnlyCollection<T>(source);
        }

        /// <summary>
        /// Returns collection based on function.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;TSource&gt;.</returns>
        /// <remarks>Original code by: Phil Campbell</remarks>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Returns collection based on function.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;TSource&gt;.</returns>
        /// <remarks>Original code by: Phil Campbell</remarks>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        { 
            return condition ? source.Where(predicate) : source;
        }
    }
}