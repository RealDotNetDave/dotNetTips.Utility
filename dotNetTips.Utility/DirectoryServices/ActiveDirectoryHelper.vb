' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 03-29-2016
'
' Last Modified By : David McCarter
' Last Modified On : 04-12-2016
' ***********************************************************************
' <copyright file="ActiveDirectoryHelper.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.DirectoryServices
Imports System.Diagnostics.Contracts
Imports System.Collections.Generic

Namespace DirectoryServices
    ''' <summary>
    ''' Active Directory Helper Class
    ''' </summary>
    ''' <remarks>Common Active Directory helper methods.</remarks>
    Public Module ActiveDirectoryHelper
        ''' <summary>
        ''' The account filter
        ''' </summary>
        Private Const AccountFilter As String = "(SAMAccountName={0})"
        ''' <summary>
        ''' The contact delivery office
        ''' </summary>
        Private Const ContactDeliveryOffice As String = "physicalDeliveryOfficeName"

        ''' <summary>
        ''' The contact department
        ''' </summary>
        Private Const ContactDepartment As String = "department"
        ''' <summary>
        ''' The contact email
        ''' </summary>
        Private Const ContactEmail As String = "mail"
        ''' <summary>
        ''' The contact fax
        ''' </summary>
        Private Const ContactFax As String = "facsimileTelephoneNumber"

        ''' <summary>
        ''' The contact first name
        ''' </summary>
        Private Const ContactFirstName As String = "givenName"

        ''' <summary>
        ''' The contact last name
        ''' </summary>
        Private Const ContactLastName As String = "sn"

        ''' <summary>
        ''' The contact manager
        ''' </summary>
        Private Const ContactManager As String = "manager"
        ''' <summary>
        ''' The contact name
        ''' </summary>
        Private Const ContactName As String = "cn"
        ''' <summary>
        ''' The contact name filter
        ''' </summary>
        Private Const ContactNameFilter As String = "(CN={0})"

        ''' <summary>
        ''' The contact telephone
        ''' </summary>
        Private Const ContactTelephone As String = "telephoneNumber"

        ''' <summary>
        ''' The contact title
        ''' </summary>
        Private Const ContactTitle As String = "title"

        ''' <summary>
        ''' The distinguished name
        ''' </summary>
        Private Const DistinguishedName As String = "distinguishedName"
        ''' <summary>
        ''' The LDAP prefix
        ''' </summary>
        Private Const LDAPPrefix As String = "LDAP://"
        ''' <summary>
        ''' The member name
        ''' </summary>
        Private Const MemberName As String = "member"
        ''' <summary>
        ''' The SAM account name
        ''' </summary>
        Private Const SAMAccountName As String = NameOf(SAMAccountName)
        ''' <summary>
        ''' Extracts the AD property value.
        ''' </summary>
        ''' <param name="propertyName">Name of the property.</param>
        ''' <param name="result">The result.</param>
        ''' <returns>System.String.</returns>
        ''' <remarks></remarks>
        Public Function ExtractADPropertyValue(ByVal propertyName As String, ByVal result As SearchResult) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(propertyName) = False, "propertyName is nothing or empty.")
            Contract.Requires(Of ArgumentNullException)(result IsNot nothing, "result is nothing.")

            Return If(result.Properties.Contains(propertyName), result.Properties(propertyName).Item(0).ToString(), String.Empty)

        End Function

        ''' <summary>
        ''' Extracts the AD property value.
        ''' </summary>
        ''' <param name="propertyName">Name of the property.</param>
        ''' <param name="entry">The entry.</param>
        ''' <returns>System.String.</returns>
        ''' <remarks></remarks>
        Public Function ExtractADPropertyValue(ByVal propertyName As String, ByVal entry As DirectoryEntry) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(propertyName) = False, "propertyName is nothing or empty.")
            Contract.Requires(Of ArgumentNullException)(entry Isnot Nothing, "entry is nothing.")

            Return If(entry.Properties.Contains(propertyName), entry.Properties(propertyName).Item(0).ToString(), String.Empty)

        End Function

        ''' <summary>
        ''' Verifies the current user in belongs to the given Active Directory group
        ''' </summary>
        ''' <param name="group">Group name</param>
        ''' <returns>True if in group</returns>
        ''' <remarks></remarks>
        Public Function IsUserInGroup(ByVal group As String) As Boolean
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(group) = False, "group is nothing or empty.")

            Return If(ApplicationHelper.IsAspNet, System.Web.HttpContext.Current.User.IsInRole(group), My.User.IsInRole(group))

        End Function

        ''' <summary>
        ''' Verifies the Active Directory Account Name belongs to one of the given groups.
        ''' </summary>
        ''' <param name="userLogin">Account Name</param>
        ''' <param name="groups">Group(s)</param>
        ''' <returns>True if the Account Name belongs to at least one group.</returns>
        ''' <exception cref="System.ArgumentNullException">logIn</exception>
        ''' <remarks></remarks>
        Public Function IsUserInGroup(ByVal userLogin As String, ByVal ParamArray groups As String()) As Boolean
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(userLogin) = False, "userLogin is nothing or empty.")
            Contract.Requires(groups IsNot Nothing AndAlso groups.Length <> 0, "groups is nothing or empty.")

            Using searcher = New DirectorySearcher()

                SetSearcherDomain(userLogin, searcher)

                For Each groupName As String In groups.AsParallel

                    searcher.Filter = String.Format(CultureInfo.CurrentCulture, ContactNameFilter, groupName)

                    Dim account As String = String.Empty
                    SplitLogin(userLogin, Nothing, account)

                    For Each result As SearchResult In searcher.FindAll.AsParallel

                        Dim resultProperties As ResultPropertyCollection = result.Properties

                        For Each members As Object In resultProperties(MemberName)

                            Using memberEntry As DirectoryEntry = New DirectoryEntry(String.Format("{0}{1}", LDAPPrefix, members))

                                'Validate if user in the group
                                If account = ExtractADPropertyValue(SAMAccountName, memberEntry) Then
                                    Return True
                                End If

                            End Using

                        Next

                    Next

                Next

            End Using

            Return False

        End Function

        ''' <summary>
        ''' Verifies the Active Directory Account Name belongs to the given groups.
        ''' </summary>
        ''' <param name="accountName">Account Name</param>
        ''' <param name="groupName">Group name</param>
        ''' <returns>True if the Account Name belongs to the group</returns>
        ''' <remarks></remarks>
        Public Function IsUserInGroup(ByVal accountName As String, ByVal groupName As String) As Boolean
            Return IsUserInGroup(accountName, New String() {groupName})
        End Function

        ''' <summary>
        ''' Loads all Active Directory users.
        ''' </summary>
        ''' <returns>System.Collections.ObjectModel.ReadOnlyCollection(Of UserInfo).</returns>
        ''' <remarks></remarks>
        Public Function LoadAllUsers() As System.Collections.ObjectModel.ReadOnlyCollection(Of UserInfo)
            Return LoadAllUsers(Nothing)
        End Function

        ''' <summary>
        ''' Loads all the users in the current domain.
        ''' </summary>
        ''' <param name="domain">The domain.</param>
        ''' <returns>List of <see cref="System.Collections.ObjectModel.ReadOnlyCollection(Of UserInfo)"></see></returns>
        ''' <remarks></remarks>
        Public Function LoadAllUsers(ByVal domain As String) As System.Collections.ObjectModel.ReadOnlyCollection(Of UserInfo)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(domain) = False, "domain is nothing or empty.")

            Dim users As New System.Collections.Generic.List(Of UserInfo)

            Using searcher As New DirectorySearcher

                'Set Domain if available

                If Not String.IsNullOrEmpty(domain) Then
                    searcher.SearchRoot = New DirectoryEntry(String.Format(CultureInfo.InvariantCulture, "{0}{1}", LDAPPrefix, domain))
                End If

                searcher.Filter = "(&(objectClass=Person)(objectClass=User)(!objectClass=computer))"
                searcher.Sort.Direction = SortDirection.Ascending
                searcher.Sort.PropertyName = ContactName

                'Load Properties that we want
                LoadUserProperties(searcher)

                For Each result As SearchResult In searcher.FindAll
                    Dim info As New UserInfo() With {.ContactName = ExtractADPropertyValue(ContactName, result), .Email = ExtractADPropertyValue(ContactEmail, result), .Department = ExtractADPropertyValue(ContactDepartment, result), .Manager = GetManagerName(ExtractADPropertyValue(ContactManager, result)), .Telephone = ExtractADPropertyValue(ContactTelephone, result), .Title = ExtractADPropertyValue(ContactTitle, result), .FirstName = ExtractADPropertyValue(ContactFirstName, result), .LastName = ExtractADPropertyValue(ContactLastName, result), .Fax = ExtractADPropertyValue(ContactFax, result), .DeliveryOffice = ExtractADPropertyValue(ContactDeliveryOffice, result)}

                    users.Add(info)

                Next

            End Using

            Return New System.Collections.ObjectModel.ReadOnlyCollection(Of UserInfo)(users)

        End Function

        ''' <summary>
        ''' Splits the login.
        ''' </summary>
        ''' <param name="logIn">The log in.</param>
        ''' <param name="domain">The domain.</param>
        ''' <param name="accountName">Name of the account.</param>
        ''' <remarks></remarks>
        Public Sub SplitLogin(ByRef logIn As String, ByRef domain As String, ByRef accountName As String)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(login) = False, "login is nothing or empty.")

            logIn = logIn.Replace(Portable.ControlChars.ForwardSlash, Portable.ControlChars.BackSlash)

            If logIn.Contains(Portable.ControlChars.BackSlash) Then
                With logIn.Split(Portable.ControlChars.BackSlash)
                    If Not domain Is Nothing Then
                        domain = .GetValue(0).ToString
                    End If
                    If Not accountName Is Nothing Then
                        accountName = .GetValue(1).ToString
                    End If
                End With
            Else
                accountName = logIn
            End If
        End Sub

        ''' <summary>
        ''' Retrieves the users Active Directory Contact Name.
        ''' </summary>
        ''' <param name="accountName">Account Name</param>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Function UserContactName(ByVal accountName As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(accountName) = False, "accountName is nothing or empty.")

            Return UserInfo(accountName).ContactName

        End Function

        ''' <summary>
        ''' Users directory entry.
        ''' </summary>
        ''' <param name="distinguishedName">Name of the distinguished.</param>
        ''' <returns>DirectoryEntry.</returns>
        ''' <remarks></remarks>
        Public Function UserDirectoryEntry(ByVal distinguishedName As String) As DirectoryEntry
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(distinguishedName) = False, "distringuised is nothing or empty.")

            Return New DirectoryEntry(LDAPPrefix + distinguishedName)

        End Function

        ''' <summary>
        ''' Looks up the LDAP distinguished name of a user.
        ''' </summary>
        ''' <param name="userLogin">The user login.</param>
        ''' <returns><see cref="String"></see><example>Distinguished name similar to: CN=mylogin,CN=Users,DC=mycompany,DC=com</example></returns>
        ''' <remarks></remarks>
        Public Function UserDistinguishedName(ByVal userLogin As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(userLogin) = False, "userLogin is nothing or empty.")

            Dim name As String = String.Empty

            Using searcher As New DirectorySearcher
                searcher.CacheResults = True

                SetSearcherDomain(userLogin, searcher)

                Dim account As String = String.Empty

                SplitLogin(userLogin, Nothing, account)

                searcher.Filter = String.Format(CultureInfo.InvariantCulture, AccountFilter, account)
                searcher.PropertiesToLoad.Add(DistinguishedName)

                Dim result As SearchResult = searcher.FindOne

                If Not IsNothing(result) Then
                    name = ExtractADPropertyValue(DistinguishedName, result)
                End If

            End Using

            Return name

        End Function

        ''' <summary>
        ''' Retrieves the users Active Directory E-Mail address
        ''' </summary>
        ''' <param name="accountName">Account Name</param>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Function UserEmail(ByVal accountName As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(accountName) = False, "accountName is nothing or empty.")

            Return UserInfo(accountName).Email

        End Function

        ''' <summary>
        ''' Retrieves the users Active Directory information.
        ''' </summary>
        ''' <param name="userLogin">Account Name</param>
        ''' <returns>UserInfo</returns>
        ''' <remarks></remarks>
        Public Function UserInfo(ByVal userLogin As String) As UserInfo
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(userLogin) = False, "userLogin is nothing or empty.")

            Dim info As New UserInfo

            Using searcher As New DirectorySearcher

                SetSearcherDomain(userLogin, searcher)

                Dim account As String = String.Empty

                SplitLogin(userLogin, Nothing, account)

                searcher.Filter = String.Format(CultureInfo.InvariantCulture, AccountFilter, account)

                LoadUserProperties(searcher)

                Dim result As SearchResult = searcher.FindOne

                If IsNothing(result) Then
                    Return info
                End If

                info.ContactName = ExtractADPropertyValue(ContactName, result)
                info.Email = ExtractADPropertyValue(ContactEmail, result)
                info.Department = ExtractADPropertyValue(ContactDepartment, result)
                info.Manager = GetManagerName(ExtractADPropertyValue(ContactManager, result))
                info.Telephone = ExtractADPropertyValue(ContactTelephone, result)
                info.Title = ExtractADPropertyValue(ContactTitle, result)
                info.FirstName = ExtractADPropertyValue(ContactFirstName, result)
                info.LastName = ExtractADPropertyValue(ContactLastName, result)
                info.Fax = ExtractADPropertyValue(ContactFax, result)
                info.DeliveryOffice = ExtractADPropertyValue(ContactDeliveryOffice, result)

            End Using

            Return info

        End Function
        ''' <summary>
        ''' Finds all users in a group.
        ''' </summary>
        ''' <param name="domain">The domain.</param>
        ''' <param name="group">The group.</param>
        ''' <returns>System.Collections.ObjectModel.ReadOnlyCollection(Of UserInfo).</returns>
        ''' <remarks></remarks>
        Public Function UsersInGroup(ByVal domain As String, ByVal group As String) As IEnumerable(Of UserInfo)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(domain) = False, "domain is nothing or empty.")
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(group) = False, "group is nothing or empty.")

            Dim users As New Generic.List(Of UserInfo)

            Using searcher As New DirectorySearcher

                'Set Domain if available
                If Not String.IsNullOrEmpty(domain) Then
                    searcher.SearchRoot = New DirectoryEntry(String.Format(CultureInfo.InvariantCulture, "{0}{1}", LDAPPrefix, domain))
                End If

                searcher.Filter = String.Format(CultureInfo.InvariantCulture, "(&(objectCategory=group)(sAMAccountName={0}))", group)

                For Each result As SearchResult In searcher.FindAll.AsParallel

                    Dim resultProperties As ResultPropertyCollection = result.Properties
                    For Each member As Object In resultProperties(MemberName)
                        Using entry As DirectoryEntry = UserDirectoryEntry(member.ToString)
                            users.Add(FillUserInfo(entry))
                        End Using
                    Next

                Next

            End Using

            Return users.AsEnumerable

        End Function

        ''' <summary>
        ''' Validates an Active Directory Account Name
        ''' </summary>
        ''' <param name="userLogin">Account Name</param>
        ''' <returns>True if Account Name is active in the current domain.</returns>
        ''' <remarks></remarks>
        Public Function ValidateAccountName(ByVal userLogin As String) As Boolean
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(userLogin) = False, "userLogin is nothing or empty.")

            Dim valid As Boolean

            Using searcher As New DirectorySearcher

                SetSearcherDomain(userLogin, searcher)

                Dim account As String = String.Empty

                SplitLogin(userLogin, Nothing, account)

                searcher.Filter = String.Format(CultureInfo.InvariantCulture, AccountFilter, account)

                Dim result As SearchResult = searcher.FindOne

                If Not IsNothing(result) Then
                    valid = True
                End If

            End Using

            Return valid

        End Function

        ''' <summary>
        ''' Fills the user information.
        ''' </summary>
        ''' <param name="entry">The entry.</param>
        ''' <returns>UserInfo.</returns>
        ''' <remarks></remarks>
        Private Function FillUserInfo(ByVal entry As DirectoryEntry) As UserInfo

            Dim info As New UserInfo

            If entry Is Nothing Then
                Return info
            End If

            info.ContactName = ExtractADPropertyValue(ContactName, entry)
            info.Email = ExtractADPropertyValue(ContactEmail, entry)
            info.Department = ExtractADPropertyValue(ContactDepartment, entry)
            info.Manager = GetManagerName(ExtractADPropertyValue(ContactManager, entry))
            info.Telephone = ExtractADPropertyValue(ContactTelephone, entry)
            info.Title = ExtractADPropertyValue(ContactTitle, entry)
            info.FirstName = ExtractADPropertyValue(ContactFirstName, entry)
            info.LastName = ExtractADPropertyValue(ContactLastName, entry)
            info.Fax = ExtractADPropertyValue(ContactFax, entry)
            info.DeliveryOffice = ExtractADPropertyValue(ContactDeliveryOffice, entry)

            Return info

        End Function

        ''' <summary>
        ''' Gets the name of the manager.
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <returns>System.String.</returns>
        ''' <remarks></remarks>
        Private Function GetManagerName(ByVal name As String) As String
            Dim managerName As String = String.Empty

            If Not String.IsNullOrEmpty(name) Then
                If name.Split(Portable.ControlChars.Comma).Length > 0 Then
                    managerName = name.Split(Portable.ControlChars.Comma)(0).Remove(0, 3)
                End If
            End If

            Return managerName

        End Function

        ''' <summary>
        ''' Loads the user properties.
        ''' </summary>
        ''' <param name="searcher">The searcher.</param>
        ''' <remarks></remarks>
        Private Sub LoadUserProperties(ByVal searcher As DirectorySearcher)
            searcher.PropertiesToLoad.Add(ContactName)
            searcher.PropertiesToLoad.Add(ContactEmail)
            searcher.PropertiesToLoad.Add(ContactDepartment)
            searcher.PropertiesToLoad.Add(ContactManager)
            searcher.PropertiesToLoad.Add(ContactTelephone)
            searcher.PropertiesToLoad.Add(ContactTitle)
            searcher.PropertiesToLoad.Add(ContactFirstName)
            searcher.PropertiesToLoad.Add(ContactLastName)
            searcher.PropertiesToLoad.Add(ContactFax)
            searcher.PropertiesToLoad.Add(ContactDeliveryOffice)
        End Sub
        ''' <summary>
        ''' Sets the searcher domain.
        ''' </summary>
        ''' <param name="userLogin">The user login.</param>
        ''' <param name="searcher">The searcher.</param>
        ''' <remarks></remarks>
        Private Sub SetSearcherDomain(ByVal userLogin As String, ByVal searcher As DirectorySearcher)
            Dim domain As String = String.Empty

            SplitLogin(userLogin, domain, Nothing)

            'Set Domain if available
            If Not String.IsNullOrEmpty(domain) Then
                searcher.SearchRoot = New DirectoryEntry(String.Format(CultureInfo.InvariantCulture, "{0}{1}", LDAPPrefix, domain))
            End If

        End Sub

    End Module
End Namespace