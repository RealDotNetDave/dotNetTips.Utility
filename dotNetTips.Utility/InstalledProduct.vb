' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 04-15-2016
' ***********************************************************************
' <copyright file="InstalledProduct.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' *************************************************************************
''' <summary>
''' Installed product information.
''' </summary>
Public Structure InstalledProduct
    ''' <summary>
    ''' Caption
    ''' </summary>
    Private _caption As String
    ''' <summary>
    ''' Product caption.
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
    ''' Description
    ''' </summary>
    Private _description As String
    ''' <summary>
    ''' Product description.
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
    ''' dentifying number
    ''' </summary>
    Private _identifyingNumber As String
    ''' <summary>
    ''' Product identifying number.
    ''' </summary>
    ''' <value>The identifying number.</value>
    Public Property IdentifyingNumber() As String
        Get
            Return _identifyingNumber
        End Get
        Set(ByVal value As String)
            _identifyingNumber = value
        End Set
    End Property
    ''' <summary>
    ''' nstall date
    ''' </summary>
    Private _installDate As Date
    ''' <summary>
    ''' Product installed date.
    ''' </summary>
    ''' <value>The install date.</value>
    Public Property InstallDate() As Date
        Get
            Return _installDate
        End Get
        Set(ByVal value As Date)
            _installDate = value
        End Set
    End Property
    ''' <summary>
    ''' nstall location
    ''' </summary>
    Private _installLocation As String
    ''' <summary>
    ''' Product installed location.
    ''' </summary>
    ''' <value>The install location.</value>
    Public Property InstallLocation() As String
        Get
            Return _installLocation
        End Get
        Set(ByVal value As String)
            _installLocation = value
        End Set
    End Property
    ''' <summary>
    ''' nstall state
    ''' </summary>
    Private _installState As Int32
    ''' <summary>
    ''' Product install state.
    ''' </summary>
    ''' <value>The state of the install.</value>
    Public Property InstallState() As Int32
        Get
            Return _installState
        End Get
        Set(ByVal value As Int32)
            _installState = value
        End Set
    End Property
    ''' <summary>
    ''' nstall state description
    ''' </summary>
    Private _installStateDescription As String
    ''' <summary>
    ''' Product install state description.
    ''' </summary>
    ''' <value>The install state description.</value>
    Public Property InstallStateDescription() As String
        Get
            Return _installStateDescription
        End Get
        Set(ByVal value As String)
            _installStateDescription = value
        End Set
    End Property
    ''' <summary>
    ''' Name
    ''' </summary>
    Private _name As String
    ''' <summary>
    ''' Product name.
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
    ''' ackage cache
    ''' </summary>
    Private _packageCache As String
    ''' <summary>
    ''' Product package cache.
    ''' </summary>
    ''' <value>The package cache.</value>
    Public Property PackageCache() As String
        Get
            Return _packageCache
        End Get
        Set(ByVal value As String)
            _packageCache = value
        End Set
    End Property
    ''' <summary>
    ''' ku number
    ''' </summary>
    Private _skuNumber As String
    ''' <summary>
    ''' Product SKU number.
    ''' </summary>
    ''' <value>The sku number.</value>
    Public Property SkuNumber() As String
        Get
            Return _skuNumber
        End Get
        Set(ByVal value As String)
            _skuNumber = value
        End Set
    End Property
    ''' <summary>
    ''' Vendor
    ''' </summary>
    Private _vendor As String
    ''' <summary>
    ''' Product vendor.
    ''' </summary>
    ''' <value>The vendor.</value>
    Public Property Vendor() As String
        Get
            Return _vendor
        End Get
        Set(ByVal value As String)
            _vendor = value
        End Set
    End Property
    ''' <summary>
    ''' Version
    ''' </summary>
    Private _version As String
    ''' <summary>
    ''' Product version.
    ''' </summary>
    ''' <value>The version.</value>
    Public Property Version() As String
        Get
            Return _version
        End Get
        Set(ByVal value As String)
            _version = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the fully qualified type name of this instance.
    ''' </summary>
    ''' <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
    Public Overrides Function ToString() As String
        Return Me.Name
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
    Public Shared Operator =(x As InstalledProduct, y As InstalledProduct) As Boolean
        Return x.Equals(y)
    End Operator

    ''' <summary>
    ''' Implements the operator &lt;&gt;.
    ''' </summary>
    ''' <param name="x">The x.</param>
    ''' <param name="y">The y.</param>
    ''' <returns>The result of the operator.</returns>
    Public Shared Operator <>(x As InstalledProduct, y As InstalledProduct) As Boolean
        Return Not x.Equals(y)
    End Operator
End Structure