' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-03-2015
'
' Last Modified By : David McCarter
' Last Modified On : 04-12-2016
' ***********************************************************************
' <copyright file="DataTableExtensions.vb" company="dotNetTips.com">
'     '     '     dotNetTips.com. All rights reserved.
'
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text
Imports dotNetTips.Utility.Portable.OOP
Imports System.Data

''' <summary>
''' Class DataTableExtensions.
''' </summary>
Public Module DataTableExtensions

    ''' <summary>
    ''' Determines whether the specified <see cref="DataTable" /> has rows.
    ''' </summary>
    ''' <param name="table">The table.</param>
    ''' <returns><c>true</c> if the specified table has rows; otherwise, <c>false</c>.</returns>
    <Extension>
    Public Function HasRows(ByVal table As DataTable) As Boolean
        Return ((table IsNot Nothing) AndAlso (table.Rows IsNot Nothing) AndAlso (table.Rows.Count > 0))
    End Function

    ''' <summary>
    ''' Copies to entity list.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dt">The dt.</param>
    ''' <returns>IEnumerable(Of T).</returns>
    ''' <remarks>Original code by: Leo Shih</remarks>
    <Extension>
    Public Function CopyToEntityList(Of T As New)(ByVal dt As DataTable) As IEnumerable(Of T)

        Dim properties = New T().GetType().GetProperties()

        Dim columns = From col In dt.Columns.Cast(Of DataColumn)()
                      Select col.ColumnName, col.DataType

        Dim pptList = (From ppt In properties
                       Where columns.Select(Function(p) p.ColumnName).Contains(ppt.Name) _
                       And columns.Select(Function(p) p.DataType).Contains(
If(Nullable.GetUnderlyingType(ppt.PropertyType), ppt.PropertyType))
                       Select ppt)

        Dim returnList As New List(Of T)

        For Each dr As DataRow In dt.Rows
            Dim newT As New T()
            For Each entityItem As PropertyInfo In pptList
                entityItem.SetValue(newT, If(IsDBNull(dr(entityItem.Name)), Nothing, dr(entityItem.Name)), Nothing)
            Next
            returnList.Add(newT)
        Next

        Return returnList
    End Function

    ''' <summary>
    ''' To the CSV.
    ''' </summary>
    ''' <param name="table">The table.</param>
    ''' <param name="delimiter">The delimiter.</param>
    ''' <param name="includeHeader">if set to <c>true</c> [include header].</param>
    <Extension>
    Public Sub ToCsv(table As DataTable, delimiter As String, includeHeader As Boolean)
        Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrEmpty(delimiter) = False, "delimiter is nothing or empty.")

        Dim result As New StringBuilder()

        If includeHeader Then

            For Each column As DataColumn In table.Columns

                result.Append(column.ColumnName)
                result.Append(delimiter)
            Next

            result.Remove(System.Threading.Interlocked.Decrement(result.Length), 0)

            result.Append(Environment.NewLine)
        End If

        For Each row As DataRow In table.Rows

            For Each item As Object In row.ItemArray

                If TypeOf item Is System.DBNull Then

                    result.Append(delimiter)
                Else

                    Dim itemAsString As String = item.ToString()

                    ' Double up all embedded double quotes

                    itemAsString = itemAsString.Replace("""", """""")

                    ' To keep things simple, always delimit with double-quotes

                    ' so we don't have to determine in which cases they're necessary

                    ' and which cases they're not.

                    itemAsString = (Convert.ToString("""", CultureInfo.InvariantCulture) & itemAsString) + """"

                    result.Append(itemAsString & delimiter)

                End If
            Next

            result.Remove(System.Threading.Interlocked.Decrement(result.Length), 0)

            result.Append(Environment.NewLine)
        Next

        Using writer As New StreamWriter("C:\log.csv", True)

            writer.Write(result.ToString())
        End Using

    End Sub

End Module