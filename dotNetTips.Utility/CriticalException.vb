' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 12-04-2013
' ***********************************************************************
' <copyright file="CriticalException.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Runtime.Serialization

''' <summary>
''' Custom critical Exception. Used to bubble up an un-handled Exception to the application level for logging and notifying the user.
''' </summary>

<Serializable()> _
Public Class CriticalException
    Inherits Exception
    ''' <summary>
    ''' Initializes a new instance of the <see cref="CriticalException"/> class.
    ''' </summary>
    ''' <param name="userMessage">The user message.</param>
    Public Sub New(ByVal userMessage As String)
        MyBase.New(userMessage)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="CriticalException"/> class.
    ''' </summary>
    ''' <param name="userMessage">The user message.</param>
    ''' <param name="originalException">The original exception.</param>
    Public Sub New(ByVal userMessage As String, ByVal originalException As Exception)
        MyBase.New(userMessage, originalException)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="CriticalException" /> class.
    ''' </summary>
    ''' <param name="info">The info.</param>
    ''' <param name="context">The context.</param>
    Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
        MyBase.New(info, context)

    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="CriticalException" /> class.
    ''' </summary>
    Public Sub New()

    End Sub

End Class