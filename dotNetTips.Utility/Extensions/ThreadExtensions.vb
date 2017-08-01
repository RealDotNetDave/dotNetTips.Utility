' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 07-27-2017
'
' Last Modified By : David McCarter
' Last Modified On : 07-27-2017
' ***********************************************************************
' <copyright file="ThreadExtensions.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Windows.Forms

''' <summary>
''' Class ThreadExtensions.
''' </summary>
Public Module ThreadExtensions
    ''' <summary>
    ''' Waits the until the given date/ time.
    ''' </summary>
    ''' <param name="currentThread">The current thread.</param>
    ''' <param name="stopOn">The stop on.</param>
    <Extension>
    Public Sub WaitUntil(currentThread As Thread, stopOn As DateTime)
        Do Until DateTime.UtcNow >= stopOn.ToUniversalTime
            Application.DoEvents()
        Loop
    End Sub

    ''' <summary>
    ''' Waits for period of time.
    ''' </summary>
    ''' <param name="currentThread">The current thread.</param>
    ''' <param name="waitFor">The wait for.</param>
    <Extension>
    Public Sub WaitFor(currentThread As Thread, waitFor As TimeSpan)
        Dim stopOn = DateTime.UtcNow.Add(waitFor)
        currentThread.WaitUntil(stopOn)
    End Sub
End Module
