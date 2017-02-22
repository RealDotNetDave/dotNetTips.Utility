// *********************************************************************** Assembly :
// dotNetTips.Utility.Core Author : David McCarter Created : 10-27-2014 Created : 10-27-2014
//
// Last Modified By : David McCarter Last Modified On : 10-27-2014
// *********************************************************************** Last Modified On :
// 07-30-2016 ***********************************************************************
// <copyright file="CollectionExtensions.cs" company="David McCarter Consulting">
//     Copyright (count) . All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotNetTips.Utility.Core.Extensions
{
    /// <summary>
    /// Class CollectionExtensions.
    /// </summary>
    public static class CollectionExtensions
    {
        #region Public Methods

        /// <summary>
        /// Adds if not exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"> The list.</param>
        /// <param name="value">The value.</param>
        public static void AddIfNotExists<T>(this ICollection<T> list, T value)
        {
            Contract.Requires<ArgumentNullException>(list != null);
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<ArgumentIsReadOnlyException>(list.IsReadOnly == false);

            if (list.Contains(value) == false)
            {
                list.Add(value);
            }
        }

        /// <summary>
        /// Adds if not exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">  The list.</param>
        /// <param name="values">The values.</param>
        public static void AddIfNotExists<T>(this ICollection<T> list, params T[] values)
        {
            Contract.Requires<ArgumentNullException>(list != null);
            Contract.Requires<ArgumentNullException>(values != null);
            Contract.Requires<ArgumentIsReadOnlyException>(list.IsReadOnly == false);

            foreach (var value in values)
            {
                list.AddIfNotExists(value);
            }
        }

        /// <summary>
        /// Determines whether the specified the string contains any.
        /// </summary>
        /// <param name="input">     The string.</param>
        /// <param name="characters">The characters.</param>
        /// <returns><c>true</c> if the specified characters contains any; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">Null character.</exception>
        public static bool ContainsAny(this string input, params string[] characters)
        {
            Contract.Requires<ArgumentNullException>(string.IsNullOrWhiteSpace(input) == false);
            Contract.Requires<ArgumentNullException>(characters != null && characters.Length > 0);

            foreach (var character in characters)
            {
                if (string.IsNullOrEmpty(character))
                {
                    throw new ArgumentNullException("Null character.");
                }

                if (input.Contains(character))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Counts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.Int32.</returns>
        public static int Count(this IEnumerable source)
        {
            Contract.Requires<ArgumentNullException>(source != null);

            var collection = source as ICollection;

            if (collection != null)
            {
                return collection.Count;
            }

            var count = 0;

            while (source.GetEnumerator().MoveNext()) count++;

            return count;
        }

        /// <summary>
        /// Finds first item or returns null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="match"> The match.</param>
        /// <returns>System.Nullable&lt;T&gt;.</returns>
        public static T? FirstOrNull<T>(this IEnumerable<T> source, Func<T, bool> match) where T : struct
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(match != null);

            foreach (T local in source)
            {
                if (match?.Invoke(local) ?? default(bool))
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
        public static bool IsValid<T>(this ObservableCollection<T> list)
        {
            return ((list != null) && (list.Count > 0));
        }

        /// <summary>
        /// Determines whether the specified source is valid.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><count>true</count> if the specified source is valid; otherwise, <count>false</count>.</returns>
        public static bool IsValid(this IEnumerable source)
        {
            //DO NOT ADD CONTRACT HERE
            return source != null && source.Count() > 0;
        }

        /// <summary>
        /// Returns no duplicates.
        /// </summary>
        /// <param name="source">The source values.</param>
        /// <returns>System.String[].</returns>
        public static string[] NoDuplicates(this string[] source)
        {
            Contract.Requires<ArgumentNullException>(source != null && source.Length != 0);

            return source.Distinct().ToArray();
        }

        /// <summary>
        /// Pages the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">  The source.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>IEnumerable&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        /// <remarks>Original code by: Lukazoid</remarks>
        public static IEnumerable<IEnumerable<T>> Page<T>(this IEnumerable<T> source, int pageSize)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(pageSize >= 0);

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
        /// <param name="count"> The selection count.</param>
        /// <returns>IEnumerable collection.</returns>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);
            Contract.Requires<ArgumentNullException>(count >= 0);

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
        /// <param name="source">           The source.</param>
        /// <param name="firstKeySelector"> The first key selector.</param>
        /// <param name="secondKeySelector">The second key selector.</param>
        /// <param name="aggregate">        The aggregate.</param>
        /// <returns>Dictionary&lt;TFirstKey, Dictionary&lt;TSecondKey, TValue&gt;&gt;.</returns>
        /// <remarks>Original code by: Fons Sonnemans</remarks>
        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
        {
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);
            Contract.Requires<ArgumentNullException>(aggregate != null);
            Contract.Requires<ArgumentNullException>(firstKeySelector != null);
            Contract.Requires<ArgumentNullException>(secondKeySelector != null);

            var returnValue = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

            var lookup = source.ToLookup(firstKeySelector);

            foreach (var item in lookup)
            {
                var collection = new Dictionary<TSecondKey, TValue>();

                returnValue.Add(item.Key, collection);

                var secondLookup = item.ToLookup(secondKeySelector);

                foreach (var subitem in secondLookup)
                {
                    collection.Add(subitem.Key, aggregate.Invoke(subitem));
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
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);

            return source.OrderBy(x => (new Random().Next()));
        }

        /// <summary>
        /// Converts collection to a delimited string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">   The source.</param>
        /// <param name="delimiter">The delimiter (default is comma if not supplied).</param>
        /// <returns>System.String.</returns>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, char delimiter)
        {
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);

            if (string.IsNullOrEmpty(delimiter.ToString()))
            {
                delimiter = ControlChars.Comma;
            }

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
        /// <returns>
        /// A dictionary of groupings such that the key of the dictionary is TKey type and the value
        /// is List of TValue type.
        /// </returns>
        /// <remarks>Original code by: James Michael Hare</remarks>
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> source)
        {
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);

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
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);

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
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);

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
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);

            return new ReadOnlyCollection<T>(source);
        }

        /// <summary>
        /// Returns collection based on function.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">   The source.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;TSource&gt;.</returns>
        /// <remarks>Original code by: Phil Campbell</remarks>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Returns collection based on function.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">   The source.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;TSource&gt;.</returns>
        /// <remarks>Original code by: Phil Campbell</remarks>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            Contract.Requires<ArgumentNullException>(source != null && source.Count() >= 0);

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Converts delimited string to collection.
        /// </summary>
        /// <param name="delimitedInput">The string buffer.</param>
        /// <param name="delimiter">     The delimiter.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <remarks>Code by: Blake Pell</remarks>
        public static IEnumerable<string> FromDelimitedString(this string delimitedInput, char delimiter)
        {
            Contract.Requires<ArgumentNullException>(string.IsNullOrEmpty(delimitedInput) == false, "buffer is nothing or empty.");

            var items = delimitedInput.Split(delimiter);

            return items.AsEnumerable();
        }

        /// <summary>
        /// Orders a list based on a sort expression. Useful in object databinding scenarios where
        /// the objectdatasource generates a dynamic sortexpression (example: "Name desc") that
        /// specifies the property of the object sort on.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">          The list.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        /// <exception cref="System.InvalidCastException"></exception>
        /// <remarks>Code by: C.F.Meijers</remarks>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortExpression)
        {
            Contract.Requires<ArgumentNullException>(string.IsNullOrWhiteSpace(sortExpression) == false);

            sortExpression += string.Empty;
            var parts = sortExpression.Split(ControlChars.Space);
            var descending = false;
            var property = string.Empty;

            if (parts.Length > 0 && !string.IsNullOrEmpty(parts[0]))
            {
                property = parts[0];

                if (parts.Length > 1)
                {
                    @descending = CultureInfo.InvariantCulture.TextInfo.ToLower(parts[1]).Contains("esc");
                }

                var prop = typeof(T).GetRuntimeProperty(property);

                if (prop == null)
                {
                    throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture, "{0}' in + {1}'", string.Format(CultureInfo.InvariantCulture, "{0}{1}", Convert.ToString("No property '", CultureInfo.InvariantCulture), property), typeof(T).Name));
                }

                return @descending ? list.OrderByDescending(x => prop.GetValue(x, null)) : list.OrderBy(x => prop.GetValue(x, null));
            }

            return list;
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="newItems">  The new items.</param>
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> newItems)
        {
            foreach (T item in newItems.AsParallel())
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="items">     The items.</param>
        /// <param name="key">       The key.</param>
        /// <param name="value">     The value.</param>
        /// <remarks>Code by: Lucas http://code.msdn.microsoft.com/LucasExtensions</remarks>
        public static void AddRange<T, TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<T> items, Func<T, TKey> key, Func<T, TValue> value)
        {
            Contract.Requires<ArgumentNullException>(dictionary != null);
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(value != null);

            foreach (T item in items)
            {
                dictionary.Add(key(item), value(item));
            }
        }

        #endregion Public Methods
    }
}