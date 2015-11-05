'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 06-09-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System
Imports System.Collections.Generic

Namespace Extensions
    ''' <summary>
    ''' Extensions for enums.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module EnumerableExtensions

        ''' <summary>
        ''' Loops over the enum.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="values">The values.</param>
        ''' <param name="action">The action.</param>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Sub ForEach(Of T)(ByVal values As IEnumerable(Of T), ByVal action As Action(Of T))
            Contract.Requires(Of ArgumentNullException)(values IsNot Nothing)
            Contract.Requires(Of ArgumentNullException)(action IsNot Nothing)

            For Each value As T In values
                action(value)
            Next
        End Sub

        ''' <summary>
        ''' Adds the range.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="collection">The collection.</param>
        ''' <param name="newItems">The new items.</param>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>dm11086
        <System.Runtime.CompilerServices.Extension()> _
        Public Sub AddRange(Of T)(ByVal collection As ICollection(Of T), ByVal newItems As IEnumerable(Of T))
            For Each item As T In newItems
                collection.Add(item)
            Next
        End Sub

        ''' <summary>
        ''' Adds the range.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <typeparam name="TKey">The type of the key.</typeparam>
        ''' <typeparam name="TValue">The type of the value.</typeparam>
        ''' <param name="dictionary">The dictionary.</param>
        ''' <param name="key">The key.</param>
        ''' <param name="value">The value.</param>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Sub AddRange(Of T, TKey, TValue)(ByVal dictionary As IDictionary(Of TKey, TValue), ByVal items As IEnumerable(Of T), ByVal key As Func(Of T, TKey), ByVal value As Func(Of T, TValue))
            For Each item As T In items
                dictionary.Add(key(item), value(item))
            Next
        End Sub

    End Module
End Namespace