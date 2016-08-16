' ***********************************************************************
' Assembly         : dotNetTips.Utility.Windows.Logger
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 04-13-2016
' ***********************************************************************
' <copyright file="EventLogTraceListener.vb" company="NicheWare - dotNetTips.com">
'     NicheWare - dotNetTips.com. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Diagnostics.Contracts
Imports System.Globalization
Imports System.Security.Permissions
Imports System.Text
Imports dotNetTips.Utility.Xml

''' <summary>
''' Class EventLogTraceListener.
''' </summary>
''' <seealso cref="dotNetTips.Utility.Windows.Logger.LoggingTraceListener" />
<HostProtection(SecurityAction.LinkDemand, Synchronization:=True)>
Public Class EventLogTraceListener
    Inherits LoggingTraceListener

    ' Fields
    ''' <summary>
    ''' event log
    ''' </summary>
    Private _eventLog As EventLog
    ''' <summary>
    ''' name set
    ''' </summary>
    Private _nameSet As Boolean

    ''' <summary>
    ''' Initializes a new instance of the <see cref="EventLogTraceListener"/> class.
    ''' </summary>
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="EventLogTraceListener"/> class.
    ''' </summary>
    ''' <param name="eventLog">The event log.</param>
    Public Sub New(ByVal eventLog As EventLog)
        MyBase.New(CStr(If((Not eventLog Is Nothing), eventLog.Source, String.Empty)))
        Contract.Requires(Of ArgumentNullException)(eventLog IsNot Nothing)

        Me.EventLog = eventLog

    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="EventLogTraceListener"/> class.
    ''' </summary>
    ''' <param name="source">The source.</param>
    Public Sub New(ByVal source As String)
        Me.EventLog = New EventLog
        Me.EventLog.Source = source
    End Sub

    ''' <summary>
    ''' When overridden in a derived class, closes the output stream so it no longer receives tracing or debugging output.
    ''' </summary>
    Public Overrides Sub Close()
        If (Not Me.EventLog Is Nothing) Then
            Me.EventLog.Close()
        End If
    End Sub

    ''' <summary>
    ''' Creates the event instance.
    ''' </summary>
    ''' <param name="severity">The severity.</param>
    ''' <param name="id">The identifier.</param>
    ''' <returns>EventInstance.</returns>
    Private Shared Function CreateEventInstance(ByVal severity As TraceEventType, ByVal id As Integer) As EventInstance
        If (id > &HFFFF) Then
            id = &HFFFF
        End If
        If (id < 0) Then
            id = 0
        End If
        Dim instance As New EventInstance(id, 0)
        If ((severity = Global.System.Diagnostics.TraceEventType.Error) OrElse (severity = Global.System.Diagnostics.TraceEventType.Critical)) Then
            instance.EntryType = EventLogEntryType.Error
            Return instance
        End If
        If (severity = TraceEventType.Warning) Then
            instance.EntryType = EventLogEntryType.Warning
            Return instance
        End If
        instance.EntryType = EventLogEntryType.Information
        Return instance
    End Function

    ''' <summary>
    ''' Writes trace information, an array of data objects and event information to the listener specific output.
    ''' </summary>
    ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    ''' <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
    ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
    ''' <param name="id">A numeric identifier for the event.</param>
    ''' <param name="data">An array of objects to emit as data.</param>
    ''' <PermissionSet>
    '''   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    '''   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ''' </PermissionSet>
    Public Overrides Sub TraceData(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal ParamArray data As Object())
        If ((Me.Filter Is Nothing) OrElse Me.Filter.ShouldTrace(eventCache, source, eventType, id, Nothing, Nothing, Nothing, data)) Then
            Dim instance As EventInstance = EventLogTraceListener.CreateEventInstance(eventType, id)
            Dim builder As New StringBuilder
            If (Not data Is Nothing) Then
                Dim i As Integer
                For i = 0 To data.Length - 1
                    If (i <> 0) Then
                        builder.Append(", ")
                    End If
                    If (Not data(i) Is Nothing) Then
                        builder.Append(data(i).ToString)
                    End If
                Next i
            End If
            Me.EventLog.WriteEvent(instance, New Object() {builder.ToString})
        End If
    End Sub

    ''' <summary>
    ''' Writes trace information, a data object and event information to the listener specific output.
    ''' </summary>
    ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    ''' <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
    ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
    ''' <param name="id">A numeric identifier for the event.</param>
    ''' <param name="data">The trace data to emit.</param>
    ''' <PermissionSet>
    '''   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    '''   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ''' </PermissionSet>
    Public Overrides Sub TraceData(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal data As Object)
        If ((Me.Filter Is Nothing) OrElse Me.Filter.ShouldTrace(eventCache, source, eventType, id, Nothing, Nothing, Nothing, New Object() {data})) Then
            Dim instance As EventInstance = CreateEventInstance(eventType, id)

            Dim actualData As Object = data

            If TypeOf data Is LogEntry Then
                actualData = GetXml(TryCast(data, LogEntry))
            End If

            Me.EventLog.WriteEvent(instance, actualData)
        End If
    End Sub

    ''' <summary>
    ''' Writes trace information, a message, and event information to the listener specific output.
    ''' </summary>
    ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    ''' <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
    ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
    ''' <param name="id">A numeric identifier for the event.</param>
    ''' <param name="message">A message to write.</param>
    ''' <PermissionSet>
    '''   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    '''   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ''' </PermissionSet>
    Public Overrides Sub TraceEvent(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal message As String)
        If ((Me.Filter Is Nothing) OrElse Me.Filter.ShouldTrace(eventCache, source, eventType, id, Nothing, Nothing, Nothing, New Object() {message})) Then
            Dim instance As EventInstance = EventLogTraceListener.CreateEventInstance(eventType, id)

            Me.EventLog.WriteEvent(instance, New Object() {message})
        End If
    End Sub

    ''' <summary>
    ''' Writes trace information, a formatted array of objects and event information to the listener specific output.
    ''' </summary>
    ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
    ''' <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
    ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
    ''' <param name="id">A numeric identifier for the event.</param>
    ''' <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
    ''' <param name="args">An object array containing zero or more objects to format.</param>
    ''' <PermissionSet>
    '''   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    '''   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ''' </PermissionSet>
    Public Overrides Sub TraceEvent(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal format As String, ByVal ParamArray args As Object())
        If ((Me.Filter Is Nothing) OrElse Me.Filter.ShouldTrace(eventCache, source, eventType, id, Nothing, Nothing, Nothing, args)) Then
            Dim instance As EventInstance = EventLogTraceListener.CreateEventInstance(eventType, id)
            If (args Is Nothing) Then
                Me.EventLog.WriteEvent(instance, New Object() {format})
            ElseIf String.IsNullOrEmpty(format) Then
                Dim values As String() = New String(args.Length - 1) {}
                Dim i As Integer
                For i = 0 To args.Length - 1
                    values(i) = args(i).ToString
                Next i
                Me.EventLog.WriteEvent(instance, values)
            Else
                Me.EventLog.WriteEvent(instance, New Object() {String.Format(CultureInfo.InvariantCulture, format, args)})
            End If
        End If
    End Sub

    ''' <summary>
    ''' When overridden in a derived class, writes the specified message to the listener you create in the derived class.
    ''' </summary>
    ''' <param name="message">A message to write.</param>
    Public Overrides Sub Write(ByVal message As String)
        If (Not Me.EventLog Is Nothing) Then
            Me.EventLog.WriteEntry(message)
        End If
    End Sub

    ''' <summary>
    ''' When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
    ''' </summary>
    ''' <param name="message">A message to write.</param>
    Public Overrides Sub WriteLine(ByVal message As String)
        Me.Write(message)
    End Sub

    ''' <summary>
    ''' Gets or sets the event log.
    ''' </summary>
    ''' <value>The event log.</value>
    Public Property EventLog() As EventLog
        Get
            Return Me._eventLog
        End Get
        Set(ByVal value As EventLog)
            Me._eventLog = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a name for this <see cref="T:System.Diagnostics.TraceListener" />.
    ''' </summary>
    ''' <value>The name.</value>
    Public Overrides Property Name() As String
        Get
            If (Not Me._nameSet AndAlso (Not Me.EventLog Is Nothing)) Then
                Me._nameSet = True
                MyBase.Name = Me.EventLog.Source
            End If
            Return MyBase.Name
        End Get
        Set(ByVal value As String)
            Me._nameSet = True
            MyBase.Name = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the XML.
    ''' </summary>
    ''' <param name="logEntry">The log entry.</param>
    ''' <returns>System.String.</returns>
    Friend Shared Function GetXml(ByVal logEntry As LogEntry) As String
        Return XmlHelper.Serialize(logEntry).ToString
    End Function

End Class