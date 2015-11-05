' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 05-14-2014
'
' Last Modified By : David McCarter
' Last Modified On : 05-14-2014
' ***********************************************************************
' <copyright file="CollectionExtensions.vb" company="David McCarter Consulting">
'     David McCarter Consulting. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Linq
Namespace Extensions
  ''' <summary>
  '''
  ''' </summary>
  Public Module CollectionExtensions
    ''' <summary>
    ''' Determines whether the specified list is valid.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="list">The list.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function IsValid(Of T)(list As System.Collections.ObjectModel.ObservableCollection(Of T)) As Boolean
      Return list IsNot Nothing AndAlso list.Count > 0
    End Function

    ''' <summary>
    ''' To the image.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function ToImage(value As Byte()) As Image
      Contracts.Contract.Requires(Of ArgumentNullException)(value IsNot Nothing AndAlso value.Length > 0)
      'Contracts.Contract.Requires(Of ArgumentOutOfRangeException)(value.Rank = 1)

      Dim image As Image

      Using stream As New MemoryStream(value)
        image = Image.FromStream(stream)
      End Using

      Return image

    End Function

    ''' <summary>
    ''' Froms the delimited string.
    ''' </summary>
    ''' <param name="ls">The ls.</param>
    ''' <param name="buf">The buf.</param>
    ''' <param name="delimiter">The delimiter.</param>
    ''' <returns></returns>
    ''' <remarks>Code by: Blake Pell</remarks>
    <Extension()>
    Public Function FromDelimitedString(ByVal ls As List(Of String), ByVal buf As String, ByVal delimiter As String) As List(Of String)
      Dim items() As String = Split(buf, delimiter)
      Return items.ToList
    End Function

    ''' <summary>
    ''' Firsts the or nothing.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="source">The source.</param>
    ''' <param name="match">The match.</param>
    ''' <returns></returns>
    ''' <remarks>Code by: Fons Sonnemans</remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function FirstOrNothing(Of T As Structure)(source As IEnumerable(Of T), match As Func(Of T, Boolean)) As Nullable(Of T)
      For Each item In source.AsParallel
        If match(item) Then
          Return item
        End If
      Next
      Return Nothing
    End Function

    ''' <summary>
    ''' Adds if not exists.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="list">The list.</param>
    ''' <param name="value">The value.</param>
    ''' <remarks>Code by: Nick Nijenhuis</remarks>
    <Extension>
    Public Sub AddIfNotExists(Of T)(list As ICollection(Of T), value As T)
      If Not list.Contains(value) Then
        list.Add(value)
      End If
    End Sub

    ''' <summary>
    ''' Orders a list based on a sortexpression. Useful in object databinding scenarios where the objectdatasource generates a dynamic sortexpression (example: "Name desc") that specifies the property of the object sort on.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="list">The list.</param>
    ''' <param name="sortExpression">The sort expression.</param>
    ''' <returns></returns>
    ''' <remarks>Code by: C.F.Meijers</remarks>
    <System.Runtime.CompilerServices.Extension>
    Public Function OrderBy(Of T)(list As IEnumerable(Of T), sortExpression As String) As IEnumerable(Of T)
      sortExpression += String.Empty
      Dim parts As String() = sortExpression.Split(" "c)
      Dim descending As Boolean = False
      Dim [property] As String = String.Empty

      If parts.Length > 0 AndAlso parts(0) <> String.Empty Then
        [property] = parts(0)

        If parts.Length > 1 Then
          descending = parts(1).ToLower().Contains("esc")
        End If

        Dim prop As PropertyInfo = GetType(T).GetProperty([property])

        If prop Is Nothing Then
          Throw New Exception((String.Format("{0}{1}", Convert.ToString("No property '"), [property])) + "' in + " + GetType(T).Name + "'")
        End If

        If descending Then
          Return list.OrderByDescending(Function(x) prop.GetValue(x, Nothing))
        Else
          Return list.OrderBy(Function(x) prop.GetValue(x, Nothing))
        End If
      End If

      Return list
    End Function

  End Module
End Namespace