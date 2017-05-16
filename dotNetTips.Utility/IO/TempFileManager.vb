' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 03-06-2017
' ***********************************************************************
' <copyright file="TempFileManager.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Namespace IO

    ''' <summary>
    ''' Class to create and manage temporary files.
    ''' </summary>
    ''' <seealso cref="System.IDisposable" />
    Public Class TempFileManager
        Implements IDisposable
        ''' <summary>
        ''' The files
        ''' </summary>
        Private _files As New System.Collections.Generic.List(Of String)
        ''' <summary>
        ''' The disposed
        ''' </summary>
        Private _disposed As Boolean

        ''' <summary>
        ''' List of files currently being managed.
        ''' </summary>
        ''' <returns><see cref="System.Collections.ObjectModel.ReadOnlyCollection(Of String)">List</see> of files.</returns>
        Public Function FilesList() As IReadOnlyCollection(Of String)

            Return New ReadOnlyCollection(Of String)(Me._files)

        End Function

        ''' <summary>
        ''' Creates a new temporary file.
        ''' </summary>
        ''' <returns><see cref="String">Path</see> to new file.</returns>
        Public Function CreateFile() As String

            Dim tempFile As String = My.Computer.FileSystem.GetTempFileName

            Me._files.Add(tempFile)

            Return tempFile

        End Function

        ''' <summary>
        ''' Deletes a temporary file.
        ''' </summary>
        ''' <param name="file"><see cref="String">Path</see> of the temporary file.</param>
        Public Sub DeleteFile(ByVal file As String)

            If My.Computer.FileSystem.FileExists(file) Then
                My.Computer.FileSystem.DeleteFile(file)
            End If

            If Me._files.Contains(file) Then
                Me._files.Remove(file)

            End If

        End Sub

        ''' <summary>
        ''' Deletes all temporary files.
        ''' </summary>
        Public Sub DeleteAllFiles()

            Dim tempFiles() As String = New String(Me._files.Count - 1) {}

            Me._files.CopyTo(tempFiles, 0)

            Array.ForEach(tempFiles, AddressOf Me.DeleteFile)

        End Sub

        ''' <summary>
        ''' Releases resources used by the <see cref="TempFileManager">TempFileManager</see> object.
        ''' </summary>
        Public Overloads Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Releases unmanaged and - optionally - managed resources
        ''' </summary>
        ''' <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not (Me._disposed) Then
                ' If disposing equals true, dispose all managed
                ' and unmanaged resources.
                If (disposing) Then
                    ' Dispose managed resources.
                    Me.DeleteAllFiles()
                End If
            End If
            Me._disposed = True
        End Sub

        ''' <summary>
        ''' Allows an <see cref="T:System.Object" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object" /> is reclaimed by garbage collection.
        ''' </summary>
        Protected Overrides Sub Finalize()
            Try
                Dispose(False)
            Finally
                MyBase.Finalize()
            End Try

        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="TempFileManager" /> class.
        ''' </summary>
        Public Sub New()
        End Sub

    End Class

End Namespace