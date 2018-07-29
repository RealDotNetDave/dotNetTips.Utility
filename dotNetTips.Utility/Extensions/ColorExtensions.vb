' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 03-29-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-29-2018
' ***********************************************************************
' <copyright file="ColorExtensions.vb" company="NicheWare - David McCarter">
'     '     NicheWare - David McCarter
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports dotNetTips.Utility.Portable.Extensions
Imports dotNetTips.Utility.Portable.OOP

Namespace Extensions
    ''' <summary>
    ''' Extension methods for color values.
    ''' </summary>
    Public Module ColorExtensions

        ''' <summary>
        ''' Converts color to HTML color.
        ''' </summary>
        ''' <param name="color">The color.</param>
        ''' <returns>HTML string color representation.</returns>
        ''' <remarks>Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions</remarks>
        <Extension>
        Public Function ToHtml(ByVal color As Color) As String
            Return ColorTranslator.ToHtml(color)
        End Function

        ''' <summary>
        ''' Converts the html color to Color.
        ''' </summary>
        ''' <param name="htmlColor">Color of the HTML.</param>
        ''' <returns>CGI+ Color structure.</returns>
        ''' <remarks>Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions</remarks>
        <Extension>
        Public Function ToHtmlColor(ByVal htmlColor As String) As Color
            Return ColorTranslator.FromHtml(htmlColor)
        End Function

        ''' <summary>
        ''' Averages the specified colors in the collection..
        ''' </summary>
        ''' <param name="colors">The colors.</param>
        ''' <returns>CGI+ Color structure.</returns>
        ''' <remarks>Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions</remarks>
        <Extension>
        Public Function Average(ByVal colors As IEnumerable(Of Color)) As Color

            Encapsulation.TryValidateParam(colors, NameOf(colors))

            Dim r As Integer = colors.Average(Function(c) c.R).Round()
            Dim g As Integer = colors.Average(Function(c) c.G).Round()
            Dim b As Integer = colors.Average(Function(c) c.B).Round()

            Return Color.FromArgb(r, g, b)
        End Function
    End Module

End Namespace