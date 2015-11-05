' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 02-03-2014
'
' Last Modified By : David McCarter
' Last Modified On : 02-03-2014
' ***********************************************************************
' <copyright file="SortedQueue.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************

Namespace Collections
    ''' <summary>
    ''' Sorted Queue
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Class SortedQueue(Of T)
        Implements IEnumerable(Of T)
        ''' <summary>
        ''' Gets or sets the sorter.
        ''' </summary>
        ''' <returns></returns>
        ''' <value>The sorter.</value>
        ''' <remarks></remarks>
        Public Property Sorter() As IComparer
            Get
                Return m_Sorter
            End Get
            Set(value As IComparer)
                m_Sorter = value
            End Set
        End Property
        Private m_Sorter As IComparer

        Private _list As New List(Of T)()

        ''' <summary>
        ''' Gets the count.
        ''' </summary>
        ''' <returns></returns>
        ''' <value>The count.</value>
        ''' <remarks></remarks>
        Public ReadOnly Property Count() As Integer
            Get
                SyncLock Me
                    Return _list.Count
                End SyncLock
            End Get
        End Property

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SortedQueue(Of T)"/> class.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Adds the specified t.
        ''' </summary>
        ''' <param name="t">The t.</param>
        ''' <remarks></remarks>
        Public Sub Add(t As T)
            SyncLock Me
                Dim idx As Integer = 0
                For Each test As T In _list
                    If Sorter.Compare(t, test) <= 0 Then
                        Exit For
                    End If
                    idx += 1
                Next
                If idx < _list.Count Then
                    _list.Insert(idx, t)
                Else
                    _list.Add(t)
                End If
            End SyncLock
        End Sub

        ''' <summary>
        ''' Removes the specified t.
        ''' </summary>
        ''' <param name="t">The t.</param>
        ''' <remarks></remarks>
        Public Sub Remove(t As T)
            SyncLock Me
                If Contains(t) Then
                    _list.Remove(t)
                End If
            End SyncLock
        End Sub

        ''' <summary>
        ''' Determines whether [contains] [the specified t].
        ''' </summary>
        ''' <param name="t">The t.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Contains(t As T) As Boolean
            SyncLock Me
                Return _list.Contains(t)
            End SyncLock
        End Function

#Region "IEnumerable<T> Members"

        ''' <summary>
        ''' Gets the enumerator.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            Return _list.GetEnumerator()
        End Function

#End Region

#Region "IEnumerable Members"

        ''' <summary>
        ''' is the enumerable_ get enumerator.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Overridable Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return _list.GetEnumerator()
        End Function

#End Region

        ''' <summary>
        ''' Clears this instance.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
            SyncLock Me
                _list.Clear()
            End SyncLock
        End Sub

        ''' <summary>
        ''' Gets at.
        ''' </summary>
        ''' <param name="p">The p.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetAt(p As Integer) As T
            SyncLock Me
                Return _list.ElementAt(p)
            End SyncLock
        End Function
    End Class
End Namespace