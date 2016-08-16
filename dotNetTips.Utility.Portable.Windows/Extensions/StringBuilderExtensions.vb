' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 12-04-2013
' ***********************************************************************
' <copyright file="StringBuilderExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Globalization

Namespace Extensions
    ''' <summary>
    ''' StringBuilder Extensions
    ''' </summary>
    ''' <remarks></remarks>
    Public Module StringBuilderExtensions

        ''' <summary>
        ''' Clears the specified StringBuilder text.
        ''' </summary>
        ''' <param name="sb">The StringBuilder object.</param>
        <Extension>
        Public Sub Clear(ByVal sb As StringBuilder)
            sb.Remove(0, sb.ToString().Length)
        End Sub

        ''' <summary>
        ''' Appends the line format.
        ''' </summary>
        ''' <param name="builder">The builder.</param>
        ''' <param name="format">The format.</param>
        ''' <param name="args">The arguments.</param>
        ''' <returns></returns>
        ''' <remarks>Code by: Koen Rouwhorst</remarks>
        <Extension>
        Public Function AppendLineFormat(ByVal builder As StringBuilder, ByVal format As String, ByVal ParamArray args As Object()) As StringBuilder
            Dim value As String = String.Format(CultureInfo.CurrentCulture, format, args)

            builder.AppendLine(value)

            Return builder
        End Function

    End Module

End Namespace