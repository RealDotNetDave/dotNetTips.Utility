' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 05-16-2016
' ***********************************************************************
' <copyright file="StringHelper.vb" company="David McCarter Consulting">
'     '     David McCarter Consulting. All rights reserved.
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Diagnostics.Contracts
Imports System.Speech.Synthesis
Imports System.Threading.Tasks

''' <summary>
''' Text helper class.
''' </summary>
Public Module StringHelper

    ''' <summary>
    ''' Converts RTF to text.
    ''' </summary>
    ''' <param name="value">RTF to convert.</param>
    ''' <returns>Converted RTF as <seealso cref="String" /></returns>
    Public Function ConvertRtfToText(ByVal value As String) As String
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(value) = False)
        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)

        Dim result As String

        Using converter As New System.Windows.Forms.RichTextBox() With {.Rtf = value}
            result = converter.Text
        End Using

        Return result

    End Function

    ''' <summary>
    ''' HTML encodes text.
    ''' </summary>
    ''' <param name="value">Text to html encode.</param>
    ''' <returns>HTML encoded text.</returns>
    Public Function HtmlEncode(ByVal value As String) As String
        'Encode using method from Web class
        Return Web.WebHelper.HtmlEncode(value)

    End Function

    ''' <summary>
    ''' Speak text as an asynchronous operation.
    ''' </summary>
    ''' <param name="text">The text.</param>
    Public Async Sub SpeakAsync(ByVal text As String)

        If String.IsNullOrWhiteSpace(text) Then
            Exit Sub
        End If

        Await Speech.SpeakAsync(text)
    End Sub

    ''' <summary>
    ''' Url encodes text.
    ''' </summary>
    ''' <param name="value"><see cref="String">Text</see> to url encode.</param>
    ''' <returns>Uri.</returns>
    Public Function UrlEncode(ByVal value As String) As Uri
        Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(value) = False)
        Contract.Ensures(Contract.Result(Of Uri)() IsNot Nothing)

        Return New Uri(Web.UrlEncode(value))

    End Function

End Module