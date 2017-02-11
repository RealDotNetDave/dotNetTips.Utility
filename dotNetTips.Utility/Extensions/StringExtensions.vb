' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 03-29-2016
'
' Last Modified By : David McCarter
' Last Modified On : 05-16-2016
' ***********************************************************************
' <copyright file="StringExtensions.vb" company="NicheWare - David McCarter">
'     '     NicheWare - David McCarter
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.ComponentModel
Imports System.Diagnostics.Contracts
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports System.Speech.Synthesis
Imports System.Text
Imports dotNetTips.Utility.Security

Namespace Extensions

    ''' <summary>
    ''' String Extensions
    ''' </summary>
    Public Module StringExtensions

        ''' <summary>
        ''' URLs encodes the string.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>Uri.</returns>
        <Extension>
        Public Function UrlEncode(ByVal value As String) As Uri
            Return StringHelper.UrlEncode(value)
        End Function

        ''' <summary>
        ''' Converts the RTF to text.
        ''' </summary>
        ''' <param name="value">The RTF value.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function ConvertRtfToText(ByVal value As String) As String
            Return StringHelper.ConvertRtfToText(value)
        End Function

        ''' <summary>
        ''' HTML encodes the text.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function HtmlEncode(ByVal value As String) As String
            Contract.Ensures(String.IsNullOrWhiteSpace(Contract.Result(Of String)()) = False)

            Return StringHelper.HtmlEncode(value)
        End Function

        ''' <summary>
        ''' Determines whether [is null or empty] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>
        ''' <c>true</c> if [is null or empty] [the specified value]; otherwise, <c>false</c>.
        ''' </returns>
        <Extension>
        Public Function IsNullOrEmpty(ByVal value As String) As Boolean
            Return String.IsNullOrEmpty(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid number] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns><c>true</c> if [is valid number] [the specified value]; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValidNumber(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidNumber(value)
        End Function

        ''' <summary>
        ''' Check to see if the sting is equal to the given text.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="equalsTo">The equals to.</param>
        ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ''' <remarks>Original code by: Joacim Andersson</remarks>
        <Extension>
        Public Function EqualsText(ByVal value As String, ByVal equalsTo As String) As Boolean
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(equalsTo) = False)

            Return ((New CaseInsensitiveComparer(CultureInfo.CurrentCulture)).Compare(value, equalsTo) = 0)
        End Function

        ''' <summary>
        ''' Parses the specified value to type.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="value">The value.</param>
        ''' <returns>T.</returns>
        ''' <remarks>Code By: Jigar Desai
        ''' <example>
        ''' Regular parsing
        ''' Dim i As Integer = "123".Parse(Of Integer)()
        ''' Dim inull As System.Nullable(Of Integer) = "123".Parse(Of System.Nullable(Of Integer))()
        ''' Dim d As DateTime = "01/12/2008".Parse(Of DateTime)()
        ''' Dim dn As System.Nullable(Of DateTime) = "01/12/2008".Parse(Of System.Nullable(Of DateTime))()
        ''' Null values
        ''' Dim sample As String = Nothing
        ''' Dim k As System.Nullable(Of Integer) = sample.Parse(Of System.Nullable(Of Integer))() ' returns null
        ''' Dim l As Integer = sample.Parse(Of Integer)() ' returns 0
        ''' Dim dd As DateTime = sample.Parse(Of DateTime)() ' returns 01/01/0001
        ''' Dim ddn As System.Nullable(Of DateTime) = sample.Parse(Of System.Nullable(Of DateTime))() ' returns null
        ''' </example></remarks>
        <Extension>
        Public Function Parse(Of T)(ByVal value As String) As T
            ' Get default value for type so if string
            ' is empty then we can return default value.
            Dim result As T = Nothing

            If Not String.IsNullOrEmpty(value) Then
                ' we are not going to handle exception here
                ' if you need SafeParse then you should create
                ' another method specially for that.
                Dim tc As TypeConverter = TypeDescriptor.GetConverter(GetType(T))
                result = DirectCast(tc.ConvertFrom(value), T)
            End If

            Return result
        End Function

        ''' <summary>
        ''' Calculates the SHA1 hash of the supplied string and returns a base 64 string.
        ''' </summary>
        ''' <param name="input">String that must be hashed.</param>
        ''' <returns>The hashed string or null if hashing failed.</returns>
        ''' <exception cref="ArgumentException">Occurs when stringToHash or key is null or empty.</exception>
        ''' <remarks>Code By: Steve Hiner</remarks>
        <Extension>
        Public Function GetSHA1Hash(ByVal input As String) As String
            Dim data As [Byte]() = Encoding.UTF8.GetBytes(input)
            Using lSHA1CryptoServiceProvider As SHA1CryptoServiceProvider = New SHA1CryptoServiceProvider()
                Dim hash As [Byte]() = lSHA1CryptoServiceProvider.ComputeHash(data)
                Return Convert.ToBase64String(hash)
            End Using
        End Function

        ''' <summary>
        ''' Converts a base64 text string to an Image.
        ''' </summary>
        ''' <param name="base64">The base64.</param>
        ''' <returns><see cref="System.Drawing.Image" /></returns>
        <Extension>
        Public Function ToImage(base64 As String) As Drawing.Image
            Return CollectionExtensions.ToImage(Convert.FromBase64String(base64))
        End Function

        ''' <summary>
        ''' To the base64.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function ToBase64(ByVal value As String) As String
            If String.IsNullOrEmpty(value) Then
                Return value
            Else
                Dim encoding As New ASCIIEncoding()
                Return Convert.ToBase64String(encoding.GetBytes(value))
            End If
        End Function

        ''' <summary>
        ''' Froms the base64.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function FromBase64(ByVal value As String) As String
            If String.IsNullOrEmpty(value) Then
                Return value
            Else
                Dim encoding As New ASCIIEncoding()
                Return encoding.GetString(Convert.FromBase64String(value))
            End If
        End Function

        ''' <summary>
        ''' Speaks the specified speak.
        ''' </summary>
        ''' <param name="text">The text to speak.</param>
        <Extension>
        Public Sub Speak(ByVal text As String)
            Using synth As New SpeechSynthesizer()
                If synth.GetInstalledVoices.Count > 0 Then
                    synth.SpeakAsyncCancelAll()
                    synth.Speak(text)
                End If
            End Using
        End Sub

        ''' <summary>
        ''' Speaks the text asynchronously.
        ''' </summary>
        ''' <param name="text">The text to speak.</param>
        <Extension>
        Public Async Sub SpeakAsync(ByVal text As String)
            If String.IsNullOrWhiteSpace(text) Then
                Exit Sub
            End If

            Await Speech.SpeakAsync(text)
        End Sub

        ''' <summary>
        ''' Extracts the specified value.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="start">The string start.</param>
        ''' <param name="end">The end.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function Extract(ByVal value As String, ByVal start As String, ByVal [end] As String) As String
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(start) = False)
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace([end]) = False)

            If Not String.IsNullOrEmpty(value) Then
                Dim i As Integer = value.IndexOf(start, StringComparison.CurrentCulture)
                Dim j As Integer = value.IndexOf([end], StringComparison.CurrentCulture)
                Return value.Substring(i, j - i)
            Else
                Return value
            End If
        End Function

        ''' <summary>
        ''' To the nullable string.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="param">The parameter.</param>
        ''' <returns>System.String.</returns>
        ''' <remarks>Code by: Chris Rock</remarks>
        <Extension>
        Public Function ToNullableString(Of T As Structure)(ByVal param As Nullable(Of T)) As String
            Return If((Not param.HasValue), String.Empty, param.Value.ToString)
        End Function

        ''' <summary>
        ''' To the title case.
        ''' </summary>
        ''' <param name="source">The source.</param>
        ''' <returns>System.String.</returns>
        <Extension>
        Public Function ToTitleCase(ByVal source As String) As String
            Return New CultureInfo(CultureInfo.CurrentCulture.DisplayName).TextInfo.ToTitleCase(source)
        End Function

        ''' <summary>
        ''' Gets the hash.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <param name="hash">The hash.</param>
        ''' <returns>System.Byte().</returns>
        Private Function GetHash(input As String, hash As HashType) As Byte()
            Dim inputBytes As Byte() = Encoding.ASCII.GetBytes(input)

            Select Case hash
                Case HashType.HMAC
                    Return HMAC.Create().ComputeHash(inputBytes)

                Case HashType.HMACMD5
                    Return HMACMD5.Create().ComputeHash(inputBytes)

                Case HashType.HMACSHA1
                    Return HMACSHA1.Create().ComputeHash(inputBytes)

                Case HashType.HMACSHA256
                    Return HMACSHA256.Create().ComputeHash(inputBytes)

                Case HashType.HMACSHA384
                    Return HMACSHA384.Create().ComputeHash(inputBytes)

                Case HashType.HMACSHA512
                    Return HMACSHA512.Create().ComputeHash(inputBytes)

                Case HashType.MACTripleDES
                    Return MACTripleDES.Create().ComputeHash(inputBytes)

                Case HashType.MD5
                    Return MD5.Create().ComputeHash(inputBytes)

                Case HashType.RIPEMD160
                    Return RIPEMD160.Create().ComputeHash(inputBytes)

                Case HashType.SHA1
                    Return SHA1.Create().ComputeHash(inputBytes)

                Case HashType.SHA256
                    Return SHA256.Create().ComputeHash(inputBytes)

                Case HashType.SHA384
                    Return SHA384.Create().ComputeHash(inputBytes)

                Case HashType.SHA512
                    Return SHA512.Create().ComputeHash(inputBytes)
                Case Else

                    Return inputBytes
            End Select
        End Function

        ''' <summary>
        ''' Computes the hash of the string using a specified hash algorithm
        ''' </summary>
        ''' <param name="input">The string to hash</param>
        ''' <param name="hashType">The hash algorithm to use</param>
        ''' <returns>The resulting hash or an empty string on error</returns>
        ''' <remarks>Original code by: Cosmin Pirlitu</remarks>
        <Extension>
        Public Function ComputeHash(input As String, hashType As HashType) As String
            Contract.Requires(Of ArgumentOutOfRangeException)(System.Enum.IsDefined(GetType(HashType), hashType))

            Dim hash As Byte() = GetHash(input, hashType)
            Dim ret As New StringBuilder()

            For i As Integer = 0 To hash.Length - 1
                ret.Append(hash(i).ToString("x2", CultureInfo.InvariantCulture))
            Next

            Return ret.ToString()

        End Function

    End Module

End Namespace