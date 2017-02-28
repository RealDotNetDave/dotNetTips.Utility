' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 12-04-2013
' ***********************************************************************
' <copyright file="General.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Diagnostics.Contracts
Imports System.Reflection

''' <summary>
''' General helper functions.
''' </summary>
Public Module General
    ''' <summary>
    ''' Writes to event log.
    ''' </summary>
    ''' <param name="message">The message to write.</param>
    ''' <param name="type">The event type.</param>
    Public Sub WriteToEventLog(ByVal message As String, ByVal type As EventLogEntryType)
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(message) = False)

        EventLog.WriteEntry(My.Application.Info.Title.ToString(CultureInfo.CurrentCulture), message, type)

    End Sub

    ''' <summary>
    ''' Does the object equal instance.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="instance">The instance.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DoesObjectEqualInstance(ByVal value As [Object], ByVal instance As Object) As Boolean

        Contract.Requires(Of ArgumentNullException)(value IsNot Nothing)
        Contract.Requires(Of ArgumentNullException)(instance IsNot Nothing)

        Dim result = Object.ReferenceEquals(value, instance)

        Return result

    End Function

    ''' <summary>
    ''' Gets the instance hash code.
    ''' </summary>
    ''' <param name="instance">The instance.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInstanceHashCode(ByVal instance As Object) As Int32
        Dim hash As Int32

        For Each prop As PropertyInfo In instance.GetType.GetRuntimeProperties().Where(Function(p) p IsNot Nothing).AsParallel
            Dim value = prop.GetValue(instance)
            If value IsNot Nothing Then
                hash = hash Xor value.GetHashCode
            End If
        Next

        Return hash
    End Function

    ''' <summary>
    ''' Starts a process.
    ''' </summary>
    ''' <param name="fileName">Name of the file.</param>
    ''' <param name="arguments">The arguments.</param>
    ''' <param name="windowsStyle">The windows style.</param>
    ''' <remarks></remarks>
    Public Sub StartProcess(fileName As String, arguments As String, windowsStyle As ProcessWindowStyle, wait As Boolean)

        Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(fileName) = False)

        Using process As New Process()
            Dim processInfo As New System.Diagnostics.ProcessStartInfo() With
                {
                    .FileName = fileName,
                    .Arguments = arguments,
                    .UseShellExecute = True,
                    .WindowStyle = windowsStyle
                }
            process.StartInfo = processInfo
            Dim result = Process.Start(processInfo)

            If (wait) Then
                Do Until result.HasExited
                    System.Threading.Thread.Sleep(500)
                Loop
            End If

        End Using
    End Sub

End Module