' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable.Windows
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 05-11-2017
' ***********************************************************************
' <copyright file="WorkItem.vb" company="dotNetTips.com">
'     '     '     David McCarter
'
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Threading
Imports dotNetTips.Utility.Portable.Extensions

Namespace Threading
    ''' <summary>
    ''' Work Item for AbortableThreadPool
    ''' </summary>
    ''' <seealso cref="System.IDisposable" />
    Public NotInheritable Class WorkItem
        Implements IDisposable
        ''' <summary>
        ''' Timeout timer
        ''' </summary>
        Private _timeoutTimer As Timer

        ''' <summary>
        ''' Occurs when [completed].
        ''' </summary>
        Public Event Completed As EventHandler(Of WorkItemEventArgs)
        ''' <summary>
        ''' Occurs when [queued].
        ''' </summary>
        Public Event Queued As EventHandler(Of EventArgs)
        ''' <summary>
        ''' Occurs when [executing].
        ''' </summary>
        Public Event Executing As EventHandler(Of EventArgs)
        ''' <summary>
        ''' Occurs when [aborted].
        ''' </summary>
        Public Event Aborted As EventHandler(Of WorkItemEventArgs)

        ''' <summary>
        ''' Initializes a new instance of the <see cref="WorkItem" /> class.
        ''' </summary>
        ''' <param name="wc">The WaitCallback.</param>
        ''' <param name="state">The state.</param>
        ''' <param name="ctx">The ExecutionContext.</param>
        Public Sub New(wc As AsyncCallback, state As Object, ctx As ExecutionContext)
            Me.Id = Guid.NewGuid()
            Me.Callback = wc
            Me.State = state
            Me.Context = ctx
        End Sub

        ''' <summary>
        ''' Gets or sets the id.
        ''' </summary>
        ''' <value>The id.</value>
        Public Property Id() As Guid
            Get
                Return _id
            End Get
            Private Set(value As Guid)
                _id = value
            End Set
        End Property
        ''' <summary>
        ''' m identifier
        ''' </summary>
        Private _id As Guid

        ''' <summary>
        ''' Gets or sets the callback.
        ''' </summary>
        ''' <value>The callback.</value>
        Public Property Callback() As AsyncCallback
            Get
                Return _callback
            End Get
            Private Set(value As AsyncCallback)
                _callback = value
            End Set
        End Property
        ''' <summary>
        ''' m callback
        ''' </summary>
        Private _callback As AsyncCallback

        ''' <summary>
        ''' Gets or sets the state.
        ''' </summary>
        ''' <value>The state.</value>
        Public Property State() As Object
            Get
                Return _state
            End Get
            Private Set(value As Object)
                _state = value
            End Set
        End Property
        ''' <summary>
        ''' m state
        ''' </summary>
        Private _state As Object

        ''' <summary>
        ''' Gets or sets the context.
        ''' </summary>
        ''' <value>The context.</value>
        Public Property Context() As ExecutionContext
            Get
                Return _context
            End Get
            Private Set(value As ExecutionContext)
                _context = value
            End Set
        End Property
        ''' <summary>
        ''' m context
        ''' </summary>
        Private _context As ExecutionContext

        ''' <summary>
        ''' Sets the timeout.
        ''' </summary>
        ''' <param name="timeout">The timeout.</param>
        Public Sub SetTimeout(timeout As TimeSpan)
            If timeout.TotalSeconds > 0 Then
                Me._timeoutTimer = New Timer(AddressOf AbortWorkItem, Nothing, timeout, New TimeSpan(0, 0, 0, 0, -1))
            End If
        End Sub

        ''' <summary>
        ''' Aborts the work item.
        ''' </summary>
        ''' <param name="state">The state.</param>
        Private Sub AbortWorkItem(state As Object)

            Me._timeoutTimer.Change(-1, 0)

            AbortableThreadPool.Cancel(Me, True)
        End Sub

        ''' <summary>
        ''' Called when [complete].
        ''' </summary>
        ''' <param name="id">The id.</param>
        Public Sub OnComplete(id As Guid)
            RaiseEvent Completed(Nothing, New WorkItemEventArgs() With {.Id = id})
        End Sub

        ''' <summary>
        ''' Called when [queued].
        ''' </summary>
        Public Sub OnQueued()
            RaiseEvent Queued(Me, Nothing)
        End Sub

        ''' <summary>
        ''' Called when [executing].
        ''' </summary>
        Public Sub OnExecuting()
            RaiseEvent Executing(Me, Nothing)
        End Sub

        ''' <summary>
        ''' Called when [aborted].
        ''' </summary>
        ''' <param name="id">The id.</param>
        Public Sub OnAborted(id As Guid)
            Dim ea = New WorkItemEventArgs() With {.Id = id}

            RaiseEvent Aborted(Nothing, ea)
        End Sub

        ''' <summary>
        ''' Releases unmanaged resources and performs other cleanup operations before the
        ''' <see cref="WorkItem" /> is reclaimed by garbage collection.
        ''' </summary>
        Protected Overrides Sub Finalize()
            Try
                Me.DisposeFields()
            Finally
                MyBase.Finalize()
            End Try
        End Sub

#Region "IDisposable Members"

        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Releases unmanaged and - optionally - managed resources
        ''' </summary>
        ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        Private Sub Dispose(disposing As Boolean)
            If disposing Then
                Me.DisposeFields()
            End If
        End Sub
#End Region
    End Class
End Namespace