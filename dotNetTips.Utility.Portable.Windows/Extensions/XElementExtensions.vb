' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 12-04-2013
' ***********************************************************************
' <copyright file="XElementExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Runtime.CompilerServices
Imports System.Xml.Linq

Namespace Extensions
    ''' <summary>
    ''' Extensions for XElement
    ''' </summary>
    ''' <remarks></remarks>
    Public Module XElementExtensions

        ''' <summary>
        ''' Trims the xml value.
        ''' </summary>
        ''' <param name="xml">The XML.</param>
        ''' <returns></returns>
        ''' <remarks>Original code by: Joacim Anderson</remarks>
        <Extension()> _
        Public Function TrimmedValue(ByVal xml As XElement) As String
            Return String.Join(vbLf, (String.Join(vbLf, ( _
                 From s In xml.Value.Split(New Char() {Convert.ToChar(10)}) _
                 Select s.Replace(vbTab, Space(1)).Trim()).ToArray)).Split( _
                 New Char() {Convert.ToChar(10)}, StringSplitOptions.RemoveEmptyEntries))
        End Function

        ''' <summary>
        ''' Extracts the element value.
        ''' </summary>
        ''' <param name="parent">The parent.</param>
        ''' <param name="elementName">Name of the element.</param>
        ''' <param name="defaultValue">The default value.</param>
        ''' <returns></returns>
        <Extension()> _
        Public Function ExtractElementValue(ByVal parent As XElement, ByVal elementName As String, ByVal defaultValue As String) As String
            Dim el = parent.Element(elementName)
            Return If(el IsNot Nothing, el.Value, defaultValue)
        End Function

        ''' <summary>
        ''' Extracts the element attribute value.
        ''' </summary>
        ''' <param name="parent">The parent.</param>
        ''' <param name="elementName">Name of the element.</param>
        ''' <param name="attributeName">Name of the attribute.</param>
        ''' <returns></returns>
        <Extension()> _
        Public Function ExtractElementAttributeValue(ByVal parent As XElement, ByVal elementName As String, ByVal attributeName As String) As String
            Return ExtractElementAttributeValue(parent, elementName, attributeName, String.Empty)
        End Function

        ''' <summary>
        ''' Extracts the element attribute value.
        ''' </summary>
        ''' <param name="parent">The parent.</param>
        ''' <param name="elementName">Name of the element.</param>
        ''' <param name="attributeName">Name of the attribute.</param>
        ''' <param name="defaultValue">The default value.</param>
        ''' <returns></returns>
        <Extension()> _
        Public Function ExtractElementAttributeValue(ByVal parent As XElement, ByVal elementName As String, ByVal attributeName As String, ByVal defaultValue As String) As String
            Dim returnValue = defaultValue

            Dim el = parent.Element(elementName)

            If el IsNot Nothing Then
                Dim at = el.Attribute(attributeName)
                If at IsNot Nothing Then
                    returnValue = at.Value
                End If
            End If

            Return returnValue
        End Function

    End Module

End Namespace