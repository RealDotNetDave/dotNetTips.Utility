'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 06-09-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System
Imports System.Collections
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Xml.XPath
Imports System.Threading.Tasks
Imports System.Threading

Namespace Logging
    ''' <summary>
    ''' TraceListener that sends email.
    ''' </summary>
    Public Class WebServiceTraceListener
        Inherits LoggingTraceListener
        Private _traceBuilder As StringBuilder
        Private Const DefaultBatchSize As Integer = 10
        Private Const DefaultCacheDuration As Integer = 10
        Private Const DefaultMaxCacheItems As Integer = 100
        Private Shared _processId As Integer
        Private Shared _processName As String
        Private Shared _queue As System.Collections.Generic.Queue(Of String)
        Private _schedulingTimer As Timer

#Region "Constructors"
        ''' <summary>
        ''' Initializes a new instance of the <see cref="EmailTraceListener"/> class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
            Me.BatchSize = DefaultBatchSize
            Me.CacheDuration = DefaultCacheDuration
            Me.MaxCacheItems = DefaultMaxCacheItems

        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="WebServiceTraceListener"/> class.
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="traceOutputOptions">The trace output options.</param>
        Public Sub New(ByVal name As String, ByVal traceOutputOptions As TraceOptions)
            MyBase.New(name)
            Me.TraceOutputOptions = traceOutputOptions
            Me.BatchSize = DefaultBatchSize
            Me.CacheDuration = DefaultCacheDuration
            Me.MaxCacheItems = DefaultMaxCacheItems
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="WebServiceTraceListener"/> class.
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="traceOutputOptions">The trace output options.</param>
        ''' <param name="filter">The filter.</param>
        Public Sub New(ByVal name As String, ByVal traceOutputOptions As TraceOptions, ByVal filter As TraceFilter)
            MyBase.New(name)
            Me.TraceOutputOptions = traceOutputOptions
            Me.Filter = filter
            Me.BatchSize = DefaultBatchSize
            Me.CacheDuration = DefaultCacheDuration
            Me.MaxCacheItems = DefaultMaxCacheItems
        End Sub

#End Region

#Region "Public Properties"
        Public BatchSize As Integer
        Public CacheDuration As Integer
        Public MaxCacheItems As Integer
        Public IgnoreCategory As String
        Public IncludeHttpInfoOnException As Boolean
#End Region

#Region "Public Methods"
        ''' <summary>
        ''' When overridden in a derived class, writes the specified message to the listener you create in the derived class.
        ''' </summary>
        ''' <param name="message">A message to write.</param>
        Public Overloads Overrides Sub Write(ByVal message As String)
            Me.WriteLine(message)
        End Sub

        ''' <summary>
        ''' When overridden in a derived class, writes a message to the listener you
        ''' create in the derived class, followed by a line terminator.
        ''' </summary>
        ''' <param name="message">A message to write.</param>
        Public Overloads Overrides Sub WriteLine(ByVal message As String)
            'Check for first item
            If _queue Is Nothing Then
                _queue = New ThreadSafeQueue(Of String)(Me.MaxCacheItems)

                Me.StartPolling()
            End If

            _queue.Enqueue(message)

            'Check for max items in queue
            If _queue.Count >= Me.MaxCacheItems Then
                Me.SchedulingTimerElapsed(Nothing)
            End If
        End Sub

        ''' <summary>
        ''' Writes trace information, a message, and event information to the listener specific output.
        ''' </summary>
        ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache"/> object that
        ''' contains the current process ID, thread ID, and stack trace information.</param>
        ''' <param name="source">A name used to identify the output, typically the name of the
        ''' application that generated the trace event.</param>
        ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType"/>
        ''' values specifying the type of event that has caused the trace.</param>
        ''' <param name="id">A numeric identifier for the event.</param>
        ''' <param name="message">A message to write.</param>
        ''' <PermissionSet>
        ''' <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        ''' <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/>
        ''' </PermissionSet>
        Public Overloads Overrides Sub TraceEvent(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal message As String)

            If Filter IsNot Nothing AndAlso Not Filter.ShouldTrace(Nothing, "Trace", eventType, 0, message, Nothing, _
            Nothing, Nothing) Then
                Exit Sub
            End If

            Me.RetrievePropertyValues()

            Me.WriteHeader(source, eventType, id, eventCache)

            Me.WriteEscaped(message)

            Me.WriteFooter(eventCache)

            Me.Write(Me._traceBuilder.ToString())
        End Sub

        ''' <summary>
        ''' Writes trace information, a data object and event information to the listener specific output.
        ''' </summary>
        ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        ''' <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType"/> values specifying the type of event that has caused the trace.</param>
        ''' <param name="id">A numeric identifier for the event.</param>
        ''' <param name="data">The trace data to emit.</param>
        ''' <PermissionSet>
        ''' <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        ''' <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/>
        ''' </PermissionSet>
        Public Overloads Overrides Sub TraceData(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal data As Object)

            If Filter IsNot Nothing AndAlso Not Filter.ShouldTrace(Nothing, "Trace", eventType, 0, String.Empty, Nothing, _
            data, Nothing) Then
                Exit Sub
            End If

            If data IsNot Nothing Then
                Me.InternalWrite("<DataItem>")

                If TypeOf data Is LogEntry Then
                    Dim entry = DirectCast(data, LogEntry)

                    'Check for category to ignore
                    If String.IsNullOrEmpty(Me.IgnoreCategory) = False Then
                        Dim cats = Me.IgnoreCategory.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
                        If cats.Any(Function(p) p.ToUpper().Trim() = entry.Category.ToString().ToUpper()) Then
                            Return
                        End If
                    End If

                    If Me.IncludeHttpInfoOnException Then
                        'entry.LoadHttpInformation()
                    End If

                    Me.InternalWrite(WebServiceTraceListener.GetXml(entry).OuterXml.ToString())
                Else
                    Me.WriteData(data)
                End If

                Me.InternalWrite("</DataItem>")
            End If

            Me.InternalWrite("</TraceData>")

            Me.WriteFooter(eventCache)

            Me.Write(Me._traceBuilder.ToString())

        End Sub

        Public Overrides Sub Flush()
            Me.CallWebService()
            MyBase.Flush()
        End Sub
#End Region

#Region "Internal Methods"
#End Region

#Region "Protected Methods"
        ''' <summary>
        ''' Gets the custom attributes supported by the trace listener.
        ''' </summary>
        ''' <returns>
        ''' A string array naming the custom attributes supported by the
        ''' trace listener, or null if there are no custom attributes.
        ''' </returns>
        Protected Overloads Overrides Function GetSupportedAttributes() As String()
            Return New String(4) {"MaxCacheItems", "BatchSize", "CacheDuration", "IgnoreCategory", "IncludeHttpInfoOnException"}
        End Function
#End Region

#Region "Private Static Methods"
        ''' <summary>
        ''' Gets the XML.
        ''' </summary>
        ''' <param name="logEntry">The log entry.</param>
        ''' <returns>XPathNavigator based on serialized LogEntry object.</returns>
        Private Shared Function GetXml(ByVal logEntry As LogEntry) As XPathNavigator
            Return New XPathDocument(New StringReader(Xml.XmlHelper.Serialize(logEntry))).CreateNavigator()
        End Function

        ''' <summary>
        ''' Calls the web service.
        ''' </summary>
        ''' <param name="xmlData">The XML data to send.</param>
        Private Sub CallWebService(ByVal xmlData As String)

            '            Dim baseUrl = [Global].RemoteServerUrl.Trim()

            '            If String.IsNullOrEmpty(baseUrl) Then
            '                baseUrl = Mitchell.Repair.Common.Properties.Settings.[Default].DefaultSiteUrl.Trim()
            '            End If
            '            If String.IsNullOrEmpty(baseUrl) Then
            '                Return
            '            End If

            'Using service = New Mitchell.Repair.ServiceReferences.DataService.DataService() With { _
            '	Key .Url = GeneralFunctions.AddUrlSlash(baseUrl) + GlobalSettings.Settings.[Default].DataWebService, _
            '	Key .AuthenticationHeaderValue = New Mitchell.Repair.System.ServiceReferences.DataService.AuthenticationHeader() With { _
            '		Key .UserName = GlobalSettings.Settings.[Default].LogOnWebServiceUserName, _
            '		Key .Password = GlobalSettings.Settings.[Default].LogOnWebSerivcePassword _
            '	} _
            '}
            '                Try
            '                    service.Timeout = 5000

            '                    If LogManagement.IsAspApp() Then
            '                        service.LogEvent(xmlData)
            '                    Else
            '                        service.LogEventAsync(xmlData)
            '                    End If
            '                Catch
            '                Finally
            '                    'Eat any exceptions, don't want to get into an endless loop.
            '                    service.Dispose()
            '                End Try
            '            End Using

        End Sub

        Private Shared Function GetProcessName() As String
            InitProcessInfo()
            Return _processName
        End Function

        ''' <summary>
        ''' Inits the process info.
        ''' </summary>
        ''' Added on: 6/11/2009 By: dm11086
        Private Shared Sub InitProcessInfo()
            Dim t = Task.Factory.StartNew(Function() New SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand())
            If _processName Is Nothing Then
                Using process__1 As Process = Process.GetCurrentProcess()
                    _processId = process__1.Id
                    _processName = process__1.ProcessName
                End Using
            End If
        End Sub

        Private Shared Function GetThreadId() As Integer
            Return Thread.CurrentThread.ManagedThreadId
        End Function

#End Region

#Region "Private Methods"

        Private Sub CallWebService()
            If _queue Is Nothing Then
                Return
            End If

            Dim header = New AuthenticationHeader() With {
        Key.UserName = GlobalSettings.Settings.[Default].LogOnWebServiceUserName,
        Key.Password = GlobalSettings.Settings.[Default].LogOnWebSerivcePassword
            }

            'load the URL
            Dim url As String = Mitchell.Repair.Common.[Global].RemoteServerUrl

            Try
                'Try to get from web.config
                'If this can be removed, this assembly can be set to Client Framework only @@@
                If String.IsNullOrEmpty(url) Then
                    url = WebConfigurationManager.AppSettings("RemoteServer")
                End If
            Catch
            End Try

            If String.IsNullOrEmpty(url) Then
                url = Mitchell.Repair.Common.Properties.Settings.[Default].DefaultSiteUrl
            End If

            If String.IsNullOrEmpty(url) Then
                'We are screwed, get out!
                Return
            End If

            url = GeneralFunctions.CombineUrlPaths(url, Mitchell.Repair.Common.Properties.Settings.[Default].LoggingService)

            Try
                Using service = New LoggingService() With { Key.AuthenticationHeaderValue = header, Key.Url = url
                }
                    Dim currentQueueCount = _queue.Count

                    While currentQueueCount > 0
                        Dim batchCount = Math.Min(Me.BatchSize, _queue.Count)
                        Dim batch = New System.Collections.Generic.List(Of String)()

                        For counter As Integer = 0 To batchCount - 1
                            batch.Add(_queue.Dequeue())
                        Next

                        service.LogEvent(batch.ToArray())

                        currentQueueCount -= batchCount
                    End While
                End Using
            Catch ex As System.Net.WebException
                'Could not communicate with the server.
                Debug.WriteLine(ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Writes the header.
        ''' </summary>
        ''' <param name="source">The source.</param>
        ''' <param name="eventType">Type of the event.</param>
        ''' <param name="id">The id.</param>
        ''' <param name="eventCache">The event cache.</param>
        Private Sub WriteHeader(ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal eventCache As TraceEventCache)
            Me.WriteStartHeader(source, eventType, id, eventCache)
            Me.WriteEndHeader()
        End Sub

        ''' <summary>
        ''' Writes the footer.
        ''' </summary>
        ''' <param name="eventCache">The event cache.</param>
        Private Sub WriteFooter(ByVal eventCache As TraceEventCache)
            Dim flag As Boolean = Me.IsEnabled(TraceOptions.LogicalOperationStack)
            Dim flag2 As Boolean = Me.IsEnabled(TraceOptions.Callstack)

            If (eventCache IsNot Nothing) AndAlso (flag OrElse flag2) Then
                Me.InternalWrite("<System.Diagnostics>")

                If flag Then
                    Me.InternalWrite("<LogicalOperationStack>")

                    Dim logicalOperationStack As Stack = eventCache.LogicalOperationStack

                    If logicalOperationStack IsNot Nothing Then
                        For Each obj2 As Object In logicalOperationStack
                            Me.InternalWrite("<LogicalOperation>")
                            Me.WriteEscaped(obj2.ToString())
                            Me.InternalWrite("</LogicalOperation>")
                        Next
                    End If

                    Me.InternalWrite("</LogicalOperationStack>")
                End If

                Me.InternalWrite("<Timestamp>")

                Me.InternalWrite(eventCache.Timestamp.ToString(CultureInfo.InvariantCulture))

                Me.InternalWrite("</Timestamp>")

                If flag2 Then
                    Me.InternalWrite("<Callstack>")
                    Me.WriteEscaped(eventCache.Callstack)
                    Me.InternalWrite("</Callstack>")
                End If

                Me.InternalWrite("</System.Diagnostics>")
            End If

            Me.InternalWrite("</ApplicationData></TraceEvent>")
        End Sub

        ''' <summary>
        ''' Writes the data.
        ''' </summary>
        ''' <param name="data">The data.</param>
        Private Sub WriteData(ByVal data As Object)
            Me.InternalWrite(data.ToString())
        End Sub

        ''' <summary>
        ''' Writes the start header.
        ''' </summary>
        ''' <param name="source">The source.</param>
        ''' <param name="eventType">Type of the event.</param>
        ''' <param name="id">The id.</param>
        ''' <param name="eventCache">The event cache.</param>
        Private Sub WriteStartHeader(ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal eventCache As TraceEventCache)
            Me._traceBuilder = New StringBuilder()

            Me.InternalWrite("<TraceEvent><System>")

            Me.InternalWrite("<EventID>")

            Me.InternalWrite(CUInt(id).ToString(CultureInfo.InvariantCulture))

            Me.InternalWrite("</EventID>")

            Me.InternalWrite("<Type>3</Type>")

            Me.InternalWrite("<SubType Name=""")

            Me.InternalWrite(eventType.ToString())

            Me.InternalWrite(""">0</SubType>")

            Me.InternalWrite("<Level>")

            Dim num As Integer = CInt(eventType)

            If num > &HFF Then
                num = &HFF
            End If

            If num < 0 Then
                num = 0
            End If

            Me.InternalWrite(num.ToString(CultureInfo.InvariantCulture))

            Me.InternalWrite("</Level>")

            Me.InternalWrite("<TimeCreated SystemUtcTime=""")

            InternalWrite(If(eventCache IsNot Nothing, eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)))

            Me.InternalWrite(""" SystemTime=""")

            InternalWrite(If(eventCache IsNot Nothing, eventCache.DateTime.ToLocalTime.ToString("o", CultureInfo.InvariantCulture), DateTime.Now.ToString("o", CultureInfo.InvariantCulture)))

            Me.InternalWrite(""" />")

            Me.InternalWrite("<Source Name=""")

            Me.WriteEscaped(source)

            Me.InternalWrite(""" />")

            Me.InternalWrite("<Correlation ActivityID=""")

            Me.InternalWrite(Guid.NewGuid().ToString("B", CultureInfo.CurrentCulture))
        End Sub

        ''' <summary>
        ''' Writes the end header.
        ''' </summary>
        Private Sub WriteEndHeader()
            Me.InternalWrite(""" />")
            Me.InternalWrite("<Execution ProcessName=""")
            Me.InternalWrite(Threading.ThreadHelper.GetProcessName)
            Me.InternalWrite(""" ThreadID=""")
            Me.WriteEscaped(Threading.ThreadHelper.GetThreadId().ToString(CultureInfo.InvariantCulture))
            Me.InternalWrite(""" />")
            Me.InternalWrite("<Channel/>")
            Me.InternalWrite("<Computer>")
            Me.InternalWrite(Environment.MachineName)
            Me.InternalWrite("</Computer>")
            Me.InternalWrite("</System>")
            Me.InternalWrite("<ApplicationData>")
        End Sub

        ''' <summary>
        ''' Writes an escaped string.
        ''' </summary>
        ''' <param name="str">The string to write.</param>
        Private Sub WriteEscaped(ByVal str As String)
            If str IsNot Nothing Then
                'Me.InternalWrite(System.Web.HttpUtility.HtmlEncode(str)) @@@
            End If
        End Sub

        ''' <summary>
        ''' Internals the write.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Private Sub InternalWrite(ByVal message As String)
            Me._traceBuilder.Append(message)
        End Sub

        Private Sub SchedulingTimerElapsed(state As Object)
            If _queue.Count <= 0 Then
                Return
            End If

            Me.StopTimer()

            Try
                'Write queue items to service;
                Me.CallWebService()
            Finally
                Me.StartTimer()
            End Try
        End Sub

        Private Sub StartPolling()
            If Me._schedulingTimer Is Nothing Then
                Me._schedulingTimer = New System.Timers.Timer(Me.SchedulingTimerElapsed)
            End If

            Me.StartTimer()
        End Sub

        ''' <summary>
        ''' Starts the timer.
        ''' </summary>
        Private Sub StartTimer()
            Dim waitTime = 60000 * Me.CacheDuration

            Me._schedulingTimer.Change((waitTime - (DateTime.Now.Second * 1000)), waitTime)
        End Sub

        ''' <summary>
        ''' Stops the timer.
        ''' </summary>
        Private Sub StopTimer()
            Me._schedulingTimer.Change(Timeout.Infinite, Timeout.Infinite)
        End Sub

#End Region
    End Class

End Namespace