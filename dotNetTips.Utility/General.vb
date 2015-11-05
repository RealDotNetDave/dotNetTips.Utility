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
Imports System
Imports System.Reflection
Imports System.Linq

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
    ''' Doeses the object equal instance.
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
        Contract.Requires(Of ArgumentNullException)(instance IsNot Nothing)

        Dim hash As Int32

        For Each prop As PropertyInfo In instance.GetType.GetRuntimeProperties().Where(Function(p) p IsNot Nothing).AsParallel
            Dim value = prop.GetValue(instance)
            If value IsNot Nothing Then
                hash = hash Xor value.GetHashCode
            End If
        Next

        Return hash
    End Function

End Module