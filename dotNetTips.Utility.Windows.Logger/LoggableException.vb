' ***********************************************************************
' Assembly         : dotNetTips.Utility.Windows.Logger
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 04-18-2016
' ***********************************************************************
' <copyright file="LoggableException.vb" company="McCarter Consulting">
'     Copyright © dotNetTips.com 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Globalization
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Security
Imports System.Text
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Runtime.Serialization

''' <summary>
''' Exception meant to be logged.
''' </summary>
''' <seealso cref="System.Exception" />
''' <seealso cref="IXmlSerializable" />
<Serializable>
Public Class LoggableException
    Inherits Exception
    Implements IXmlSerializable
#Region "Public Constructors"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LoggableException"/> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="ex">The ex.</param>
    ''' <param name="userMessage">The user message.</param>
    Public Sub New(message As String, ex As Exception, userMessage As String)
        MyBase.New(message, ex)
        userMessage = userMessage
    End Sub

#End Region

#Region "Public Properties"

    ''' <summary>
    ''' Gets the messages.
    ''' </summary>
    ''' <value>The messages.</value>
    Public Overridable ReadOnly Property Messages() As IDictionary(Of LogItem, String)
        Get
            Dim exceptions = LoggingHelper.RetrieveAllExceptions(Me)
            Dim errorMessages = New Dictionary(Of LogItem, String)()

            Dim enumerator = exceptions.GetEnumerator()
            For Each current In exceptions
                Dim exceptionLevel = If((errorMessages.Count = 0), LogItem.Exception, LogItem.InnerException)

                errorMessages.Add(LogItem.ExceptionType, current.[GetType]().FullName)
                errorMessages.Add(LogItem.Properties, ReflectException(current))

                If current.StackTrace IsNot Nothing Then
                    errorMessages.Add(LogItem.StackTrace, current.StackTrace)
                End If
            Next

            Return errorMessages
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the user message.
    ''' </summary>
    ''' <value>The user message.</value>
    Public Overridable Property UserMessage() As String
        Get
            Return _userMessage
        End Get
        Set
            _userMessage = Value
        End Set
    End Property
    ''' <summary>
    ''' user message
    ''' </summary>
    Private _userMessage As String

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' is the XML serializable_ get schema.
    ''' </summary>
    ''' <returns>XmlSchema.</returns>
    ''' <exception cref="System.NotImplementedException"></exception>
    Private Function IXmlSerializable_GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
        Throw New NotImplementedException()
    End Function

    ''' <summary>
    ''' is the XML serializable_ read XML.
    ''' </summary>
    ''' <param name="reader">The reader.</param>
    ''' <exception cref="NotImplementedException"></exception>
    <CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")>
    Private Sub IXmlSerializable_ReadXml(reader As XmlReader) Implements IXmlSerializable.ReadXml
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' is the XML serializable_ write XML.
    ''' </summary>
    ''' <param name="writer">The writer.</param>
    ''' <exception cref="System.NotImplementedException"></exception>
    Private Sub IXmlSerializable_WriteXml(writer As XmlWriter) Implements IXmlSerializable.WriteXml
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Reflects the exception.
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <returns>System.String.</returns>
    Private Shared Function ReflectException(ex As Exception) As String
        Dim sb = New StringBuilder()

        For Each current In ex.[GetType]().GetRuntimeProperties()
            Dim objectValue As Object = Nothing
            Try
                objectValue = RuntimeHelpers.GetObjectValue(current.GetValue(ex, Nothing))
            Catch securityEx As SecurityException
                ProjectData.SetProjectError(securityEx)
                Dim exception As SecurityException = securityEx
                objectValue = Nothing
                ProjectData.ClearProjectError()
            End Try
            If (objectValue IsNot Nothing) AndAlso (objectValue.ToString() <> objectValue.[GetType]().FullName) Then
                sb.AppendLine(String.Format(CultureInfo.CurrentCulture, "{0}: {1}", New Object() {current.Name, RuntimeHelpers.GetObjectValue(current)}))
            End If
        Next

        Return sb.ToString()
    End Function

#End Region

#Region "ISerialization Implementation"

    ''' <summary>
    ''' When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
    ''' </summary>
    ''' <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    ''' <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
    ''' <PermissionSet>
    '''   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
    '''   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
    ''' </PermissionSet>
    Public Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)

        MyBase.GetObjectData(info, context)

        info.AddValue(NameOf(_userMessage), _userMessage)

    End Sub

    Public Sub New()
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Protected Sub New(serializationInfo As SerializationInfo, streamingContext As StreamingContext)
        Throw New NotImplementedException()
    End Sub

#End Region
End Class