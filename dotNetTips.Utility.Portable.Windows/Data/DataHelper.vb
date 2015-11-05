' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 02-01-2014
' ***********************************************************************
' <copyright file="DataHelper.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Globalization

Namespace Data

    ''' <summary>
    ''' Common data helper methods.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module DataHelper

        ''' <summary>
        ''' Converts a date to a SQL Server int value.
        ''' </summary>
        ''' <param name="value">Date to convert.</param>
        ''' <returns>Date as <see cref="int32">integer</see>.</returns>
        ''' <remarks></remarks>
        Public Function ConvertDateToSqlInt32(ByVal value As Date) As Int32
            Return Int32.Parse(value.Year.ToString(CultureInfo.InvariantCulture) + value.Month.ToString("00", CultureInfo.InvariantCulture) + value.Day.ToString("00", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)
        End Function

        ''' <summary>
        ''' Converts time to a SQL Server in value.
        ''' </summary>
        ''' <param name="value">Time to convert.</param>
        ''' <returns>Time as <see cref="Int32">integer</see>.</returns>
        ''' <remarks></remarks>
        Public Function ConvertTimeToSqlInt32(ByVal value As Date) As Int32
            Return Int32.Parse(value.Hour.ToString(CultureInfo.InvariantCulture) + value.Minute.ToString("00", CultureInfo.InvariantCulture) + value.Second.ToString("00", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)
        End Function

        ''' <summary>
        ''' Converts a SQL Server int date and time to a DateTime value.
        ''' </summary>
        ''' <param name="sqlDate">SQL Server date.</param>
        ''' <param name="sqlTime">SQL Server time.</param>
        ''' <returns><see cref="DateTime">Date and Time</see>.</returns>
        ''' <remarks></remarks>
        Public Function ConvertSqlDateTime(ByVal sqlDate As Int32, ByVal sqlTime As Int32) As DateTime
            Dim tempDate As String = sqlDate.ToString(CultureInfo.InvariantCulture)
            Dim tempTime As String = sqlTime.ToString("000000", CultureInfo.InvariantCulture)

            Return New Date(Int32.Parse(tempDate.Substring(0, 4), CultureInfo.InvariantCulture), Int32.Parse(tempDate.Substring(4, 2), CultureInfo.InvariantCulture), Int32.Parse(tempDate.Substring(6, 2), CultureInfo.InvariantCulture), Int32.Parse(tempTime.Substring(0, 2), CultureInfo.InvariantCulture), Int32.Parse(tempTime.Substring(2, 2), CultureInfo.InvariantCulture), Int32.Parse(tempTime.Substring(4, 2), CultureInfo.InvariantCulture))

        End Function

    End Module
End Namespace