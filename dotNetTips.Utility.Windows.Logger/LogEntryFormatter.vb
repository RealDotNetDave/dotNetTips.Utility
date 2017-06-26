' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 02-07-2017
' ***********************************************************************
' <copyright file="LogEntryFormatter.vb" company="NicheWare - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.Reflection
Imports System.Text

''' <summary>
''' Formats the LogEntry class data.
''' </summary>
Friend Class LogEntryFormatter
    ''' <summary>
    ''' Name of the additional information entry that holds the header.
    ''' </summary>
    Private Const LineSeparator As String = "======================================"
    ''' <summary>
    ''' The additional information
    ''' </summary>
    Private _additionalInfo As NameValueCollection

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntryFormatter" /> class.
    ''' </summary>
    Public Sub New()
        Me.New(New NameValueCollection())
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogEntryFormatter" /> class.
    ''' </summary>
    ''' <param name="additionalInfo">The additional info.</param>
    Public Sub New(ByVal additionalInfo As NameValueCollection)
        Me._additionalInfo = additionalInfo
    End Sub

#Region "Public Methods"
    ''' <summary>
    ''' Get the formatted message to be logged.
    ''' </summary>
    ''' <param name="entry">The exception object whose information should be
    ''' written to log file.</param>
    ''' <returns>The formatted message.</returns>
    Public Function GetMessage(ByVal entry As LogEntry) As String
        Dim message = New StringBuilder()

        If entry IsNot Nothing Then
            ' Record the contents of the AdditionalInfo collection.
            message.AppendLine([String].Format(CultureInfo.InvariantCulture, "{0}: {1}", entry.Category, entry.Message))
            message.AppendLine(LineSeparator)

            If entry.ErrorMessages.Count > 0 Then
                message.AppendLine("Exception Details:")
                message.AppendLine(LineSeparator)

                For Each item In entry.ErrorMessages
                    message.AppendLine(item)
                Next
            End If

            ReflectException(entry, message)

            For Each info As String In Me._additionalInfo
                message.AppendLine([String].Format(CultureInfo.InvariantCulture, "--> {0}", Me._additionalInfo.[Get](info)))
            Next
        End If

        Return message.ToString()
    End Function
#End Region

#Region "Private Methods"
    ''' <summary>
    ''' Reflects the exception.
    ''' </summary>
    ''' <param name="entry">The log entry.</param>
    ''' <param name="eventInfo">The event info.</param>
    Private Shared Sub ReflectException(ByVal entry As LogEntry, ByVal eventInfo As StringBuilder)
        For Each propInfo In entry.[GetType]().GetProperties()
            'Ignore if property is set to XmlIgnore
            If IgnoreProperty(propInfo) = False Then
                ' Do not log information for the InnerException or StackTrace. This information is
                ' captured later in the process.
                If propInfo.Name <> "ErrorMessages" And propInfo.Name <> NameOf(StackTrace) And propInfo.Name <> NameOf(Data) Then
                    Dim propValue As Object = Nothing

                    Try
                        propValue = propInfo.GetValue(entry, Nothing)
                    Catch ex As NullReferenceException
                        Trace.TraceError(ex.Message)
                    End Try

                    If propValue Is Nothing Then
                        'strEventInfo.AppendLine(propinfo.Name + ": NULL");
                    Else
                        ProcessAdditionalInfo(propInfo, propValue, eventInfo)
                    End If
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Determines if the property needs to be ignored.
    ''' </summary>
    ''' <param name="propInfo">The property information.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    Private Shared Function IgnoreProperty(ByVal propInfo As PropertyInfo) As Boolean
        Dim ignore = False

        For Each attrib In propInfo.GetCustomAttributes(True)
            If attrib.GetType Is GetType(System.Xml.Serialization.XmlIgnoreAttribute) Then
                ignore = True
                Exit For
            End If
        Next

        Return ignore
    End Function

    ''' <summary>
    ''' Processes the additional info.
    ''' </summary>
    ''' <param name="propInfo">The prop info.</param>
    ''' <param name="propValue">The prop value.</param>
    ''' <param name="builder">The StringBuilder.</param>
    Private Shared Sub ProcessAdditionalInfo(ByVal propInfo As PropertyInfo, ByVal propValue As Object, ByVal builder As StringBuilder)
        ' Loop through the collection of AdditionalInformation if the exception type is a BaseApplicationException.
        If propInfo.Name = "AdditionalInformation" Then
            If propValue IsNot Nothing Then
                ' Cast the collection into a local variable.
                Dim currAdditionalInfo = DirectCast(propValue, Collection(Of InfoItem))

                ' Check if the collection contains values.
                If currAdditionalInfo.Count > 0 Then
                    builder.AppendLine("AdditionalInformation:")

                    ' Loop through the collection adding the information to the string builder.
                    For Each item In currAdditionalInfo.OrderBy(Function(adl) adl.[Property])
                        If String.IsNullOrEmpty(item.Text) = False Then
                            builder.AppendLine([String].Format(CultureInfo.InvariantCulture, "--> {0}: {1}", item.[Property], item.Text.Trim()))
                        End If
                    Next

                End If
            End If
        Else
            ' Otherwise just write the ToString() value of the property.
            builder.AppendLine([String].Format(CultureInfo.InvariantCulture, "{0}: {1}", propInfo.Name, FormatType(propValue)))
        End If
    End Sub

    ''' <summary>
    ''' Formats the type.
    ''' </summary>
    ''' <param name="propValue">The prop value.</param>
    ''' <returns>Formated value</returns>
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
#End Region
End Class