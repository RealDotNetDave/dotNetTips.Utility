' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 12-04-2013
' ***********************************************************************
' <copyright file="SecurityHelper.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Text
Imports System.Globalization

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

    End Module
End Namespace