' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-10-2013
'
' Last Modified By : David McCarter
' Last Modified On : 08-04-2013
' ***********************************************************************
' <copyright file="InvalidEnumTypeException.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System
Imports System.Runtime.Serialization

''' <summary>
'''
''' </summary>
<Serializable> _
Public Class InvalidEnumTypeException
    Inherits Exception
    ''' <summary>
    ''' Initializes a new instance of the <see cref="InvalidEnumTypeException"/> class.
    ''' </summary>
    ''' <param name="message">The error message.</param>
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="InvalidEnumTypeException"/> class.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="InvalidEnumTypeException"/> class.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="innerException">The inner exception.</param>
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="InvalidEnumTypeException"/> class.
    ''' </summary>
    ''' <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/>
    ''' that holds the serialized object data about the exception being thrown.</param>
    ''' <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/>
    ''' that contains contextual information about the source or destination.</param>
    ''' <exception cref="T:System.ArgumentNullException">
    ''' The <paramref name="info"/> parameter is null.
    ''' </exception>
    ''' <exception cref="T:System.Runtime.Serialization.SerializationException">
    ''' The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
    ''' </exception>
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class