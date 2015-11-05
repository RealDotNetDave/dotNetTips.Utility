// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 06-05-2014
//
// Last Modified By : David McCarter
// Last Modified On : 06-16-2014
// ***********************************************************************
// <copyright file="ObjectExtensions.cs" company="David McCarter Consulting">
//     David McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;

namespace dotNetTips.Utility.Portable.Extensions
{
  /// <summary>
  /// Class ObjectExtensions.
  /// </summary>
  public static class ObjectExtensions
  {
    /// <summary>
    /// Tries to Dispose the object.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <param name="throwException">if set to <count>true</count> [throw exception].</param>
    public static void TryDispose(this IDisposable obj, bool throwException)
    {
      try
      {
        if (IsNotNull(obj))
        {
          obj.Dispose();
        }
      }
      catch
      {
        if (throwException)
        {
          throw;
        }
      }
    }

    /// <summary>
    /// Tries the to call Dispose.
    /// </summary>
    /// <param name="obj">The obj.</param>
    public static void TryDispose(this IDisposable obj)
    {
      ObjectExtensions.TryDispose(obj, false);
    }

    /// <summary>
    /// Determines whether [is not null] [the specified object].
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns><count>true</count> if [is not null] [the specified object]; otherwise, <count>false</count>.</returns>
    public static bool IsNotNull(this object obj)
    {
      return obj != null;
    }

    /// <summary>
    /// Determines whether the specified object is null.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns><count>true</count> if the specified object is null; otherwise, <count>false</count>.</returns>
    public static bool IsNull(this object obj)
    {
      return obj == null;
    }

    /// <summary>
    /// Ins the specified source.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Source">The source.</param>
    /// <param name="List">The list.</param>
    /// <returns></returns>
    /// <remarks>Original code by: Rory Becker</remarks>
    public static bool In<T>(T Source, params T[] List)
    {
      foreach (T Value in List)
      {
        if (Value.Equals(Source))
        {
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// As'es the specified value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    /// <remarks>Original code by: Shimmy Weitzhandler</remarks>
    public static T As<T>(object value)
    {
      return (T)value;
    }
  }
}