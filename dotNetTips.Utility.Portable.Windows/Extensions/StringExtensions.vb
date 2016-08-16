' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 08-05-2016
' ***********************************************************************
' <copyright file="StringExtensions.vb" company="David McCarter Consulting">
'     '     '     David McCarter Consulting. All rights reserved.
'
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Text.RegularExpressions

Namespace Extensions

    ''' <summary>
    ''' String Extensions
    ''' </summary>
    Public Module StringExtensions

        ''' <summary>
        ''' Determines whether [is not null or empty] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="minimum">The minimum.</param>
        ''' <param name="maximum">The maximum.</param>
        ''' <returns><c>true</c> if [is not null or empty] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function CheckLength(ByVal value As String, ByVal minimum As Integer, ByVal maximum As Integer) As Boolean
            Return ValidationHelper.CheckLength(value, minimum, maximum)
        End Function

        ''' <summary>
        ''' Determines whether the specified input contains any.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <param name="values">The values.</param>
        ''' <returns><c>true</c> if the specified input contains any; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function ContainsAny(input As String, ParamArray values As String()) As Boolean
            If Not String.IsNullOrEmpty(input) OrElse values.Length > 0 Then
                For Each value As String In values.AsParallel
                    If input.Contains(value) Then
                        Return True
                    End If
                Next
            End If
            Return False
        End Function

        ''' <summary>
        ''' Determines whether [is not null or empty] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is not null or empty] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsNotNullOrEmpty(ByVal value As String) As Boolean
            Return Not String.IsNullOrEmpty(value)
        End Function

        ''' <summary>
        ''' SQL encodes the string.
        ''' </summary>
        ''' <param name="value">The string value.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function IsValidCreditCard(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidCreditCard(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid email] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is valid email] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValidEmail(ByVal value As String) As Boolean
            Return dotNetTips.Utility.Portable.Windows.ValidationHelper.IsValidEmail(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid file path] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is valid file path] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValidFilePath(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidFilePath(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid first last name] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is valid first last name] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValidFirstLastName(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidFirstLastName(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid isbn] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is valid isbn] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValidIsbn(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidIsbn(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid scientific] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is valid scientific] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValidScientific(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidScientific(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid string] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is valid string] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValidString(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidString(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid temperature] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is valid temperature] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValidTemperature(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidTemperature(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid URL] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is valid URL] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValidUrl(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidUrl(value)
        End Function

        ''' <summary>
        ''' Joins the strings.
        ''' </summary>
        ''' <param name="values">The values.</param>
        ''' <param name="separator">The separator.</param>
        ''' <returns>System.String.</returns>
        ''' <exception cref="System.ArgumentNullException"></exception>
        <Extension>
        Public Function JoinStrings(ByVal values As IEnumerable(Of String), ByVal separator As String) As String
            If values Is Nothing Then
                Throw New ArgumentNullException(NameOf(values))
            End If

            Return String.Join(separator, values.ToArray())
        End Function

        ''' <summary>
        ''' Joins the strings.
        ''' </summary>
        ''' <param name="values">The values.</param>
        ''' <returns>System.String.</returns>
        ''' <exception cref="System.ArgumentNullException"></exception>
        <Extension>
        Public Function JoinStrings(ByVal values As IEnumerable(Of String)) As String
            If values Is Nothing Then
                Throw New ArgumentNullException(NameOf(values))
            End If

            Return String.Join(Nothing, values.ToArray())
        End Function

        ''' <summary>
        ''' Lefts the specified input.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <param name="length">The length.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function Left(ByVal input As String, ByVal length As Integer) As String
            length = System.Math.Max(length, 0)

            Return If(input.Length > length, input.Substring(0, length), input)

        End Function

        ''' <summary>
        ''' Printables the only.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function PrintableOnly(ByVal value As String) As String
            Return StringHelper.PrintableOnly(value)
        End Function

        ''' <summary>
        ''' Removes the character.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <param name="character">The character.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function RemoveCharacter(input As String, character As Char) As String
            Return StringHelper.RemoveCharacter(input, character)
        End Function

        ''' <summary>
        ''' Removes from end.
        ''' </summary>
        ''' <param name="s">The s.</param>
        ''' <param name="oldValue">The old value.</param>
        ''' <returns>System.String.</returns>
        ''' <exception cref="System.ArgumentNullException">
        ''' </exception>
        <Extension>
        Public Function RemoveFromEnd(ByVal s As String, ByVal oldValue As String) As String
            If s Is Nothing Then
                Throw New ArgumentNullException(NameOf(s))
            End If
            If oldValue Is Nothing Then
                Throw New ArgumentNullException(NameOf(oldValue))
            End If

            Return If(s.EndsWith(oldValue, StringComparison.CurrentCulture), s.Substring(0, s.Length - oldValue.Length), s)
        End Function

        ''' <summary>
        ''' Removes the spaces.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function RemoveSpaces(input As String) As String
            Return StringHelper.RemoveSpaces(input)
        End Function

        ''' <summary>
        ''' Rights the specified value.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="length">The length.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function Right(ByVal value As String, ByVal length As Integer) As String
            length = System.Math.Max(length, 0)
            Return If(value.Length > length, value.Substring(value.Length - length, length), value)
        End Function

        ''' <summary>
        ''' SQLs the encode.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function SqlEncode(ByVal value As String) As String
            Return StringHelper.SqlEncode(value)
        End Function

        ''' <summary>
        ''' Strips the specified s.
        ''' </summary>
        ''' <param name="s">The s.</param>
        ''' <param name="character">The character.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function Strip(ByVal s As String, ByVal character As Char) As String
            Return s.Replace(character, String.Empty)
        End Function

        ''' <summary>
        ''' Strips the specified s.
        ''' </summary>
        ''' <param name="s">The s.</param>
        ''' <param name="value">The value.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function Strip(ByVal s As String, ByVal ParamArray value As Char()) As String
            For Each c As Char In value.AsParallel
                s = s.Replace(c, String.Empty)
            Next
            Return s
        End Function

        ''' <summary>
        ''' Strips the specified s.
        ''' </summary>
        ''' <param name="s">The s.</param>
        ''' <param name="value">The value.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function Strip(ByVal s As String, ByVal value As String) As String
            s = s.Replace(value, String.Empty)
            Return s
        End Function

        ''' <summary>
        ''' Strips the phone number.
        ''' </summary>
        ''' <param name="number">The number.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function StripPhoneNumber(ByVal number As String) As String
            '' Dim converter = New Regex("((\d){3}-1234567)|((\d){3}\-(\d){3}\-4567)|((\d){3}1234567)")
            Dim converter = New Regex("\D+")
            Return converter.Replace(number, String.Empty).Trim()
        End Function

        ''' <summary>
        ''' Tabs the stop.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Function TabStop(ByVal sender As String) As String
            Return sender & System.Text.RegularExpressions.Regex.Unescape("\t")
        End Function

        ''' <summary>
        ''' To the bytes.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="encoding">The encoding.</param>
        ''' <returns>System.Byte().</returns>
        ''' <exception cref="System.ArgumentNullException">
        ''' </exception>
        <Extension>
        Public Function ToBytes(ByVal value As String, ByVal encoding As Encoding) As Byte()
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value))
            End If
            If encoding Is Nothing Then
                Throw New ArgumentNullException(NameOf(encoding))
            End If

            Return encoding.GetBytes(value)
        End Function

        ''' <summary>
        ''' Returns a <see cref="System.String" /> that represents this instance.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="encoding">The encoding.</param>
        ''' <returns>A <see cref="System.String" /> that represents this instance.</returns>
        ''' <exception cref="System.ArgumentNullException">
        ''' </exception>
        <Extension>
        Public Function ToString(ByVal value As Byte(), ByVal encoding As Encoding) As String
            If value Is Nothing Then
                Throw New ArgumentNullException(NameOf(value))
            End If
            If encoding Is Nothing Then
                Throw New ArgumentNullException(NameOf(encoding))
            End If

            Return encoding.GetString(value, 0, value.Length - 1)
        End Function

        ''' <summary>
        ''' To the string join.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="values">The values.</param>
        ''' <param name="separator">The separator.</param>
        ''' <returns>System.String.</returns>
        ''' <exception cref="System.ArgumentNullException"></exception>
        <Extension>
        Public Function ToStringJoin(Of T)(ByVal values As IEnumerable(Of T), ByVal separator As String) As String
            If values Is Nothing Then
                Throw New ArgumentNullException(NameOf(values))
            End If

            Dim strings As String() = values.[Select](Function(v) v.ToString()).ToArray()
            Return String.Join(separator, strings)
        End Function

        ''' <summary>
        ''' Joins the string.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="values">The values.</param>
        ''' <returns>System.String.</returns>
        ''' <exception cref="System.ArgumentNullException"></exception>
        ''' <exception cref="ArgumentNullException"></exception>
        ''' <remarks>Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions</remarks>
        <Extension>
        Public Function ToStringJoin(Of T)(ByVal values As IEnumerable(Of T)) As String
            If values Is Nothing Then
                Throw New ArgumentNullException(NameOf(values))
            End If

            Dim strings As String() = values.[Select](Function(v) v.ToString()).ToArray()
            Return String.Join(Nothing, strings)
        End Function

        ''' <summary>
        ''' Truncates the string to a specified length and replace the truncated to a ...
        ''' </summary>
        ''' <param name="text">string that will be truncated</param>
        ''' <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        ''' <returns>truncated string</returns>
        ''' <remarks>Code By: Earljon Hidalgo</remarks>
        <Extension>
        Public Function Truncate(ByVal text As String, ByVal maxLength As Integer) As String
            ' replaces the truncated string to a ...
            Const suffix As String = "..."
            Dim truncatedString As String = text

            If maxLength <= 0 Then
                Return truncatedString
            End If
            Dim strLength As Integer = maxLength - suffix.Length

            If strLength <= 0 Then
                Return truncatedString
            End If

            If text Is Nothing OrElse text.Length <= maxLength Then
                Return truncatedString
            End If

            truncatedString = text.Substring(0, strLength)
            truncatedString = truncatedString.TrimEnd()
            truncatedString += suffix
            Return truncatedString
        End Function
    End Module

End Namespace