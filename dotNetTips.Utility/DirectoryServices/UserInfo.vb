' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-07-2016
'
' Last Modified By : David McCarter
' Last Modified On : 03-06-2017
' ***********************************************************************
' <copyright file="UserInfo.vb" company="McCarter Consulting - David McCarter">
'     '     David McCarter - dotNetTips.com © 2017
'
' </copyright>
' <summary></summary>
' *************************************************************************

Namespace DirectoryServices

    ''' <summary>
    ''' User's Active Directory Info
    ''' </summary>
    Public Structure UserInfo
        ''' <summary>
        ''' Account Contact Name
        ''' </summary>
        ''' <value>The name of the contact.</value>
        Public Property ContactName() As String

        ''' <summary>
        ''' Gets or sets the email.
        ''' </summary>
        ''' <value>The email.</value>
        Public Property Email() As String
        ''' <summary>
        ''' Gets or sets the department.
        ''' </summary>
        ''' <value>The department.</value>
        Public Property Department() As String
        ''' <summary>
        ''' Gets or sets the manager.
        ''' </summary>
        ''' <value>The manager.</value>
        Public Property Manager() As String
        ''' <summary>
        ''' Gets or sets the telephone.
        ''' </summary>
        ''' <value>The telephone.</value>
        Public Property Telephone() As String
        ''' <summary>
        ''' Gets or sets the title.
        ''' </summary>
        ''' <value>The title.</value>
        Public Property Title() As String
        ''' <summary>
        ''' Gets or sets the fax.
        ''' </summary>
        ''' <value>The fax.</value>
        Public Property Fax() As String
        ''' <summary>
        ''' Gets or sets the first name.
        ''' </summary>
        ''' <value>The first name.</value>
        Public Property FirstName() As String
        ''' <summary>
        ''' Gets or sets the last name.
        ''' </summary>
        ''' <value>The last name.</value>
        Public Property LastName() As String
        ''' <summary>
        ''' Gets or sets the delivery office.
        ''' </summary>
        ''' <value>The delivery office.</value>
        Public Property DeliveryOffice() As String

        ''' <summary>
        ''' Returns a <see cref="System.String" /> that represents this instance.
        ''' </summary>
        ''' <returns>A <see cref="System.String" /> that represents this instance.</returns>
        Public Overrides Function ToString() As String
            Return Me.ContactName
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
            Return DoesObjectEqualInstance(Me, obj)
        End Function

        ''' <summary>
        ''' Implements the operator =.
        ''' </summary>
        ''' <param name="x">The x.</param>
        ''' <param name="y">The y.</param>
        ''' <returns>The result of the operator.</returns>
        Public Shared Operator =(x As UserInfo, y As UserInfo) As Boolean
            Return x.Equals(y)
        End Operator

        ''' <summary>
        ''' Implements the operator &lt;&gt;.
        ''' </summary>
        ''' <param name="x">The x.</param>
        ''' <param name="y">The y.</param>
        ''' <returns>The result of the operator.</returns>
        Public Shared Operator <>(x As UserInfo, y As UserInfo) As Boolean
            Return Not x.Equals(y)
        End Operator
    End Structure
End Namespace