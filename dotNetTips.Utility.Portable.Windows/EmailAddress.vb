' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 03-21-2014
' ***********************************************************************
' <copyright file="EmailAddress.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Globalization

''' <summary>
''' Email address
''' </summary>
Public Structure EmailAddress

    Private _address As String
    Private _name As String
    Private _emailAddressType As EmailAddressType

    ''' <summary>
    ''' Address
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsAddressValid() As Boolean
        Return ValidationHelper.IsValidEmail(Me.Address)
    End Function

    ''' <summary>
    ''' Determines if contact name is valid.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsNameValid() As Boolean
        Return ValidationHelper.IsValidString(Me.Name)
    End Function

    ''' <summary>
    ''' Combines contact name and email address for use in sending email.
    ''' </summary>
    ''' <param name="name">Contact name</param>
    ''' <param name="address">Contact email address</param>
    ''' <returns>Combined address as <see cref="String"></see></returns>
    ''' <remarks></remarks>
    Public Shared Function CombineNameAddress(ByVal name As String, ByVal address As String) As String
        Return String.Format(CultureInfo.CurrentCulture, "{0} <{1}>", name, address)
    End Function

    ''' <summary>
    ''' Creates object.
    ''' </summary>
    ''' <param name="address">Sets email address</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal address As String)
        Me.Address = address
        Me.EmailAddressType = EmailAddressType.SendTo
    End Sub

    ''' <summary>
    ''' Creates object.
    ''' </summary>
    ''' <param name="address">Sets email address</param>
    ''' <param name="name">Sets contacts name</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal address As String, ByVal name As String)
        Me.New(address)
        Me.Name = name
    End Sub

    ''' <summary>
    ''' Gets the hash code.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Overrides Function GetHashCode() As Integer
        Throw New NotImplementedException()
    End Function

    ''' <summary>
    ''' Equalses the specified object.
    ''' </summary>
    ''' <param name="obj">The object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Overrides Function Equals(obj As [Object]) As Boolean
        Throw New NotImplementedException()
    End Function
End Structure