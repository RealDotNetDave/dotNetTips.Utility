' ***********************************************************************
' Assembly         : dotNetTips.Utility.Windows.Logger
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 04-18-2016
' ***********************************************************************
' <copyright file="ExceptionFormatter.vb" company="McCarter Consulting">
'     Copyright © dotNetTips.com 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.Reflection
Imports System.Security
Imports System.Security.Principal
Imports System.Text

''' <summary>
''' Formats an Exception
''' </summary>
Friend Class ExceptionFormatter
    ''' <summary>
    ''' Name of the information entry that holds the header.
    ''' </summary>

    Private ReadOnly _information As NameValueCollection
    ''' <summary>
    ''' application name
    ''' </summary>
    Private ReadOnly _applicationName As String

#Region "Constructors"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ExceptionFormatter"/> class.
    ''' </summary>
    ''' <param name="information">The information.</param>
    ''' <param name="applicationName">Name of the application.</param>
    Public Sub New(ByVal information As NameValueCollection, ByVal applicationName As String)
        Me._information = information
        Me._applicationName = applicationName
    End Sub

    ''' <summary>
    ''' Initialize a new instance of the <see cref="ExceptionFormatter" /> class.
    ''' </summary>
    Public Sub New()
        Me.New(New NameValueCollection(), String.Empty)
    End Sub
#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Gets the message.
    ''' </summary>
    ''' <param name="ex">The exception object whose information should be written to log file.</param>
    ''' <returns>System.String.</returns>
    Public Function GetMessage(ByVal ex As Exception) As String
        Dim message = New StringBuilder()

        Me.CollectInformation()

        ' Record the contents of the information collection.
        message.AppendLine([String].Format(CultureInfo.InvariantCulture, "Exception Summary for: {0}", Me._applicationName))
        message.AppendLine(My.Resources.LineSeparator)

        For Each info As String In Me._information
            message.AppendLine([String].Format(CultureInfo.InvariantCulture, "--> {0}", Me._information.[Get](info)))
        Next

        If ex IsNot Nothing Then
            Dim currException As Exception = ex

            Do
                message.AppendLine(My.Resources.LineSeparator)
                message.AppendLine("Exception Information Details:")
                message.AppendLine(My.Resources.LineSeparator)

                message.AppendLine([String].Format(CultureInfo.InvariantCulture, "Exception Type: {0}", currException.[GetType]().FullName))

                ReflectException(currException, message)

                ' Record the StackTrace with separate label.
                If currException.StackTrace IsNot Nothing Then
                    message.AppendLine("StackTrace Information Details: ")
                    message.AppendLine(My.Resources.LineSeparator)
                    message.AppendLine(currException.StackTrace)
                End If

                ' Reset the temp exception object.
                currException = currException.InnerException
            Loop While currException IsNot Nothing
        End If

        Return message.ToString()
    End Function

#End Region

#Region "Private Static Methods"

    ''' <summary>
    ''' Formats the type.
    ''' </summary>
    ''' <param name="propValue">The types value.</param>
    ''' <returns>System.String.</returns>
    Private Shared Function FormatType(ByVal propValue As Object) As String
        Dim formattedValue = String.Empty

        Select Case propValue.[GetType]().Name
            Case "DateTime"
                formattedValue = DirectCast(propValue, DateTime).ToString("o", CultureInfo.InvariantCulture)
                Exit Select
            Case Else
                formattedValue = propValue.ToString()
                Exit Select
        End Select

        Return formattedValue
    End Function

    ''' <summary>
    ''' Reflects the exception.
    ''' </summary>
    ''' <param name="exp">The Exception.</param>
    ''' <param name="eventInfo">The event info.</param>
    Private Shared Sub ReflectException(ByVal exp As Exception, ByVal eventInfo As StringBuilder)
        Dim publicProperties As PropertyInfo() = exp.[GetType]().GetProperties()

        For Each propInfo In publicProperties
            ' Do not log information for the InnerException or StackTrace. This information is
            ' captured later in the process.
            If propInfo.Name <> "InnerException" And propInfo.Name <> NameOf(StackTrace) And propInfo.Name <> NameOf(Data) Then
                Dim propValue As Object = Nothing

                Try
                    propValue = propInfo.GetValue(exp, Nothing)
                Catch ex As NullReferenceException
                    Trace.TraceError(ex.Message)
                End Try

                If propValue Is Nothing Then
                    eventInfo.AppendLine([String].Format(CultureInfo.InvariantCulture, "{0}: NULL", propInfo.Name))
                Else
                    ProcessInformation(propInfo, propValue, eventInfo)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Processes the information.
    ''' </summary>
    ''' <param name="propInfo">The property information.</param>
    ''' <param name="propValue">The property value.</param>
    ''' <param name="builder">The builder.</param>
    Private Shared Sub ProcessInformation(ByVal propInfo As PropertyInfo, ByVal propValue As Object, ByVal builder As StringBuilder)
        Contracts.Contract.Requires(Of ArgumentNullException)(propInfo IsNot Nothing)
        Contracts.Contract.Requires(Of ArgumentNullException)(builder IsNot Nothing)

        ' Loop through the collection of Information if the exception type is a BaseApplicationException.
        If propInfo.Name = NameOf(InfoItem) Then
            If propValue IsNot Nothing Then
                ' Cast the collection into a local variable.
                Dim currentInformation = DirectCast(propValue, Collection(Of InfoItem))

                ' Check if the collection contains values.
                If currentInformation.Count > 0 Then
                    builder.AppendLine("Info:")

                    ' Loop through the collection adding the information to the string builder.
                    For Each item In currentInformation.OrderBy(Function(adl) adl.[Property])
                        If String.IsNullOrEmpty(item.Text) = False Then
                            builder.AppendLine([String].Format(CultureInfo.InvariantCulture, "--> {0}: {1}", item.[Property], item.Text.Trim()))
                        End If
                    Next
                End If
            End If
        Else
            ' Otherwise just write the ToString() value of the property.
            builder.AppendLine([String].Format(CultureInfo.InstalledUICulture, "{0}: {1}", propInfo.Name, FormatType(propValue)))
        End If
    End Sub

    ''' <summary>
    ''' Gets the name of the machine.
    ''' </summary>
    ''' <returns>System.String.</returns>
    Private Shared Function GetMachineName() As String
        Try
            Return Environment.MachineName
        Catch ex As SecurityException
            Trace.TraceError(ex.Message)
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Gets the windows identity.
    ''' </summary>
    ''' <returns>System.String.</returns>
    Private Shared Function GetWindowsIdentity() As String
        Try
            Return WindowsIdentity.GetCurrent().Name
        Catch ex As SecurityException
            Trace.TraceError(ex.Message)
            Return String.Empty
        End Try
    End Function
#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Collects the information.
    ''' </summary>
    Private Sub CollectInformation()
        If Me._information("MachineName:") IsNot Nothing Then
            Exit Sub
        End If
        Me._information.Add("Application:", My.Application.Info.Title)
        Me._information.Add("Company:", My.Application.Info.CompanyName)
        Me._information.Add("MachineName:", GetMachineName())
        Me._information.Add("TimeStamp:", DateTime.UtcNow.ToString(CultureInfo.CurrentCulture))
        Me._information.Add("AppDomainName:", AppDomain.CurrentDomain.FriendlyName)
        Me._information.Add("WindowsIdentity:", GetWindowsIdentity())
        Me._information.Add("OSVersion:", Environment.OSVersion.ToString)
    End Sub

#End Region
End Class