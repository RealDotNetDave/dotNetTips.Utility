' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : davidmccarter
' Created          : 12-31-2014
'
' Last Modified By : davidmccarter
' Last Modified On : 10-06-2014
' ***********************************************************************
' <copyright file="ThreadHelper.vb" company="dotNetTips.com">
'     dotNetTips.com. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Text

Namespace Threading
    ''' <summary>
    ''' Threading methods.
    ''' </summary>
    ''' <remarks></remarks>
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
        ''' <returns></returns>
        Public Function GetProcessName() As String
            Dim buffer As New StringBuilder(&H400)
            Return If(UnsafeNativeMethods.GetModuleFileName(UnsafeNativeMethods.GetModuleHandle(Nothing), buffer, buffer.Capacity) > 0, buffer.ToString(), String.Empty)
        End Function

    End Module

End Namespace