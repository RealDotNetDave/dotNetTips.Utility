' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 06-26-2017
'
' Last Modified By : David McCarter
' Last Modified On : 07-27-2017
' ***********************************************************************
' <copyright file="ThreadHelper.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' ***********************************************************************

Imports System.Text

Namespace Threading
    ''' <summary>
    ''' Threading methods.
    ''' </summary>
    Public Module ThreadHelper
        ''' <summary>
        ''' Gets the thread id.
        ''' </summary>
        ''' <returns>Thread id.</returns>
        Public Function GetThreadId() As Integer
            Return GetCurrentThreadId()
        End Function

        ''' <summary>
        ''' Gets the name of the current process.
        ''' </summary>
        ''' <returns>System.String.</returns>
        Public Function GetProcessName() As String
            Dim buffer As New StringBuilder(&H400)
            Return If(UnsafeNativeMethods.GetModuleFileName(UnsafeNativeMethods.GetModuleHandle(Nothing), buffer, buffer.Capacity) > 0, buffer.ToString(), String.Empty)
        End Function

    End Module

End Namespace