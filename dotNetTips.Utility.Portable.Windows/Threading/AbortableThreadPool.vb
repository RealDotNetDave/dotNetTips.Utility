'***********************************************************************
' Assembly         : Mitchell.ABSe.EnterpriseServices
' Author           : dm11086
' Created          : 03-25-2009
'
' Last Modified By : dm11086
' Last Modified On : 04-02-2009
' Description      :
'
' Copyright        : (c) Mitchell. All rights reserved.
'***********************************************************************
Imports System.Collections.Generic
Imports System.Threading
Imports System.Linq

Namespace Collections
    ''' <summary>
    ''' The code originate from this MSDN article:
    ''' http://msdn.microsoft.com/msdnmag/issues/06/03/NETMatters/
    ''' With improvemts.
    ''' </summary>
    Public NotInheritable Class AbortableThreadPool
        Private Sub New()
        End Sub
        Private Shared _callbacks As New LinkedList(Of WorkItem)()

        ''' <summary>
        ''' Any modification to this variable should be done under the _callbacks lock!
        ''' </summary>
        Private Shared _threads As New Dictionary(Of WorkItem, Thread)()

        Private Shared _waitForAllThreadToComplete As AutoResetEvent

        Public Shared Function QueueUserWorkItem(callback As AsyncCallback, state As Object) As WorkItem
            If callback Is Nothing Then
                Throw New ArgumentNullException("callback")
            End If

            Dim item As New WorkItem(callback, state, ExecutionContext.Capture())

            SyncLock _callbacks
                _callbacks.AddLast(item)
            End SyncLock

            ThreadPool.QueueUserWorkItem(AddressOf HandleItem)
            Return item
        End Function

        Private Shared Sub HandleItem(ignored As Object)
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

                '@@@  ExecutionContext.Run(item.Context, Function(state) item.Callback(item.State), Nothing)
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

        Public Shared Function GetStatus(itemId As Guid) As WorkItemStatus
            SyncLock _callbacks
                Dim wi As WorkItem = _threads.SingleOrDefault(Function(i) i.Key.Id = itemId).Key

                If wi IsNot Nothing Then
                    Return GetStatus(wi)
                Else
                    Return WorkItemStatus.Undetermined
                End If
            End SyncLock
        End Function

        Public Shared Function GetStatus(item As WorkItem) As WorkItemStatus
            If item Is Nothing Then
                Throw New ArgumentNullException("item")
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

            'System.Diagnostics.Debug.WriteLine("ReturnStatus = " + returnStatus);

            Return returnStatus
        End Function

        Public Shared Function Cancel(itemId As Guid, allowAbort As Boolean) As WorkItemStatus
            SyncLock _callbacks
                Dim wi As WorkItem = _threads.SingleOrDefault(Function(i) i.Key.Id = itemId).Key

                If wi IsNot Nothing Then
                    Return Cancel(wi, allowAbort)
                Else
                    Return WorkItemStatus.Undetermined

                End If
            End SyncLock
        End Function

        Public Shared Function Cancel(item As WorkItem, allowAbort As Boolean) As WorkItemStatus
            If item Is Nothing Then
                Throw New ArgumentNullException("item")
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

            'System.Diagnostics.Debug.WriteLine("ReturnStatus = " + returnStatus);

            Return returnStatus
        End Function

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
    End Class
End Namespace