' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 06-26-2017
' ***********************************************************************
' <copyright file="ValidationHelper.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' *************************************************************************
Imports dotNetTips.Utility.Portable.OOP
Imports dotNetTips.Utility.Portable.Windows.My.Resources
Imports System.Text.RegularExpressions

''' <summary>
''' Validation helper class.
''' </summary>
Public Module ValidationHelper

    ''' <summary>
    ''' Determines whether the specified expression is date.
    ''' </summary>
    ''' <param name="expression">The expression.</param>
    ''' <returns><c>true</c> if the specified expression is date; otherwise, <c>false</c>.</returns>
    Friend Function IsDate(ByVal expression As Object) As Boolean
        Dim time As DateTime

        If expression Is Nothing Then
            Return False
        End If

        If TypeOf expression Is DateTime Then
            Return True
        End If

        Dim str = TryCast(expression, String)

        Return ((Not str Is Nothing) AndAlso Date.TryParse(str, (time)))
    End Function

    ''' <summary>
    ''' Validates the string.
    ''' </summary>
    ''' <param name="input">The text to validate.</param>
    ''' <param name="expression">RegEx expression to use for validation.</param>
    ''' <returns>True if valid.</returns>
    Friend Function ValidateString(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False, "Must send in valid input string.")
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(expression) = False, "Must send in valid regular expression")

        Return (New Regex(expression)).IsMatch(input)
    End Function

    ''' <summary>
    ''' Validates the length of a string.
    ''' </summary>
    ''' <param name="input">Text to validate.</param>
    ''' <param name="maximum">Maximum length.</param>
    ''' <returns>True if valid.</returns>
    Public Function CheckLength(ByVal input As String, ByVal maximum As Int32) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)
        Encapsulation.TryValidateParam(Of ArgumentOutOfRangeException)((maximum < 0) Or (maximum > Int32.MaxValue))

        Return If((input.Length > maximum), False, True)

    End Function
    ''' <summary>
    ''' Validates the length of a string.
    ''' </summary>
    ''' <param name="input">Text to validate.</param>
    ''' <param name="minimum">Minimum length.</param>
    ''' <param name="maximum">Maximum length.</param>
    ''' <returns>True of valid.</returns>
    Public Function CheckLength(ByVal input As String, ByVal minimum As Int32, ByVal maximum As Int32) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)
        Encapsulation.TryValidateParam(Of ArgumentOutOfRangeException)((minimum < 0) Or (minimum > Int32.MaxValue))
        Encapsulation.TryValidateParam(Of ArgumentOutOfRangeException)((maximum < 0) Or (maximum > Int32.MaxValue))

        Return If((input.Length >= minimum) And (input.Length <= maximum), True, False)

    End Function

    ''' <summary>
    ''' Determines whether [is valid credit card] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <returns><c>true</c> if [is valid credit card] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidCreditCard(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_CREDIT_CARD)

    End Function

    ''' <summary>
    ''' Determines whether [is valid credit card] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="expression">The regular expression.</param>
    ''' <returns><c>true</c> if [is valid credit card] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidCreditCard(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_CREDIT_CARD
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Validates value is a date.
    ''' </summary>
    ''' <param name="input">Text to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidDate(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return IsDate(input)

    End Function

    ''' <summary>
    ''' Determines whether [is valid domain] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <returns><c>true</c> if [is valid domain] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidDomain(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_DOMAIN)

    End Function

    ''' <summary>
    ''' Determines whether [is valid domain] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="expression">The regular expression.</param>
    ''' <returns><c>true</c> if [is valid domain] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidDomain(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_DOMAIN
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Validates if value is valid email address.
    ''' </summary>
    ''' <param name="input">Address to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidEmail(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_EMAIL)

    End Function

    ''' <summary>
    ''' Validates if value is valid email address.
    ''' </summary>
    ''' <param name="input">Address to validate.</param>
    ''' <param name="expression">RegEx expression to use to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidEmail(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_EMAIL
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Determines whether [is valid file path] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <returns><c>true</c> if [is valid file path] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidFilePath(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_FILE_PATH)

    End Function

    ''' <summary>
    ''' Determines whether [is valid file path] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="expression">The regular expression.</param>
    ''' <returns><c>true</c> if [is valid file path] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidFilePath(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_FILE_PATH
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Determines whether [is valid first last name] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <returns><c>true</c> if [is valid first last name] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidFirstLastName(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_FIRST_LAST_NAME)

    End Function

    ''' <summary>
    ''' Determines whether [is valid first last name] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="expression">The regular expression.</param>
    ''' <returns><c>true</c> if [is valid first last name] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidFirstLastName(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_FIRST_LAST_NAME
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Determines whether [is valid isbn] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <returns><c>true</c> if [is valid isbn] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidIsbn(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_ISBN)

    End Function

    ''' <summary>
    ''' Determines whether [is valid isbn] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="expression">The regular expression.</param>
    ''' <returns><c>true</c> if [is valid isbn] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidIsbn(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_ISBN
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Validates if value is valid US phone number.
    ''' </summary>
    ''' <param name="input">Phone number to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidPhone(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_US_PHONE_NUMBER)
    End Function

    ''' <summary>
    ''' Validates if value is valid phone number.
    ''' </summary>
    ''' <param name="input">Phone number to validate.</param>
    ''' <param name="expression">RegEx expression to use to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidPhone(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_US_PHONE_NUMBER
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Validates if value is valid US zip code.
    ''' </summary>
    ''' <param name="input">Zip code to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidPostalCode(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_US_ZIP_CODE)
    End Function

    ''' <summary>
    ''' Validates if value is valid postal code, defaults to US zip code.
    ''' </summary>
    ''' <param name="input">Postal code to validate.</param>
    ''' <param name="expression">RegEx expression to use to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidPostalCode(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_US_ZIP_CODE
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Determines whether [is valid scientific] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <returns><c>true</c> if [is valid scientific] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidScientific(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_SCIENENTIFIC)

    End Function

    ''' <summary>
    ''' Determines whether [is valid scientific] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="expression">The regular expression.</param>
    ''' <returns><c>true</c> if [is valid scientific] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidScientific(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_SCIENENTIFIC
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Validates if value is valid US Social Security number.
    ''' </summary>
    ''' <param name="input">Social Security number to validate.</param>
    ''' <returns>True if valid Social Security number.</returns>
    Public Function IsValidSocialSecurity(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_US_SSN)

    End Function

    ''' <summary>
    ''' Validates if values is valid Social Security number.
    ''' </summary>
    ''' <param name="input">Social Security number to validate.</param>
    ''' <param name="expression">RegEx expression to use to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidSocialSecurity(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_US_SSN
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Determines whether [is valid string] [the specified input].
    ''' </summary>
    ''' <param name="input">The text to validate.</param>
    ''' <returns><c>true</c> if [is valid string] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidString(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_STRING)

    End Function

    ''' <summary>
    ''' Determines whether [is valid string] [the specified input].
    ''' </summary>
    ''' <param name="input">The text to validate.</param>
    ''' <param name="expression">RegEx expression to use for validation.</param>
    ''' <returns><c>true</c> if [is valid string] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidString(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_STRING
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Determines whether [is valid string] [the specified input].
    ''' </summary>
    ''' <param name="input">The text to validate.</param>
    ''' <param name="safeForWeb">if set to <c>true</c> [safe for web].</param>
    ''' <returns><c>true</c> if [is valid string] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidString(ByVal input As String, ByVal safeForWeb As Boolean) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return If(safeForWeb AndAlso ValidateString(input, REGEXP_STRING), Not ValidateString(input, REGEXP_STRING_WEB_SAFE), ValidateString(input, REGEXP_STRING))

    End Function

    ''' <summary>
    ''' Determines whether [is valid string] [the specified input].
    ''' </summary>
    ''' <param name="input">The text to validate.</param>
    ''' <param name="safeForWeb">if set to <c>true</c> [safe for web].</param>
    ''' <param name="expression">RegEx expression to use for validation.</param>
    ''' <returns><c>true</c> if [is valid string] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidString(ByVal input As String, ByVal safeForWeb As Boolean, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_STRING
        End If

        Return If(safeForWeb AndAlso ValidateString(input, expression), Not ValidateString(input, REGEXP_STRING_WEB_SAFE), ValidateString(input, expression))

    End Function

    ''' <summary>
    ''' Determines whether [is valid temperature] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <returns><c>true</c> if [is valid temperature] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidTemperature(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_TEMP)

    End Function

    ''' <summary>
    ''' Determines whether [is valid temperature] [the specified input].
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="expression">The regular expression.</param>
    ''' <returns><c>true</c> if [is valid temperature] [the specified input]; otherwise, <c>false</c>.</returns>
    Public Function IsValidTemperature(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_TEMP
        End If

        Return ValidateString(input, expression)

    End Function

    ''' <summary>
    ''' Validates if value is valid URL.
    ''' </summary>
    ''' <param name="input">URL to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidUrl(ByVal input As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        Return ValidateString(input, REGEXP_URL)

    End Function

    ''' <summary>
    ''' Validates if value is a valid URL.
    ''' </summary>
    ''' <param name="input">URL to validate.</param>
    ''' <param name="expression">RegEx expression to use to validate.</param>
    ''' <returns>True if valid.</returns>
    Public Function IsValidUrl(ByVal input As String, ByVal expression As String) As Boolean
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

        If String.IsNullOrEmpty(expression) Then
            expression = REGEXP_URL
        End If

        Return ValidateString(input, expression)

    End Function

End Module