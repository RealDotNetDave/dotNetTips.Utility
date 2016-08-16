' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 08-03-2016
' ***********************************************************************
' <copyright file="ObjectExtensions.vb" company="David McCarter Consulting">
'     '     David McCarter Consulting. All rights reserved.
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Reflection
Imports System.Globalization
Imports System.Runtime.CompilerServices

Namespace Extensions
    ''' <summary>
    ''' Extensions for objects.
    ''' </summary>
    Public Module ObjectExtensions

        ''' <summary>
        ''' Determines whether the specified list is valid.
        ''' </summary>
        ''' <param name="list">The list.</param>
        ''' <returns><c>true</c> if the specified list is valid; otherwise, <c>false</c>.</returns>
        <Extension>
        Public Function IsValid(list As IEnumerable) As Boolean
            Return list.IsValid()
        End Function

        ''' <summary>
        ''' Orders the by.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="list">The list.</param>
        ''' <param name="sortExpression">The sort expression.</param>
        ''' <returns>IEnumerable(Of T).</returns>
        ''' <exception cref="System.NullReferenceException"></exception>
        Public Function OrderBy(Of T)(ByVal list As IEnumerable(Of T), ByVal sortExpression As String) As IEnumerable(Of T)

            sortExpression += String.Empty
            Dim parts As String() = sortExpression.Split(" "c)
            Dim descending As Boolean = False
            Dim [property] As String = String.Empty

            If parts.Length > 0 AndAlso Not (Not parts(0) Is Nothing AndAlso String.IsNullOrEmpty(parts(0))) Then
                [property] = parts(0)

                If parts.Length > 1 Then
                    descending = parts(1).ToLower().Contains("esc")
                End If

                Dim prop As PropertyInfo = GetType(T).GetRuntimeProperty([property])

                If prop Is Nothing Then
                    Throw New NullReferenceException(String.Format(CultureInfo.CurrentCulture, "{0}{1}'", ("No property '" & [property] & "' in + "), GetType(T).Name))
                End If

                Return If(descending, list.OrderByDescending(Function(ByVal x) prop.GetValue(x, Nothing)), list.OrderBy(Function(ByVal x) prop.GetValue(x, Nothing)))
            End If

            Return list
        End Function

    End Module
End Namespace