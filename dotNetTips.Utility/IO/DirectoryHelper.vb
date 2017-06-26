' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 04-15-2016
'
' Last Modified By : David McCarter
' Last Modified On : 06-24-2017
' ***********************************************************************
' <copyright file="DirectoryHelper.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.Generic
Imports System.IO
Imports System.Threading.Tasks

Namespace IO
    ''' <summary>
    ''' Class DirectoryHelper.
    ''' </summary>
    Public Module DirectoryHelper
        ''' <summary>
        ''' Applications the application data folder.
        ''' </summary>
        ''' <returns>System.String.</returns>
        Public Function AppApplicationDataFolder() As String
            Dim path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), My.Application.Info.CompanyName.Trim)

            Return path

        End Function

        ''' <summary>
        ''' Loads the files.
        ''' </summary>
        ''' <param name="directories">The directories.</param>
        ''' <param name="searchPattern">The search pattern.</param>
        ''' <param name="searchOption">The search option.</param>
        ''' <param name="throwException">if set to <c>true</c> [throw exception].</param>
        ''' <returns>IEnumerable(Of FileInfo).</returns>
        Public Function LoadFiles(ByVal directories As IEnumerable(Of DirectoryInfo), searchPattern As String, searchOption As SearchOption, throwException As Boolean) As IEnumerable(Of FileInfo)

            Dim files = New List(Of FileInfo)

            For Each directory In directories.AsParallel
                Try
                    SearchForFiles(searchPattern, searchOption, files, directory)
                Catch securityEx As UnauthorizedAccessException
                    If throwException Then
                        Throw
                    End If
                End Try
            Next

            Return files.Distinct.AsEnumerable

        End Function

        ''' <summary>
        ''' Searches for files.
        ''' </summary>
        ''' <param name="searchPattern">The search pattern.</param>
        ''' <param name="searchOption">The search option.</param>
        ''' <param name="files">The files.</param>
        ''' <param name="directory">The directory.</param>
        Private Sub SearchForFiles(searchPattern As String, searchOption As SearchOption, files As List(Of FileInfo), directory As DirectoryInfo)
            If (directory.Exists) Then
                Dim foundFiles = directory.EnumerateFiles(searchPattern, searchOption)
                SyncLock files
                    files.AddRange(foundFiles)
                End SyncLock
            End If
        End Sub

        ''' <summary>
        ''' Loads the files.
        ''' </summary>
        ''' <param name="directories">The directories.</param>
        ''' <param name="searchPattern">The search pattern.</param>
        ''' <param name="searchOption">The search option.</param>
        ''' <returns>IEnumerable(Of FileInfo).</returns>
        Public Function LoadFiles(ByVal directories As IEnumerable(Of DirectoryInfo), searchPattern As String, searchOption As SearchOption) As IEnumerable(Of FileInfo)

            Dim files = New List(Of FileInfo)

            For Each directory As DirectoryInfo In directories.AsParallel
                If (directory.Exists) Then
                    Dim foundFiles = directory.EnumerateFiles(searchPattern, searchOption)
                    SyncLock files
                        files.AddRange(foundFiles)
                    End SyncLock
                End If
            Next

            Return files.Distinct.AsEnumerable

        End Function

        ''' <summary>
        ''' delete directory as an asynchronous operation.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        ''' <returns>Task(Of System.Boolean).</returns>
        ''' <exception cref="System.ArgumentNullException">directory</exception>
        Public Async Function DeleteDirectoryAsync(directory As DirectoryInfo) As Task(Of Boolean)
            If directory Is Nothing Then
                Throw New ArgumentNullException(NameOf(directory))
            End If

            If directory.Exists Then
                Await Task.Factory.StartNew(Function()
                                                directory.Delete(recursive:=True)
                                                Return True
                                            End Function).ConfigureAwait(True)
            End If
            Return False
        End Function
    End Module
End Namespace