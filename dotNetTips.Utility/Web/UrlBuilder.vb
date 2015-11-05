' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-14-2013
'
' Last Modified By : David McCarter
' Last Modified On : 08-24-2013
' ***********************************************************************
' <copyright file="UrlBuilder.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System
Imports System.Collections.Specialized
Imports System.Threading
Imports dotNetTips.Utility.Extensions

Namespace Web
    ''' <summary>
    '''
    ''' </summary>
    Public Class UrlBuilder
        Inherits UriBuilder
        Private _queryString As StringDictionary = Nothing

#Region "Properties"

        ''' <summary>
        ''' Queries the string.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QueryString() As StringDictionary

            If _queryString Is Nothing Then
                _queryString = New StringDictionary()
            End If

            Return _queryString
        End Function

        ''' <summary>
        ''' Gets or sets the name of the page.
        ''' </summary>
        ''' <value>The name of the page.</value>
        ''' <remarks></remarks>
        ''' TODO: Move to new version
        Public Property PageName() As String
            Get
                Dim path As String = Me.Path
                Return path.Substring(path.LastIndexOf("/", StringComparison.CurrentCulture) + 1)
            End Get
            Set(value As String)
                Dim path As String = Me.Path
                If path.Contains("/") Then
                    path = path.Substring(0, path.LastIndexOf("/", StringComparison.CurrentCulture))
                End If
                Me.Path = String.Concat(path, "/", value)
            End Set
        End Property
#End Region

#Region "Constructor overloads"
        ''' <summary>
        ''' Initializes a new instance of the <see cref="UrlBuilder" /> class.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="UrlBuilder" /> class.
        ''' </summary>
        ''' <param name="uri">The URI.</param>
        ''' <remarks></remarks>
        Public Sub New(uri As String)
            MyBase.New(uri)
            Contracts.Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(uri) = False)

            PopulateQueryString()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="UrlBuilder" /> class.
        ''' </summary>
        ''' <param name="uri">The URI.</param>
        ''' <remarks></remarks>
        Public Sub New(uri As Uri)
            MyBase.New(uri)
            PopulateQueryString()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="UrlBuilder" /> class.
        ''' </summary>
        ''' <param name="schemeName">Name of the scheme.</param>
        ''' <param name="hostName">Name of the host.</param>
        ''' <remarks></remarks>
        Public Sub New(schemeName As String, hostName As String)
            MyBase.New(schemeName, hostName)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="UrlBuilder" /> class.
        ''' </summary>
        ''' <param name="scheme">The scheme.</param>
        ''' <param name="host">The host.</param>
        ''' <param name="portNumber">The port number.</param>
        ''' <remarks></remarks>
        Public Sub New(scheme As String, host As String, portNumber As Integer)
            MyBase.New(scheme, host, portNumber)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="UrlBuilder" /> class.
        ''' </summary>
        ''' <param name="scheme">The scheme.</param>
        ''' <param name="host">The host.</param>
        ''' <param name="port">The port.</param>
        ''' <param name="pathValue">The path value.</param>
        ''' <remarks></remarks>
        Public Sub New(scheme As String, host As String, port As Integer, pathValue As String)
            MyBase.New(scheme, host, port, pathValue)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="UrlBuilder" /> class.
        ''' </summary>
        ''' <param name="scheme">The scheme.</param>
        ''' <param name="host">The host.</param>
        ''' <param name="port">The port.</param>
        ''' <param name="path">The path.</param>
        ''' <param name="extraValue">The extra value.</param>
        ''' <remarks></remarks>
        Public Sub New(scheme As String, host As String, port As Integer, path As String, extraValue As String)
            MyBase.New(scheme, host, port, path, extraValue)
        End Sub

        'public UrlBuilder(System.Web.UI.Page page)
        '    : base(page.Request.Url.AbsoluteUri)
        '{
        '    PopulateQueryString();
        '}

#End Region

#Region "Public methods"
        ''' <summary>
        ''' Toes the string.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            GetQueryString()

            Return Me.Uri.AbsoluteUri
        End Function

        'public void Navigate()
        '{
        '    _Navigate(true);
        '}

        'public void Navigate(bool endResponse)
        '{
        '    _Navigate(endResponse);
        '}

        'private void _Navigate(bool endResponse)
        '{
        '    string uri = this.ToString();
        '    HttpContext.Current.Response.Redirect(uri, endResponse);
        '}
#End Region

#Region "Private methods"
        Private Sub PopulateQueryString()
            Dim query As String = Me.Query

            If String.IsNullOrEmpty(query) Then
                Return
            End If

            If _queryString Is Nothing Then
                _queryString = New StringDictionary()
            End If

            _queryString.Clear()

            query = query.Substring(1)
            'remove the ?
            Dim pairs As String() = query.Split(New Char() {"&"c})
            For Each s As String In pairs
                Dim pair As String() = s.Split(New Char() {"="c})

                _queryString(pair(0)) = If((pair.Length > 1), pair(1), String.Empty)
            Next
        End Sub

        Private Sub GetQueryString()
            Dim count As Integer = _queryString.Count

            If count = 0 Then
                Me.Query = String.Empty
                Return
            End If

            Dim keys As String() = New String(count - 1) {}
            Dim values As String() = New String(count - 1) {}
            Dim pairs As String() = New String(count - 1) {}

            _queryString.Keys.CopyTo(keys, 0)
            _queryString.Values.CopyTo(values, 0)

            For i As Integer = 0 To count - 1
                pairs(i) = String.Concat(keys(i), "=", values(i))
            Next

            Me.Query = String.Join("&", pairs)

        End Sub
#End Region
    End Class
End Namespace