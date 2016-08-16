' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 04-15-2016
' ***********************************************************************
' <copyright file="ComputerMemory.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' *************************************************************************
''' <summary>
''' Computer memory information.
''' </summary>
Public Structure ComputerMemory
    ''' <summary>
    ''' vailable physical memory
    ''' </summary>
    Private _availablePhysicalMemory As Long
    ''' <summary>
    ''' Available physical memory.
    ''' </summary>
    ''' <value>The available physical memory.</value>
    Public Property AvailablePhysicalMemory() As Long
        Get
            Return _availablePhysicalMemory
        End Get
        Set(ByVal value As Long)
            _availablePhysicalMemory = value
        End Set
    End Property
    ''' <summary>
    ''' vailable virtual memory
    ''' </summary>
    Private _availableVirtualMemory As Long
    ''' <summary>
    ''' Available virtual memory.
    ''' </summary>
    ''' <value>The available virtual memory.</value>
    Public Property AvailableVirtualMemory() As Long
        Get
            Return _availableVirtualMemory
        End Get
        Set(ByVal value As Long)
            _availableVirtualMemory = value
        End Set
    End Property
    ''' <summary>
    ''' otal physical memory
    ''' </summary>
    Private _totalPhysicalMemory As Long
    ''' <summary>
    ''' Total physical memory.
    ''' </summary>
    ''' <value>The total physical memory.</value>
    Public Property TotalPhysicalMemory() As Long
        Get
            Return _totalPhysicalMemory
        End Get
        Set(ByVal value As Long)
            _totalPhysicalMemory = value
        End Set
    End Property
    ''' <summary>
    ''' otal virtual memory
    ''' </summary>
    Private _totalVirtualMemory As Long
    ''' <summary>
    ''' Total virtual memory.
    ''' </summary>
    ''' <value>The total virtual memory.</value>
    Public Property TotalVirtualMemory() As Long
        Get
            Return _totalVirtualMemory
        End Get
        Set(ByVal value As Long)
            _totalVirtualMemory = value
        End Set
    End Property

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
    Public Shared Operator =(x As ComputerMemory, y As ComputerMemory) As Boolean
        Return x.Equals(y)
    End Operator

    ''' <summary>
    ''' Implements the operator &lt;&gt;.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator <>(x As ComputerMemory, y As ComputerMemory) As Boolean
        Return Not x.Equals(y)
    End Operator
End Structure