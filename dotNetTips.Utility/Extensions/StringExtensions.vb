' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 06-05-2014
'
' Last Modified By : David McCarter
' Last Modified On : 07-08-2014
' ***********************************************************************
' <copyright file="StringExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports System.Speech.Synthesis
Imports System.Text

Namespace Extensions

    ''' <summary>
    ''' String Extensions
    ''' </summary>
    ''' <remarks></remarks>
    Public Module StringExtensions

        ''' <summary>
        ''' Returns an empty string if the current string object is null.
        ''' </summary>
        ''' <remarks>Code By: jbaurle</remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function DefaultIfNull(ByVal s As String) As String
            Return If(s IsNot Nothing, s, String.Empty)
        End Function

        ''' <summary>
        ''' Returns the specified default value if the current string object is null.
        ''' </summary>
        ''' <remarks>Code By: jbaurle</remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function DefaultIfNull(ByVal s As String, ByVal defaultValue As String) As String
            Return If(s IsNot Nothing, s, (If(defaultValue, String.Empty)))
        End Function

        ''' <summary>
        ''' Returns the specified default value if the current string object is null
        ''' or empty.
        ''' </summary>
        ''' <remarks>Code By: jbaurle</remarks>
        <Extension()>
        Public Function DefaultIfNullOrEmpty(ByVal value As String, ByVal defaultValue As String) As String
            Contracts.Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(defaultValue) = False)

            Return If(String.IsNullOrEmpty(value), value, defaultValue)
        End Function

        ''' <summary>
        ''' URLs encodes the string.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        <Extension()>
        Public Function UrlEncode(ByVal value As String) As Uri
            Return StringHelper.UrlEncode(value)
        End Function

        ''' <summary>
        ''' Converts the RTF to text.
        ''' </summary>
        ''' <param name="value">The RTF value.</param>
        ''' <returns></returns>
        <Extension()>
        Public Function ConvertRtfToText(ByVal value As String) As String
            Return StringHelper.ConvertRtfToText(value)
        End Function

        ''' <summary>
        ''' HTML encodes the text.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        <Extension()>
        Public Function HtmlEncode(ByVal value As String) As String
            Return StringHelper.HtmlEncode(value)
        End Function

        ''' <summary>
        ''' Determines whether [is valid number] [the specified value].
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns>
        ''' <c>true</c> if [is valid number] [the specified value]; otherwise, <c>false</c>.
        ''' </returns>
        <Extension()>
        Public Function IsValidNumber(ByVal value As String) As Boolean
            Return ValidationHelper.IsValidNumber(value)
        End Function

        ''' <summary>
        ''' Check to see if the sting is equal to the given text.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="equalsTo">The equals to.</param>
        ''' <returns></returns>
        ''' <remarks>Original code by: Joacim Andersson</remarks>
        <Extension()>
        Public Function EqualsText(ByVal value As String, ByVal equalsTo As String) As Boolean
            Return ((New CaseInsensitiveComparer(CultureInfo.CurrentCulture)).Compare(value, equalsTo) = 0)
        End Function

        ''' <summary>
        ''' Parses the specified value to type.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' Code By: Jigar Desai
        ''' <example>
        ''' Regular parsing
        '''Dim i As Integer = "123".Parse(Of Integer)()
        '''Dim inull As System.Nullable(Of Integer) = "123".Parse(Of System.Nullable(Of Integer))()
        '''Dim d As DateTime = "01/12/2008".Parse(Of DateTime)()
        '''Dim dn As System.Nullable(Of DateTime) = "01/12/2008".Parse(Of System.Nullable(Of DateTime))()
        ''' Null values
        '''Dim sample As String = Nothing
        '''Dim k As System.Nullable(Of Integer) = sample.Parse(Of System.Nullable(Of Integer))() ' returns null
        '''Dim l As Integer = sample.Parse(Of Integer)() ' returns 0
        '''Dim dd As DateTime = sample.Parse(Of DateTime)() ' returns 01/01/0001
        '''Dim ddn As System.Nullable(Of DateTime) = sample.Parse(Of System.Nullable(Of DateTime))() ' returns null
        ''' </example>
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()>
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
        ''' <remarks>
        ''' Code By: Steve Hiner
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function GetSHA1Hash(ByVal input As String) As String
            Contracts.Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(input) = False)

            Dim data As [Byte]() = Encoding.UTF8.GetBytes(input)
            Using lSHA1CryptoServiceProvider As SHA1CryptoServiceProvider = New SHA1CryptoServiceProvider()
                Dim hash As [Byte]() = lSHA1CryptoServiceProvider.ComputeHash(data)
                Return Convert.ToBase64String(hash)
            End Using
        End Function

        ''' <summary>
        ''' Converts a base64 text string to an Image.
        ''' </summary>
        ''' <param name="base64"></param>
        ''' <returns><see cref="System.Drawing.Image"/></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ToImage(base64 As String) As System.Drawing.Image
            Contracts.Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(base64) = False)

            Return CollectionExtensions.ToImage(Convert.FromBase64String(base64))
        End Function

        ''' <summary>
        ''' Loads from file.
        ''' </summary>
        ''' <param name="Expression">The expression.</param>
        ''' <param name="Filename">The filename.</param>
        ''' <returns></returns>
        ''' <remarks>Original code by: Massimo Da Frassini</remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function LoadFromFile(ByVal Expression As String, ByVal Filename As String) As Boolean
            Try
                Using oRead As New System.IO.StreamReader(Filename, System.Text.Encoding.Default)
                    Expression = oRead.ReadToEnd
                End Using
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' To the base64.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ToBase64(ByVal value As String) As String
            If String.IsNullOrEmpty(value) Then
                Return value
            Else
                Dim encoding As New System.Text.ASCIIEncoding()
                Return Convert.ToBase64String(encoding.GetBytes(value))
            End If
        End Function

        ''' <summary>
        ''' Froms the base64.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function FromBase64(ByVal value As String) As String
            If String.IsNullOrEmpty(value) Then
                Return value
            Else
                Dim encoding As New System.Text.ASCIIEncoding()
                Return encoding.GetString(Convert.FromBase64String(value))
            End If
        End Function

        ''' <summary>
        ''' Speaks the specified speak.
        ''' </summary>
        ''' <param name="speak">The speak.</param>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Sub Speak(ByVal speak As String)
            Using synth As New SpeechSynthesizer()
                synth.Speak(speak)
            End Using
        End Sub

        ''' <summary>
        ''' Indents the specified string.
        ''' </summary>
        ''' <param name="str">The string.</param>
        ''' <param name="length">The length.</param>
        ''' <param name="indentationCharacter">The indentation character.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function Indent(str As String, length As Integer, Optional indentationCharacter As String = " ") As String

            'If string is nothing or the indentation length is 0 then return the unprocessed string
            If String.IsNullOrEmpty(str) OrElse length = 0 Then Return str

            Dim sb As New StringBuilder()

            'An indentation length smaller then 0 changes this function from an indent function to an append function.
            'For that the final string has to start of with the initial str.
            If length < 0 Then sb.Append(str)

            'Append the indentation string
            For i = 1 To Math.Abs(length)
                sb.Append(indentationCharacter)
            Next

            'If the indentation length was greater then 0 the string gets added now.
            If length > 0 Then sb.Append(str)

            Return sb.ToString

        End Function

        ''' <summary>
        ''' Noes the duplicates.
        ''' </summary>
        ''' <param name="values">The values.</param>
        ''' <returns></returns>
        ''' <remarks>Code by: Kevin S Gallagher</remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function NoDuplicates(ByVal values As String()) As String()
            Return (From value In values Select value Distinct Order By value).ToArray
        End Function

        ''' <summary>
        ''' Extracts the specified value.
        ''' </summary>
        ''' <param name="value">The value.</param>
        ''' <param name="strStart">The string start.</param>
        ''' <param name="strEnd">The string end.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function Extract(ByVal value As String, ByVal strStart As String, ByVal strEnd As String) As String
            If Not String.IsNullOrEmpty(value) Then
                Dim i As Integer = value.IndexOf(strStart)
                Dim j As Integer = value.IndexOf(strEnd)
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
        ''' <returns></returns>
        ''' <remarks>Code by: Chris Rock</remarks>
        <Extension()>
        Public Function ToNullableString(Of T As Structure)(ByVal param As Nullable(Of T)) As String
            If (Not param.HasValue) Then
                Return String.Empty
            Else
                Return param.Value.ToString
            End If
        End Function

        ''' <summary>
        ''' Changes the case.
        ''' </summary>
        ''' <param name="source">The source.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ChangeCase(ByVal source As String) As String
            Return New System.Globalization.CultureInfo("en-US").TextInfo.ToTitleCase(source)
        End Function

        ''' <summary>
        ''' Supported hash algorithms
        ''' </summary>
        Public Enum eHashType
            HMAC
            HMACMD5
            HMACSHA1
            HMACSHA256
            HMACSHA384
            HMACSHA512
            MACTripleDES
            MD5
            RIPEMD160
            SHA1
            SHA256
            SHA384
            SHA512
        End Enum

        Private Function GetHash(input As String, hash As eHashType) As Byte()
            Dim inputBytes As Byte() = Encoding.ASCII.GetBytes(input)

            Select Case hash
                Case eHashType.HMAC
                    Return HMAC.Create().ComputeHash(inputBytes)

                Case eHashType.HMACMD5
                    Return HMACMD5.Create().ComputeHash(inputBytes)

                Case eHashType.HMACSHA1
                    Return HMACSHA1.Create().ComputeHash(inputBytes)

                Case eHashType.HMACSHA256
                    Return HMACSHA256.Create().ComputeHash(inputBytes)

                Case eHashType.HMACSHA384
                    Return HMACSHA384.Create().ComputeHash(inputBytes)

                Case eHashType.HMACSHA512
                    Return HMACSHA512.Create().ComputeHash(inputBytes)

                Case eHashType.MACTripleDES
                    Return MACTripleDES.Create().ComputeHash(inputBytes)

                Case eHashType.MD5
                    Return MD5.Create().ComputeHash(inputBytes)

                Case eHashType.RIPEMD160
                    Return RIPEMD160.Create().ComputeHash(inputBytes)

                Case eHashType.SHA1
                    Return SHA1.Create().ComputeHash(inputBytes)

                Case eHashType.SHA256
                    Return SHA256.Create().ComputeHash(inputBytes)

                Case eHashType.SHA384
                    Return SHA384.Create().ComputeHash(inputBytes)

                Case eHashType.SHA512
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
        <System.Runtime.CompilerServices.Extension>
        Public Function ComputeHash(input As String, hashType As eHashType) As String
            Try
                Dim hash As Byte() = GetHash(input, hashType)
                Dim ret As New StringBuilder()

                For i As Integer = 0 To hash.Length - 1
                    ret.Append(hash(i).ToString("x2"))
                Next

                Return ret.ToString()
            Catch
                Return String.Empty
            End Try
        End Function

    End Module

End Namespace