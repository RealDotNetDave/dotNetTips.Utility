' ***********************************************************************
' Assembly         : dotNetTips.Utility.Logger
' Author           : dotnetdave
' Created          : 03-29-2016
'
' Last Modified By : dotnetdave
' Last Modified On : 03-25-2016
' ***********************************************************************
' <copyright file="LoggingHelper.vb" company="">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************

Imports System.Diagnostics.Contracts
Imports System.Globalization
Imports System.Runtime.InteropServices

''' <summary>
''' Error logging class
''' </summary>
Public NotInheritable Class LoggingHelper

    ''' <summary>
    ''' Prevents a default instance of the <see cref="LoggingHelper"/> class from being created.
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Retrieves all exceptions.
    ''' </summary>
    ''' <param name="ex">The ex.</param>
    ''' <returns>IEnumerable(Of Exception).</returns>
    Public Shared Function RetrieveAllExceptions(ex As Exception) As IEnumerable(Of Exception)
        Dim collection As List(Of Exception) = Nothing

        If ex IsNot Nothing Then
            collection = New List(Of Exception)() From { _
                ex _
            }
            If ex.InnerException IsNot Nothing Then
                collection.AddRange(RetrieveAllExceptions(ex.InnerException))
            End If
        End If

        Return collection.AsEnumerable()
    End Function
    ''' <summary>
    ''' Logs messages. Adds the current date/time to the beginning of the message.
    ''' </summary>
    ''' <param name="message">Message text to log</param>
    Public Shared Sub WriteEntry(ByVal message As String)
        WriteToLog(Now.ToString(CultureInfo.CurrentCulture) + Microsoft.VisualBasic.Constants.vbNewLine + message, Nothing)
    End Sub

    ''' <summary>
    ''' Logs messages. Adds the current date/time to the beginning of the message.
    ''' </summary>
    ''' <param name="message">Message text to log</param>
    ''' <param name="severity">Message severity</param>
    Public Shared Sub WriteEntry(ByVal message As String, ByVal severity As System.Diagnostics.TraceEventType)
        WriteToLog(Now.ToString(CultureInfo.CurrentCulture) + Microsoft.VisualBasic.Constants.vbNewLine + message, severity)
    End Sub

    ''' <summary>
    ''' Writes the entry.
    ''' </summary>
    ''' <param name="entry">The entry as <see cref="LogEntry">LogEntry</see>.</param>
    Public Shared Sub WriteEntry(ByVal entry As LogEntry)
        Contract.Requires(Of ArgumentNullException)(entry IsNot Nothing)

        If ApplicationHelper.IsAspNet() Then
            Dim log As New Microsoft.VisualBasic.Logging.AspLog
            log.TraceSource.TraceData(entry.Severity, entry.EventId, entry)
        Else
            My.Application.Log.TraceSource.TraceData(entry.Severity, entry.EventId, entry)
        End If

    End Sub

    ''' <summary>
    ''' Writes to log.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="severity">The severity.</param>
    Private Shared Sub WriteToLog(ByVal message As String, ByVal severity As System.Diagnostics.TraceEventType)

        If ApplicationHelper.IsAspNet() Then
            Dim log As New Microsoft.VisualBasic.Logging.AspLog
            log.WriteEntry(message, severity)
        Else
            My.Application.Log.WriteEntry(message, severity)
        End If

    End Sub

    ''' <summary>
    ''' Gets the current thread identifier.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    <DllImport("kernel32.dll")>
    Friend Shared Function GetCurrentThreadId() As Integer
    End Function

    ''' <summary>
    ''' Gets the current process identifier.
    ''' </summary>
    ''' <returns>System.Int32.</returns>
    <DllImport("kernel32.dll")> _
    Friend Shared Function GetCurrentProcessId() As Integer
    End Function

    ''' <summary>
    ''' Gets the module handle.
    ''' </summary>
    ''' <param name="moduleName">Name of the module.</param>
    ''' <returns>IntPtr.</returns>
    <DllImport("kernel32.dll", CharSet:=CharSet.Unicode)>
    Friend Shared Function GetModuleHandle(ByVal moduleName As String) As IntPtr
    End Function

    ''' <summary>
    ''' Gets the name of the module file.
    ''' </summary>
    ''' <param name="hModule">The h module.</param>
    ''' <param name="lpFilename">The lp filename.</param>
    ''' <param name="nSize">Size of the n.</param>
    ''' <returns>System.Int32.</returns>
    <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Friend Shared Function GetModuleFileName(<[In]()> ByVal hModule As IntPtr, <Out()> ByVal lpFilename As System.Text.StringBuilder, <[In](), MarshalAs(UnmanagedType.U4)> ByVal nSize As Integer) As Integer
    End Function

End Class