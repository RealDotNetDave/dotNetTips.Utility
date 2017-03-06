' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-30-2016
' ***********************************************************************
' <copyright file="AbortableThreadPool.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.Generic
Imports System.Threading
Imports dotNetTips.Utility.Portable.OOP

Namespace Threading
    ''' <summary>
    ''' The code originate from this MSDN article:
    ''' http://msdn.microsoft.com/msdnmag/issues/06/03/NETMatters/
    ''' With improvemts.
    ''' </summary>
    Public NotInheritable Class AbortableThreadPool
        Implements IDisposable
        ''' <summary>
        ''' Prevents a default instance of the <see cref="AbortableThreadPool"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Callbacks
        ''' </summary>
        Private Shared _callbacks As New LinkedList(Of WorkItem)()

        ''' <summary>
        ''' Any modification to this variable should be done under the _callbacks lock!
        ''' </summary>
        Private Shared _threads As New Dictionary(Of WorkItem, Thread)()

        ''' <summary>
        ''' ait for all thread to complete
        ''' </summary>
        Private Shared _waitForAllThreadToComplete As AutoResetEvent

        ''' <summary>
        ''' Queues the user work item.
        ''' </summary>
        ''' <param name="callback">The callback.</param>
        ''' <param name="state">The state.</param>
        ''' <returns>WorkItem.</returns>
        Public Shared Function QueueUserWorkItem(callback As AsyncCallback, state As Object) As WorkItem
            Encapsulation.TryValidateParam(Of ArgumentNullException)(callback IsNot Nothing)

            Dim item As New WorkItem(callback, state, ExecutionContext.Capture())

            SyncLock _callbacks
                _callbacks.AddLast(item)
            End SyncLock

            ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf HandleItem))

            Return item
        End Function

        ''' <summary>
        ''' Handles the item.
        ''' </summary>
        Public Shared Sub HandleItem()
            Dim item As WorkItem = Nothing
            Try
                SyncLock _callbacks
                    If _callbacks.Count > 0 Then
                        item = _callbacks.First.Value
                        _callbacks.RemoveFirst()
                    End If

                    If item Is Nothing Then
                        Return
                    End If

                    _threads.Add(item, Thread.CurrentThread)
                    item.OnQueued()
                End SyncLock

                item.OnExecuting()

                ExecutionContext.Run(item.Context, Function(state) item.Callback(), Nothing)
            Finally
                SyncLock _callbacks
                    If item IsNot Nothing Then
                        item.Dispose()
                        _threads.Remove(item)

                        item.OnComplete(item.Id)

                        If _threads.Count = 0 AndAlso _waitForAllThreadToComplete IsNot Nothing Then
                            _waitForAllThreadToComplete.[Set]()
                        End If
                    End If
                End SyncLock
            End Try
        End Sub

        ''' <summary>
        ''' Gets the status.
        ''' </summary>
        ''' <param name="itemId">The item identifier.</param>
        ''' <returns>WorkItemStatus.</returns>
        Public Shared Function GetStatus(itemId As Guid) As WorkItemStatus
            SyncLock _callbacks
                Dim wi As WorkItem = _threads.SingleOrDefault(Function(i) i.Key.Id = itemId).Key

                Return If(wi IsNot Nothing, GetStatus(wi), WorkItemStatus.Undetermined)
            End SyncLock
        End Function

        ''' <summary>
        ''' Gets the status.
        ''' </summary>
        ''' <param name="item">The item.</param>
        ''' <returns>WorkItemStatus.</returns>
        ''' <exception cref="ArgumentNullException"></exception>
        Public Shared Function GetStatus(item As WorkItem) As WorkItemStatus
            If item Is Nothing Then
                Throw New ArgumentNullException(NameOf(item))
            End If

            Dim returnStatus = WorkItemStatus.Undetermined

            SyncLock _callbacks
                Dim node As LinkedListNode(Of WorkItem) = _callbacks.Find(item)
                If node IsNot Nothing Then
                    returnStatus = WorkItemStatus.Queued
                ElseIf _threads.ContainsKey(item) Then
                    returnStatus = WorkItemStatus.Executing
                Else
                    returnStatus = WorkItemStatus.Completed
                End If
            End SyncLock

            Return returnStatus
        End Function

        ''' <summary>
        ''' Cancels the specified item identifier.
        ''' </summary>
        ''' <param name="itemId">The item identifier.</param>
        ''' <param name="allowAbort">if set to <c>true</c> [allow abort].</param>
        ''' <returns>WorkItemStatus.</returns>
        Public Shared Function Cancel(itemId As Guid, allowAbort As Boolean) As WorkItemStatus
            SyncLock _callbacks
                Dim wi As WorkItem = _threads.SingleOrDefault(Function(i) i.Key.Id = itemId).Key

                Return If(wi IsNot Nothing, Cancel(wi, allowAbort), WorkItemStatus.Undetermined)
            End SyncLock
        End Function

        ''' <summary>
        ''' Cancels the specified item.
        ''' </summary>
        ''' <param name="item">The item.</param>
        ''' <param name="allowAbort">if set to <c>true</c> [allow abort].</param>
        ''' <returns>WorkItemStatus.</returns>
        ''' <exception cref="ArgumentNullException">item</exception>
        Public Shared Function Cancel(item As WorkItem, allowAbort As Boolean) As WorkItemStatus
            If item Is Nothing Then
                Throw New ArgumentNullException(NameOf(item))
            End If

            Dim returnStatus = WorkItemStatus.Undetermined

            SyncLock _callbacks
                Dim node As LinkedListNode(Of WorkItem) = _callbacks.Find(item)
                If node IsNot Nothing Then
                    _callbacks.Remove(node)
                    returnStatus = WorkItemStatus.Queued
                ElseIf _threads.ContainsKey(item) Then
                    If allowAbort Then
                        item.Dispose()
                        '@@@ _threads(item).Abort()
                        _threads.Remove(item)
                        item.OnAborted(item.Id)

                        returnStatus = WorkItemStatus.Aborted
                    Else
                        returnStatus = WorkItemStatus.Executing
                    End If
                Else

                    returnStatus = WorkItemStatus.Completed
                End If
            End SyncLock

            Return returnStatus
        End Function

        ''' <summary>
        ''' Cancels all.
        ''' </summary>
        ''' <param name="allowAbort">if set to <c>true</c> [allow abort].</param>
        Public Shared Sub CancelAll(allowAbort As Boolean)
            SyncLock _callbacks
                _callbacks.Clear()

                If allowAbort Then
                    For Each thread As Thread In _threads.Values
                        '@@@ thread.Abort()
                    Next

                    _threads.Clear()
                End If
            End SyncLock
        End Sub

        ''' <summary>
        ''' Waits for all threads to finish.
        ''' </summary>
        Public Shared Sub WaitForAllThreadsToFinish()
            SyncLock _callbacks
                If _threads.Count = 0 Then
                    Return
                End If

                _waitForAllThreadToComplete = New AutoResetEvent(True)

                Try
                    _waitForAllThreadToComplete.WaitOne()
                Finally
                    _waitForAllThreadToComplete.Dispose()
                    _waitForAllThreadToComplete = Nothing
                End Try
            End SyncLock
        End Sub

#Region "IDisposable Implementation"

        Private disposed As Boolean

        Public Sub Dispose(ByVal disposing As Boolean)
            SyncLock Me
                ' Do nothing if the object has already been disposed of.
                If disposed Then
                    Exit Sub
                End If

                If disposing Then
                    ' Release disposable objects used by this instance here.

                    If Not _waitForAllThreadToComplete Is Nothing Then
                        _waitForAllThreadToComplete.Dispose()
                    End If
                End If

                ' Release unmanaged resources here. Don't access reference type fields.

                ' Remember that the object has been disposed of.
                disposed = True
            End SyncLock
        End Sub

        Public Sub Dispose() _
                Implements IDisposable.Dispose
            Dispose(True)
            ' Unregister object for finalization.
            GC.SuppressFinalize(Me)
        End Sub

#End Region
    End Class
End Namespace