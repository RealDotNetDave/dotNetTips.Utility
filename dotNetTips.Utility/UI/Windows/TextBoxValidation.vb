'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 06-09-2009
'
' Last Modified By : David McCarter
' Last Modified On : 06-09-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System.Drawing

Namespace UI.Windows
    ''' <summary>
    ''' Type of auto validation for a TextBox.
    ''' </summary>
    Public Enum TextBoxValidation
        ''' <summary>
        ''' Turns off validation.
        ''' </summary>
        ''' <remarks></remarks>
        None
        ''' <summary>
        ''' Validates credit card number.
        ''' </summary>
        ''' <remarks></remarks>
        CreditCard
        ''' <summary>
        ''' Custom validation.
        ''' </summary>
        ''' <remarks></remarks>
        Custom
        ''' <summary>
        ''' Validates date.
        ''' </summary>
        ''' <remarks></remarks>
        [Date]
        ''' <summary>
        ''' Validates domain address.
        ''' </summary>
        ''' <remarks></remarks>
        Domain
        ''' <summary>
        ''' Validates email address.
        ''' </summary>
        ''' <remarks></remarks>
        Email
        ''' <summary>
        ''' Validates file path.
        ''' </summary>
        ''' <remarks></remarks>
        FilePath
        ''' <summary>
        ''' Validates first and last name.
        ''' </summary>
        ''' <remarks></remarks>
        FirstLastName
        ''' <summary>
        ''' Validates ISBN number.
        ''' </summary>
        ''' <remarks></remarks>
        Isbn
        ''' <summary>
        ''' Validates number.
        ''' </summary>
        ''' <remarks></remarks>
        Number
        ''' <summary>
        ''' Validates US phone number.
        ''' </summary>
        ''' <remarks></remarks>
        Phone
        ''' <summary>
        ''' Validates scientific number.
        ''' </summary>
        ''' <remarks></remarks>
        Scientific
        ''' <summary>
        ''' Validates US Social Security number.
        ''' </summary>
        ''' <remarks></remarks>
        SocialSecurity
        ''' <summary>
        ''' Validates a string.
        ''' </summary>
        ''' <remarks></remarks>
        [String]
        ''' <summary>
        ''' Validates a temperature.
        ''' </summary>
        ''' <remarks></remarks>
        Temperature
        ''' <summary>
        ''' Validates a URL address.
        ''' </summary>
        ''' <remarks></remarks>
        Url
        ''' <summary>
        ''' Validates a US zip code.
        ''' </summary>
        ''' <remarks></remarks>
        ZipCode
    End Enum
End Namespace