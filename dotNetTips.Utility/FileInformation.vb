' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 08-16-2016
' ***********************************************************************
' <copyright file="FileInformation.vb" company="NicheWare - David McCarter">
'     '     NicheWare - David McCarter
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports dotNetTips.Utility
''' <summary>
''' File Information
''' </summary>
<Serializable>
Public Structure FileInformation
    Implements IEquatable(Of FileInformation)
    ''' <summary>
    ''' Attributes
    ''' </summary>
    Private _attributes As System.IO.FileAttributes
    ''' <summary>
    ''' File attributes.
    ''' </summary>
    ''' <value>The attributes.</value>
    Public Property Attributes() As System.IO.FileAttributes
        Get
            Return _attributes
        End Get
        Set(ByVal value As System.IO.FileAttributes)
            _attributes = value
        End Set
    End Property
    ''' <summary>
    ''' reation time
    ''' </summary>
    Private _creationTime As Date
    ''' <summary>
    ''' File creation time.
    ''' </summary>
    ''' <value>The creation time.</value>
    Public Property CreationTime() As Date
        Get
            Return _creationTime
        End Get
        Set(ByVal value As Date)
            _creationTime = value
        End Set
    End Property
    ''' <summary>
    ''' irectory name
    ''' </summary>
    Private _directoryName As String
    ''' <summary>
    ''' File directory name.
    ''' </summary>
    ''' <value>The name of the directory.</value>
    Public Property DirectoryName() As String
        Get
            Return _directoryName
        End Get
        Set(ByVal value As String)
            _directoryName = value
        End Set
    End Property
    ''' <summary>
    ''' Extension
    ''' </summary>
    Private _extension As String
    ''' <summary>
    ''' File extention.
    ''' </summary>
    ''' <value>The extension.</value>
    Public Property Extension() As String
        Get
            Return _extension
        End Get
        Set(ByVal value As String)
            _extension = value
        End Set
    End Property
    ''' <summary>
    ''' ull name
    ''' </summary>
    Private _fullName As String
    ''' <summary>
    ''' File full name.
    ''' </summary>
    ''' <value>The full name.</value>
    Public Property FullName() As String
        Get
            Return _fullName
        End Get
        Set(ByVal value As String)
            _fullName = value
        End Set
    End Property
    ''' <summary>
    ''' s read only
    ''' </summary>
    Private _isReadOnly As Boolean
    ''' <summary>
    ''' File is read only.
    ''' </summary>
    ''' <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
    Public Property IsReadOnly() As Boolean
        Get
            Return _isReadOnly
        End Get
        Set(ByVal value As Boolean)
            _isReadOnly = value
        End Set
    End Property
    ''' <summary>
    ''' ast access time
    ''' </summary>
    Private _lastAccessTime As Date
    ''' <summary>
    ''' File last access time.
    ''' </summary>
    ''' <value>The last access time.</value>
    Public Property LastAccessTime() As Date
        Get
            Return _lastAccessTime
        End Get
        Set(ByVal value As Date)
            _lastAccessTime = value
        End Set
    End Property
    ''' <summary>
    ''' ast write time
    ''' </summary>
    Private _lastWriteTime As Date
    ''' <summary>
    ''' File last write time.
    ''' </summary>
    ''' <value>The last write time.</value>
    Public Property LastWriteTime() As Date
        Get
            Return _lastWriteTime
        End Get
        Set(ByVal value As Date)
            _lastWriteTime = value
        End Set
    End Property
    ''' <summary>
    ''' Length
    ''' </summary>
    Private _length As Long
    ''' <summary>
    ''' File length.
    ''' </summary>
    ''' <value>The length.</value>
    Public Property Length() As Long
        Get
            Return _length
        End Get
        Set(ByVal value As Long)
            _length = value
        End Set
    End Property
    ''' <summary>
    ''' Name
    ''' </summary>
    Private _name As String
    ''' <summary>
    ''' File name.
    ''' </summary>
    ''' <value>The name.</value>
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the fully qualified type name of this instance.
    ''' </summary>
    ''' <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
    Public Overrides Function ToString() As String
        Return Me.FullName
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
    Public Shared Operator =(x As FileInformation, y As FileInformation) As Boolean
        Return x.Equals(y)
    End Operator

    ''' <summary>
    ''' Implements the operator &lt;&gt;.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator <>(x As FileInformation, y As FileInformation) As Boolean
        Return Not x.Equals(y)
    End Operator

    ''' <summary>
    ''' Indicates whether the current object is equal to another object of the same type.
    ''' </summary>
    ''' <param name="other">An object to compare with this object.</param>
    ''' <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
    Public Overloads Function Equals(other As FileInformation) As Boolean Implements IEquatable(Of FileInformation).Equals
        If IsNothing(other) Then
            Return False
        End If

        Return Me.FullName.Equals(other.FullName, StringComparison.CurrentCulture)

    End Function
End Structure