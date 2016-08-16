' ***********************************************************************
' Assembly         : dotNetTips.Utility.Windows.Logger
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 05-12-2016
' ***********************************************************************
' <copyright file="XmlTraceListener.vb" company="McCarter Consulting">
'     Copyright © dotNetTips.com 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Diagnostics.Contracts
Imports System.Globalization
Imports System.IO
Imports System.Security.Permissions
Imports System.Text
Imports System.Threading
Imports System.Xml.XPath
Imports dotNetTips.Utility.Extensions
Imports dotNetTips.Utility.IO

''' <summary>
''' TraceListener that saves events to xml file.
''' </summary>
''' <seealso cref="dotNetTips.Utility.Windows.Logger.LoggingTraceListener" />
Public Class XmlTraceListener
    Inherits LoggingTraceListener
    ''' <summary>
    ''' File lock timeout
    ''' </summary>
    Private Const FileLockTimeout As Integer = 9000
    ''' <summary>
    ''' file lock
    ''' </summary>
    Private Shared _fileLock As New Object()
    ''' <summary>
    ''' last file check
    ''' </summary>
    Private Shared _lastFileCheck As New DateTime()
    ''' <summary>
    ''' process identifier
    ''' </summary>
    Private Shared _processId As Integer
    ''' <summary>
    ''' process name
    ''' </summary>
    Private Shared _processName As String
    ''' <summary>
    ''' backup files
    ''' </summary>
    Private _backupFiles As Boolean
    ''' <summary>
    ''' days to keep files
    ''' </summary>
    Private _daysToKeepFiles As Integer
    ''' <summary>
    ''' file name
    ''' </summary>
    Private _fileName As String
    ''' <summary>
    ''' ignore category
    ''' </summary>
    Private _ignoreCategory As String
    ''' <summary>
    ''' include HTTP information on exception
    ''' </summary>
    Private _includeHttpInfoOnException As Boolean
    ''' <summary>
    ''' trace builder
    ''' </summary>
    Private _traceBuilder As StringBuilder

#Region "Constructors"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="XmlTraceListener"/> class.
    ''' </summary>
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="XmlTraceListener" /> class.
    ''' </summary>
    ''' <param name="fileName">Name of the file.</param>
    Public Sub New(ByVal fileName As String)
        Me.New(VerifyFileAndPath(fileName), False)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="XmlTraceListener" /> class.
    ''' </summary>
    ''' <param name="fileName">Name of the file.</param>
    ''' <param name="backupFiles">if set to <c>true</c> [backup files].</param>
    Public Sub New(ByVal fileName As String, ByVal backupFiles As Boolean)
        Me.New("unnamed", VerifyFileAndPath(fileName), backupFiles, TraceOptions.None)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="XmlTraceListener" /> class.
    ''' </summary>
    ''' <param name="name">The name.</param>
    ''' <param name="fileName">Name of the file.</param>
    ''' <param name="backupFiles">if set to <c>true</c> [backup files].</param>
    ''' <param name="traceOutputOptions">The trace output options.</param>
    Public Sub New(ByVal name As String, ByVal fileName As String, ByVal backupFiles As Boolean, ByVal traceOutputOptions As TraceOptions)
        MyBase.New(name)
        Me.TraceOutputOptions = traceOutputOptions
        Me.FileName = VerifyFileAndPath(fileName)
        Me.BackupFiles = backupFiles
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="XmlTraceListener" /> class.
    ''' </summary>
    ''' <param name="name">The name.</param>
    ''' <param name="fileName">Name of the file.</param>
    ''' <param name="backupFiles">if set to <c>true</c> [backup files].</param>
    ''' <param name="traceOutputOptions">The trace output options.</param>
    ''' <param name="filter">The filter.</param>
    Public Sub New(ByVal name As String, ByVal fileName As String, ByVal backupFiles As Boolean, ByVal traceOutputOptions As TraceOptions, ByVal filter As TraceFilter)
        MyBase.New(name)
        Me.TraceOutputOptions = traceOutputOptions
        Me.Filter = filter
        Me.FileName = VerifyFileAndPath(fileName)
        Me.BackupFiles = backupFiles
    End Sub
#End Region

#Region "Public Properties"
    ''' <summary>
    ''' Gets or sets a value indicating whether [backup files].
    ''' </summary>
    ''' <value><c>true</c> if [backup files]; otherwise, <c>false</c>.</value>
    Public Property BackupFiles() As Boolean
        Get
            Return _backupFiles
        End Get
        Set(ByVal value As Boolean)
            _backupFiles = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or sets the days to keep files.
    ''' </summary>
    ''' <value>The days to keep files.</value>
    Public Property DaysToKeepFiles() As Integer
        Get
            Return _daysToKeepFiles
        End Get
        Set(ByVal value As Integer)
            _daysToKeepFiles = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name of the file.
    ''' </summary>
    ''' <value>The name of the file.</value>
    Public Property FileName() As String
        Get
            Return _fileName
        End Get
        Set(ByVal value As String)
            _fileName = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or sets the ignore category.
    ''' </summary>
    ''' <value>The ignore category.</value>
    Public Property IgnoreCategory() As String
        Get
            Return _ignoreCategory
        End Get
        Set(value As String)
            _ignoreCategory = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or sets the include HTTP info on exception.
    ''' </summary>
    ''' <value>The include HTTP info on exception.</value>
    Public Property IncludeHttpInfoOnException() As Boolean
        Get
            Return _includeHttpInfoOnException
        End Get
        Set(value As Boolean)
            _includeHttpInfoOnException = value
        End Set
    End Property
#End Region

#Region "Public Methods"
    ''' <summary>
    ''' Writes trace information, a data object and event information to the
    ''' listener specific output.
    ''' </summary>
    ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" />
    ''' object that contains the current process ID, thread ID, and stack trace information.</param>
    ''' <param name="source">A name used to identify the output, typically the name of
    ''' the application that generated the trace event.</param>
    ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" />
    ''' values specifying the type of event that has caused the trace.</param>
    ''' <param name="id">A numeric identifier for the event.</param>
    ''' <param name="data">The trace data to emit.</param>
    ''' <PermissionSet>
    '''   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    '''   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
    ''' </PermissionSet>
    Public Overloads Overrides Sub TraceData(ByVal eventCache As TraceEventCache, ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal data As Object)
        If Filter IsNot Nothing AndAlso Not Filter.ShouldTrace(Nothing, NameOf(Trace), eventType, 0, String.Empty, Nothing,
        data, Nothing) Then
            Exit Sub
        End If

        If TypeOf data Is String Then
            Me.TraceEvent(eventCache, NameOf(Trace), eventType, id, data.ToString())
            Exit Sub
        End If

        Me.RetrievePropertyValues()

        Me.WriteHeader(source, eventType, id, eventCache)

        Me.InternalWrite("<TraceData>")

        If data IsNot Nothing Then
            Me.InternalWrite("<DataItem>")

            If TypeOf data Is LogEntry Then
                Dim entry = DirectCast(data, LogEntry)

                'Check for category to ignore
                If String.IsNullOrEmpty(Me.IgnoreCategory) = False Then
                    Dim cats = Me.IgnoreCategory.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
                    If cats.Any(Function(p) p.ToUpper(CultureInfo.CurrentCulture).Trim() = entry.Category.ToString().ToUpper(CultureInfo.CurrentCulture)) Then
                        Return
                    End If
                End If

                If Me.IncludeHttpInfoOnException Then
                    'entry.LoadHttpInformation()
                End If

                Me.InternalWrite(XmlTraceListener.GetXml(entry).OuterXml.ToString(CultureInfo.CurrentCulture))
            Else
                Me.WriteData(data)
            End If

            Me.InternalWrite("</DataItem>")
        End If

        Me.InternalWrite("</TraceData>")

        Me.WriteFooter(eventCache)

        Me.AppendFile()
    End Sub

    ''' <summary>
    ''' Writes trace information, a message, and event information to the listener
    ''' specific output.
    ''' </summary>
    ''' <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" />
    ''' object that contains the current process ID, thread ID, and stack trace information.</param>
    ''' <param name="source">A name used to identify the output, typically the name of
    ''' the application that generated the trace event.</param>
    ''' <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" />
    ''' values specifying the type of event that has caused the trace.</param>
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

        Me.RetrievePropertyValues()

        Me.WriteHeader(source, eventType, id, eventCache)

        Me.WriteEscaped(message)

        Me.WriteFooter(eventCache)

        Me.AppendFile()
    End Sub

    ''' <summary>
    ''' When overridden in a derived class, writes the specified message to the
    ''' listener you create in the derived class.
    ''' </summary>
    ''' <param name="message">A message to write.</param>
    Public Overloads Overrides Sub Write(ByVal message As String)
        Me.TraceEvent(Nothing, NameOf(Trace), TraceEventType.Information, 0, message)
    End Sub

    ''' <summary>
    ''' When overridden in a derived class, writes a message to the listener
    ''' you create in the derived class, followed by a line terminator.
    ''' </summary>
    ''' <param name="message">A message to write.</param>
    Public Overloads Overrides Sub WriteLine(ByVal message As String)
        Me.Write(message)
    End Sub
#End Region

#Region "Internal Methods"
    ''' <summary>
    ''' Verifies the file and path.
    ''' </summary>
    ''' <param name="fileName">Name of the file.</param>
    ''' <returns>Valid file name and path.</returns>
    ''' <remarks>ASP.NET application should provide full path and file name that the user the
    ''' process is running under has full write/delete rights.</remarks>
    Friend Shared Function VerifyFileAndPath(ByVal fileName As String) As String
        Dim newFileName = String.Empty
        newFileName = If(String.IsNullOrEmpty(Path.GetDirectoryName(fileName)), (Path.Combine(DirectoryHelper.AppApplicationDataFolder, Path.GetFileName(fileName))), fileName)

        'Ensure directory exists, if not create
        If Directory.Exists(Path.GetDirectoryName(newFileName)) = False Then
            Directory.CreateDirectory(Path.GetDirectoryName(newFileName))
        End If

        Return newFileName
    End Function

#End Region

#Region "Protected Methods"
    ''' <summary>
    ''' Gets the custom attributes supported by the trace listener.
    ''' </summary>
    ''' <returns>A string array naming the custom attributes supported by the trace listener, or null if there are no custom attributes.</returns>
    Protected Overloads Overrides Function GetSupportedAttributes() As String()
        Return New String(3) {NameOf(BackupFiles), NameOf(DaysToKeepFiles), NameOf(IgnoreCategory), NameOf(IncludeHttpInfoOnException)}
    End Function
#End Region

#Region "Private Static Methods"
    ''' <summary>
    ''' Gets the name of the process.
    ''' </summary>
    ''' <returns>System.String.</returns>
    Private Shared Function GetProcessName() As String
        InitProcessInfo()
        Return _processName
    End Function

    ''' <summary>
    ''' Gets the thread identifier.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    Private Shared Function GetThreadId() As Integer
        Return Thread.CurrentThread.ManagedThreadId
    End Function

    ''' <summary>
    ''' Gets the XML.
    ''' </summary>
    ''' <param name="logEntry">The log entry.</param>
    ''' <returns>Returns the LogEntry object as a serialized XPathNavigator object.</returns>
    Private Shared Function GetXml(ByVal logEntry As LogEntry) As XPathNavigator
        Return New XPathDocument(New StringReader(Xml.XmlHelper.Serialize(logEntry))).CreateNavigator()
    End Function
#End Region

#Region "Private Methods"
    ''' <summary>
    ''' Inits the process info.
    ''' </summary>
    ''' Added on: 6/11/2009 By: dm11086
    Private Shared Sub InitProcessInfo()
        Dim permission = New SecurityPermission(SecurityPermissionFlag.UnmanagedCode)
        permission.Demand()

        If _processName IsNot Nothing Then
            Return
        End If

        Using process As Process = Process.GetCurrentProcess()
            _processId = process.Id
            _processName = process.ProcessName
        End Using
    End Sub
    ''' <summary>
    ''' Appends xml the file.
    ''' </summary>
    Private Sub AppendFile()
        'Verify Directory
        If Not New FileInfo(Me.FileName).Directory.Exists Then
            Trace.WriteLine("Can not write xml log file. Directory does not exist.")
            Exit Sub
        End If

        Me.BackupFile()

        If Monitor.TryEnter(_fileLock, FileLockTimeout) Then
            Try
                Using fs = File.Open(Me.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None)
                    Const rootBeginNode = "<TraceEvents>"
                    Const rootCloseNode = "</TraceEvents>"

                    If fs.Length <> 0 Then
                        fs.Seek(-rootCloseNode.Length, System.IO.SeekOrigin.[End])
                    Else
                        Me._traceBuilder.Insert(0, [String].Format(CultureInfo.InvariantCulture, "<?xml version=""1.0"" encoding=""utf-8""?>{0}", rootBeginNode))
                    End If

                    Me._traceBuilder.Append(rootCloseNode)

                    Dim encoded = New ASCIIEncoding().GetBytes(Me._traceBuilder.ToString())
                    Try
                        fs.Write(encoded, 0, encoded.Length)
                    Catch ex As IOException
                        Trace.WriteLine(ex.ToString())
                    End Try
                End Using
            Finally
                Monitor.[Exit](_fileLock)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Backups the file.
    ''' </summary>
    Private Sub BackupFile()
        If DateTime.Now.[Date] < _lastFileCheck.AddDays(1).[Date] Then
            Exit Sub
        End If

        'Backup file
        If Me.BackupFiles AndAlso File.Exists(Me.FileName) AndAlso (File.GetLastWriteTime(Me.FileName).[Date] < DateTime.Now.[Date]) Then

            Dim info = New FileInfo(Me.FileName)
            Dim newFileName = Path.Combine(info.DirectoryName, [String].Format(CultureInfo.InvariantCulture, "{0}{1}.bak", info.LastWriteTime.ToString("yyyy-MM-dd-", CultureInfo.InvariantCulture), info.Name))

            Try
                info.MoveTo(newFileName)
            Catch ex As IOException
                Trace.WriteLine(ex.ToString())
            End Try
        End If

        Me.CleanupFiles()
        _lastFileCheck = DateTime.Now
    End Sub

    ''' <summary>
    ''' Cleanups the old log files.
    ''' </summary>
    Private Sub CleanupFiles()
        If String.IsNullOrEmpty(Me.FileName) <> False OrElse Me.DaysToKeepFiles <= 0 Then
            Exit Sub
        End If

        Dim files = From fi In Directory.GetFiles(Path.GetDirectoryName(Me.FileName), [String].Format("*{0}*.bak", Path.GetFileName(Me.FileName)))
                    Where DateTime.Now.[Date] > Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(fi).LastWriteTime.AddDays(Me.DaysToKeepFiles).[Date]
                    Select fi

        For Each item In files
            Try
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(item)
            Catch ex As IOException
                Trace.WriteLine(ex.Message)
            End Try
        Next
    End Sub

    ''' <summary>
    ''' Writes a message to the internal string builder.
    ''' </summary>
    ''' <param name="message">The message.</param>
    Private Sub InternalWrite(ByVal message As String)
        Me._traceBuilder.Append(message)
    End Sub

    ''' <summary>
    ''' Writes the data.
    ''' </summary>
    ''' <param name="data">The data.</param>
    Private Sub WriteData(ByVal data As Object)
        Contract.Requires(Of ArgumentNullException)(data IsNot Nothing)

        Me.InternalWrite(data.ToString())
    End Sub

    ''' <summary>
    ''' Writes the end header.
    ''' </summary>
    Private Sub WriteEndHeader()
        Me.InternalWrite(""" />")
        Me.InternalWrite("<Execution ProcessName=""")
        Me.InternalWrite(Threading.ThreadHelper.GetProcessName())
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
    ''' Writes the escaped.
    ''' </summary>
    ''' <param name="input">The input.</param>
    Private Sub WriteEscaped(ByVal input As String)
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(input) = False)

        Me.InternalWrite(input.HtmlEncode)
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

            Me.InternalWrite(If(eventCache IsNot Nothing, eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)))

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
    ''' Writes the start header.
    ''' </summary>
    ''' <param name="source">The source.</param>
    ''' <param name="eventType">Type of the event.</param>
    ''' <param name="id">The id.</param>
    ''' <param name="eventCache">The event cache.</param>
    Private Sub WriteStartHeader(ByVal source As String, ByVal eventType As TraceEventType, ByVal id As Integer, ByVal eventCache As TraceEventCache)

        Me._traceBuilder = New StringBuilder()

        Me.InternalWrite("<TraceEvent><System>")

        Me.InternalWrite("<EventId>")

        Me.InternalWrite(CUInt(id).ToString(CultureInfo.InvariantCulture))

        Me.InternalWrite("</EventId>")

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
#End Region

End Class