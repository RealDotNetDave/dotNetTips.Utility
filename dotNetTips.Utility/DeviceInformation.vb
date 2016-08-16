' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 08-16-2016
' ***********************************************************************
' <copyright file="DeviceInformation.vb" company="NicheWare - David McCarter">
'     '     '     NicheWare - David McCarter
'
'
' </copyright>
' <summary></summary>
' *************************************************************************
''' <summary>
''' Struct DeviceInformation
''' </summary>
Public Structure DeviceInformation
    ''' <summary>
    ''' _caption
    ''' </summary>
    Private _caption As String

    ''' <summary>
    ''' Gets or sets the caption.
    ''' </summary>
    ''' <value>The caption.</value>
    Public Property Caption() As String
        Get
            Return _caption
        End Get
        Set(ByVal value As String)
            _caption = value
        End Set
    End Property
    ''' <summary>
    ''' _description
    ''' </summary>
    Private _description As String
    ''' <summary>
    ''' File description.
    ''' </summary>
    ''' <value>The description.</value>
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the fully qualified type name of this instance.
    ''' </summary>
    ''' <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
    Public Overrides Function ToString() As String
        Return Me.Caption
    End Function

    ''' <summary>
    ''' Returns the hash code for this instance.
    ''' </summary>
    ''' <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    Public Overloads Overrides Function GetHashCode() As Integer
        Return General.GetInstanceHashCode(Me)
    End Function

    ''' <summary>
    ''' Indicates whether this instance and a specified object are equal.
    ''' </summary>
    ''' <param name="obj">Another object to compare to.</param>
    ''' <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
    Public Overloads Overrides Function Equals(ByVal obj As [Object]) As Boolean
        Return General.DoesObjectEqualInstance(Me, obj)
    End Function

    ''' <summary>
    ''' Implements the operator =.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator =(x As DeviceInformation, y As DeviceInformation) As Boolean
        Return x.Equals(y)
    End Operator

    ''' <summary>
    ''' Implements the operator &lt;&gt;.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator <>(x As DeviceInformation, y As DeviceInformation) As Boolean
        Return Not x.Equals(y)
    End Operator
End Structure