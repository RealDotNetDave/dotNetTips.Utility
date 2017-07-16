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

''' <summary>
''' Enum ProgressState
''' </summary>
Public Enum ProgressState
    ''' <summary>
    ''' The error
    ''' </summary>
    [Error]
    ''' <summary>
    ''' The deleted
    ''' </summary>
    Deleted
    ''' <summary>
    ''' The moved
    ''' </summary>
    Moved
    ''' <summary>
    ''' The copied
    ''' </summary>
    Copied
End Enum
