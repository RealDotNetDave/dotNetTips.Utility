' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 07-09-2017
'
' Last Modified By : David McCarter
' Last Modified On : 07-09-2017
' ***********************************************************************
' <copyright file="Processor.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System
Namespace IO
    ''' <summary>
    ''' Class FileProgressEventArgs.
    ''' </summary>
    ''' <seealso cref="System.EventArgs" />
    Public Class ProgressEventArgs
        Inherits EventArgs
        ''' <summary>
        ''' The name
        ''' </summary>
        Public Name As String
        ''' <summary>
        ''' The size
        ''' </summary>
        Public Size As Long
        ''' <summary>
        ''' The progress state
        ''' </summary>
        Public ProgressState As ProgressState
        ''' <summary>
        ''' The message
        ''' </summary>
        Public Message As String = String.Empty
    End Class
End Namespace