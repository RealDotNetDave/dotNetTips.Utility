' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 07-16-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-16-2016
' ***********************************************************************
' <copyright file="InvalidEnumTypeException.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.Generic
Imports System.Runtime.Serialization

''' <summary>
''' Class InvalidEnumTypeException.
''' </summary>
''' <seealso cref="System.Exception" />
<Serializable()>
Public Class InvalidEnumTypeException
    Inherits Exception
    ''' <summary>
    ''' Initializes a new instance of the <see cref="InvalidEnumTypeException" /> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="InvalidEnumTypeException" /> class.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="InvalidEnumTypeException" /> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="innerException">The inner exception.</param>
    Public Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="InvalidEnumTypeException" /> class.
    ''' </summary>
    ''' <param name="info">The info.</param>
    ''' <param name="context">The context.</param>
    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class