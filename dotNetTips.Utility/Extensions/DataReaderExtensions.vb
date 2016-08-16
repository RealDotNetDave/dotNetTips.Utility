' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 03-29-2016
'
' Last Modified By : David McCarter
' Last Modified On : 04-11-2016
' ***********************************************************************
' <copyright file="DataReaderExtensions.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports System.Text

''' <summary>
''' Class DataReaderExtensions.
''' </summary>
Public Module DataReaderExtensions
    ''' <summary>
    ''' To the CSV.
    ''' </summary>
    ''' <param name="dataReader">The data reader.</param>
    ''' <param name="includeHeaderAsFirstRow">The include header as first row.</param>
    ''' <param name="separator">The separator.</param>
    ''' <returns>List(Of System.String).</returns>
    ''' <remarks>Code by: Thierry Fierens</remarks>
    <Extension>
    Public Function ToCsv(dataReader As IDataReader, includeHeaderAsFirstRow As Boolean, separator As String) As List(Of String)
        Dim csvRows As New List(Of String)()
        Dim sb As StringBuilder = Nothing

        If includeHeaderAsFirstRow Then
            sb = New StringBuilder()
            For index As Integer = 0 To dataReader.FieldCount - 1
                If dataReader.GetName(index) IsNot Nothing Then
                    sb.Append(dataReader.GetName(index))
                End If

                If index < dataReader.FieldCount - 1 Then
                    sb.Append(separator)
                End If
            Next
            csvRows.Add(sb.ToString())
        End If

        While dataReader.Read()
            sb = New StringBuilder()
            For index As Integer = 0 To dataReader.FieldCount - 2
                If Not dataReader.IsDBNull(index) Then
                    Dim value As String = dataReader.GetValue(index).ToString()
                    If dataReader.GetFieldType(index) = GetType([String]) Then
                        'If double quotes are used in value, ensure each are replaced but 2.
                        If value.IndexOf("""", StringComparison.Ordinal) >= 0 Then
                            value = value.Replace("""", """""")
                        End If

                        'If separator are is in value, ensure it is put in double quotes.
                        If value.IndexOf(separator, StringComparison.Ordinal) >= 0 Then
                            value = (Convert.ToString("""", CultureInfo.InvariantCulture) & value) + """"
                        End If
                    End If
                    sb.Append(value)
                End If

                If index < dataReader.FieldCount - 1 Then
                    sb.Append(separator)
                End If
            Next

            If Not dataReader.IsDBNull(dataReader.FieldCount - 1) Then
                sb.Append(dataReader.GetValue(dataReader.FieldCount - 1).ToString().Replace(separator, " "))
            End If

            csvRows.Add(sb.ToString())
        End While
        Return csvRows
    End Function
End Module