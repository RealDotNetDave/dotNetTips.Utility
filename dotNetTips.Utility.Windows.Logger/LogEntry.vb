' ***********************************************************************
' Assembly         : dotNetTips.Utility.Windows.Logger
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-05-2016
' ***********************************************************************
' <copyright file="LogEntry.vb" company="McCarter Consulting">
'     Copyright © dotNetTips.com 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Reflection
Imports System.Security
Imports System.Security.Permissions
Imports System.Text
Imports System.Threading
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Devices

''' <summary>
''' Used for logging events and Exceptions
''' </summary>
''' <seealso cref="System.Xml.Serialization.IXmlSerializable" />
Public Class LogEntry
    Implements IXmlSerializable
    ''' <summary>
    ''' Element error messages
    ''' </summary>
    Private Const ElementErrorMessages As String = NameOf(ErrorMessages)
    ''' <summary>
    ''' Element execution
    ''' </summary>
    Private Const ElementExecution = "Execution"
    ''' <summary>
    ''' Element information
    ''' </summary>
    Private Const ElementInformation As String = "Info"
    ''' <summary>
    ''' default information
    ''' </summary>
    Private Shared _defaultInformation As New Collection(Of InfoItem)()

    ''' <summary>
    ''' _category
    ''' </summary>
    Private _category As Category
    ''' <summary>
    ''' error messages
    ''' </summary>
    Private _errorMessages As New Collection(Of String)()

    ''' <summary>
    ''' event identifier
    ''' </summary>
    Private _eventId As Integer

    ''' <summary>
    ''' _id
    ''' </summary>
    Private _id As String
    ''' <summary>
    ''' _information
    ''' </summary>
    Private _information As New InfoItemCollection()

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntry" /> class.
    ''' </summary>
    Public Sub New()
        Me.New(String.Empty, TraceEventType.Information, False, Nothing)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntry" /> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    Public Sub New(message As String)
        Me.New(message, TraceEventType.Information, False, Nothing)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntry" /> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="severity">The severity.</param>
    Public Sub New(message As String, severity As TraceEventType)
        Me.New(message, severity, False, Nothing)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntry"/> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="severity">The severity.</param>
    ''' <param name="information">The information.</param>
    Public Sub New(message As String, severity As TraceEventType, ParamArray information As InfoItem())
        Me.New(message, severity, False, information)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntry"/> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="severity">The severity.</param>
    ''' <param name="includeDebuggingInfo">if set to <c>true</c> [include debugging information].</param>
    ''' <param name="information">The information.</param>
    Public Sub New(message As String, severity As TraceEventType, includeDebuggingInfo As Boolean, ParamArray information As InfoItem())

        Me.LoadProperties(includeDebuggingInfo)
        Me.Message = message
        Me.Severity = severity
        Me.AddInfoItems(information)

    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntry" /> class.
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <param name="severity">The severity.</param>
    Public Sub New(ex As Exception, severity As TraceEventType)
        Me.New(String.Empty, ex, severity, Nothing)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntry" /> class.
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    Public Sub New(ex As Exception)
        Me.New(String.Empty, ex, TraceEventType.[Error], Nothing)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntry" /> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="ex">The ex.</param>
    ''' <param name="severity">The severity.</param>
    Public Sub New(message As String, ex As Exception, severity As TraceEventType)
        Me.New(message, ex, severity, Nothing)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntry"/> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="ex">The ex.</param>
    ''' <param name="severity">The severity.</param>
    ''' <param name="information">The information.</param>
    Public Sub New(message As String, ex As Exception, severity As TraceEventType, ParamArray information As InfoItem())
        Me.LoadProperties(True)
        Me.Message = message
        Me.AddException(ex)
        Me.Severity = severity
        Me.AddInfoItems(information)
    End Sub
    ''' <summary>
    ''' Gets or sets the event category.
    ''' </summary>
    ''' <value>The category.</value>
    Public Property Category() As Category
        Get
            Return _category
        End Get
        Set(ByVal value As Category)
            _category = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the error messages.
    ''' </summary>
    ''' <value>The error messages.</value>
    Public Property ErrorMessages() As Collection(Of String)
        Get
            Return Me._errorMessages
        End Get
        <System.ComponentModel.Browsable(False)>
        Private Set(value As Collection(Of String))
            Me._errorMessages = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or sets the event id. This usually comes from the trace event.
    ''' </summary>
    ''' <value>The event identifier.</value>
    Public Property EventId() As Integer
        Get
            Return _eventId
        End Get
        Set(ByVal value As Integer)
            _eventId = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or sets (private) the event id.
    ''' </summary>
    ''' <value>The identifier.</value>
    Public Property Id() As String
        Get
            Return _id
        End Get
        <System.ComponentModel.Browsable(False)>
        Private Set(ByVal value As String)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the information.
    ''' </summary>
    ''' <value>The information.</value>
    Public ReadOnly Property Information() As InfoItemCollection
        Get
            Return Me._information
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether this instance is written to data source (i.e. database).
    ''' </summary>
    ''' <value><c>true</c> if this instance is logged; otherwise, <c>false</c>.</value>
    <XmlIgnore()>
    Public Property IsLogged() As Boolean

    ''' <summary>
    ''' Gets or sets the name of the machine.
    ''' </summary>
    ''' <value>The name of the machine.</value>
    Public Property MachineName() As String

    ''' <summary>
    ''' Gets or sets the event message.
    ''' </summary>
    ''' <value>The message.</value>
    Public Property Message() As String
    ''' <summary>
    ''' Gets or sets the process id.
    ''' </summary>
    ''' <value>The process id.</value>
    Public Property ProcessId() As Integer

    ''' <summary>
    ''' Gets or sets the name of the process.
    ''' </summary>
    ''' <value>The name of the process.</value>
    Public Property ProcessName() As String

    ''' <summary>
    ''' Gets or sets the event severity.
    ''' </summary>
    ''' <value>The severity.</value>
    Public Property Severity() As TraceEventType

    ''' <summary>
    ''' Gets the name of the severity.
    ''' </summary>
    ''' <value>The name of the severity.</value>
    <XmlIgnore()>
    Public ReadOnly Property SeverityName() As String
        Get
            Return Me.Severity.ToString()
        End Get
    End Property
    ''' <summary>
    ''' Gets or sets the source (usually code method) of where the event occurred.
    ''' </summary>
    ''' <value>The source.</value>
    Public Property Source() As String

    ''' <summary>
    ''' Gets or sets the source version (usually the assembly version).
    ''' </summary>
    ''' <value>The source version.</value>
    Public Property SourceVersion() As String

    ''' <summary>
    ''' Gets or sets the thread id.
    ''' </summary>
    ''' <value>The thread id.</value>
    Public Property ThreadId() As Integer

    ''' <summary>
    ''' Gets or sets the time stamp.
    ''' </summary>
    ''' <value>The time stamp.</value>
    ''' <remarks>Default to current UTC time.</remarks>
    Public Property TimeStamp() As DateTime

    ''' <summary>
    ''' Gets or sets the event title.
    ''' </summary>
    ''' <value>The title.</value>
    Public Property Title() As String
    ''' <summary>
    ''' Gets or sets the user.
    ''' </summary>
    ''' <value>The user.</value>
    Public Property User() As String

#End Region

#Region "Private Properties"
    ''' <summary>
    ''' Gets the current assembly info.
    ''' </summary>
    ''' <value>The current assembly info.</value>
    Private Shared ReadOnly Property CurrentAssemblyInfo() As AssemblyInfo
        Get
            Return New ApplicationBase().Info
        End Get
    End Property

    ''' <summary>
    ''' Gets the current computer information.
    ''' </summary>
    ''' <value>The current computer information.</value>
    Private Shared ReadOnly Property CurrentComputerInfo() As ComputerInfo
        Get
            Return ComputerHelper.ComputerInformation()
        End Get
    End Property

    ''' <summary>
    ''' Adds an error message to the <see cref="ErrorMessages" /> collection.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <exception cref="System.ArgumentNullException"></exception>
    Public Overridable Sub AddErrorMessage(ByVal message As String)
        If String.IsNullOrEmpty(message) Then
            Throw New ArgumentNullException(NameOf(message))
        End If
        Me.ErrorMessages.Add(message)
    End Sub

    ''' <summary>
    ''' Adds the exception messages to the <see cref="ErrorMessages" /> collection.
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <exception cref="System.ArgumentNullException"></exception>
    Public Overridable Sub AddException(ByVal ex As Exception)
        If ex Is Nothing Then
            Throw New ArgumentNullException(NameOf(ex))
        End If

        Me.Exception = ex
        Me.LogExceptionMessage(ex)
    End Sub

#Region "Serialization"

    ''' <summary>
    ''' This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.
    ''' </summary>
    ''' <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.</returns>
    ''' <exception cref="System.NotImplementedException"></exception>
    Public Function GetSchema() As System.Xml.Schema.XmlSchema Implements IXmlSerializable.GetSchema
        Throw New NotImplementedException()
    End Function

    'Private ReadOnly Property CurrentComputerScreen() As Windows.Forms.Screen
    '    Get
    '        Return New Microsoft.VisualBasic.Devices.Computer().Screen
    '    End Get
    'End Property
#End Region

#Region "Public Methods"

    Private _exception As Exception

    Public Property Exception() As Exception
        Get
            Return Me._exception
        End Get
        Private Set(value As Exception)
            Me._exception = value
        End Set
    End Property

    ''' <summary>
    ''' Logs the exception messages.
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <exception cref="System.ArgumentNullException"></exception>
    Public Overridable Sub LogExceptionMessage(ByVal ex As Exception)
        If ex Is Nothing Then
            Throw New ArgumentNullException(NameOf(ex))
        End If

        Dim eb As New StringBuilder()

        For Each tempException In Me.RetrieveAllExceptons(ex)
            eb.AppendLine(If(ErrorMessages.Count = 0, My.Resources.ExceptionDetails, My.Resources.InnerExceptionDetails))
            eb.AppendLine(My.Resources.LineSeparator)
            eb.AppendLine(String.Format(CultureInfo.InvariantCulture, "{0}: {1}", My.Resources.ExceptionType, tempException.[GetType]().FullName))

            ReflectException(tempException, eb)

            ' Record the StackTrace with separate label.
            If tempException.StackTrace IsNot Nothing Then
                eb.AppendLine(My.Resources.ExceptionStackTraceDetails)
                eb.AppendLine(My.Resources.LineSeparator)
                eb.AppendLine(tempException.StackTrace)
            End If

            Me.AddErrorMessage(eb.ToString())
            eb.Clear()
        Next
    End Sub

    ''' <summary>
    ''' Generates an object from its XML representation.
    ''' </summary>
    ''' <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
    Public Sub ReadXml(ByVal reader As System.Xml.XmlReader) Implements IXmlSerializable.ReadXml

        Dim entry = (From le In XDocument.Load(reader).Descendants(XName.[Get](NameOf(LogEntry)))
                     Select le).FirstOrDefault()

        If entry Is Nothing Then
            Exit Sub
        End If

        For Each currentElement As XElement In entry.Elements()
            Select Case currentElement.Name.LocalName
                Case NameOf(Category)
                    Me.Category = DirectCast([Enum].Parse(GetType(Category), LogEntry.ExtractElement(currentElement)), Category)
                    Exit Select
                Case NameOf(EventId)
                    Integer.Parse(LogEntry.ExtractElement(currentElement), CultureInfo.InvariantCulture)
                    Exit Select
                Case NameOf(Id)
                    Me.Id = LogEntry.ExtractElement(currentElement)
                    Exit Select
                Case NameOf(Message)
                    Me.Message = LogEntry.ExtractElement(currentElement)
                    Exit Select
                Case NameOf(Severity)
                    Me.Severity = DirectCast([Enum].Parse(GetType(TraceEventType), LogEntry.ExtractElement(currentElement)), TraceEventType)
                    Exit Select
                Case NameOf(TimeStamp)
                    Me.TimeStamp = DateTime.Parse(LogEntry.ExtractElement(currentElement), CultureInfo.InvariantCulture)
                    Exit Select
                Case NameOf(Title)
                    Me.Title = LogEntry.ExtractElement(currentElement)
                    Exit Select
                Case NameOf(User)
                    Me.User = LogEntry.ExtractElement(currentElement)
                    Exit Select
                Case NameOf(Information)
                    For Each applicationInfoElement In currentElement.Elements()
                        Select Case applicationInfoElement.Name.LocalName
                            Case NameOf(MachineName)
                                Me.MachineName = LogEntry.ExtractElement(applicationInfoElement)
                                Exit Select
                            Case NameOf(Source)
                                Me.Source = LogEntry.ExtractElement(applicationInfoElement)
                                Exit Select
                            Case ElementExecution
                                For Each att As XAttribute In applicationInfoElement.Attributes()
                                    Select Case att.Name.LocalName
                                        Case NameOf(ProcessName)
                                            ProcessName = att.Value
                                            Exit Select

                                        Case "ProcessID"
                                            Dim pid As Integer
                                            If Integer.TryParse(att.Value, pid) Then
                                                ProcessId = pid
                                            End If
                                            Exit Select

                                        Case "ThreadID"
                                            Dim tid As Integer
                                            If Integer.TryParse(att.Value, tid) Then
                                                ThreadId = tid
                                            End If
                                            Exit Select
                                    End Select
                                Next
                                Exit Select
                            Case Else
                                Exit Select
                        End Select
                    Next
                    Exit Select
                Case ElementErrorMessages
                    If currentElement.HasElements Then
                        For Each tempMessage As String In currentElement.Elements()
                            Me.AddErrorMessage(tempMessage)
                        Next
                    End If
                    Exit Select
                Case ElementInformation
                    If currentElement.HasElements Then
                        For Each tempInfo As XElement In currentElement.Elements()
                            Dim prop As String = String.Empty
                            Dim val As String = String.Empty
                            For Each el As XElement In tempInfo.Elements()
                                If el.Name = "Property" Then
                                    prop = el.Value
                                ElseIf el.Name = NameOf(Text) Then
                                    val = el.Value
                                End If
                            Next
                            If (Not String.IsNullOrWhiteSpace(prop)) AndAlso (Not String.IsNullOrWhiteSpace(val)) Then
                                Me.Information.Add(New InfoItem(prop, val))
                            End If
                        Next
                    End If
                Case Else
                    Exit Select
            End Select
        Next
    End Sub

    ''' <summary>
    ''' Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    ''' </summary>
    ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
    Public Overloads Overrides Function ToString() As String
        If String.IsNullOrEmpty(Me.Title) = False Then
            Return Me.Title
        ElseIf String.IsNullOrEmpty(Me.Message) = False Then
            Return Me.Message
        ElseIf Me.ErrorMessages.Count > 0 Then
            Dim sb = New StringBuilder()
            For Each item In Me.ErrorMessages
                sb.AppendLine(item)
            Next
            Return sb.ToString()
        Else
            Return Me.Id.ToString(CultureInfo.CurrentCulture)
        End If
    End Function

    ''' <summary>
    ''' Converts an object into its XML representation.
    ''' </summary>
    ''' <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
    Public Sub WriteXml(ByVal writer As System.Xml.XmlWriter) Implements IXmlSerializable.WriteXml
        writer.WriteElementString(NameOf(Id), Me.Id.ToString(CultureInfo.CurrentCulture))
        writer.WriteElementString(NameOf(EventId), Me.EventId.ToString(CultureInfo.InvariantCulture))
        writer.WriteElementString(NameOf(TimeStamp), Me.TimeStamp.ToString("o", CultureInfo.InvariantCulture))
        writer.WriteElementString(NameOf(Severity), Me.SeverityName)
        writer.WriteElementString(NameOf(Category), Me.Category.ToString())
        writer.WriteElementString(NameOf(User), Me.User)

        If Not String.IsNullOrEmpty(Me.Title) Then
            writer.WriteElementString(NameOf(Title), Me.Title)
        End If

        If Not String.IsNullOrEmpty(Me.Message) Then
            writer.WriteElementString(NameOf(Message), Me.Message)
        End If

        If Me.ErrorMessages.Count > 0 Then
            writer.WriteStartElement(NameOf(ErrorMessages))
            For Each item In Me.ErrorMessages
                writer.WriteElementString(NameOf(Message), item)
            Next
            writer.WriteEndElement()
        End If

        writer.WriteStartElement(NameOf(Information))

        If Not String.IsNullOrEmpty(Me.MachineName) Then
            writer.WriteElementString(NameOf(MachineName), Me.MachineName)
        End If

        If Not String.IsNullOrEmpty(Me.Source) Then
            writer.WriteElementString(NameOf(Source), Me.Source)
        End If

        writer.WriteEndElement()

        If Me.Information.Count > 0 Then
            writer.WriteStartElement(ElementInformation)
            For Each item In Me.Information.OrderBy(Function(adl) adl.[Property])
                writer.WriteElementString(item.[Property], item.Text)
            Next
            writer.WriteEndElement()
        End If
    End Sub
#End Region
#End Region
#Region "Private Static Methods"
    ''' <summary>
    ''' Extracts an xml element value.
    ''' </summary>
    ''' <param name="element">The element.</param>
    ''' <returns>Value as <see cref="String" /></returns>
    Private Shared Function ExtractElement(ByVal element As XElement) As String
        Return If(element.IsEmpty, String.Empty, element.Value)
    End Function

    ''' <summary>
    ''' Determines whether [is unmanaged code permission available].
    ''' </summary>
    ''' <returns><c>true</c> if [is unmanaged code permission available]; otherwise, <c>false</c>.</returns>
    Private Shared Function IsUnmanagedCodePermissionAvailable() As Boolean
        Dim permissionAvailable As Boolean = False

        Dim permissionSet = New PermissionSet(PermissionState.None)
        permissionSet.AddPermission(New SecurityPermission(SecurityPermissionFlag.UnmanagedCode))

        If permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet) Then
            Try
                permissionSet.Demand()

                permissionAvailable = True
            Catch generatedExceptionName As SecurityException
                permissionAvailable = False
            End Try
        End If

        Return permissionAvailable
    End Function

    ''' <summary>
    ''' Loads the default information.
    ''' </summary>
    Private Shared Sub LoadDefaultInformation()
        _defaultInformation.Add(New InfoItem("Application.ClrVersion", Environment.Version.ToString()))
        _defaultInformation.Add(New InfoItem("Application.64BitProcess", Environment.Is64BitProcess.ToString()))
        _defaultInformation.Add(New InfoItem("Computer.OS.64Bit", Environment.Is64BitOperatingSystem.ToString(CultureInfo.CurrentCulture)))
        _defaultInformation.Add(New InfoItem("Computer.OS.Name", CurrentComputerInfo.OSFullName))
        _defaultInformation.Add(New InfoItem("Computer.OS.Platform", CurrentComputerInfo.OSPlatform))
        _defaultInformation.Add(New InfoItem("Computer.OS.Version", CurrentComputerInfo.OSVersion))
        _defaultInformation.Add(New InfoItem("Computer.ProcessorCount", System.Environment.ProcessorCount.ToString(CultureInfo.CurrentCulture)))
    End Sub

    ''' <summary>
    ''' Processes the additional property info into a <see cref="StringBuilder" />.
    ''' </summary>
    ''' <param name="info">The PropertyInfo.</param>
    ''' <param name="value">The NameValueCollection.</param>
    ''' <param name="exceptionBuilder">The exception builder.</param>
    Private Shared Sub ProcessAdditionalPropertyInfo(ByVal info As PropertyInfo, ByVal value As Object, ByVal exceptionBuilder As StringBuilder)
        ' Loop through the collection of AdditionalInformation if the exception type is a BaseApplicationException.
        If (info.Name = ElementInformation) AndAlso (value IsNot Nothing) AndAlso (TypeOf value Is Collection(Of InfoItem)) Then
            ' Cast the collection into a local variable.
            Dim currAdditionalInfo = TryCast(value, Collection(Of InfoItem))
            ' Check if the collection contains values.
            If currAdditionalInfo.Count > 0 Then
                exceptionBuilder.AppendLine("AdditionalInformation:")

                ' Loop through the collection adding the information to the string builder.
                For Each item In currAdditionalInfo.OrderBy(Function(adl) adl.[Property])
                    If String.IsNullOrEmpty(item.Text) = False Then
                        exceptionBuilder.AppendLine([String].Format(CultureInfo.InvariantCulture, "--> {0}: {1}", item.[Property], item.Text.Trim()))
                    End If
                Next
            End If
        Else
            ' Otherwise just write the ToString() value of the property.
            exceptionBuilder.AppendLine(String.Format(CultureInfo.CurrentCulture, "{0}: {1}", info.Name, value))
        End If
    End Sub

    ''' <summary>
    ''' Reflects the properties in an Exception (ignores inner Exceptions and Stack Trace).
    ''' </summary>
    ''' <param name="ex">The Exception.</param>
    ''' <param name="exceptionBuilder">The exception builder.</param>
    Private Shared Sub ReflectException(ByVal ex As Exception, ByVal exceptionBuilder As StringBuilder)
        ' Do not log information for the InnerException or StackTrace. This information is
        ' captured later in the process.
        For Each info In From p In ex.[GetType]().GetProperties()
                         Where p.Name <> "InnerException" And p.Name <> NameOf(StackTrace)
                         Select p
            Dim value As Object = Nothing
            Try
                value = info.GetValue(ex, Nothing)
            Catch generatedExceptionName As SecurityException
                value = Nothing
            End Try
            If (value Is Nothing) OrElse value.ToString() = value.[GetType]().FullName Then
                'Ignore
            Else
                'exceptionBuilder.AppendLine(String.Format("{0}: NULL", info.Name))
                ProcessAdditionalPropertyInfo(info, value, exceptionBuilder)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Adds the information item.
    ''' </summary>
    ''' <param name="item">The item.</param>
    Private Sub AddInfoItem(ByVal item As InfoItem)
        Me._information.Add(item)
    End Sub

    ''' <summary>
    ''' Adds the information items.
    ''' </summary>
    ''' <param name="items">The items.</param>
    Private Sub AddInfoItems(ByVal items As InfoItem())
        If items IsNot Nothing Then
            For Each item As InfoItem In items
                Me.Information.Add(item)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Loads the name of the current domain.
    ''' </summary>
    Private Sub LoadCurrentDomainName()
        Try
            Me.Information.Add(New InfoItem("ApplicationDomain.Name", AppDomain.CurrentDomain.FriendlyName))
        Catch ex As SecurityException
            Me.Information.Add(New InfoItem("ApplicationDomain.Name", String.Format(CultureInfo.InvariantCulture, My.Resources.IntrinsicPropertyError, New Object() {ex.Message})))
        End Try
    End Sub

    ''' <summary>
    ''' Loads the current thread identifier.
    ''' </summary>
    ''' <returns>System.String.</returns>
    Private Function LoadCurrentThreadId() As String
        Return LoggingHelper.GetCurrentThreadId().ToString(NumberFormatInfo.InvariantInfo)
    End Function

    ''' <summary>
    ''' Loads the name of the current thread.
    ''' </summary>
    Private Sub LoadCurrentThreadName()
        Try
            Me.Information.Add(New InfoItem("Thread.Name", Thread.CurrentThread.Name))
        Catch ex As SecurityException
            Me.Information.Add(New InfoItem("Thread.Name", String.Format(CultureInfo.InvariantCulture, My.Resources.IntrinsicPropertyError, New Object() {ex.Message})))
        End Try
    End Sub

    ''' <summary>
    ''' Loads the debugging information.
    ''' </summary>
    Private Sub LoadDebuggingInformation()
        If (IsUnmanagedCodePermissionAvailable()) Then
            Me.LoadUnmanagedCodeValues()
        End If

        Me.LoadCurrentThreadName()
        Me.LoadCurrentDomainName()
        Me.LoadInformation()
    End Sub
#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Loads the default additional information which includes application, user and computer information.
    ''' </summary>
    Private Sub LoadInformation()
        Me.AddInfoItem(New InfoItem("Application.MappedMemory", Environment.WorkingSet.ToString("####,###,###,###", CultureInfo.InvariantCulture)))
        Me.AddInfoItem(New InfoItem("Computer.Culture", My.Computer.Info.InstalledUICulture.ToString()))

        Me.AddInfoItem(New InfoItem("User.Interactive", Environment.UserInteractive.ToString(CultureInfo.CurrentCulture)))

        If Environment.UserInteractive Then
            Me.AddInfoItem(New InfoItem("Computer.Screen.Bounds", My.Computer.Screen.Bounds.ToString()))
            Me.AddInfoItem(New InfoItem("Computer.Screen.WorkingArea", My.Computer.Screen.WorkingArea.ToString()))
        End If

        Me.AddInfoItem(New InfoItem("User.Domain", Environment.UserDomainName))
        Me.AddInfoItem(New InfoItem("Computer.FreePhysicalMemory", My.Computer.Info.AvailablePhysicalMemory.ToString("###,###,###,###", CultureInfo.InvariantCulture)))
        Me.AddInfoItem(New InfoItem("Computer.FreeVirtualMemory", My.Computer.Info.AvailableVirtualMemory.ToString("###,###,###,###", CultureInfo.InvariantCulture)))
        Me.AddInfoItem(New InfoItem("Computer.TotalPhysicalMemory", My.Computer.Info.TotalPhysicalMemory.ToString("###,###,###,###", CultureInfo.InvariantCulture)))
        Me.AddInfoItem(New InfoItem("Computer.TotalVirtualMemory", My.Computer.Info.TotalVirtualMemory.ToString("###,###,###,###", CultureInfo.InvariantCulture)))

        ''Load process info
        Dim currentProcess = Process.GetCurrentProcess()

        Me.AddInfoItem(New InfoItem("Process.BasePriority", currentProcess.BasePriority.ToString(CultureInfo.InvariantCulture)))

        Me.AddInfoItem(New InfoItem("Process.PriorityClass", currentProcess.PriorityClass.ToString()))

        Me.AddInfoItem(New InfoItem("Process.StartTime", currentProcess.StartTime.ToString()))

        Me.AddInfoItem(New InfoItem("Process.BasePriority", Process.GetCurrentProcess.BasePriority.ToString()))

        Me.AddInfoItem(New InfoItem("Server.Uptime", ComputerHelper.ServerUpTime(Environment.MachineName).Uptime.ToString()))

        Me.AddInfoItem(New InfoItem("Server.BootTime", ComputerHelper.ServerUpTime(Environment.MachineName).BootTime.ToString()))

        If _defaultInformation.Count() = 0 Then
            LogEntry.LoadDefaultInformation()
            For Each item In _defaultInformation
                Me.AddInfoItem(item)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Loads the name of the process.
    ''' </summary>
    ''' <returns>System.String.</returns>
    Private Function LoadProcessName() As String
        Dim buffer As New StringBuilder(&H400)

        Return If(LoggingHelper.GetModuleFileName(LoggingHelper.GetModuleHandle(Nothing), buffer, buffer.Capacity) > 0, buffer.ToString(), String.Empty)
    End Function

    ''' <summary>
    ''' Loads the default property values.
    ''' </summary>
    ''' <param name="includeDebugging">if set to <c>true</c> [include debugging information].</param>
    Private Sub LoadProperties(ByVal includeDebugging As Boolean)
        Me.Id = Guid.NewGuid().ToString()
        Me.Severity = TraceEventType.Information
        Me.Category = Category.None
        Me.TimeStamp = DateTime.UtcNow
        Me.Source = CurrentAssemblyInfo.AssemblyName
        Me.SourceVersion = CurrentAssemblyInfo.Version.ToString()
        Me.User = Environment.UserName
        Me.MachineName = New Computer().Name

        If includeDebugging Then
            Me.LoadDebuggingInformation()
        End If
    End Sub

    ''' <summary>
    ''' Loads the unmanaged code values.
    ''' </summary>
    Private Sub LoadUnmanagedCodeValues()
        Try
            Me.Information.Add(New InfoItem("Process.Id", LoggingHelper.GetCurrentProcessId().ToString(CultureInfo.InvariantCulture)))
        Catch ex As SecurityException
            Me.Information.Add(New InfoItem("Process.Id", String.Format(CultureInfo.InvariantCulture, My.Resources.IntrinsicPropertyError, New Object() {ex.Message})))
        End Try

        Try
            Me.Information.Add(New InfoItem("Process.Name", Threading.ThreadHelper.GetProcessName()))
        Catch ex As SecurityException
            Me.Information.Add(New InfoItem("Process.Name", String.Format(CultureInfo.InvariantCulture, My.Resources.IntrinsicPropertyError, New Object() {ex.Message})))
        End Try

        Try
            Me.Information.Add(New InfoItem("Thread.Id", Threading.ThreadHelper.GetThreadId().ToString))
        Catch ex As SecurityException
            Me.Information.Add(New InfoItem("Thread.Id", String.Format(CultureInfo.InvariantCulture, My.Resources.IntrinsicPropertyError, New Object() {ex.Message})))
        End Try
    End Sub

    ''' <summary>
    ''' Retrieves all exceptions including inner exceptions.
    ''' </summary>
    ''' <param name="ex">The Exception to transverse.</param>
    ''' <returns>IList(Of Exception).</returns>
    Private Function RetrieveAllExceptons(ByVal ex As Exception) As IList(Of Exception)
        Dim exceptions As IList(Of Exception) = Nothing

        If ex IsNot Nothing Then
            exceptions = New List(Of Exception)()
            exceptions.Add(ex)
            If ex.InnerException IsNot Nothing Then
                For Each item In Me.RetrieveAllExceptons(ex.InnerException)
                    exceptions.Add(item)
                Next
            End If
        End If

        Return exceptions
    End Function
#End Region

End Class