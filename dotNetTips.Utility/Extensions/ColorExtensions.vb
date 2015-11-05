'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 06-09-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Diagnostics.Contracts

Namespace Extensions
    ''' <summary>
    ''' Extension methods for color values.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module ColorExtensions

        ''' <summary>
        ''' Converts color to HTML color.
        ''' </summary>
        ''' <param name="color">The color.</param>
        ''' <returns>HTML string color representation.</returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToHtml(ByVal color As Color) As String
            Return ColorTranslator.ToHtml(color)
        End Function

        ''' <summary>
        ''' Converts the html color to Color.
        ''' </summary>
        ''' <param name="htmlColor">Color of the HTML.</param>
        ''' <returns>CGI+ Color structure.</returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToHtmlColor(ByVal htmlColor As String) As Color
            Return ColorTranslator.FromHtml(htmlColor)
        End Function

        ''' <summary>
        ''' Averages the specified colors.
        ''' </summary>
        ''' <param name="colors">The colors.</param>
        ''' <returns>CGI+ Color structure.</returns>
        ''' <remarks>
        ''' Code by: Lucas
        ''' http://code.msdn.microsoft.com/LucasExtensions
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension()> _
        Public Function Average(ByVal colors As IEnumerable(Of Color)) As Color
            Contract.Requires(Of ArgumentNullException)(colors IsNot Nothing)

            Dim r As Integer = colors.Average(Function(c) c.R).Round()
            Dim g As Integer = colors.Average(Function(c) c.G).Round()
            Dim b As Integer = colors.Average(Function(c) c.B).Round()

            Return Color.FromArgb(r, g, b)
        End Function
    End Module

End Namespace