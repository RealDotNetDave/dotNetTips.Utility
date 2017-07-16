' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 07-09-2017
'
' Last Modified By : David McCarter
' Last Modified On : 07-16-2017
' ***********************************************************************
' <copyright file="Processor.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' *************************************************************************
Imports dotNetTips.Utility.My.Resources
Imports dotNetTips.Utility.Portable.OOP
Imports System
Imports System.Collections.Generic
Imports System.IO

''' <summary>
''' Class Processor.
''' </summary>
Public Class Processor

    ''' <summary>
    ''' Occurs when processor processes a file or folder.
    ''' </summary>
    Event Processed As EventHandler(Of ProgressEventArgs)

    ''' <summary>
    ''' Handles the <see cref="E:Processed" /> event.
    ''' </summary>
    ''' <param name="e">The <see cref="ProgressEventArgs" /> instance containing the event data.</param>
    Protected Overridable Sub OnProcessed(e As ProgressEventArgs)
        RaiseEvent Processed(Me, e)
    End Sub

    ''' <summary>
    ''' Copies files to new location. Will not throw exceptions.
    ''' </summary>
    ''' <param name="files">The files.</param>
    ''' <param name="destinationFolder">The destination folder.</param>
    ''' <returns>System.Object.</returns>
    ''' <remarks>Use the Processed event to find out if file copied succeeded or failed.</remarks>
    Public Function CopyFiles(files As IEnumerable(Of FileInfo), destinationFolder As DirectoryInfo)
        ''TODO: UNTESTED
        Encapsulation.TryValidateParam(files, NameOf(files))
        Encapsulation.TryValidateParam(Of ArgumentNullException)(destinationFolder IsNot Nothing, NameOf(destinationFolder))

        Dim successCount = 0

        Dim backUpFolderRoot = destinationFolder.FullName.Split(Portable.ControlChars.BackSlash).Last().Trim()

        For Each tempFile In files.AsParallel
            If tempFile.Exists Then
                Try
                    Dim newFileName = tempFile.FullName.Replace(destinationFolder.FullName, backUpFolderRoot)
                    tempFile.CopyTo(newFileName, True)

                    successCount += 1

                    OnProcessed(New ProgressEventArgs With {.Name = tempFile.FullName, .ProgressState = ProgressState.Copied, .Size = tempFile.Length})
                Catch ex As Exception
                    OnProcessed(New ProgressEventArgs With {.Name = tempFile.FullName, .ProgressState = ProgressState.Error, .Size = tempFile.Length, .Message = ex.Message})
                End Try

            Else
                OnProcessed(New ProgressEventArgs With {.Name = tempFile.FullName, .ProgressState = ProgressState.Error, .Size = tempFile.Length, .Message = Resources.FileNotFound})
            End If
        Next

        Return successCount

    End Function

    ''' <summary>
    ''' Deletes file list.
    ''' </summary>
    ''' <param name="files">The file list to delete.</param>
    ''' <returns>System.Int32 with the number of files that were successfully deleted.</returns>
    ''' <remarks>Use the <seealso cref="Processed">Processed</seealso> event to find out if file deletion succeeded or failed.</remarks>
    Public Function DeleteFiles(files As IEnumerable(Of FileInfo)) As Integer
        Encapsulation.TryValidateParam(files, NameOf(files))

        Dim successCount = 0

        For Each tempFile In files.AsParallel
            If tempFile.Exists Then
                Try
                    tempFile.Delete()

                    successCount += 1

                    OnProcessed(New ProgressEventArgs With {.Name = tempFile.FullName, .ProgressState = ProgressState.Deleted, .Size = tempFile.Length})

                Catch ex As Exception
                    OnProcessed(New ProgressEventArgs With {.Name = tempFile.FullName, .ProgressState = ProgressState.Error, .Size = tempFile.Length, .Message = ex.Message})
                End Try

            Else
                OnProcessed(New ProgressEventArgs With {.Name = tempFile.FullName, .ProgressState = ProgressState.Error, .Size = tempFile.Length, .Message = Resources.FileNotFound})
            End If
        Next

        Return successCount
    End Function

    ''' <summary>
    ''' Deletes the folders.
    ''' </summary>
    ''' <param name="folders">The folders.</param>
    ''' <returns>System.Int32.</returns>
    Public Function DeleteFolders(folders As IEnumerable(Of DirectoryInfo)) As Integer
        Encapsulation.TryValidateParam(folders, NameOf(folders))

        Dim successCount = 0

        For Each tempFolder In folders.AsParallel
            If tempFolder.Exists Then
                Try
                    tempFolder.Delete(True)

                    successCount += 1

                    OnProcessed(New ProgressEventArgs With {.Name = tempFolder.FullName, .ProgressState = ProgressState.Deleted})

                Catch ex As Exception
                    OnProcessed(New ProgressEventArgs With {.Name = tempFolder.FullName, .ProgressState = ProgressState.Error, .Message = ex.Message})
                End Try
            Else
                OnProcessed(New ProgressEventArgs With {.Name = tempFolder.FullName, .ProgressState = ProgressState.Error, .Message = Resources.FolderNotFound})
            End If
        Next

        Return successCount

    End Function


End Class
