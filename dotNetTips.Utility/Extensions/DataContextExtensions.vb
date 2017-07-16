' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-03-2015
'
' Last Modified By : David McCarter
' Last Modified On : 03-06-2017
' ***********************************************************************
' <copyright file="DataContextExtensions.vb" company="dotNetTips.com">
'     '     dotNetTips.com. All rights reserved.
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.Generic
Imports System.Data.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices

''' <summary>
''' Class DataContextExtensions.
''' </summary>
Public Module DataContextExtensions
    ''' <summary>
    ''' Gets the tracked objects.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="context">The dc.</param>
    ''' <returns>IList(Of Tuple(Of T, T)).</returns>
    <Extension>
    Public Function GetTrackedObjects(Of T)(ByVal context As DataContext) As IList(Of Tuple(Of T, T))
        Dim result As New List(Of Tuple(Of T, T))
        Dim dcType = GetType(DataContext)

        ' Use reflection to get to the underlying items being tracked in the DataContext
        Const bindings = BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.GetField

        Dim services = dcType.GetField("services", bindings).GetValue(context)

        Dim tracker = services.GetType().GetField("tracker", bindings).GetValue(services)

        Dim trackerItems = DirectCast(tracker.GetType().GetField("items", bindings).GetValue(tracker), IDictionary)

        ' iterate through each update in context, adding only those that are of type T to the method's result variable
        For Each entry As DictionaryEntry In trackerItems.AsParallel()
            Dim original As Object = entry.Value.GetType().GetField(NameOf(original), bindings).GetValue(entry.Value)
            If TypeOf entry.Key Is T AndAlso TypeOf original Is T Then
                result.Add(New Tuple(Of T, T)(DirectCast(original, T), DirectCast(entry.Key, T)))
            End If
        Next

        Return result
    End Function
End Module