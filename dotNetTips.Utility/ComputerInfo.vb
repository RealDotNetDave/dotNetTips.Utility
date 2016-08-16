'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 05-20-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
''' <summary>
''' Computer Information
''' </summary>
Public Structure ComputerInfo
    Private _installedUICulture As String
    ''' <summary>
    ''' Installed UI culture.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InstalledUICulture() As String
        Get
            Return _installedUICulture
        End Get
        Set(ByVal value As String)
            _installedUICulture = value
        End Set
    End Property
    Private _osFullName As String
    ''' <summary>
    ''' Operating system full name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OSFullName() As String
        Get
            Return _osFullName
        End Get
        Set(ByVal value As String)
            _osFullName = value
        End Set
    End Property
    Private _osPlatform As String
    ''' <summary>
    ''' Operating system platform.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OSPlatform() As String
        Get
            Return _osPlatform
        End Get
        Set(ByVal value As String)
            _osPlatform = value
        End Set
    End Property
    Private _osVersion As String
    ''' <summary>
    ''' Operating system version.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OSVersion() As String
        Get
            Return _osVersion
        End Get
        Set(ByVal value As String)
            _osVersion = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the hash code for this instance.
    ''' </summary>
    ''' <returns>
    ''' A 32-bit signed integer that is the hash code for this instance.
    ''' </returns>
    Public Overloads Overrides Function GetHashCode() As Integer
        Return General.GetInstanceHashCode(Me)
    End Function

    ''' <summary>
    ''' Indicates whether this instance and a specified object are equal.
    ''' </summary>
    ''' <param name="obj">Another object to compare to.</param>
    ''' <returns>
    ''' true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
    ''' </returns>
    Public Overloads Overrides Function Equals(ByVal obj As [Object]) As Boolean
        Return General.DoesObjectEqualInstance(Me, obj)
    End Function

    ''' <summary>
    ''' Implements the operator =.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator =(x As ComputerInfo, y As ComputerInfo) As Boolean
        Return x.Equals(y)
    End Operator

    ''' <summary>
    ''' Implements the operator &lt;&gt;.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator <>(x As ComputerInfo, y As ComputerInfo) As Boolean
        Return Not x.Equals(y)
    End Operator
End Structure