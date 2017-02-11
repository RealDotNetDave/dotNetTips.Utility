' ***********************************************************************
' Assembly         : dotNetTips.Utility.Windows.Logger
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-05-2016
' ***********************************************************************
' <copyright file="InfoItemCollection.vb" company="McCarter Consulting">
'     '     '     Copyright © dotNetTips.com 2016
'
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Diagnostics.Contracts

''' <summary>
''' Class InfoItemCollection. This class cannot be inherited.
''' </summary>
''' <seealso cref="System.Collections.Generic.List(Of dotNetTips.Utility.Windows.Logger.InfoItem)" />
<Serializable>
Public NotInheritable Class InfoItemCollection
    Inherits List(Of InfoItem)

    ''' <summary>
    ''' Adds the item to the <see cref="T:System.Collections.Concurrent.BlockingCollection`1" />.
    ''' </summary>
    ''' <param name="item">The item to be added to the collection. The value can be a null reference.</param>
    Public Shadows Sub Add(item As InfoItem)
        Contract.Requires(Of ArgumentNullException)(item IsNot Nothing)

        If String.IsNullOrWhiteSpace(item.[Property]) OrElse String.IsNullOrWhiteSpace(item.Text) Then
            Exit Sub
        End If

        'Check for dup item
        If MyBase.Where(Function(p) p.[Property] = item.[Property]).Count() = 0 Then
            MyBase.Add(item)
        End If
    End Sub

    ''' <summary>
    ''' Adds the specified item property.
    ''' </summary>
    ''' <param name="itemProperty">The item property.</param>
    ''' <param name="itemText">The item text.</param>
    Public Shadows Sub Add(itemProperty As String, itemText As String)
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(itemProperty) = False)
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(itemText))

        Add(New InfoItem(itemProperty, itemText))
    End Sub

    ''' <summary>
    ''' Adds the range.
    ''' </summary>
    ''' <param name="collection">The collection.</param>
    Public Shadows Sub AddRange(collection As IEnumerable(Of InfoItem))
        Contract.Requires(Of ArgumentNullException)(collection IsNot Nothing)

        For Each adi In collection
            Me.Add(adi)
        Next

    End Sub

    ''' <summary>
    ''' Adds the range.
    ''' </summary>
    ''' <param name="collection">The collection.</param>
    Public Shadows Sub AddRange(collection As IDictionary(Of String, String))
        Contract.Requires(Of ArgumentNullException)(collection IsNot Nothing)

        For Each adi In collection
            Me.Add(adi.Key, adi.Value)
        Next
    End Sub

    ''' <summary>
    ''' Selects the by property.
    ''' </summary>
    ''' <param name="property">The property.</param>
    ''' <returns>InfoItem.</returns>
    Public Function SelectByProperty([property] As String) As InfoItem
        Return Me.FirstOrDefault(Function(p) p.[Property] = [property])
    End Function
End Class