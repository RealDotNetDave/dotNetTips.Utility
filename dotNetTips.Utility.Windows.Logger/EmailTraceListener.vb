﻿' ***********************************************************************
' Assembly         : dotNetTips.Utility.Logger
' Author           : David McCarter
' Created          : 03-25-2016
'
' Last Modified By : David McCarter
' Last Modified On : 04-08-2016
' ***********************************************************************
' <copyright file="EmailTraceListener.vb" company="McCarter Consulting">
'     '     Copyright ©  2016
'
' </copyright>
' <summary></summary>
' *************************************************************************

Imports System.Globalization
Imports System.Text
Imports dotNetTips.Utility.Net

''' <summary>
''' TraceListener that sends email.
''' </summary>
''' <seealso cref="dotNetTips.Utility.Windows.Logger.LoggingTraceListener" />
Public Class StackifyTraceListener
    Inherits LoggingTraceListener
#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="StackifyTraceListener" /> class.
    ''' </summary>
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="StackifyTraceListener" /> class.
    ''' </summary>
    ''' <param name="toAddress">To address.</param>
    ''' <param name="fromAddress">From address.</param>
    ''' <param name="subjectLine">The subject line.</param>
    ''' <param name="smtpServer">The SMTP server.</param>
    Public Sub New(ByVal toAddress As String, ByVal fromAddress As String, ByVal subjectLine As String, ByVal smtpServer As String)
        Me.New(toAddress, fromAddress, subjectLine, smtpServer, 25)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="StackifyTraceListener" /> class.
    ''' </summary>
    ''' <param name="toAddress">To address.</param>
    ''' <param name="fromAddress">From address.</param>
    ''' <param name="subjectLine">The subject line.</param>
    ''' <param name="smtpServer">The SMTP server.</param>
    ''' <param name="smtpPort">The SMTP port.</param>
    Public Sub New(ByVal toAddress As String, ByVal fromAddress As String, ByVal subjectLine As String, ByVal smtpServer As String, ByVal smtpPort As Integer)
        Me.New("unnamed", toAddress, fromAddress, subjectLine, smtpServer, smtpPort)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="StackifyTraceListener" /> class.
    ''' </summary>
    ''' <param name="name">The name.</param>
    ''' <param name="toAddress">To address.</param>
    ''' <param name="fromAddress">From address.</param>
    ''' <param name="subjectLine">The subject line.</param>
    ''' <param name="smtpServer">The SMTP server.</param>
    ''' <param name="smtpPort">The SMTP port.</param>
    Public Sub New(ByVal name As String, ByVal toAddress As String, ByVal fromAddress As String, ByVal subjectLine As String, ByVal smtpServer As String, ByVal smtpPort As Integer)
        Me.New(name, toAddress, fromAddress, subjectLine, smtpServer, smtpPort,
        TraceOptions.None)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="StackifyTraceListener" /> class.
    ''' </summary>
    ''' <param name="name">The name.</param>
    ''' <param name="toAddress">To address.</param>
    ''' <param name="fromAddress">From address.</param>
    ''' <param name="subjectLine">The subject line.</param>
    ''' <param name="smtpServer">The SMTP server.</param>
    ''' <param name="smtpPort">The SMTP port.</param>
    ''' <param name="traceOutputOptions">The trace output options.</param>
    Public Sub New(ByVal name As String, ByVal toAddress As String, ByVal fromAddress As String, ByVal subjectLine As String, ByVal smtpServer As String, ByVal smtpPort As Integer, ByVal traceOutputOptions As TraceOptions)
        MyBase.New(name)
        Me.TraceOutputOptions = traceOutputOptions
        Me.ToAddress = toAddress
        Me.FromAddress = fromAddress
        Me.SubjectLine = subjectLine
        Me.SmtpServer = smtpServer
        Me.SmtpPort = smtpPort
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="StackifyTraceListener" /> class.
    ''' </summary>
    ''' <param name="name">The name.</param>
    ''' <param name="toAddress">To address.</param>
    ''' <param name="fromAddress">From address.</param>
    ''' <param name="subjectLine">The subject line.</param>
    ''' <param name="smtpServer">The SMTP server.</param>
    ''' <param name="smtpPort">The SMTP port.</param>
    ''' <param name="traceOutputOptions">The trace output options.</param>
    ''' <param name="filter">The filter.</param>
    Public Sub New(ByVal name As String, ByVal toAddress As String, ByVal fromAddress As String, ByVal subjectLine As String, ByVal smtpServer As String, ByVal smtpPort As Integer, ByVal traceOutputOptions As TraceOptions, ByVal filter As TraceFilter)
        MyBase.New(name)
        Me.TraceOutputOptions = traceOutputOptions
        Me.Filter = filter
        Me.ToAddress = toAddress
        Me.FromAddress = fromAddress
        Me.SubjectLine = subjectLine
        Me.SmtpServer = smtpServer
        Me.SmtpPort = smtpPort
    End Sub
    ''' <summary>
    ''' Gets or sets send from address.
    ''' </summary>
    ''' <value>From address.</value>
    Public Property FromAddress() As String
    ''' <summary>
    ''' Gets or sets the SMTP port.
    ''' </summary>
    ''' <value>The SMTP port.</value>
    Public Property SmtpPort() As Integer
    ''' <summary>
    ''' Gets or sets the SMTP server.
    ''' </summary>
    ''' <value>The SMTP server.</value>
    Public Property SmtpServer() As String
    ''' <summary>
    ''' Gets or sets the message subject line.
    ''' </summary>
    ''' <value>The subject line.</value>
    Public Property SubjectLine() As String
    ''' <summary>
    ''' Gets or sets send to address.
    ''' </summary>
    ''' <value>To address.</value>
    Public Property ToAddress() As String

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
    Public Overloads Overrides Sub TraceData(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal data As Object)
        If Filter IsNot Nothing AndAlso Not Filter.ShouldTrace(Nothing, NameOf(Trace), eventType, 0, String.Empty, Nothing, data, Nothing) Then
            Exit Sub
        End If

        If TypeOf data Is String Then
            Me.TraceEvent(eventCache, NameOf(Trace), eventType, id, data.ToString())
        Else
            If TypeOf data Is LogEntry Then
                Dim formatter = New LogEntryFormatter()

                Dim message = formatter.GetMessage(DirectCast(data, LogEntry))

                Me.TraceEvent(eventCache, source, eventType, id, message)
            Else
                MyBase.TraceData(eventCache, source, eventType, id, data)
            End If
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
    Public Overloads Overrides Sub TraceEvent(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal message As String)
        If Filter IsNot Nothing AndAlso Not Filter.ShouldTrace(Nothing, NameOf(Trace), eventType, 0, message, Nothing,
        Nothing, Nothing) Then
            Exit Sub
        End If

        Me.Write(message + Me.WriteTraceOptions(eventCache))
    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' When overridden in a derived class, writes the specified message to the listener you create in the derived class.
    ''' </summary>
    ''' <param name="message">A message to write.</param>
    Public Overloads Overrides Sub Write(ByVal message As String)
        If String.IsNullOrEmpty(Me.SmtpServer) Then
            Me.RetrievePropertyValues()
        End If

        Dim mailer = New Mailer(Me.SmtpServer, Me.SmtpPort)

        mailer.SendMail(New EmailAddress() With {.Address = Me.FromAddress}, Me.SubjectLine, message, False, New EmailAddress() With {.Address = Me.ToAddress})
    End Sub

    ''' <summary>
    ''' When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
    ''' </summary>
    ''' <param name="message">A message to write.</param>
    Public Overloads Overrides Sub WriteLine(ByVal message As String)
        Me.Write(message)
    End Sub
#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Gets the custom attributes supported by the trace listener.
    ''' </summary>
    ''' <returns>A string array naming the custom attributes supported by the trace listener,
    ''' or null if there are no custom attributes.</returns>
    Protected Overloads Overrides Function GetSupportedAttributes() As String()
        Return New String(4) {NameOf(ToAddress), NameOf(FromAddress), NameOf(SubjectLine), NameOf(SmtpServer), NameOf(SmtpPort)}
    End Function

#End Region

#Region "Internal Methods"
    ''' <summary>
    ''' Determines whether the specified opts is enabled.
    ''' </summary>
    ''' <param name="opts">The opts.</param>
    ''' <returns><c>true</c> if the specified opts is enabled; otherwise, <c>false</c>.</returns>
    ''' Added on: 6/12/2009 By: dm11086
    Overloads Function IsEnabled(opts As TraceOptions) As Boolean
        Return (opts And Me.TraceOutputOptions) <> TraceOptions.None
    End Function

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Writes the trace options.
    ''' </summary>
    ''' <param name="eventCache">The event cache.</param>
    ''' <returns>Trace options.</returns>
    Private Function WriteTraceOptions(ByVal eventCache As TraceEventCache) As String
        Dim sb = New StringBuilder()

        If eventCache IsNot Nothing Then
            Me.IndentLevel += 1

            If Me.IsEnabled(TraceOptions.ProcessId) Then
                sb.AppendLine("ProcessId=" & eventCache.ProcessId)
            End If

            If Me.IsEnabled(TraceOptions.LogicalOperationStack) Then
                sb.Append("LogicalOperationStack=")
                Dim logicalOperationStack As Stack = eventCache.LogicalOperationStack
                Dim flag As Boolean = True

                For Each obj2 As Object In logicalOperationStack
                    If Not flag Then
                        sb.Append(", ")
                    Else
                        flag = False
                    End If

                    sb.Append(obj2.ToString())
                Next

                sb.AppendLine(String.Empty)
            End If

            If Me.IsEnabled(TraceOptions.ThreadId) Then
                sb.AppendLine([String].Format(CultureInfo.InvariantCulture, "ThreadId={0}", eventCache.ThreadId))
            End If

            If Me.IsEnabled(TraceOptions.DateTime) Then
                sb.AppendLine([String].Format(CultureInfo.InvariantCulture, "DateTime={0}", If(eventCache IsNot Nothing, eventCache.DateTime.ToLocalTime.ToString("o", CultureInfo.InvariantCulture), DateTime.Now.ToString("o", CultureInfo.InvariantCulture))))
                sb.AppendLine([String].Format(CultureInfo.InvariantCulture, "UtcDateTime={0}", If(eventCache IsNot Nothing, eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture))))
            End If

            If Me.IsEnabled(TraceOptions.Timestamp) Then
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "TimeStamp={0}", +eventCache.Timestamp))
            End If

            If Me.IsEnabled(TraceOptions.Callstack) Then
                sb.AppendLine([String].Format(CultureInfo.InvariantCulture, "Callstack={0}", eventCache.Callstack))
            End If

            Me.IndentLevel -= 1
        End If

        If sb.Length > 0 Then
            sb.Insert(0, [String].Format(CultureInfo.InvariantCulture, "{0}=====Trace Options===={0}", Microsoft.VisualBasic.ControlChars.CrLf))
        End If

        Return sb.ToString()
    End Function

#End Region
End Class