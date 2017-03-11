' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 05-20-2016
'
' Last Modified By : David McCarter
' Last Modified On : 08-16-2016
' ***********************************************************************
' <copyright file="Deflater.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.IO
Imports System.IO.Compression

Namespace IO
    ''' <summary>
    ''' This class performs files compression and decompression
    ''' <remarks>Original code by: Bechir Bejaoui</remarks>
    ''' </summary>
    Public Class Deflater
        Implements IDisposable

        ''' <summary>
        ''' This byte array is used to stock both
        ''' The input file contents and out put file
        ''' contents as bytes
        ''' </summary>
        Private _buffer As Byte()

        ''' <summary>
        ''' This is the class responsible of
        ''' zipping and unzipping files
        ''' </summary>
        Private _zipperStream As DeflateStream

        ''' <summary>
        ''' This is a default constructor
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' This is an overloaded constructor
        ''' </summary>
        ''' <param name="sourceFileName">This represents the
        ''' full source file name of the one going to be zipped</param>
        ''' <param name="destinationFileName">This represents the
        ''' full source file name of the one going to be unziped</param>
        Public Sub New(sourceFileName As String, destinationFileName As String)

            _zipperStream = Nothing

            Me.SourceFileName = sourceFileName

            Me.DestinationFileName = destinationFileName
        End Sub

        ''' <summary>
        ''' This is the source file full path property
        ''' </summary>
        ''' <value>The name of the source file.</value>
        Public Property SourceFileName() As String = String.Empty

        ''' <summary>
        ''' This is the destination full path property
        ''' </summary>
        ''' <value>The name of the destination file.</value>
        Public Property DestinationFileName() As String = String.Empty

        ''' <summary>
        ''' This is the method responsible for compression, it is marked
        ''' as protected because we use it is called at the constructor
        ''' level when a compression mode is chosen instead of using it directly
        ''' </summary>
        Public Sub CompressFile()

            If File.Exists(Me.SourceFileName) Then

                Using inputFile As FileStream = File.Open(SourceFileName, FileMode.Open), outputFile As FileStream = File.Create(DestinationFileName)

                    Using oZipper = New DeflateStream(outputFile, CompressionMode.Compress)

                        _buffer = New Byte(Convert.ToInt32(inputFile.Length - 1)) {}

                        Dim counter As Integer = 0

                        While (InlineAssignHelper(counter, inputFile.Read(_buffer, 0, _buffer.Length))) <> 0

                            oZipper.Write(_buffer, 0, counter)

                        End While
                    End Using

                    _buffer = Nothing

                End Using
            End If

        End Sub

        ''' <summary>
        ''' This is the method responsible for compression, it is marked
        ''' as protected because we use it is called at the constructor
        ''' level when a decompression mode is chosen instead of using it directly
        ''' </summary>
        Public Sub DecompressFile()
            If File.Exists(Me.SourceFileName) Then
                Using inputFile As FileStream = File.Open(SourceFileName, FileMode.Open), outputFile As FileStream = File.Create(DestinationFileName)

                    Using zipper = New DeflateStream(inputFile, CompressionMode.Decompress)
                        Dim test As New Byte()

                        _buffer = New Byte(Convert.ToInt32(inputFile.Length - 1)) {}

                        Dim counter As Integer

                        While (InlineAssignHelper(counter, zipper.Read(_buffer, 0, _buffer.Length))) <> 0

                            outputFile.Write(_buffer, 0, counter)

                        End While
                    End Using

                    _buffer = Nothing

                End Using
            End If

        End Sub

        ''' <summary>
        ''' Inlines the assign helper.
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="target">The target.</param>
        ''' <param name="value">The value.</param>
        ''' <returns>T.</returns>
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function


#Region "IDisposable Implementation"

        Protected disposed As Boolean

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            SyncLock Me
                ' Do nothing if the object has already been disposed of.
                If disposed Then
                    Exit Sub
                End If

                If disposing Then
                    ' Release disposable objects used by this instance here.

                    If Not _zipperStream Is Nothing Then
                        _zipperStream.Dispose()
                    End If
                End If

                ' Release unmanaged resources here. Don't access reference type fields.

                ' Remember that the object has been disposed of.
                disposed = True
            End SyncLock
        End Sub

        Public Sub Dispose() _
                Implements IDisposable.Dispose
            Dispose(True)
            ' Unregister object for finalization.
            GC.SuppressFinalize(Me)
        End Sub

#End Region
    End Class
End Namespace