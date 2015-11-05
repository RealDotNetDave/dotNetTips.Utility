Imports System.Runtime.CompilerServices

Public Module DateTimeExtensions
    ''' <summary>
    ''' Returns the next occurrence of the day of the week specified.
    ''' </summary>
    ''' <param name="dt">The dt.</param>
    ''' <param name="dayOfWeek">The day of week.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function GetNext(ByVal dt As DateTime, ByVal dayOfWeek As DayOfWeek) As DateTime
        Dim daysToAdd As Integer = 0
        If dt.DayOfWeek < dayOfWeek Then
            daysToAdd = dayOfWeek - dt.DayOfWeek
        Else
            daysToAdd = (7 - dt.DayOfWeek) + dayOfWeek
        End If
        Return dt.AddDays(daysToAdd)
    End Function

    ''' <summary>
    ''' Gets the prior occurrence of the day of the week specified.
    ''' </summary>
    ''' <param name="dt">The dt.</param>
    ''' <param name="dayOfWeek">The day of week.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function GetLast(ByVal dt As DateTime, ByVal dayOfWeek As DayOfWeek) As DateTime
        Dim daysToSubtract As Integer = 0
        If dt.DayOfWeek > dayOfWeek Then
            daysToSubtract = dt.DayOfWeek - dayOfWeek
        Else
            daysToSubtract = (7 - dayOfWeek) + dt.DayOfWeek
        End If
        Return dt.AddDays(daysToSubtract * -1)
    End Function

    ''' <summary>
    ''' To the friendly date string.
    ''' </summary>
    ''' <param name="Date">The date.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension>
    Public Function ToFriendlyDateString([Date] As DateTime) As String
        Dim FormattedDate As String = ""
        If [Date].[Date] = DateTime.Today Then
            FormattedDate = "Today"
        ElseIf [Date].[Date] = DateTime.Today.AddDays(-1) Then
            FormattedDate = "Yesterday"
        ElseIf [Date].[Date] > DateTime.Today.AddDays(-6) Then
            ' *** Show the Day of the week
            FormattedDate = [Date].ToString("dddd").ToString()
        Else
            FormattedDate = [Date].ToString("MMMM dd, yyyy")
        End If

        'append the time portion to the output
        FormattedDate += " @ " + [Date].ToString("t").ToLower()
        Return FormattedDate
    End Function
End Module