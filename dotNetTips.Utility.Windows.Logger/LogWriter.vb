' ***********************************************************************
' Assembly         : dotNetTips.Utility.Windows.Logger
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 06-02-2016
' ***********************************************************************
' <copyright file="LogWriter.vb" company="McCarter Consulting">
'     Copyright © dotNetTips.com 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System
Imports System.Diagnostics
Imports System.Globalization
Imports System.Diagnostics.Contracts
Imports dotNetTips.Utility.Extensions

''' <summary>
''' Class LogWriter.
''' </summary>
Public Module LogWriter

#Region "Public Methods"

    ''' <summary>
    ''' Writes the entry.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="severity">The severity.</param>
    Public Sub WriteEntry(ByVal message As String, ByVal severity As TraceEventType)
        WriteEntry(message, severity, 0)
    End Sub

    ''' <summary>
    ''' Writes the entry.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="severity">The severity.</param>
    ''' <param name="id">The identifier.</param>
    Public Sub WriteEntry(ByVal message As String, ByVal severity As TraceEventType, ByVal id As Integer)
        WriteEntry(New LogEntry(message, severity) With {.EventId = id})
    End Sub

    ''' <summary>
    ''' Writes the entry.
    ''' </summary>
    ''' <param name="entry">The entry.</param>
    Public Sub WriteEntry(ByVal entry As LogEntry)
        Contract.Requires(Of ArgumentNullException)(entry IsNot Nothing)

        If String.IsNullOrEmpty(entry.Source) Then
            entry.Source = New CallingMethod(GetType(LogWriter)).Method.[Module].Name
        End If

        LogManagement.GetLog().TraceSource.TraceData(entry.Severity, entry.EventId, entry)
    End Sub

    ''' <summary>
    ''' Writes the exception.
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <param name="severity">The severity.</param>
    ''' <param name="information">The information.</param>
    Public Sub WriteException(ByVal ex As Exception, ByVal severity As TraceEventType, ByVal information As String)
        WriteException(ex, severity, information, Nothing, 0)
    End Sub

    ''' <summary>
    ''' Writes the exception.
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <param name="severity">The severity.</param>
    ''' <param name="information">The information.</param>
    ''' <param name="userPopupMessage">The user popup message.</param>
    Public Sub WriteException(ByVal ex As Exception, ByVal severity As TraceEventType, ByVal information As String, ByVal userPopupMessage As String)
        WriteException(ex, severity, information, userPopupMessage, 0)
    End Sub

    ''' <summary>
    ''' Writes the exception.
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <param name="severity">The severity.</param>
    ''' <param name="information">The information.</param>
    ''' <param name="userPopupMessage">The user popup message.</param>
    ''' <param name="id">The identifier.</param>
    Public Sub WriteException(ByVal ex As Exception, ByVal severity As TraceEventType, ByVal information As String, ByVal userPopupMessage As String, ByVal id As Integer)
        Contract.Requires(Of ArgumentNullException)(ex IsNot Nothing)

        Dim entry = New LogEntry(information, ex, severity) With {.Message = ex.Message, .EventId = id, .Category = Category.Exception}

        If String.IsNullOrEmpty(information) = False Then
            entry.Information.Add(New InfoItem(NameOf(InfoItem), information))
        End If

        'Add calling method info
        Dim callingMethod = New StackTrace(ex).GetFrame(0).GetMethod()
        If callingMethod IsNot Nothing Then
            entry.Information.Add(New InfoItem("Exception.CallingMethod", callingMethod.Name))
            entry.Information.Add(New InfoItem("Exception.CallingAssembly", callingMethod.[Module].Name))
            entry.Information.Add(New InfoItem("Exception.CallingType", callingMethod.DeclaringType.FullName))

            If String.IsNullOrEmpty(entry.Source) Then
                entry.Source = callingMethod.[Module].Name
            End If
        Else
            If String.IsNullOrEmpty(entry.Source) Then
                entry.Source = New CallingMethod(GetType(LogWriter)).Method.[Module].Name
            End If
        End If

        WriteEntry(entry)

        DisplayUserMessage(userPopupMessage)
    End Sub
#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Displays the user message.
    ''' </summary>
    ''' <param name="userMessage">The user message.</param>
    Private Sub DisplayUserMessage(ByVal userMessage As String)
        If Environment.UserInteractive AndAlso String.IsNullOrEmpty(userMessage) = False Then
            'Try
            '   If LogManagement.IsAspApp() AndAlso System.Web.HttpContext.Current.Request.Browser.EcmaScriptVersion.Major > 1 Then
            '      System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">")
            '      System.Web.HttpContext.Current.Response.Write([String].Format(CultureInfo.InvariantCulture, "alert(""{0}"")", userMessage))
            '      System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
            '   Else
            System.Windows.Forms.MessageBox.Show(userMessage)
            'End If
            '   Catch
            '   Trace.WriteLine("Problem displaying user message")
            'End Try
        End If
    End Sub
#End Region
End Module