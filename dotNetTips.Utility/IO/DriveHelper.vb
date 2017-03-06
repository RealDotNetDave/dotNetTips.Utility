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
Imports System.Management
Imports dotNetTips.Utility.Portable.OOP

Namespace IO
    ''' <summary>
    ''' Drive helper methods.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module DriveHelper
        ''' <summary>
        ''' Finds the serial number of a drive.
        ''' </summary>
        ''' <param name="drive">Drive name.</param>
        ''' <returns>Serial number.</returns>
        ''' <remarks>This call could take some time. Recommend multi-threading.</remarks>
        Public Function GetDriveSerialNumber(ByVal drive As String) As String
            Encapsulation.TryValidateParam(Of ArgumentNullException)(String.IsNullOrWhiteSpace(drive) = False)

            Dim driveSerial As String = String.Empty

            'No matter what is sent in, get just the drive letter
            Dim driveFixed As String = System.IO.Path.GetPathRoot(drive)
            driveFixed = Replace(driveFixed, "\", String.Empty)

            'Perform Query
            Using querySearch As New ManagementObjectSearcher(String.Format("SELECT VolumeSerialNumber FROM Win32_LogicalDisk Where Name = '{0}'", driveFixed))
                Using queryCollection As ManagementObjectCollection = querySearch.Get()
                    Dim moItem As ManagementObject
                    For Each moItem In queryCollection
                        driveSerial = CStr(moItem.Item("VolumeSerialNumber"))
                        Exit For
                    Next
                End Using
            End Using

            Return driveSerial

        End Function

    End Module
End Namespace