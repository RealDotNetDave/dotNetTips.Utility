' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 08-16-2016
' ***********************************************************************
' <copyright file="DirectoryInformation.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' *************************************************************************
Imports dotNetTips.Utility.Portable
''' <summary>
''' Directory Information
''' </summary>
<Serializable>
Public Structure DirectoryInformation
    Implements IEquatable(Of DirectoryInformation)
    ''' <summary>
    ''' Attributes
    ''' </summary>
    Private _attributes As System.IO.FileAttributes
    ''' <summary>
    ''' Directory attributes.
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
    ''' Directory createtion time.
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
    ''' ull name
    ''' </summary>
    Private _fullName As String
    ''' <summary>
    ''' Directory full name.
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
    ''' ast access time
    ''' </summary>
    Private _lastAccessTime As Date
    ''' <summary>
    ''' Directory last access time.
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
    ''' Directory last write time.
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
    ''' Name
    ''' </summary>
    Private _name As String
    ''' <summary>
    ''' Directory name.
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
    ''' arent directory
    ''' </summary>
    Private _parentDirectory As String
    ''' <summary>
    ''' Parent directory.
    ''' </summary>
    ''' <value>The parent directory.</value>
    Public Property ParentDirectory() As String
        Get
            Return _parentDirectory
        End Get
        Set(ByVal value As String)
            _parentDirectory = value
        End Set
    End Property
    ''' <summary>
    ''' Root
    ''' </summary>
    Private _root As String
    ''' <summary>
    ''' Root.
    ''' </summary>
    ''' <value>The root.</value>
    Public Property Root() As String
        Get
            Return _root
        End Get
        Set(ByVal value As String)
            _root = value
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
    ''' Indicates whether the current object is equal to another object of the same type.
    ''' </summary>
    ''' <param name="other">An object to compare with this object.</param>
    ''' <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
    Public Overloads Function Equals(other As DirectoryInformation) As Boolean Implements IEquatable(Of DirectoryInformation).Equals
        If IsNothing(other) Then
            Return False
        End If

        Return Me.FullName.Equals(other.FullName, StringComparison.CurrentCulture)

    End Function

    ''' <summary>
    ''' Equals the specified object.
    ''' </summary>
    ''' <param name="obj">The object.</param>
    ''' <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
    Public Overloads Overrides Function Equals(obj As [Object]) As Boolean
        General.DoesObjectEqualInstance(obj, Me)
    End Function

    ''' <summary>
    ''' Implements the operator =.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator =(x As DirectoryInformation, y As DirectoryInformation) As Boolean
        Return x.Equals(y)
    End Operator

    ''' <summary>
    ''' Implements the operator &lt;&gt;.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator <>(x As DirectoryInformation, y As DirectoryInformation) As Boolean
        Return Not x.Equals(y)
    End Operator
End Structure