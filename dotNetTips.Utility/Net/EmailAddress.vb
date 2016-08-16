' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 04-15-2016
' ***********************************************************************
' <copyright file="EmailAddress.vb" company="David McCarter Consulting">
'     '     David McCarter Consulting. All rights reserved.
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Globalization
Namespace Net
    ''' <summary>
    ''' Email address
    ''' </summary>
    Public Structure EmailAddress

        ''' <summary>
        ''' Address
        ''' </summary>
        Private _address As String
        ''' <summary>
        ''' Name
        ''' </summary>
        Private _name As String
        ''' <summary>
        ''' mail address type
        ''' </summary>
        Private _emailAddressType As EmailAddressType

        ''' <summary>
        ''' Address
        ''' </summary>
        ''' <value>The address.</value>
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the type of the email address.
        ''' </summary>
        ''' <value>The type of the email address.</value>
        Public Property EmailAddressType() As EmailAddressType
            Get
                Return _emailAddressType
            End Get
            Set(ByVal value As EmailAddressType)
                Me._emailAddressType = value
            End Set
        End Property
        ''' <summary>
        ''' Contacts name.
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
        ''' Determines if email address is valid.
        ''' </summary>
        ''' <returns><c>true</c> if [is address valid]; otherwise, <c>false</c>.</returns>
        Public Function IsAddressValid() As Boolean
            Return dotNetTips.Utility.Portable.Windows.ValidationHelper.IsValidEmail(Me.Address)
        End Function

        ''' <summary>
        ''' Determines if contact name is valid.
        ''' </summary>
        ''' <returns><c>true</c> if [is name valid]; otherwise, <c>false</c>.</returns>
        Public Function IsNameValid() As Boolean
            Return dotNetTips.Utility.Portable.Windows.ValidationHelper.IsValidString(Me.Name)
        End Function

        ''' <summary>
        ''' Combines contact name and email address for use in sending email.
        ''' </summary>
        ''' <param name="name">Contact name</param>
        ''' <param name="address">Contact email address</param>
        ''' <returns>Combined address as <see cref="String"></see></returns>
        Public Shared Function CombineNameAddress(ByVal name As String, ByVal address As String) As String
            Return String.Format(CultureInfo.CurrentCulture, "{0} <{1}>", name, address)
        End Function

        ''' <summary>
        ''' Creates object.
        ''' </summary>
        ''' <param name="address">Sets email address</param>
        Public Sub New(ByVal address As String)
            Me.Address = address
            Me.EmailAddressType = EmailAddressType.SendTo
        End Sub

        ''' <summary>
        ''' Creates object.
        ''' </summary>
        ''' <param name="address">Sets email address</param>
        ''' <param name="name">Sets contacts name</param>
        Public Sub New(ByVal address As String, ByVal name As String)
            Me.New(address)
            Me.Name = name
        End Sub

        ''' <summary>
        ''' Gets the hash code.
        ''' </summary>
        ''' <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        Public Overloads Overrides Function GetHashCode() As Integer
            General.GetInstanceHashCode(Me)
        End Function

        ''' <summary>
        ''' Equalses the specified object.
        ''' </summary>
        ''' <param name="obj">The object.</param>
        ''' <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
        Public Overloads Overrides Function Equals(obj As [Object]) As Boolean
            Return General.DoesObjectEqualInstance(Me, obj)
        End Function

        ''' <summary>
        ''' Implements the operator =.
        ''' </summary>
        ''' <param name="x">The x.</param>
        ''' <param name="y">The y.</param>
        ''' <returns>The result of the operator.</returns>
        Public Shared Operator =(x As EmailAddress, y As EmailAddress) As Boolean
            Return x.Equals(y)
        End Operator

        ''' <summary>
        ''' Implements the operator &lt;&gt;.
        ''' </summary>
        ''' <param name="x">The x.</param>
        ''' <param name="y">The y.</param>
        ''' <returns>The result of the operator.</returns>
        Public Shared Operator <>(x As EmailAddress, y As EmailAddress) As Boolean
            Return Not x.Equals(y)
        End Operator
    End Structure
End Namespace