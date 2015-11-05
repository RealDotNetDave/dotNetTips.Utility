' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 04-16-2014
' ***********************************************************************
' <copyright file="StringExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Globalization

Namespace Extensions

  ''' <summary>
  ''' String Extensions
  ''' </summary>
  ''' <remarks></remarks>
  Public Module StringExtensions

    ''' <summary>
    ''' Determines whether [is null or empty] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is null or empty] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsNullOrEmpty(ByVal value As String) As Boolean
      Return String.IsNullOrEmpty(value)
    End Function

    ''' <summary>
    ''' Determines whether [is not null or empty] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is not null or empty] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsNotNullOrEmpty(ByVal value As String) As Boolean
      Return Not String.IsNullOrEmpty(value)
    End Function

    ''' <summary>
    ''' SQL encodes the string.
    ''' </summary>
    ''' <param name="value">The string value.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function SqlEncode(ByVal value As String) As String
      Return StringHelper.SqlEncode(value)
    End Function

    ''' <summary>
    ''' Removes unprintable characters.
    ''' </summary>
    ''' <param name="value">The string value.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function PrintableOnly(ByVal value As String) As String
      Return StringHelper.PrintableOnly(value)
    End Function

    ''' <summary>
    ''' Determines whether [is valid email] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is valid email] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsValidEmail(ByVal value As String) As Boolean
      Return ValidationHelper.IsValidEmail(value)
    End Function

    ''' <summary>
    ''' Checks the length of the text.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="minimum">The minimum.</param>
    ''' <param name="maximum">The maximum.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function CheckLength(ByVal value As String, ByVal minimum As Integer, ByVal maximum As Integer) As Boolean
      Return ValidationHelper.CheckLength(value, minimum, maximum)
    End Function

    ''' <summary>
    ''' Determines whether [is valid credit card] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is valid credit card] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsValidCreditCard(ByVal value As String) As Boolean
      Return ValidationHelper.IsValidCreditCard(value)
    End Function

    ''' <summary>
    ''' Determines whether [is valid file path] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is valid file path] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsValidFilePath(ByVal value As String) As Boolean
      Return ValidationHelper.IsValidFilePath(value)
    End Function

    ''' <summary>
    ''' Determines whether [is valid first last name] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is valid first last name] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsValidFirstLastName(ByVal value As String) As Boolean
      Return ValidationHelper.IsValidFirstLastName(value)
    End Function

    ''' <summary>
    ''' Determines whether [is valid isbn] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is valid isbn] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsValidIsbn(ByVal value As String) As Boolean
      Return ValidationHelper.IsValidIsbn(value)
    End Function

    ''' <summary>
    ''' Determines whether [is valid scientific] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is valid scientific] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsValidScientific(ByVal value As String) As Boolean
      Return ValidationHelper.IsValidScientific(value)
    End Function

    ''' <summary>
    ''' Determines whether [is valid string] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is valid string] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsValidString(ByVal value As String) As Boolean
      Return ValidationHelper.IsValidString(value)
    End Function

    ''' <summary>
    ''' Determines whether [is valid temperature] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is valid temperature] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsValidTemperature(ByVal value As String) As Boolean
      Return ValidationHelper.IsValidTemperature(value)
    End Function

    ''' <summary>
    ''' Determines whether [is valid URL] [the specified value].
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns>
    ''' <c>true</c> if [is valid URL] [the specified value]; otherwise, <c>false</c>.
    ''' </returns>
    <Extension()>
    Public Function IsValidUrl(ByVal value As String) As Boolean
      Return ValidationHelper.IsValidUrl(value)
    End Function

    ''' <summary>
    ''' Joins the strings.
    ''' </summary>
    ''' <param name="values">The values.</param>
    ''' <param name="separator">The separator.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code by: Lucas
    ''' http://code.msdn.microsoft.com/LucasExtensions
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function JoinStrings(ByVal values As IEnumerable(Of String), ByVal separator As String) As String
      If values Is Nothing Then
        Throw New ArgumentNullException("values")
      End If

      Return String.Join(separator, values.ToArray())
    End Function

    ''' <summary>
    ''' Joins the strings.
    ''' </summary>
    ''' <param name="values">The values.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code by: Lucas
    ''' http://code.msdn.microsoft.com/LucasExtensions
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function JoinStrings(ByVal values As IEnumerable(Of String)) As String
      If values Is Nothing Then
        Throw New ArgumentNullException("values")
      End If

      Return String.Join(Nothing, values.ToArray())
    End Function

    ''' <summary>
    ''' Joins the string.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="values">The values.</param>
    ''' <param name="separator">The separator.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code by: Lucas
    ''' http://code.msdn.microsoft.com/LucasExtensions
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function ToStringJoin(Of T)(ByVal values As IEnumerable(Of T), ByVal separator As String) As String
      If values Is Nothing Then
        Throw New ArgumentNullException("values")
      End If

      Dim strings As String() = values.[Select](Function(v) v.ToString()).ToArray()
      Return String.Join(separator, strings)
    End Function

    ''' <summary>
    ''' Joins the string.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="values">The values.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code by: Lucas
    ''' http://code.msdn.microsoft.com/LucasExtensions
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function ToStringJoin(Of T)(ByVal values As IEnumerable(Of T)) As String
      If values Is Nothing Then
        Throw New ArgumentNullException("values")
      End If

      Dim strings As String() = values.[Select](Function(v) v.ToString()).ToArray()
      Return String.Join(Nothing, strings)
    End Function

    ''' <summary>
    ''' Removes from end.
    ''' </summary>
    ''' <param name="s">The string to remove from.</param>
    ''' <param name="oldValue">The old value.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code by: Lucas
    ''' http://code.msdn.microsoft.com/LucasExtensions
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function RemoveFromEnd(ByVal s As String, ByVal oldValue As String) As String
      If s Is Nothing Then
        Throw New ArgumentNullException("s")
      End If
      If oldValue Is Nothing Then
        Throw New ArgumentNullException("oldValue")
      End If

      Return If(s.EndsWith(oldValue, StringComparison.CurrentCulture), s.Substring(0, s.Length - oldValue.Length), s)
    End Function

    ''' <summary>
    ''' Coverts the string into bytes.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="encoding">The encoding.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code by: Lucas
    ''' http://code.msdn.microsoft.com/LucasExtensions
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function ToBytes(ByVal value As String, ByVal encoding As Encoding) As Byte()
      If value Is Nothing Then
        Throw New ArgumentNullException("value")
      End If
      If encoding Is Nothing Then
        Throw New ArgumentNullException("encoding")
      End If

      Return encoding.GetBytes(value)
    End Function

    ''' <summary>
    '''Converts bytes to string.
    ''' </summary>
    ''' <param name="value">The bytes.</param>
    ''' <param name="encoding">The encoding.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code by: Lucas
    ''' http://code.msdn.microsoft.com/LucasExtensions
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function ToString(ByVal value As Byte(), ByVal encoding As Encoding) As String
      If value Is Nothing Then
        Throw New ArgumentNullException("bytes")
      End If
      If encoding Is Nothing Then
        Throw New ArgumentNullException("encoding")
      End If

      Return encoding.GetString(value, 0, value.Length - 1)
    End Function

    ''' <summary>
    ''' Returns the last few characters of the string with a length
    ''' specified by the given parameter. If the string's length is less than the
    ''' given length the complete string is returned. If length is zero or
    ''' less an empty string is returned
    ''' </summary>
    ''' <param name="s">the string to process</param>
    ''' <param name="length">Number of characters to return</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code By: chris.meijers@chrisis.eu
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function Right(ByVal s As String, ByVal length As Integer) As String
      length = System.Math.Max(length, 0)
      If s.Length > length Then
        Return s.Substring(s.Length - length, length)
      Else
        Return s
      End If
    End Function

    ''' <summary>
    ''' Truncates the string to a specified length and replace the truncated to a ...
    ''' </summary>
    ''' <param name="text">string that will be truncated</param>
    ''' <param name="maxLength">total length of characters to maintain before the truncate happens</param>
    ''' <returns>truncated string</returns>
    ''' <remarks>
    ''' Code By: Earljon Hidalgo
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
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

    ''' <summary>
    ''' Returns the first few characters of the string with a length
    ''' specified by the given parameter. If the string's length is less than the
    ''' given length the complete string is returned. If length is zero or
    ''' less an empty string is returned
    ''' </summary>
    ''' <param name="s">the string to process</param>
    ''' <param name="length">Number of characters to return</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code By: C.F.Meijers
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function Left(ByVal s As String, ByVal length As Integer) As String
      length = System.Math.Max(length, 0)

      If s.Length > length Then
        Return s.Substring(0, length)
      Else
        Return s
      End If
    End Function

    ''' <summary>
    ''' Strip a string of the specified character.
    ''' </summary>
    ''' <param name="s">The string to process</param>
    ''' <param name="character">The character to remove.</param>
    ''' <returns></returns>
    ''' <example>
    ''' string s = "abcde";
    ''' s = s.Strip('b');  //s becomes 'acde;
    ''' </example>
    <System.Runtime.CompilerServices.Extension()>
    Public Function Strip(ByVal s As String, ByVal character As Char) As String
      Return s.Replace(character, String.Empty)
    End Function

    ''' <summary>
    ''' Strip a string of the specified characters.
    ''' </summary>
    ''' <param name="s">the string to process</param>
    ''' <param name="value">list of characters to remove from the string</param>
    ''' <example>
    ''' string s = "abcde";
    ''' s = s.Strip('a', 'd');  //s becomes 'bce;
    ''' </example>
    ''' <remarks>
    ''' Code By: C.F.Meijers
    ''' </remarks>
    ''' <returns></returns>
    <System.Runtime.CompilerServices.Extension()>
    Public Function Strip(ByVal s As String, ByVal ParamArray value As Char()) As String
      For Each c As Char In value.AsParallel
        s = s.Replace(c, String.Empty)
      Next
      Return s
    End Function
    ''' <summary>
    ''' Strip a string of the specified substring.
    ''' </summary>
    ''' <param name="s">the string to process</param>
    ''' <param name="value">substring to remove</param>
    ''' <example>
    ''' string s = "abcde";
    ''' s = s.Strip("bcd");  //s becomes 'ae;
    ''' </example>
    ''' <remarks>
    ''' Code By: C.F.Meijers
    ''' </remarks>
    ''' <returns></returns>
    <System.Runtime.CompilerServices.Extension()>
    Public Function Strip(ByVal s As String, ByVal value As String) As String
      s = s.Replace(value, String.Empty)
      Return s
    End Function

    ''' <summary>
    ''' Formats the size of the file.
    ''' </summary>
    ''' <param name="fileSize">Size of the file.</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Code By: Earljon Hidalgo
    ''' </remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function FormatFileSize(ByVal fileSize As Long) As String
      Dim suffix As String() = {"bytes", "KB", "MB", "GB"}
      Dim j As Long = 0

      While fileSize > 1024 AndAlso j < 4
        fileSize = CLng(fileSize / 1024)
        j += 1
      End While
      Return ((String.Format("{0} ", fileSize)) + suffix(CInt(j)))
    End Function

    ''' <summary>
    ''' Determines whether the specified input contains any string.
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="values">The values.</param>
    ''' <returns><c>true</c> if the specified input contains any string; otherwise, <c>false</c>.</returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
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
    ''' Removes the spaces.
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function RemoveSpaces(input As String) As String
      Return StringHelper.RemoveSpaces(input)
    End Function

    ''' <summary>
    ''' Removes the character.
    ''' </summary>
    ''' <param name="input">The input.</param>
    ''' <param name="character">The character.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function RemoveCharacter(input As String, character As Char) As String
      Return StringHelper.RemoveCharacter(input, character)
    End Function

    ''' <summary>
    ''' Strips the phone number.
    ''' </summary>
    ''' <param name="number">The number.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function StripPhoneNumber(ByVal number As String) As String
      '' Dim converter = New Regex("((\d){3}-1234567)|((\d){3}\-(\d){3}\-4567)|((\d){3}1234567)")
      Dim converter = New Regex("\D+")
      Return converter.Replace(number, String.Empty).Trim()
    End Function

    ''' <summary>
    ''' Adds/insert a TAB stop to this string
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    ''' <remarks>Code by: Kevin S Gallagher</remarks>
    <Runtime.CompilerServices.Extension()>
    Function TabStop(ByVal sender As String) As String
      Return sender & System.Text.RegularExpressions.Regex.Unescape("\t")
    End Function

  End Module

End Namespace