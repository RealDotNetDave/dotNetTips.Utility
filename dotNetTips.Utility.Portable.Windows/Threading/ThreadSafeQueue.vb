Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Threading

Namespace Collections
    ''' <summary>
    ''' Queue: Thread Safe
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public NotInheritable Class ThreadSafeQueue(Of T)
        Inherits Queue(Of T)
        Implements IDisposable
#Region "Constructors"


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(capacity As Integer)
            MyBase.New(capacity)
        End Sub

        Public Sub New(items As IEnumerable(Of T))
            MyBase.New(items)
        End Sub

        Private ReadOnly _lock As New ReaderWriterLockSlim()
#End Region

#Region "Public Methods"

        ''' <summary>
        ''' Dequeues all items in the queue.
        ''' </summary>
        ''' <returns></returns>
        Public Function DequeueAll() As Collection(Of T)
            _lock.EnterWriteLock()

            Try
                Dim returnList = New Collection(Of T)()

                While Me.Count > 0
                    returnList.Add(MyBase.Dequeue())
                End While

                Return returnList
            Finally
                _lock.ExitWriteLock()
            End Try
        End Function

        ''' <summary>
        ''' Enqueues all items.
        ''' </summary>
        ''' <param name="items">The items.</param>
        Public Sub EnqueueAll(items As Collection(Of T))
            _lock.EnterWriteLock()

            Try
                For Each item As var In items
                    Me.Enqueue(item)
                Next
            Finally
                _lock.ExitWriteLock()
            End Try
        End Sub

        ''' <summary>
        ''' Enqueues all items.
        ''' </summary>
        ''' <param name="items">The items.</param>
        Public Sub EnqueueAll(items As IEnumerable(Of T))
            _lock.EnterWriteLock()

            Try
                For Each item As T In items
                    Me.Enqueue(item)
                Next
            Finally
                _lock.ExitWriteLock()
            End Try
        End Sub

        ''' <summary>
        ''' Adds an object to the end of the <see cref="T:System.Collections.Generic.Queue`1"/>.
        ''' </summary>
        ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.Queue`1"/>. The value can be null for reference types.</param>
        Public Shadows Sub Enqueue(item As T)
            _lock.EnterWriteLock()

            Try
                MyBase.Enqueue(item)
                Monitor.Pulse(Me)
            Finally
                _lock.ExitWriteLock()
            End Try
        End Sub

        ''' <summary>
        ''' Dequeues with a specified timeout.
        ''' </summary>
        ''' <param name="timeout">The timeout.</param>
        ''' <returns></returns>
        Public Shadows Function Dequeue(timeout As Integer) As T
            _lock.EnterWriteLock()

            Try
                While Me.Count = 0
                    If Not Monitor.Wait(Me, timeout) Then
                        Throw New InvalidOperationException("Timeout")
                    End If
                End While

                Return MyBase.Dequeue()
            Finally
                _lock.ExitWriteLock()
            End Try
        End Function

        ''' <summary>
        ''' Dequeues with a specified timeout.
        ''' </summary>
        ''' <param name="timeout">The timeout.</param>
        ''' <returns></returns>
        Public Overloads Function Dequeue(timeout As TimeSpan) As T
            _lock.EnterWriteLock()

            Try
                Return Dequeue(timeout.Milliseconds)
            Finally
                _lock.ExitWriteLock()
            End Try
        End Function

        ''' <summary>
        ''' Removes and returns the object at the beginning of the <see cref="T:System.Collections.Generic.Queue`1"/>.
        ''' </summary>
        ''' <returns>
        ''' The object that is removed from the beginning of the <see cref="T:System.Collections.Generic.Queue`1"/>.
        ''' </returns>
        ''' <exception cref="T:System.InvalidOperationException">
        ''' The <see cref="T:System.Collections.Generic.Queue`1"/> is empty.
        ''' </exception>
        Public Shadows Function Dequeue() As T
            _lock.EnterWriteLock()

            Try
                Return Dequeue(Timeout.Infinite)
            Finally
                _lock.ExitWriteLock()
            End Try
        End Function

        ''' <summary>
        ''' Removes all objects from the <see cref="T:System.Collections.Generic.Queue`1"/>.
        ''' </summary>
        Public Shadows Sub Clear()
            _lock.EnterWriteLock()

            Try
                MyBase.Clear()
            Finally
                _lock.ExitWriteLock()
            End Try
        End Sub
#End Region

        Protected Overrides Sub Finalize()
            Try
                Me.Dispose(False)
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#Region "IDisposable Members"

        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Private Sub Dispose(disposing As Boolean)
            If disposing Then
                If Me._lock IsNot Nothing Then
                    Me._lock.Dispose()
                End If
            End If
        End Sub

#End Region
    End Class
End Namespace