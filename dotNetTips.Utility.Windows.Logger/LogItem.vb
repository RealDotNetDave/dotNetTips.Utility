' ***********************************************************************
' Assembly         : dotNetTips.Utility.Windows.Logger
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 03-25-2016
' ***********************************************************************
' <copyright file="LogItem.vb" company="McCarter Consulting">
'     Copyright © dotNetTips.com 2016
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
''' Enum LogItem
''' </summary>
Public Enum LogItem
    ''' <summary>
    ''' Exception type
    ''' </summary>
    ExceptionType
    ''' <summary>
    ''' Exception
    ''' </summary>
    Exception
    ''' <summary>
    ''' Inner exception
    ''' </summary>
    InnerException
    ''' <summary>
    ''' Stack trace
    ''' </summary>
    StackTrace
    ''' <summary>
    ''' Properties
    ''' </summary>
    Properties
End Enum