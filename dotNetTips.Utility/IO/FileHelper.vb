' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 05-11-2017
' ***********************************************************************
' <copyright file="FileHelper.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports dotNetTips.Utility.Portable.OOP

Namespace IO
    ''' <summary>
    ''' File helper functions.
    ''' </summary>
    Public Module FileHelper

        ''' <summary>
        ''' Occurs when [read text file progress].
        ''' </summary>
        Event ReadTextFileProgress As EventHandler(Of ProcessorEventArgs)

        'Public Sub CompressFile(sourceFileName As String, destinationFileName As String)
        '    Encapsulation.TryValidateParam(sourceFileName, NameOf(sourceFileName))
        '    Encapsulation.TryValidateParam(destinationFileName, NameOf(destinationFileName))

        '    If File.Exists(sourceFileName) Then
        '        Using archive = ZipFile.Open(destinationFileName, ZipArchiveMode.Create)
        '            Dim file = archive.CreateEntryFromFile(sourceFileName, Path.GetFileName(destinationFileName), CompressionLevel.Optimal)
        '        End Using
        '    End If
        'End Sub

        ''' <summary>
        ''' Copies file.
        ''' </summary>
        ''' <param name="sourceFileName">Source file name.</param>
        ''' <param name="destinationFileName">Destination file name.</param>
        ''' <param name="showUI">Shows UI during copy process.</param>
        Public Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As Microsoft.VisualBasic.FileIO.UIOption)

            My.Computer.FileSystem.CopyFile(sourceFileName, destinationFileName, showUI)

        End Sub

        ''' <summary>
        ''' Copies the file.
        ''' </summary>
        ''' <param name="sourceFileName">Name of the source file.</param>
        ''' <param name="destinationFileName">Name of the destination file.</param>
        ''' <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        Public Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal overwrite As Boolean)

            My.Computer.FileSystem.CopyFile(sourceFileName, destinationFileName, overwrite)

        End Sub

        ''' <summary>
        ''' Copies file.
        ''' </summary>
        ''' <param name="sourceFileName">Source file name.</param>
        ''' <param name="destinationFileName">Destination file name.</param>
        ''' <param name="showUI">Shows UI during copy process.</param>
        ''' <param name="onUserCancel">Allows user to cancel process.</param>
        Public Sub CopyFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As Microsoft.VisualBasic.FileIO.UIOption, ByVal onUserCancel As Microsoft.VisualBasic.FileIO.UICancelOption)

            My.Computer.FileSystem.CopyFile(sourceFileName, destinationFileName, showUI, onUserCancel)

        End Sub

        ''' <summary>
        ''' Deletes file.
        ''' </summary>
        ''' <param name="file">File name to delete.</param>
        ''' <param name="showUI">Shows UI during delete process.</param>
        ''' <param name="recycle">Sends file to recycle bin.</param>
        Public Sub DeleteFile(ByVal file As String, ByVal showUI As Microsoft.VisualBasic.FileIO.UIOption, ByVal recycle As Microsoft.VisualBasic.FileIO.RecycleOption)
            My.Computer.FileSystem.DeleteFile(file, showUI, recycle)
        End Sub

        ''' <summary>
        ''' Deletes file.
        ''' </summary>
        ''' <param name="file">File name to delete.</param>
        ''' <param name="showUI">Shows UI during delete process.</param>
        ''' <param name="recycle">Sends file to recycle folder.</param>
        ''' <param name="onUserCancel">Allows user to cancel process.</param>
        Public Sub DeleteFile(ByVal file As String, ByVal showUI As Microsoft.VisualBasic.FileIO.UIOption, ByVal recycle As Microsoft.VisualBasic.FileIO.RecycleOption, ByVal onUserCancel As Microsoft.VisualBasic.FileIO.UICancelOption)
            My.Computer.FileSystem.DeleteFile(file, showUI, recycle, onUserCancel)
        End Sub

        ''' <summary>
        ''' Moves file.
        ''' </summary>
        ''' <param name="sourceFileName">File to move.</param>
        ''' <param name="destinationFileName">Destination file name.</param>
        ''' <param name="showUI">Shows UI during move process.</param>
        Public Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As Microsoft.VisualBasic.FileIO.UIOption)
            My.Computer.FileSystem.MoveFile(sourceFileName, destinationFileName, showUI)
        End Sub

        ''' <summary>
        ''' Moves file.
        ''' </summary>
        ''' <param name="sourceFileName">File to move.</param>
        ''' <param name="destinationFileName">Destination file name.</param>
        ''' <param name="showUI">Shows UI during move process.</param>
        ''' <param name="onUserCancel">Allows user to cancel process.</param>
        Public Sub MoveFile(ByVal sourceFileName As String, ByVal destinationFileName As String, ByVal showUI As Microsoft.VisualBasic.FileIO.UIOption, ByVal onUserCancel As Microsoft.VisualBasic.FileIO.UICancelOption)
            My.Computer.FileSystem.MoveFile(sourceFileName, destinationFileName, showUI, onUserCancel)
        End Sub

        ''' <summary>
        ''' Locks a text file and returns content. Also throws the ReadTextFileProgress event.
        ''' </summary>
        ''' <param name="fileName">File name to read.</param>
        ''' <param name="encoding">Text encoding.</param>
        ''' <returns>File contents.</returns>
        ''' <exception cref="System.ArgumentNullException">
        ''' fileName
        ''' or
        ''' encoding
        ''' </exception>
        Function ReadTextFile(ByVal fileName As String, ByVal encoding As Encoding) As String
            Dim fileText As New StringBuilder()
            Dim fileBytes As Int32
            Dim fileTotalLength As Long
            Dim fileLengthCopied As Long
            Dim cancel As Boolean

            If String.IsNullOrEmpty(fileName) Then
                Throw New ArgumentNullException(NameOf(fileName))
            End If

            If IsNothing(encoding) Then
                Throw New ArgumentNullException(NameOf(encoding))
            End If

            'Open up the file
            Using readFileStream As New FileStream(fileName, FileMode.Open, FileAccess.Read)
                fileTotalLength = readFileStream.Length
                readFileStream.Lock(0, fileTotalLength)

                'Read the first chunk of data
                Dim fileData(512) As Byte
                fileBytes = readFileStream.Read(fileData, 0, 512)

                'Loop through and read the rest of the data
                While fileBytes > 0
                    fileText.Append(encoding.GetString(fileData, 0, fileBytes))
                    fileLengthCopied += fileBytes

                    Dim arguments As New ProcessorEventArgs() With {.TotalLength = fileTotalLength, .LengthCopied = fileLengthCopied, .Percent = CDbl((fileLengthCopied / fileTotalLength) * 100), .Cancel = cancel}

                    'Send back progress
                    RaiseEvent ReadTextFileProgress(Nothing, arguments)

                    If cancel Then
                        'Clear variable
                        fileText.Remove(1, fileText.Length)
                        Exit While
                    End If

                    fileBytes = readFileStream.Read(fileData, 0, 512)

                End While

                readFileStream.Unlock(0, fileTotalLength)
            End Using

            Return If(Not cancel, fileText.ToString(), String.Empty)

        End Function

    End Module
End Namespace