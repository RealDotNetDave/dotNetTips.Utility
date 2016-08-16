' ***********************************************************************
' Assembly         : dotNetTips.Utility.Logger
' Author           : David McCarter
' Created          : 03-25-2016
'
' Last Modified By : David McCarter
' Last Modified On : 03-25-2016
' ***********************************************************************
' <copyright file="Category.vb" company="McCarter Consulting">
'     Copyright ©  2016
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System

''' <summary>
''' Logging Category
''' </summary>
Public Enum Category
    ''' <summary>
    ''' Not set
    ''' </summary>
    None
    ''' <summary>
    ''' Event
    ''' </summary>
    [Event]
    ''' <summary>
    ''' Exception
    ''' </summary>
    Exception

    ''' <summary>
    ''' The client stats
    ''' </summary>
    ClientStats

    ''' <summary>
    ''' The server stats
    ''' </summary>
    ServerStats

    ''' <summary>
    ''' The security audit
    ''' </summary>
    SecurityAudit

    ''' <summary>
    ''' The performance
    ''' </summary>
    Performance
End Enum