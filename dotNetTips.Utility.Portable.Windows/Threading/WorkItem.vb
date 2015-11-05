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
Imports System.Threading

Namespace Collections
    ''' <summary>
    ''' Work Item for ApportableThreadPool
    ''' </summary>
    Public NotInheritable Class WorkItem
        Implements IDisposable
        Private _timeoutTimer As Timer

        Public Event Completed As EventHandler(Of WorkItemEventArgs)
        Public Event Queued As EventHandler(Of EventArgs)
        Public Event Executing As EventHandler(Of EventArgs)
        Public Event Aborted As EventHandler(Of WorkItemEventArgs)

        ''' <summary>
        ''' Initializes a new instance of the <see cref="WorkItem"/> class.
        ''' </summary>
        ''' <param name="wc">The WaitCallback.</param>
        ''' <param name="state">The state.</param>
        ''' <param name="ctx">The ExecutionContext.</param>
        Public Sub New(wc As WaitCallback, state As Object, ctx As ExecutionContext)
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
                Return m_Id
            End Get
            Private Set(value As Guid)
                m_Id = value
            End Set
        End Property
        Private m_Id As Guid

        ''' <summary>
        ''' Gets or sets the callback.
        ''' </summary>
        ''' <value>The callback.</value>
        Public Property Callback() As WaitCallback
            Get
                Return m_Callback
            End Get
            Private Set(value As WaitCallback)
                m_Callback = value
            End Set
        End Property
        Private m_Callback As WaitCallback

        ''' <summary>
        ''' Gets or sets the state.
        ''' </summary>
        ''' <value>The state.</value>
        Public Property State() As Object
            Get
                Return m_State
            End Get
            Private Set(value As Object)
                m_State = value
            End Set
        End Property
        Private m_State As Object

        ''' <summary>
        ''' Gets or sets the context.
        ''' </summary>
        ''' <value>The context.</value>
        Public Property Context() As ExecutionContext
            Get
                Return m_Context
            End Get
            Private Set(value As ExecutionContext)
                m_Context = value
            End Set
        End Property
        Private m_Context As ExecutionContext

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
            System.Diagnostics.Debug.WriteLine("Aborting Work Item @ " & DateTime.Now.ToString())

            Me._timeoutTimer.Change(-1, 0)

            AbortableThreadPool.Cancel(Me, True)
        End Sub

        ''' <summary>
        ''' Called when [complete].
        ''' </summary>
        ''' <param name="id">The id.</param>
        Public Sub OnComplete(id As Guid)
            If Me.Completed IsNot Nothing Then
				Dim ea = New WorkItemEventArgs() With { _
					Key .Id = id _
				}
                System.Diagnostics.Debug.WriteLine("Calling Complete = " & Convert.ToString(id))
                Me.Completed(Nothing, ea)
            End If
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
            If Me.Aborted IsNot Nothing Then
				Dim ea = New WorkItemEventArgs() With { _
					Key .Id = id _
				}
                System.Diagnostics.Debug.WriteLine("Calling Aborted = " & Convert.ToString(id))
                Me.Aborted(Nothing, ea)
            End If
        End Sub

        ''' <summary>
        ''' Releases unmanaged resources and performs other cleanup operations before the
        ''' <see cref="WorkItem"/> is reclaimed by garbage collection.
        ''' </summary>
        Protected Overrides Sub Finalize()
            Try
                Me.Dispose(False)
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
                If Me._timeoutTimer IsNot Nothing Then
                    Me._timeoutTimer.Dispose()
                End If
            End If
        End Sub
#End Region
    End Class
End Namespace