'***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 06-16-2009
' Description      :
'
' Copyright        : (c) dotNetTips.com. All rights reserved.
'***********************************************************************
Imports System.IO

Namespace IO

    ''' <summary>
    ''' General path helper methods.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module PathHelper

        ''' <summary>
        ''' Converts a long path name into a short path name.
        ''' </summary>
        ''' <param name="longPathName">Long path name to convert.</param>
        ''' <returns>Short path name.</returns>
        ''' <remarks></remarks>
        Public Function ConvertLongPathToShort(ByVal longPathName As String) As String

            If String.IsNullOrEmpty(longPathName) Then
                Throw New ArgumentNullException(NameOf(longPathName))
            End If

            Dim shortNameBuffer As New System.Text.StringBuilder
            Dim size As Integer = UnsafeNativeMethods.GetShortPathName(longPathName, shortNameBuffer, shortNameBuffer.Capacity)

            If size < shortNameBuffer.Capacity Then
                Return shortNameBuffer.ToString()
            End If

            shortNameBuffer.Capacity = size + 1

            If UnsafeNativeMethods.GetShortPathName(longPathName, shortNameBuffer, shortNameBuffer.Capacity) = 0 Then
                shortNameBuffer.Remove(0, shortNameBuffer.Length)
            End If

            Return shortNameBuffer.ToString()

        End Function

        ''' <summary>
        ''' Finds a path on the computer. Checks all local drives only.
        ''' </summary>
        ''' <param name="path">Path to find, can include file name.</param>
        ''' <returns>First path found.</returns>
        ''' <remarks></remarks>
        Public Function FindPathOnDrives(ByVal path As String) As String

            Dim tempPath As String = String.Empty

            If Not File.Exists(System.IO.Path.GetFullPath(path)) Then
                Dim pathRoot As String
                pathRoot = System.IO.Path.GetPathRoot(path)

                Dim drives = From d In dotNetTips.Utility.ComputerHelper.ComputerReadyDrives
                             Where d.IsReady
                             Select Replace(path, pathRoot, d.Name)

                'Search all local drives for the path/file
                For Each testPath As String In drives

                    If File.Exists(testPath) Then
                        tempPath = testPath
                        Exit For
                    End If

                Next

            Else
                tempPath = path
            End If

            Return tempPath

        End Function

    End Module

End Namespace