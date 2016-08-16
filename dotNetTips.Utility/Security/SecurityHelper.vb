'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 09-26-2008
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System.Text

Namespace Security
    ''' <summary>
    ''' Security helper class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module SecurityHelper

        ''' <summary>
        ''' Encodes the HTML.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <returns></returns>
        Public Function EncodeHtml(ByVal input As String) As String
            If (input Is Nothing) Then
                Return Nothing
            End If
            If (input.Length = 0) Then
                Return String.Empty
            End If
            Dim builder As New Text.StringBuilder(String.Empty, (input.Length * 2))
            Dim ch As Char
            For Each ch In input
                If ((((ch > "`"c) AndAlso (ch < "{"c)) OrElse ((ch > "@"c) AndAlso (ch < "["c))) OrElse (((ch = " "c) OrElse ((ch > "/"c) AndAlso (ch < ":"c))) OrElse (((ch = "."c) OrElse (ch = ","c)) OrElse ((ch = "-"c) OrElse (ch = "_"c))))) Then
                    builder.Append(ch)
                Else
                    builder.Append((String.Format(CultureInfo.CurrentCulture, "&#{0};", CInt(AscW(ch)).ToString(CultureInfo.InvariantCulture))))
                End If
            Next
            Return builder.ToString
        End Function

        ''' <summary>
        ''' Encodes the HTML attribute.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <returns></returns>
        Public Function EncodeHtmlAttribute(ByVal input As String) As String
            Return EncodeHtml(input)
        End Function

        ''' <summary>
        ''' Encodes the URL.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <returns></returns>
        Public Function EncodeUrl(ByVal input As String) As String
            If (input Is Nothing) Then
                Return Nothing
            End If
            If (input.Length = 0) Then
                Return String.Empty
            End If
            Dim builder As New Text.StringBuilder(String.Empty, (input.Length * 2))
            Dim ch As Char
            For Each ch In input
                If ((((ch > "`"c) AndAlso (ch < "{"c)) OrElse ((ch > "@"c) AndAlso (ch < "["c))) OrElse (((ch > "/"c) AndAlso (ch < ":"c)) OrElse (((ch = "."c) OrElse (ch = "-"c)) OrElse (ch = "_"c)))) Then
                    builder.Append(ch)
                ElseIf (ch > ""c) Then
                    builder.Append((String.Format(CultureInfo.CurrentCulture, "%u{0}", TwoByteHex(ch))))
                Else
                    builder.Append((String.Format(CultureInfo.CurrentCulture, "%{0}", SingleByteHex(ch))))
                End If
            Next
            Return builder.ToString
        End Function

        ''' <summary>
        ''' Encodes the VBS.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <returns></returns>
        Public Function EncodeVbs(ByVal input As String) As String
            If (input Is Nothing) Then
                Return Nothing
            End If
            If (input.Length = 0) Then
                Return """"""
            End If
            Dim builder As New Text.StringBuilder(String.Empty, (input.Length * 2))
            Dim flag As Boolean = False
            Dim ch As Char
            For Each ch In input
                If ((((ch > "`"c) AndAlso (ch < "{"c)) OrElse ((ch > "@"c) AndAlso (ch < "["c))) OrElse (((ch = " "c) OrElse ((ch > "/"c) AndAlso (ch < ":"c))) OrElse (((ch = "."c) OrElse (ch = ","c)) OrElse ((ch = "-"c) OrElse (ch = "_"c))))) Then
                    If Not flag Then
                        builder.Append("&""")
                        flag = True
                    End If
                    builder.Append(ch)
                Else
                    If flag Then
                        builder.Append("""")
                        flag = False
                    End If
                    builder.Append((String.Format(CultureInfo.CurrentCulture, "&chrw({0})", CType(CUInt(AscW(ch)), UInt32).ToString(CultureInfo.InvariantCulture))))
                End If
            Next
            If ((builder.Length > 0) AndAlso (builder.Chars(0) = "&"c)) Then
                builder.Remove(0, 1)
            End If
            If (builder.Length = 0) Then
                builder.Insert(0, """""")
            End If
            If flag Then
                builder.Append("""")
            End If
            Return builder.ToString
        End Function

        ''' <summary>
        ''' Encodes the JS.
        ''' </summary>
        ''' <param name="input">The input.</param>
        ''' <returns></returns>
        Public Function EncodeJS(ByVal input As String) As String
            If (input Is Nothing) Then
                Return Nothing
            End If
            If (input.Length = 0) Then
                Return "''"
            End If
            Dim builder As New StringBuilder("'", (input.Length * 2))
            Dim ch As Char
            For Each ch In input
                If ((((ch > "`"c) AndAlso (ch < "{"c)) OrElse ((ch > "@"c) AndAlso (ch < "["c))) OrElse (((ch = " "c) OrElse ((ch > "/"c) AndAlso (ch < ":"c))) OrElse (((ch = "."c) OrElse (ch = ","c)) OrElse ((ch = "-"c) OrElse (ch = "_"c))))) Then
                    builder.Append(ch)
                ElseIf (ch > ""c) Then
                    builder.Append((String.Format(CultureInfo.CurrentCulture, "\u{0}", TwoByteHex(ch))))
                Else
                    builder.Append((String.Format(CultureInfo.CurrentCulture, "\x{0}", SingleByteHex(ch))))
                End If
            Next
            builder.Append("'")
            Return builder.ToString
        End Function

        Private Function SingleByteHex(ByVal c As Char) As String
            Dim num As UInt32 = CUInt(AscW(c))
            Return num.ToString("x", CultureInfo.InvariantCulture).PadLeft(2, "0"c)
        End Function

        Private Function TwoByteHex(ByVal c As Char) As String
            Dim num As UInt32 = CUInt(AscW(c))
            Return num.ToString("x", CultureInfo.InvariantCulture).PadLeft(4, "0"c)
        End Function

        ''' <summary>
        ''' Loads text into a SecureString.
        ''' </summary>
        ''' <param name="input">Text to load.</param>
        ''' <param name="makeReadOnly">Make SecureString read-only.</param>
        ''' <returns>SecureString</returns>
        ''' <remarks></remarks>
        Public Function LoadSecureString(ByVal input As String, ByVal makeReadOnly As Boolean) As System.Security.SecureString
            LoadSecureString = Nothing

            If Not String.IsNullOrEmpty(input) Then
                LoadSecureString = New System.Security.SecureString

                Array.ForEach(input.ToCharArray, AddressOf LoadSecureString.AppendChar)

                If makeReadOnly Then
                    LoadSecureString.MakeReadOnly()
                End If

            End If

        End Function

        ''' <summary>
        ''' Loads text into a SecureString.
        ''' </summary>
        ''' <param name="input">Text to load.</param>
        ''' <returns>SecureString.</returns>
        ''' <remarks></remarks>
        Public Function LoadSecureString(ByVal input As String) As System.Security.SecureString
            Return LoadSecureString(input, False)
        End Function

        ''' <summary>
        ''' Returns value of a SecureString.
        ''' </summary>
        ''' <param name="input">SecureString to read.</param>
        ''' <returns>String value of the SecureString.</returns>
        ''' <remarks></remarks>
        Public Function ReadSecureString(ByVal input As System.Security.SecureString) As String
            Dim result As String = String.Empty
            Dim inputIntPtr As System.IntPtr

            Try
                inputIntPtr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input)

                result = System.Runtime.InteropServices.Marshal.PtrToStringUni(inputIntPtr)

            Finally
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(inputIntPtr)
            End Try

            Return result

        End Function

        ''' <summary>
        ''' Compares two secure strings.
        ''' </summary>
        ''' <param name="value">Secure string value to compare.</param>
        ''' <param name="compare">Secure string value to validate.</param>
        ''' <returns>True/False</returns>
        ''' <remarks></remarks>
        Public Function CompareSecureStrings(ByVal value As System.Security.SecureString, ByVal compare As System.Security.SecureString) As Boolean
            Dim valid As Boolean = False
            Dim inputIntPtr As System.IntPtr
            Dim compareIntPtr As System.IntPtr

            Try
                inputIntPtr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(value)
                compareIntPtr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(compare)

                valid = (System.Runtime.InteropServices.Marshal.PtrToStringUni(inputIntPtr) = System.Runtime.InteropServices.Marshal.PtrToStringUni(compareIntPtr))

            Finally
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(inputIntPtr)
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(compareIntPtr)
            End Try

            Return valid

        End Function
    End Module
End Namespace