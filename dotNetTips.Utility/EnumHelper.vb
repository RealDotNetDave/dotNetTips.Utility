' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 07-16-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-16-2016
' ***********************************************************************
' <copyright file="EnumHelper.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Reflection

''' <summary>
''' Enum helper code.
''' Some of this code came from Howard Richards + Omar Besiso.
''' </summary>
Public Module EnumHelper

    ''' <summary>
    ''' Gets the enumeration values.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="fixNames">if set to <c>true</c> [fix names].</param>
    ''' <param name="useXmlNames">if set to <c>true</c> [use XML names].</param>
    ''' <returns>Collection(Of EnumValue).</returns>
    ''' <exception cref="InvalidEnumTypeException">
    ''' </exception>
    Public Function GetEnumerationValues(Of T As Structure)(ByVal fixNames As Boolean, ByVal useXmlNames As Boolean) As Collection(Of EnumValue)
        Dim enumType = GetType(T)

        If enumType Is Nothing Then
            Throw New InvalidEnumTypeException(String.Format(CultureInfo.CurrentCulture, "Failed to find type {0}", enumType.Name))
        End If

        'Check that the type is based on Enum
        If Not enumType.BaseType Is GetType([Enum]) Then
            Throw New InvalidEnumTypeException(String.Format(CultureInfo.CurrentCulture, "Type {0} is not an enum", enumType.Name))
        End If

        'Get the enum values
        Dim allValues = CType(System.Enum.GetValues(enumType), Integer())

        'Get list of names
        'Add values to result
        Dim result = New Collection(Of EnumValue)()

        'TODO: This does not work if enums are not defined 0,1,2, etc
        For i As Integer = 0 To allValues.Length - 1
            result.Add(New EnumValue(allValues(i), (GetEnumNames(enumType, fixNames, useXmlNames))(i)))
        Next

        Return result
    End Function

    ''' <summary>
    ''' Gets the selected enum values.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="enumInputValue">The enum input value.</param>
    ''' <returns>Collection(Of T).</returns>
    ''' <exception cref="System.ArgumentException">enumInputValue;Nothing</exception>
    Public Function GetSelectedEnumValues(Of T As Structure)(ByVal enumInputValue As T) As Collection(Of T)
        Dim enumType As Type = GetType(T)
        If Not enumType.IsEnum Then
            Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, "{0} is not an Enum.", enumType.ToString()), NameOf(enumInputValue), Nothing)
        End If

        Dim enumValues As Array = [Enum].GetValues(enumType)
        Dim enumOutputCollection As New Collection(Of T)()

        For Each enumValue As T In enumValues
            Dim enumValueLong As Long = Convert.ToInt64(enumValue, CultureInfo.CurrentCulture)

            If (enumValueLong And Convert.ToInt64(enumInputValue, CultureInfo.CurrentCulture)) = enumValueLong AndAlso enumValueLong <> 0 Then
                enumOutputCollection.Add(enumValue)
            End If
        Next

        If enumOutputCollection.Count = 0 AndAlso enumValues.GetLength(0) > 1 Then
            Dim enumNoneValue As T = DirectCast(enumValues.GetValue(0), T)
            If Convert.ToInt64(enumNoneValue, CultureInfo.CurrentCulture) = 0 Then
                enumOutputCollection.Add(enumNoneValue)
            End If
        End If

        Return enumOutputCollection
    End Function

    ''' <summary>
    ''' Helper function that returns an array of names for Enum values
    ''' </summary>
    ''' <param name="t">The t.</param>
    ''' <param name="fixNames">Set true if you want underscores converted to spaces and CamelCase converted to 'Camel Case'</param>
    ''' <param name="useXml">if set to <c>true</c> [use XML].</param>
    ''' <returns>List(Of System.String).</returns>
    Private Function GetEnumNames(ByVal t As Type, ByVal fixNames As Boolean, ByVal useXml As Boolean) As List(Of String)
        'Set up result
        Dim result As New List(Of String)()

        'Enum names (text) are defined as fields for the type
        For Each enumValue As FieldInfo In t.GetFields(BindingFlags.[Public] Or BindingFlags.[Static])
            'If XML is specified and found, use this name
            If useXml Then
                Dim attribute As System.Xml.Serialization.XmlEnumAttribute = DirectCast(System.Attribute.GetCustomAttribute(enumValue, GetType(System.Xml.Serialization.XmlEnumAttribute)), System.Xml.Serialization.XmlEnumAttribute)
                If attribute IsNot Nothing Then
                    result.Add(attribute.Name)
                    Continue For
                End If
            End If
            'Attempt to use the Description attribute (if present)
            Dim description As System.ComponentModel.DescriptionAttribute = DirectCast(Attribute.GetCustomAttribute(enumValue, GetType(System.ComponentModel.DescriptionAttribute)), System.ComponentModel.DescriptionAttribute)
            If description IsNot Nothing Then
                'use this value
                result.Add(description.Description)
            Else
                'If not found use the Enum name, with adjustment if requested
                result.Add(If(fixNames, AdjustName(enumValue.Name), enumValue.Name))

            End If
        Next
        Return result
    End Function

    ''' <summary>
    ''' Helper function to allow name adjustment
    ''' </summary>
    ''' <param name="name">Name to adjust</param>
    ''' <returns>Amended name</returns>
    Private Function AdjustName(ByVal name As String) As String
        Return AdjustCamelCase(name.Replace("_", " ")).Replace(" ", " ")
    End Function

    ''' <summary>
    ''' Helper function to add spaces to CamelCase names
    ''' </summary>
    ''' <param name="name">The name.</param>
    ''' <returns>Name with spaces added</returns>
    ''' <remarks>Converts values such as 'ThisIsATest' to 'This Is A Test'</remarks>
    Private Function AdjustCamelCase(ByVal name As String) As String
        Const toTitleCase As String = "(\B[A-Z])"
        Return System.Text.RegularExpressions.Regex.Replace(name, toTitleCase, " $1")
    End Function

End Module