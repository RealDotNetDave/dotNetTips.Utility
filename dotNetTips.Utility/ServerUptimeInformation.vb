' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 05-11-2017
' ***********************************************************************
' <copyright file="ServerUptimeInformation.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' *************************************************************************
''' <summary>
''' Server up time information.
''' </summary>
Public Structure ServerUptimeInformation
    ''' <summary>
    ''' oot time
    ''' </summary>
    Private _bootTime As DateTime
    ''' <summary>
    ''' Boot time.
    ''' </summary>
    ''' <value>The boot time.</value>
    Public Property BootTime() As DateTime
        Get
            Return _bootTime
        End Get
        Set(ByVal value As DateTime)
            _bootTime = value
        End Set
    End Property
    ''' <summary>
    ''' Current time
    ''' </summary>
    Private _currentTime As DateTime
    ''' <summary>
    ''' Current time.
    ''' </summary>
    ''' <value>The current time.</value>
    Public Property CurrentTime() As DateTime
        Get
            Return _currentTime
        End Get
        Set(ByVal value As DateTime)
            _currentTime = value
        End Set
    End Property
    ''' <summary>
    ''' Up time
    ''' </summary>
    Private _upTime As TimeSpan
    ''' <summary>
    ''' How long the computer has been running.
    ''' </summary>
    ''' <value>The uptime.</value>
    Public Property Uptime() As TimeSpan
        Get
            Return _upTime
        End Get
        Set(ByVal value As TimeSpan)
            _upTime = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the fully qualified type name of this instance.
    ''' </summary>
    ''' <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
    Public Overrides Function ToString() As String
        Return Me.Uptime.ToString
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
    Public Shared Operator =(x As ServerUptimeInformation, y As ServerUptimeInformation) As Boolean
        Return x.Equals(y)
    End Operator

    ''' <summary>
    ''' Implements the operator &lt;&gt;.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator <>(x As ServerUptimeInformation, y As ServerUptimeInformation) As Boolean
        Return Not x.Equals(y)
    End Operator
End Structure