Imports System.Collections.Generic
Imports System.Data.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices

Public Module DataContextExtensions
  ''' <summary>
  ''' Gets the tracked objects.
  ''' </summary>
  ''' <typeparam name="T"></typeparam>
  ''' <param name="dc">The dc.</param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  <Extension()>
  Public Function GetTrackedObjects(Of T)(ByVal dc As DataContext) As IList(Of Tuple(Of T, T))
    Dim result As New List(Of Tuple(Of T, T))
    Dim dcType = GetType(DataContext)

    ' Use reflection to get to the underlying items being tracked in the DataContext
    Dim bindings = BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.GetField
    Dim services = dcType.GetField("services", bindings).GetValue(dc)
    Dim tracker = services.GetType().GetField("tracker", bindings).GetValue(services)
    Dim trackerItems = DirectCast(tracker.GetType().GetField("items", bindings).GetValue(tracker), IDictionary)

    ' iterate through each update in context, adding only those that are of type T to the method's result variable
    For Each entry As DictionaryEntry In trackerItems
      Dim original As Object = entry.Value.GetType().GetField("original", bindings).GetValue(entry.Value)
      If TypeOf entry.Key Is T AndAlso TypeOf original Is T Then
        result.Add(New Tuple(Of T, T)(DirectCast(original, T), DirectCast(entry.Key, T)))
      End If
    Next

    Return result
  End Function
End Module