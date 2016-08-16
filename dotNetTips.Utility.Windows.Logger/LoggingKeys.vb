' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-10-2013
'
' Last Modified By : David McCarter
' Last Modified On : 08-04-2013
' ***********************************************************************
' <copyright file="LoggingKeys.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
''' <summary>
'''
''' </summary>
Public NotInheritable Class LoggingKeys
    Private Sub New()
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HttpRequestUrl As String = "Http.Request.Url"
    ''' <summary>
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HttpRequestUserHostAddress As String = "Http.Request.UserHostAddress"
    ''' <summary>
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HttpRequestUserHostName As String = "Http.Request.UserHostName"
    ''' <summary>
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HttpRequestTotalBytes As String = "Http.Request.TotalBytes"
    ''' <summary>
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HttpRequestHeaderPrefix As String = "Http.Request.Header."

End Class