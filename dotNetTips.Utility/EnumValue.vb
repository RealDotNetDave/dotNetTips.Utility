' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 07-16-2016
'
' Last Modified By : David McCarter
' Last Modified On : 08-16-2016
' ***********************************************************************
' <copyright file="EnumValue.vb" company="NicheWare - David McCarter">
'     '     NicheWare - David McCarter
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.Generic

''' <summary>
''' Enum value information.
''' </summary>
Public Structure EnumValue
    ''' <summary>
    ''' _value
    ''' </summary>
    Private _value As Int32
    ''' <summary>
    ''' _name
    ''' </summary>
    Private _name As String

    ''' <summary>
    ''' Gets or sets the value.
    ''' </summary>
    ''' <value>The value.</value>
    Public Property Value() As Int32
        Get
            Return Me._value
        End Get
        Set(ByVal value As Int32)
            Me._value = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name.
    ''' </summary>
    ''' <value>The name.</value>
    Public Property Name() As String
        Get
            Return Me._name
        End Get
        Set(ByVal value As String)
            Me._name = value
        End Set
    End Property

    ''' <summary>
    ''' Initializes a new instance of the <see cref="EnumValue" /> struct.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="name">The name.</param>
    Public Sub New(ByVal value As Int32, ByVal name As String)
        Me.Value = value
        Me.Name = name
    End Sub

    ''' <summary>
    ''' Returns a hash code for this instance.
    ''' </summary>
    ''' <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    Public Overloads Overrides Function GetHashCode() As Integer
        Return General.GetInstanceHashCode(Me)
    End Function

    ''' <summary>
    ''' Determines whether the specified <see cref="System.Object" /> is equal to this instance.
    ''' </summary>
    ''' <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    ''' <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    Public Overloads Overrides Function Equals(ByVal obj As [Object]) As Boolean
        Return General.DoesObjectEqualInstance(Me, obj)
    End Function


    ''' <summary>
    ''' Implements the operator =.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator =(x As EnumValue, y As EnumValue) As Boolean
        Return x.Equals(y)
    End Operator

    ''' <summary>
    ''' Implements the operator &lt;&gt;.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator <>(x As EnumValue, y As EnumValue) As Boolean
        Return Not x.Equals(y)
    End Operator
End Structure