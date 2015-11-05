using System;

namespace dotNetTips.Utility.Portable
{
  /// <summary>
  /// 	Extensions for DateTime
  /// </summary>
  public static class DateTimeExtensions
  {
    /// <summary>
    /// Intersectses the specified start date.
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <param name="intersectingStartDate">The intersecting start date.</param>
    /// <param name="intersectingEndDate">The intersecting end date.</param>
    /// <returns></returns>
    /// <remarks>Code by: Walter Quesada</remarks>
    public static bool Intersects(this DateTime startDate, DateTime endDate, DateTime intersectingStartDate, DateTime intersectingEndDate)
    {
      return (intersectingEndDate >= startDate && intersectingStartDate <= endDate);
    }
  }
}