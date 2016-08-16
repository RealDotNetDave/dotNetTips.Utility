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
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports Microsoft.VisualBasic.Logging
Imports System.Reflection
Imports System.Globalization
Imports System.Diagnostics.Contracts

''' <summary>
''' Trace listener log manager.
''' </summary>
''' <remarks></remarks>
Public Module LogManagement
    Private _log As Log

#Region "Public Methods"

    ''' <summary>
    ''' Gets the selected enum values.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="enumInputValue">The enum input value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSelectedEnumValues(Of T As Structure)(enumInputValue As T) As Collection(Of T)
        Dim enumType As Type = GetType(T)

        If Not enumType.IsEnum Then
            Throw New ArgumentException([String].Format(CultureInfo.InvariantCulture, "{0} is not an Enum.", enumType), NameOf(enumInputValue), Nothing)
        End If

        Dim enumValues As Array = [Enum].GetValues(enumType)
        Dim inputValueLong As Long = Convert.ToInt64(enumInputValue, CultureInfo.InvariantCulture)
        Dim enumOutputCollection As New Collection(Of T)()

        For Each enumValue As T In enumValues
            Dim enumValueLong As Long = Convert.ToInt64(enumValue, CultureInfo.InvariantCulture)

            If (enumValueLong And inputValueLong) = enumValueLong AndAlso enumValueLong <> 0 Then
                enumOutputCollection.Add(enumValue)
            End If
        Next

        If enumOutputCollection.Count = 0 AndAlso enumValues.GetLength(0) > 1 Then
            Dim enumNoneValue As T = DirectCast(enumValues.GetValue(0), T)
            If Convert.ToInt64(enumNoneValue, CultureInfo.InvariantCulture) = 0 Then
                enumOutputCollection.Add(enumNoneValue)
            End If
        End If

        Return enumOutputCollection
    End Function

    ''' <summary>
    ''' Gets the enumeration values.
    ''' </summary>
    ''' <param name="enumType">Type of the enum.</param>
    ''' <param name="fixNames">if set to <c>true</c> [fix names].</param>
    ''' <param name="useXmlNames">if set to <c>true</c> [use XML names].</param>
    ''' <returns>Enum values.</returns>
    ''' Added on: 6/11/2009 By: dm11086
    Public Function GetEnumerationValues(enumType As Type, fixNames As Boolean, useXmlNames As Boolean) As Dictionary(Of Integer, String)
        Contract.Requires(Of ArgumentNullException)(enumType IsNot Nothing)

        If enumType Is Nothing Then
            Throw New InvalidEnumTypeException([String].Format(CultureInfo.InvariantCulture, "Failed to find type {0}", enumType.Name))
        End If

        'Check that the type is based on Enum
        If Not enumType.IsSubclassOf(GetType([Enum])) Then
            Throw New InvalidEnumTypeException([String].Format(CultureInfo.InvariantCulture, "Type {0} is not an enum", enumType.Name))
        End If

        'Get the enum values
        Dim allValues = DirectCast([Enum].GetValues(enumType), Integer())

        'Get list of names
        Dim allNames = GetEnumNames(enumType, fixNames, useXmlNames)

        'Add values to result
        Dim result = New Dictionary(Of Integer, String)()
        For i As Integer = 0 To allValues.Length - 1
            result.Add(allValues(i), allNames(i))
        Next

        Return result
    End Function

    ''' <summary>
    ''' Gets the Tracing level values.
    ''' </summary>
    ''' <param name="level">The source level.</param>
    ''' <returns>List of values.</returns>
    Public Function TracingLevelValues(ByVal level As SourceLevels) As ReadOnlyCollection(Of Integer)
        Contract.Requires(Of ArgumentOutOfRangeException)([Enum].IsDefined(GetType(SourceLevels), level))

        Dim valuesNew = New List(Of Integer)()

        For Each value As Integer In [Enum].GetValues(level.[GetType]())
            If (CInt(level) And value) = value Then
                valuesNew.Add(value)
            End If
        Next

        Return New ReadOnlyCollection(Of Integer)(valuesNew)
    End Function

    ''' <summary>
    ''' Changes the tracing level.
    ''' </summary>
    ''' <param name="level">The level.</param>
    Public Sub ChangeTracingLevel(ByVal level As SourceLevels)
        GetLog().TraceSource.Switch.Level = level
    End Sub

    ''' <summary>
    ''' Currents the tracing level.
    ''' </summary>
    ''' <returns>Current tracing level.</returns>
    Public Function CurrentTracingLevel() As SourceLevels
        Return GetLog().TraceSource.Switch.Level
    End Function

    ''' <summary>
    ''' Listeners in this instance.
    ''' </summary>
    ''' <returns>List of Listener names.</returns>
    Public Function Listeners() As IEnumerable(Of String)
        Dim currentListeners = New List(Of String)()

        For Each listener As TraceListener In GetLog().TraceSource.Listeners
            currentListeners.Add(listener.Name)
        Next

        Return currentListeners.AsEnumerable()
    End Function

    ''' <summary>
    ''' Removes the Listener.
    ''' </summary>
    ''' <param name="listenerName">Name of the listener.</param>
    Public Sub RemoveListener(ByVal listenerName As String)
        GetLog().TraceSource.Listeners.Remove(listenerName)
    End Sub

    ''' <summary>
    ''' Restores the Listeners.
    ''' </summary>
    Public Sub RestoreListeners()
        _log = If(IsAspApp(), DirectCast(New AspLog(), Log), New Log())
    End Sub
#End Region

#Region "Internal Methods"
    ''' <summary>
    ''' Determines whether [is ASP app].
    ''' </summary>
    ''' <returns>
    ''' <c>true</c> if [is ASP app]; otherwise, <c>false</c>.
    ''' </returns>
    Friend Function IsAspApp() As Boolean
        Dim directory = AppDomain.CurrentDomain.DynamicDirectory

        Return If(directory Is Nothing, False, directory.Contains("\Temporary ASP.NET Files\"))
    End Function

    ''' <summary>
    ''' Gets the correct logging object.
    ''' </summary>
    ''' <returns>Logging object.</returns>
    Friend Function GetLog() As Log
        If _log Is Nothing Then
            _log = If(IsAspApp(), DirectCast(New AspLog(), Log), New Log())
        End If

        Return _log
    End Function
#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Gets the enum names.
    ''' </summary>
    ''' <param name="t">The type.</param>
    ''' <param name="fixNames">if set to <c>true</c> [fix names].</param>
    ''' <param name="useXml">if set to <c>true</c> [use XML].</param>
    ''' <returns>List of enum names.</returns>
    Private Function GetEnumNames(t As Type, fixNames As Boolean, useXml As Boolean) As List(Of String)
        'Set up result
        Dim result As New List(Of String)()

        'Enum names (text) are defined as fields for the type
        For Each enumValue As FieldInfo In t.GetFields(BindingFlags.[Public] Or BindingFlags.[Static])
            'If XML is specified and found, use this name
            If useXml Then
                Dim xmlAttr As System.Xml.Serialization.XmlEnumAttribute = DirectCast(Attribute.GetCustomAttribute(enumValue, GetType(System.Xml.Serialization.XmlEnumAttribute)), System.Xml.Serialization.XmlEnumAttribute)
                If xmlAttr IsNot Nothing Then
                    result.Add(xmlAttr.Name)
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
                If fixNames Then
                    result.Add(AdjustName(enumValue.Name))
                Else
                    result.Add(enumValue.Name)
                End If
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' Helper function to allow name adjustment
    ''' </summary>
    ''' <param name="name">Name to adjust</param>
    ''' <returns>Amended name</returns>
    ''' <remarks>
    ''' </remarks>
    Private Function AdjustName(name As String) As String
        Return AdjustCamelCase(name.Replace("_", " ")).Replace(" ", " ")
    End Function

    ''' <summary>
    ''' Helper function to add spaces to CamelCase names
    ''' </summary>
    ''' <param name="name">Text to convert.</param>
    ''' <returns>Name with spaces added</returns>
    ''' <remarks>
    ''' Converts values such as 'ThisIsATest' to 'This Is A Test'
    ''' </remarks>
    Private Function AdjustCamelCase(name As String) As String
        Const toTitleCase As String = "(\B[A-Z])"
        Return System.Text.RegularExpressions.Regex.Replace(name, toTitleCase, " $1")
    End Function
#End Region

End Module