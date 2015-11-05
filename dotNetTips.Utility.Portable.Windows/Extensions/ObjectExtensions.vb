' ***********************************************************************
' Assembly         : dotNetTips.Utility.Portable
' Author           : David McCarter
' Created          : 12-04-2013
'
' Last Modified By : David McCarter
' Last Modified On : 04-16-2014
' ***********************************************************************
' <copyright file="ObjectExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Reflection
Imports System.Globalization

Namespace Extensions
  ''' <summary>
  ''' Extensions for objects.
  ''' </summary>
  ''' <remarks></remarks>
  Public Module ObjectExtensions

    ''' <summary>
    ''' Orders the by.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="list">The list.</param>
    ''' <param name="sortExpression">The sort expression.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
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

        If descending Then
          Return list.OrderByDescending(Function(x) prop.GetValue(x, Nothing))
        Else
          Return list.OrderBy(Function(x) prop.GetValue(x, Nothing))
        End If
      End If

      Return list
    End Function

    ''' <summary>
    ''' Splits the specified list.
    ''' </summary>
    ''' <param name="list">The list.</param>
    ''' <param name="groupCount">The group count.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function Split(list As IList, groupCount As Integer) As IList
      Return SplitList(Of IList)(CType(list, Global.System.Collections.Generic.List(Of Global.System.Collections.IList)), groupCount)
    End Function

    ''' <summary>
    ''' Determines whether the specified list is valid.
    ''' </summary>
    ''' <param name="list">The list.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function IsValid(list As IList) As Boolean
      Return list IsNot Nothing AndAlso list.Count > 0
    End Function

    ''' <summary>
    ''' Determines whether the specified list is valid.
    ''' </summary>
    ''' <param name="list">The list.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function IsValid(list As IEnumerable) As Boolean
      Return list.IsValid()
    End Function

    Private Function SplitList(Of T)(items As List(Of T), groupCount As Integer) As List(Of List(Of T))
      Dim allGroups As New List(Of List(Of T))()

      'split the list into equal groups
      Dim startIndex As Integer = 0
      Dim groupLength As Integer = CInt(System.Math.Round(CDbl(items.Count) / CDbl(groupCount), 0))
      While startIndex < items.Count
        Dim group As New List(Of T)()
        group.AddRange(items.GetRange(startIndex, groupLength))
        startIndex += groupLength

        'adjust group-length for last group
        If startIndex + groupLength > items.Count Then
          groupLength = items.Count - startIndex
        End If

        allGroups.Add(group)
      End While

      'merge last two groups, if more than required groups are formed
      If allGroups.Count > groupCount AndAlso allGroups.Count > 2 Then
        allGroups(allGroups.Count - 2).AddRange(allGroups.Last())
        allGroups.RemoveAt(allGroups.Count - 1)
      End If

      Return (allGroups)
    End Function

  End Module
End Namespace