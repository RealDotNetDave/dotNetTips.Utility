' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 12-04-2013
' ***********************************************************************
' <copyright file="StringHelper.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Diagnostics.Contracts

''' <summary>
''' Text helper class.
''' </summary>
''' <remarks></remarks>
Public Module StringHelper

    ''' <summary>
    ''' HTML encodes text.
    ''' </summary>
    ''' <param name="value">Text to html encode.</param>
    ''' <returns>HTML encoded text.</returns>
    ''' <remarks></remarks>
    Public Function HtmlEncode(ByVal value As String) As String
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(value) = True)
        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)

        'Encode using method from Web class
        Return Web.WebHelper.HtmlEncode(value)

    End Function

    ''' <summary>
    ''' Url encodes text.
    ''' </summary>
    ''' <param name="value"><see cref="String">Text</see> to url encode.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UrlEncode(ByVal value As String) As Uri
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(value) = False)
        Contract.Ensures(Contract.Result(Of Uri)() IsNot Nothing)

        Return New Uri(Web.UrlEncode(value))

    End Function

    ''' <summary>
    ''' Converts RTF to text.
    ''' </summary>
    ''' <param name="value">RTF to convert.</param>
    ''' <returns>Converted RTF as <seealso cref="String"/></returns>
    ''' <remarks></remarks>
    Public Function ConvertRtfToText(ByVal value As String) As String
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(value) = False)
        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)

        Dim result As String

        Using converter As New System.Windows.Forms.RichTextBox() With {.Rtf = value}
            result = converter.Text
        End Using

        Return result

    End Function

End Module