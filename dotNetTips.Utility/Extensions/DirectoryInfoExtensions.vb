' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 03-29-2016
'
' Last Modified By : David McCarter
' Last Modified On : 04-12-2016
' ***********************************************************************
' <copyright file="DirectoryInfoExtensions.vb" company="NicheWare - David McCarter">
'     NicheWare - David McCarter
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.IO
Imports System.Diagnostics.Contracts
Imports System.Runtime.CompilerServices

Namespace Extensions
    ''' <summary>
    ''' Extensions for DirectoryInfo
    ''' </summary>
    Public Module DirectoryInfoExtensions
        ''' <summary>
        ''' Delete files in a folder that are like the searchPattern, don't include sub-folders.
        ''' </summary>
        ''' <param name="directory">The directory.</param>
        ''' <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        ''' <returns>Number of files that have been deleted.</returns>
        ''' <remarks>Code By: gaston@f-a-q.be
        ''' <example>
        ''' Dim di As New DirectoryInfo("c:\temp")
        ''' di.DeleteFiles("*.xml")
        ''' </example></remarks>
        <Extension>
        Public Function DeleteFiles(ByVal directory As DirectoryInfo, ByVal searchPattern As String) As Integer
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrEmpty(searchPattern) = False, "searchPattern is nothing or empty.")

            Return DeleteFiles(directory, searchPattern, Tristate.False)
        End Function

        ''' <summary>
        ''' Gets the total size of a directory.
        ''' </summary>
        ''' <param name="info">The DirectoryInfo object.</param>
        ''' <returns>Size of directory.</returns>
        ''' <remarks>Original code written by: Joel at http://hardcodedblog.blogspot.com/</remarks>
        <Extension>
        Public Function GetSize(ByVal info As DirectoryInfo) As Long
            Dim size As Long = 0

            For Each folder As DirectoryInfo In info.GetDirectories().AsParallel
                size += folder.GetSize
            Next

            ' Recursive call
            For Each file As FileInfo In info.GetFiles().AsParallel
                size += file.Length
            Next

            Return size

        End Function

        ''' <summary>
        ''' Delete files in a folder that are like the searchPattern
        ''' </summary>
        ''' <param name="info">The information.</param>
        ''' <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        ''' <param name="includeSubdirectories">The include subdirectories.</param>
        ''' <returns>Number of files that have been deleted.</returns>
        ''' <remarks>Original Code By: gaston@f-a-q.be
        ''' <example>
        ''' Dim di As New DirectoryInfo("c:\temp")
        ''' di.DeleteFiles("*.xml", True)
        ''' </example></remarks>
        <Extension>
        Public Function DeleteFiles(ByVal info As DirectoryInfo, ByVal searchPattern As String, ByVal includeSubdirectories As Tristate) As Integer
            Contract.Requires(Of ArgumentNullException)(String.IsNullOrWhiteSpace(searchPattern) = False)
            Contract.Requires(Of ArgumentNullException)([Enum].IsDefined(GetType(Tristate), includeSubdirectories))

            Dim deleted As Integer = 0
            For Each fi As FileInfo In info.GetFiles(searchPattern, If(includeSubdirectories = Tristate.True, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly)).AsParallel
                fi.Delete()
                deleted += 1
            Next
            Return deleted
        End Function
    End Module

End Namespace