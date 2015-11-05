// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 10-27-2014
//
// Last Modified By : David McCarter
// Last Modified On : 10-27-2014
// ***********************************************************************
// <copyright file="CollectionExtensions.cs" company="David McCarter Consulting">
//     Copyright (count) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using dotNetTips.Utility.Portable.Extensions;

namespace dotNetTips.Utility.Portable.Extensions
{
  /// <summary>
  /// Class CollectionExtensions.
  /// </summary>
  public static class CollectionExtensions
  {
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

      int count = 0;

      while (source.GetEnumerator().MoveNext()) count++;

      return count;
    }

    /// <summary>
    /// Converts collection to a delimited string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">The source.</param>
    /// <param name="delimiter">The delimiter (default is comma if not supplied).</param>
    /// <returns>System.String.</returns>
    public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter)
    {
      Contract.Requires<ArgumentNullException>(source != null);

      if (string.IsNullOrEmpty(delimiter))
      {
        delimiter = ControlChars.Comma.ToString();
      }

      var sb = new System.Text.StringBuilder();

      foreach (var item in source)
      {
        sb.Append(sb.Length > 0 ? delimiter : item.ToString());
      }

      return sb.ToString();
    }

    /// <summary>
    /// Determines whether the specified the string contains any.
    /// </summary>
    /// <param name="theString">The string.</param>
    /// <param name="characters">The characters.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool ContainsAny(this string theString, params string[] characters)
    {
      Contract.Requires<ArgumentNullException>(characters != null);

      foreach (var character in characters)
      {
        if (theString.Contains(character.ToString()))
        {
          return true;
        }
      }
      return false;
    }

    /// <summary>
    ///Groups the elements of a sequence according to a specified firstKey selector function and rotates the unique values from the secondKey selector function into multiple values in the output, and performs aggregations.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TFirstKey">The type of the first key.</typeparam>
    /// <typeparam name="TSecondKey">The type of the second key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="firstKeySelector">The first key selector.</param>
    /// <param name="secondKeySelector">The second key selector.</param>
    /// <param name="aggregate">The aggregate.</param>
    /// <returns></returns>
    /// <remarks>Code by: Fons Sonnemans</remarks>
    public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
    {
      var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

      var l = source.ToLookup(firstKeySelector);
      foreach (var item in l)
      {
        var dict = new Dictionary<TSecondKey, TValue>();
        retVal.Add(item.Key, dict);
        var subdict = item.ToLookup(secondKeySelector);
        foreach (var subitem in subdict)
        {
          dict.Add(subitem.Key, aggregate(subitem));
        }
      }

      return retVal;
    }

    /// <summary>
    /// Async creates a Geneirc.List.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">The list.</param>
    /// <returns></returns>
    /// <remarks>Code by: Fons Sonnemans</remarks>
    public static Task<List<T>> ToListAsync<T>(this IQueryable<T> list)
    {
      return Task.Run(() => list.ToList());
    }

    /// <summary>
    /// To the observable collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="coll">The coll.</param>
    /// <returns></returns>
    /// <remarks>Code by: Fons Sonnemans</remarks>
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
    {
      var c = new ObservableCollection<T>();
      foreach (var e in coll)
        c.Add(e);
      return c;
    }

    /// <summary>
    /// Randomizes the specified target.
    /// </summary>
    /// <typeparam name="t"></typeparam>
    /// <param name="target">The target.</param>
    /// <returns></returns>
    /// <remarks>Code by: Phil Campbell</remarks>
    public static IEnumerable<t> Randomize<t>(this IEnumerable<t> target)
    {
      return target.OrderBy(x => (new Random().Next()));
    }

    /// <summary>
    /// Converts an enumeration of groupings into a Dictionary of those groupings.
    /// </summary>
    /// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
    /// <typeparam name="TValue">Element type of the grouping and dictionary list.</typeparam>
    /// <param name="groupings">The enumeration of groupings from a GroupBy() clause.</param>
    /// <returns>A dictionary of groupings such that the key of the dictionary is TKey type and the value is List of TValue type.</returns>
    /// <remarks>Code by: James Michael Hare</remarks>
    public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
    {
      return groupings.ToDictionary(group => group.Key, group => group.ToList());
    }

    /// <summary>
    /// Distincts the specified this.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="this">The this.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    /// <remarks>Code by:  Martin Rosén-Lidholm</remarks>
    public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> @this, Func<T, TKey> keySelector)
    {
      return @this.GroupBy(keySelector).Select(grps => grps).Select(e => e.First());
    }

    /// <summary>
    /// Wheres if.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="condition">if set to <c>true</c> [condition].</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    /// <remarks>Code by: Phil Campbell</remarks>
    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
    {
      if (condition)
        return source.Where(predicate);
      else
        return source;
    }

    /// <summary>
    /// Wheres if.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="condition">if set to <c>true</c> [condition].</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    /// <remarks>Code by: Phil Campbell</remarks>
    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
    {
      return condition ? source.Where(predicate) : source;
    }
  }
}