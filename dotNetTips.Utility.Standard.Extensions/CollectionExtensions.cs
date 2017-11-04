// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 09-15-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="CollectionExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotNetTips.Utility.Standard.Extensions
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
        /// <param name="list">The list.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException">list - List cannot be null.
        /// or
        /// value - Value cannot be null.</exception>
        /// <exception cref="ArgumentException">list - List cannot be read-only.</exception>
        public static void AddIfNotExists<T>(this ICollection<T> list, T value)
        {
            if (list.Contains(value))
            {
                return;
            }
            else
            {
                list.Add(value);
            }
        }

        /// <summary>
        /// Copies to list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public static List<T> CopyToList<T>(this List<T> source) => new List<T>(source);

        /// <summary>
        /// Adds if not exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="values">The values.</param>
        /// <exception cref="ArgumentNullException">list - List cannot be null.
        /// or
        /// values - Values cannot be null.</exception>
        /// <exception cref="ArgumentException">list - List cannot be read-only.</exception>
        public static void AddIfNotExists<T>(this ICollection<T> list, params T[] values)
        {
            foreach (var value in values)
            {
                list.AddIfNotExists(value);
            }
        }

        /// <summary>
        /// Determines whether the specified the string contains any.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <param name="characters">The characters.</param>
        /// <returns><c>true</c> if the specified characters contains any; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">input - List cannot be null.
        /// or
        /// characters - Characters cannot be null or 0 length.
        /// or
        /// Null character.</exception>
        /// <exception cref="System.ArgumentNullException">Null character.</exception>
        public static bool ContainsAny(this string input, params string[] characters)
        {
            foreach (var character in characters)
            {
                if (input.Contains(character))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Counts the specified list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null.</exception>
        public static int Count(this IEnumerable list)
        {

            if (list is ICollection collection)
            {
                return collection.Count;
            }

            var count = 0;

            while (list.GetEnumerator().MoveNext())
            {
                count++;
            }

            return count;
        }

        /// <summary>
        /// Finds first item or returns null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="match">The match.</param>
        /// <returns>System.Nullable&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null.
        /// or
        /// match - Match cannot be null.</exception>
        public static T? FirstOrNull<T>(this IEnumerable<T> list, Func<T, bool> match) where T : struct
        {
            foreach (var local in list)
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
        public static bool IsValid<T>(this ObservableCollection<T> list) => ((list != null) && (list.Count > 0));

        /// <summary>
        /// Determines whether the specified list is valid.
        /// </summary>
        /// <param name="source">The list.</param>
        /// <returns><count>true</count> if the specified list is valid; otherwise, <count>false</count>.</returns>
        public static bool IsValid(this IEnumerable source) =>
            source != null && source.Count() > 0;

        /// <summary>
        /// Returns no duplicates.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>System.String[].</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 length.</exception>
        public static string[] NoDuplicates(this string[] list) => list.Distinct().ToArray();

        /// <summary>
        /// Pages the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>IEnumerable&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null.
        /// or
        /// list - Page size cannot be 0 length.</exception>
        public static IEnumerable<IEnumerable<T>> Page<T>(this IEnumerable<T> list, int pageSize)
        {
            using (var enumerator = list.GetEnumerator())
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
        /// Picks random items out of the list.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="count">The selection count.</param>
        /// <returns>IEnumerable list.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.
        /// or
        /// list - Count cannot be 0.</exception>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> list, int count)
        {
            return list.Randomize().Take(count);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified firstKey selector function and
        /// rotates the unique values from the secondKey selector function into multiple values in
        /// the output, and performs aggregations.
        /// </summary>
        /// <typeparam name="TSource">The type of the t list.</typeparam>
        /// <typeparam name="TFirstKey">The type of the t first key.</typeparam>
        /// <typeparam name="TSecondKey">The type of the t second key.</typeparam>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="firstKeySelector">The first key selector.</param>
        /// <param name="secondKeySelector">The second key selector.</param>
        /// <param name="aggregate">The aggregate.</param>
        /// <returns>Dictionary&lt;TFirstKey, Dictionary&lt;TSecondKey, TValue&gt;&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.
        /// or
        /// list - Aggregate cannot be null.
        /// or
        /// firstKeySelector - First key selector cannot be null.
        /// or
        /// secondKeySelector - Second key selector cannot be null.</exception>
        /// <remarks>Original code by: Fons Sonnemans</remarks>
        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> list, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
        {
            var returnValue = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

            var lookup = list.ToLookup(firstKeySelector);

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
        /// Randomizes the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.</exception>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> list) => list.OrderBy(x => (new Random().Next()));

        /// <summary>
        /// Converts list to a delimited string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="delimiter">The delimiter (default is comma if not supplied).</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.</exception>
        public static string ToDelimitedString<T>(this IEnumerable<T> list, char delimiter)
        {

            var sb = new StringBuilder();

            foreach (var item in list)
            {
                if (sb.Length > 0)
                {
                    sb.Append(delimiter.ToString(CultureInfo.CurrentCulture));
                }
                sb.Append(item.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts an enumeration of groupings into a Dictionary of those groupings.
        /// </summary>
        /// <typeparam name="TKey">Key type of the grouping and list.</typeparam>
        /// <typeparam name="TValue">Element type of the grouping and list list.</typeparam>
        /// <param name="list">The list.</param>
        /// <returns>A list of groupings such that the key of the list is TKey type and the value
        /// is List of TValue type.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.</exception>
        /// <remarks>Original code by: James Michael Hare</remarks>
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> list) => list.ToDictionary(group => group.Key, group => group.ToList());

        /// <summary>
        /// Creates a Generic.List.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>Task&lt;List&lt;T&gt;&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.</exception>
        /// <remarks>Original code by: Fons Sonnemans</remarks>
        public static Task<List<T>> ToListAsync<T>(this IEnumerable<T> list) => Task.Run(() => list.ToList());

        /// <summary>
        /// To the observable list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>ObservableCollection&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.</exception>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list) => new ObservableCollection<T>(list);

        /// <summary>
        /// Creates a read only list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>ReadOnlyCollection&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.</exception>
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IList<T> list) => new ReadOnlyCollection<T>(list);

        /// <summary>
        /// Returns list based on function.
        /// </summary>
        /// <typeparam name="TSource">The type of the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;TSource&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.</exception>
        /// <remarks>Original code by: Phil Campbell</remarks>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> list, bool condition, Func<TSource, bool> predicate)
        {
            return condition ? list.Where(predicate) : list;
        }

        /// <summary>
        /// Returns list based on function.
        /// </summary>
        /// <typeparam name="TSource">The type of the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>IEnumerable&lt;TSource&gt;.</returns>
        /// <exception cref="ArgumentNullException">list - Source cannot be null or have a 0 value.</exception>
        /// <remarks>Original code by: Phil Campbell</remarks>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> list, bool condition, Func<TSource, int, bool> predicate)
        {
            return condition ? list.Where(predicate) : list;
        }

        /// <summary>
        /// Converts delimited string to list.
        /// </summary>
        /// <param name="delimitedInput">The string buffer.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        /// <remarks>Code by: Blake Pell</remarks>
        public static IEnumerable<string> FromDelimitedString(this string delimitedInput, char delimiter)
        {
            var items = delimitedInput.Split(delimiter);

            return items.AsEnumerable();
        }

        /// <summary>
        /// Orders a list based on a sort expression. Useful in object data binding scenarios where
        /// the ObjectDataSource generates a dynamic sort expression (example: "Name desc") that
        /// specifies the property of the object sort on.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="System.InvalidCastException">sortExpression - Sort expression cannot be null.</exception>
        /// <remarks>Code by: C.F.Meijers</remarks>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortExpression)
        {
            sortExpression += string.Empty;
            var parts = sortExpression.Split(Convert.ToChar(" ", CultureInfo.InvariantCulture));
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

                if (prop is null)
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
        /// <param name="list">The list.</param>
        /// <param name="newItems">The new items.</param>
        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> newItems)
        {
            Parallel.ForEach(newItems, (item) =>
             {
                 list.Add(item);
             });
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="items">The items.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentNullException">list - Dictionary cannot be null.
        /// or
        /// key - Key cannot be null.</exception>
        /// <exception cref="ArgumentException">value - Value cannot be null.</exception>
        /// <remarks>Code by: Lucas</remarks>
        public static void AddRange<T, TKey, TValue>(this IDictionary<TKey, TValue> list, IEnumerable<T> items, Func<T, TKey> key, Func<T, TValue> value)
        {
            foreach (var item in items)
            {
                list.Add(key(item), value(item));
            }
        }
        #endregion Public Methods
    }
}