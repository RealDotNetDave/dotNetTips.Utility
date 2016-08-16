' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-10-2013
'
' Last Modified By : David McCarter
' Last Modified On : 08-24-2013
' ***********************************************************************
' <copyright file="TcpTraceListener.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System
Imports System.Diagnostics
Imports System.Globalization
Imports System.Net
Imports System.Net.Sockets

''' <summary>
'''
''' </summary>
Public Class TcpTraceListener
  Inherits TraceListener
  Private _listener As TcpListener
  Private _client As TcpClient
  Private _networkStream As NetworkStream
  Private _readyToWrite As Boolean
  ' Have we got a person listening, ready to get messages?
  Private _initInProgress As Boolean
  ' Are we in the process of initializing (it's an async op)
#Region "Constructors"
  ''' <summary>
  ''' Initializes a new instance of the <see cref="TcpTraceListener"/> class.
  ''' </summary>
  ''' <param name="port">The port.</param>
  ''' <remarks></remarks>
  Public Sub New(port As Integer)
    Me.New()
    If port > 0 Then
      Me.TcpPort = port
    End If
  End Sub

  ''' <summary>
  ''' Initializes a new instance of the <see cref="TcpTraceListener" /> class.
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub New()
  End Sub
#End Region

#Region "Properties"
  ''' <summary>
  ''' Gets or sets the TCP port.
  ''' </summary>
  ''' <value>The TCP port.</value>
  ''' <remarks></remarks>
  Public Property TcpPort() As Integer
    Get
      Return m_TcpPort
    End Get
    Set(value As Integer)
      m_TcpPort = value
    End Set
  End Property
  Private m_TcpPort As Integer
#End Region

#Region "Public Methods"
  ''' <summary>
  ''' Writes trace information, a data object and event information to the listener specific output.
  ''' </summary>
  ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
  ''' <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
  ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType"/> values specifying the type of event that has caused the trace.</param>
  ''' <param name="id">A numeric identifier for the event.</param>
  ''' <param name="data">The trace data to emit.</param>
  ''' <PermissionSet>
  '''   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
  '''   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/>
  '''   </PermissionSet>
  ''' <remarks></remarks>
  Public Overrides Sub TraceData(eventCache As TraceEventCache, source As String, eventType As TraceEventType, id As Integer, data As Object)
    If Filter IsNot Nothing AndAlso Not Filter.ShouldTrace(Nothing, NameOf(Trace), eventType, 0, String.Empty, Nothing, _
        data, Nothing) Then
      Return
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
  ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
  ''' <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
  ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType"/> values specifying the type of event that has caused the trace.</param>
  ''' <param name="id">A numeric identifier for the event.</param>
  ''' <param name="message">A message to write.</param>
  ''' <PermissionSet>
  '''   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
  '''   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/>
  '''   </PermissionSet>
  ''' <remarks></remarks>
  Public Overrides Sub TraceEvent(eventCache As TraceEventCache, source As String, eventType As TraceEventType, id As Integer, message As String)
    If Filter IsNot Nothing AndAlso Not Filter.ShouldTrace(Nothing, NameOf(Trace), eventType, 0, message, Nothing, _
        Nothing, Nothing) Then
      Return
    End If

    Me.Write(message)
    ' + this.WriteTraceOptions(eventCache));
  End Sub

  ''' <summary>
  ''' When overridden in a derived class, writes the specified message to the listener you create in the derived class.
  ''' </summary>
  ''' <param name="message">A message to write.</param>
  Public Overrides Sub Write(message As String)
    If Me.TcpPort <= 0 Then
      Me.RetrievePropertyValues()
    End If

    Me.WriteString(message)
  End Sub

  ''' <summary>
  ''' When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
  ''' </summary>
  ''' <param name="message">A message to write.</param>
  Public Overrides Sub WriteLine(message As String)
    Me.Write(message)
  End Sub
#End Region

#Region "Internal Methods"
  ''' <summary>
  ''' Determines whether the specified opts is enabled.
  ''' </summary>
  ''' <param name="opts">The opts.</param>
  ''' <returns>
  '''   <c>true</c> if the specified opts is enabled; otherwise, <c>false</c>.
  ''' </returns>
  ''' Added on: 6/12/2009 By: dm11086
  Friend Function IsEnabled(opts As TraceOptions) As Boolean
    Return (opts And Me.TraceOutputOptions) <> TraceOptions.None
  End Function
#End Region

#Region "Protected Methods"
  ''' <summary>
  ''' Gets the custom attributes supported by the trace listener.
  ''' </summary>
  ''' <returns>
  ''' A string array naming the custom attributes supported by the trace listener,
  ''' or null if there are no custom attributes.
  ''' </returns>
  ''' Added on: 6/12/2009 By: dm11086
  Protected Overrides Function GetSupportedAttributes() As String()
    Return New String(0) {NameOf(TcpPort)}
  End Function
#End Region

#Region "Private Methods"

  ''' <summary>
  ''' Callback when a client connects to our port. We need to accept
  ''' their connection and setup a stream to write to.
  ''' </summary>
  ''' <param name="ar">Async Result</param>
  Private Sub ClientConnected(ar As IAsyncResult)
    Me._client = Me._listener.EndAcceptTcpClient(ar)
    Me._networkStream = Me._client.GetStream()

    ' Indicate that we're ready to write data to the stream,
    ' and that we're no longer waiting on a 2nd thread to complete
    ' the initialization.
    Me._readyToWrite = True
    Me._initInProgress = False
  End Sub

  ''' <summary>
  ''' Begin the [async] process of listening to the port and
  ''' waiting for a client connection.
  ''' </summary>
  Private Sub InitializeEndpoint()
    ' Just in case we're called twice when we're in the middle of
    ' the async operation, we double check.
    If Not Me._initInProgress Then
      Me._initInProgress = True
      Me._readyToWrite = False

      'Try
      ' This is here in case we're being called after a client has
      ' disconnected. We want to stop/start the listener.
      If Me._listener IsNot Nothing Then
        Me._listener.[Stop]()
      End If

      ' Okay, we're going to start listening. See if an explicit port is
      ' defined for us, and fallback to the default.
      If Me.TcpPort <= 0 Then
        Me.TcpPort = 10005
      End If

      Dim localEndPoint = New IPEndPoint(Dns.GetHostEntry(Environment.MachineName).AddressList(0), Me.TcpPort)

      Debug.WriteLine(localEndPoint.ToString())

      Me._listener = New TcpListener(localEndPoint)
      Me._listener.Start()
      Me._initInProgress = False

      ' Initiate the new thread to wait for a connecting client.
      ' The reason this is async is that we don't want to block
      ' the "write" method.
      '@@@ This is broken
      'Dim result = Me._listener.BeginAcceptTcpClient(Me.ClientConnected, Nothing)
      'Catch
      '    Me._initInProgress = False
      'End Try
    End If
  End Sub

  ''' <summary>
  ''' Retrieves the property values.
  ''' </summary>
  ''' Added on: 6/12/2009 By: dm11086
  Private Sub RetrievePropertyValues()
    'Array.ForEach(Me.GetSupportedAttributes(), Function(supAttrib) Do
    '         Dim prop = Me.[GetType]().GetProperty(supAttrib)
    '         If prop IsNot Nothing AndAlso Me.Attributes.ContainsKey(supAttrib) Then
    '             Dim ic As IConvertible = DirectCast(Me.Attributes(supAttrib), IConvertible)
    '             Dim targetType As Type = prop.PropertyType
    '             prop.SetValue(Me, ic.ToType(targetType, CultureInfo.CurrentCulture), Nothing)
    '         End If
    'End Function)
  End Sub

  ''' <summary>
  ''' This is the main method that writes data to the network stream.
  ''' It checks to see whether we're ready to write data, and if not,
  ''' then it just throws it away.
  ''' Also, if we think we can write data but we fail, then chances are
  ''' that the connection was broken, and we need to re-init it.
  ''' </summary>
  ''' <param name="message">Message</param>
  Private Sub WriteString(message As String)
    ' This should be called only on the first call to WriteString.
    If Not Me._readyToWrite AndAlso Not Me._initInProgress Then
      Me.InitializeEndpoint()
    End If

    If Me._readyToWrite Then
      ' Convert the string into a buffer of bytes.
      Dim buff As Byte() = System.Text.Encoding.ASCII.GetBytes(message)
      'Try
      Me._networkStream.Write(buff, 0, buff.Length)
      'Catch
      '    ' We have a network stream, but for some reason we can't
      '    ' write to it. This suggests that it disconnected. So
      '    ' re-try to initialize
      '    Me.InitializeEndpoint()
      'End Try
      ' Throw the message away if there's nobody to hear it.
      'System.Diagnostics.Debug.Write(message);
    Else
    End If
  End Sub

  'private string WriteTraceOptions(TraceEventCache eventCache)
  '{
  '   var sb = new StringBuilder();

  '   if (eventCache != null)
  '   {
  '      this.IndentLevel++;

  '      if (this.IsEnabled(TraceOptions.ProcessId))
  '      {
  '         sb.AppendLine("ProcessId=" + eventCache.ProcessId);
  '      }

  '      if (this.IsEnabled(TraceOptions.LogicalOperationStack))
  '      {
  '         sb.Append("LogicalOperationStack=");
  '         Stack logicalOperationStack = eventCache.LogicalOperationStack;
  '         bool flag = true;

  '         foreach (object obj2 in logicalOperationStack)
  '         {
  '            if (!flag)
  '            {
  '               sb.Append(", ");
  '            }
  '            else
  '            {
  '               flag = false;
  '            }

  '            sb.Append(obj2.ToString());
  '         }

  '         sb.AppendLine(string.Empty);
  '      }

  '      if (this.IsEnabled(TraceOptions.ThreadId))
  '      {
  '         sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "ThreadId={0}", eventCache.ThreadId));
  '      }

  '      if (this.IsEnabled(TraceOptions.DateTime))
  '      {
  '         sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "DateTime={0}", eventCache != null ? eventCache.DateTime.ToLocalTime().ToString("o", CultureInfo.InvariantCulture) : DateTime.Now.ToString("0", CultureInfo.InstalledUICulture)));
  '         sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "UtcDateTime={0}", eventCache != null ? eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture) : DateTime.UtcNow.ToString("0", CultureInfo.InstalledUICulture)));
  '      }

  '      if (this.IsEnabled(TraceOptions.Timestamp))
  '      {
  '         sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "Timestamp={0}", +eventCache.Timestamp));
  '      }

  '      if (this.IsEnabled(TraceOptions.Callstack))
  '      {
  '         sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "Callstack={0}", eventCache.Callstack));
  '      }

  '      this.IndentLevel--;
  '   }

  '   if (sb.Length > 0)
  '   {
  '      sb.Insert(0, String.Format(CultureInfo.InvariantCulture, "{0}=====Trace Options===={0}", Microsoft.VisualBasic.ControlChars.CrLf));
  '   }

  '   return sb.ToString();
  '}
#End Region
End Class