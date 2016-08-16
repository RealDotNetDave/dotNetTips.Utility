' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 06-09-2009
'
' Last Modified By : David McCarter
' Last Modified On : 04-15-2016
' ***********************************************************************
' <copyright file="GeoLocationInfo.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' *************************************************************************
Namespace Web
    ''' <summary>
    ''' Geo Location Information
    ''' </summary>
    Public Structure GeoLocationInfo
        ''' <summary>
        ''' Latitude
        ''' </summary>
        Private _latitude As Decimal
        ''' <summary>
        ''' Gets or sets the latitude.
        ''' </summary>
        ''' <value>The latitude.</value>
        Public Property Latitude() As Decimal
            Get
                Return _latitude
            End Get
            Set(ByVal value As Decimal)
                _latitude = value
            End Set
        End Property
        ''' <summary>
        ''' Longitude
        ''' </summary>
        Private _longitude As Decimal
        ''' <summary>
        ''' Gets or sets the longitude.
        ''' </summary>
        ''' <value>The longitude.</value>
        Public Property Longitude() As Decimal
            Get
                Return _longitude
            End Get
            Set(ByVal value As Decimal)
                _longitude = value
            End Set
        End Property
        ''' <summary>
        ''' City
        ''' </summary>
        Private _city As String
        ''' <summary>
        ''' Gets or sets the city.
        ''' </summary>
        ''' <value>The city.</value>
        Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property
        ''' <summary>
        ''' ountry name
        ''' </summary>
        Private _countryName As String
        ''' <summary>
        ''' Gets or sets the name of the country.
        ''' </summary>
        ''' <value>The name of the country.</value>
        Public Property CountryName() As String
            Get
                Return _countryName
            End Get
            Set(ByVal value As String)
                _countryName = value
            End Set
        End Property
        ''' <summary>
        ''' ountry code
        ''' </summary>
        Private _countryCode As String
        ''' <summary>
        ''' Gets or sets the country code.
        ''' </summary>
        ''' <value>The country code.</value>
        Public Property CountryCode() As String
            Get
                Return _countryCode
            End Get
            Set(ByVal value As String)
                _countryCode = value
            End Set
        End Property
        ''' <summary>
        ''' Name
        ''' </summary>
        Private _name As String
        ''' <summary>
        ''' Gets or sets the name.
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
        ''' egion code
        ''' </summary>
        Private _regionCode As String
        ''' <summary>
        ''' Gets or sets the region code.
        ''' </summary>
        ''' <value>The region code.</value>
        Public Property RegionCode() As String
            Get
                Return _regionCode
            End Get
            Set(ByVal value As String)
                _regionCode = value
            End Set
        End Property
        ''' <summary>
        ''' egion name
        ''' </summary>
        Private _regionName As String
        ''' <summary>
        ''' Gets or sets the name of the region.
        ''' </summary>
        ''' <value>The name of the region.</value>
        Public Property RegionName() As String
            Get
                Return _regionName
            End Get
            Set(ByVal value As String)
                _regionName = value
            End Set
        End Property
        ''' <summary>
        ''' ostal code
        ''' </summary>
        Private _postalCode As String
        ''' <summary>
        ''' Gets or sets the postal code.
        ''' </summary>
        ''' <value>The postal code.</value>
        Public Property PostalCode() As String
            Get
                Return _postalCode
            End Get
            Set(ByVal value As String)
                _postalCode = value
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
        Public Shared Operator =(x As GeoLocationInfo, y As GeoLocationInfo) As Boolean
            Return x.Equals(y)
        End Operator

        ''' <summary>
        ''' Implements the operator &lt;&gt;.
        ''' </summary>
        ''' <param name="x">The x.</param>
        ''' <param name="y">The y.</param>
        ''' <returns>The result of the operator.</returns>
        Public Shared Operator <>(x As GeoLocationInfo, y As GeoLocationInfo) As Boolean
            Return Not x.Equals(y)
        End Operator


    End Structure
End Namespace