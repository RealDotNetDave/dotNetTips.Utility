' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 04-20-2016
' ***********************************************************************
' <copyright file="InfoItem.vb" company="McCarter Consulting">
'     Copyright © dotNetTips.com 2016
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System


''' <summary>
''' Additional Information for a <see cref="LogEntry"></see> item.
''' </summary>
<Serializable()>
Public NotInheritable Class InfoItem
    ''' <summary>
    ''' _property
    ''' </summary>
    Private _property As String
    ''' <summary>
    ''' _text
    ''' </summary>
    Private _text As String

    ''' <summary>
    ''' Initializes a new instance of the <see cref="InfoItem"/> class.
    ''' </summary>
    ''' <param name="property">The property.</param>
    ''' <param name="text">The text.</param>
    Public Sub New([property] As String, text As String)
        Me._property = [property]
        Me._text = If(String.IsNullOrEmpty(text), "UNKOWN", text)
    End Sub

    ''' <summary>
    ''' Gets Additional Information property name.
    ''' </summary>
    ''' <value>The property.</value>
    Public ReadOnly Property [Property]() As String
        Get
            Return Me._property
        End Get
    End Property

    ''' <summary>
    ''' Gets Additional Information property text.
    ''' </summary>
    ''' <value>The text.</value>
    Public ReadOnly Property Text() As String
        Get
            Return Me._text
        End Get
    End Property

    ''' <summary>
    ''' Indicates whether this instance and a specified object are equal.
    ''' </summary>
    ''' <param name="obj">Another object to compare to.</param>
    ''' <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        Return General.DoesObjectEqualInstance(Me, obj)
    End Function

    ''' <summary>
    ''' Returns the hash code for this instance.
    ''' </summary>
    ''' <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    Public Overloads Overrides Function GetHashCode() As Integer
        Return General.GetInstanceHashCode(Me)
    End Function

End Class