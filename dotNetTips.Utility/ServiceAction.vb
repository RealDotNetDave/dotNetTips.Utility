' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : david
' Created          : 05-05-2017
'
' Last Modified By : david
' Last Modified On : 05-13-2017
' ***********************************************************************
' <copyright file="Services.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' *************************************************************************

''' <summary>
''' Class ServiceAction.
''' </summary>
Public Class ServiceAction
    ''' <summary>
    ''' Gets or sets the name of the service.
    ''' </summary>
    ''' <value>The name of the service.</value>
    Public Property ServiceName() As String
    ''' <summary>
    ''' Gets or sets the service action request.
    ''' </summary>
    ''' <value>The service action request.</value>
    Public Property ServiceActionRequest() As ServiceActionRequest
    ''' <summary>
    ''' Gets or sets the service action result.
    ''' </summary>
    ''' <value>The service action result.</value>
    Public Property ServiceActionResult() As ServiceActionResult
End Class
