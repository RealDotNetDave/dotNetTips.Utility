' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 05-11-2017
' ***********************************************************************
' <copyright file="DriveHelper.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Management
Imports dotNetTips.Utility.Portable.OOP

Namespace IO
    ''' <summary>
    ''' Drive helper methods.
    ''' </summary>
    Public Module DriveHelper
        ''' <summary>
        ''' Finds the serial number of a drive.
        ''' </summary>
        ''' <param name="drive">Drive name.</param>
        ''' <returns>Serial number.</returns>
        ''' <remarks>This call could take some time. Recommend multi-threading.</remarks>
        Public Function GetDriveSerialNumber(ByVal drive As String) As String
            Encapsulation.TryValidateParam(drive, NameOf(drive))

            Dim driveSerial = String.Empty

            'No matter what is sent in, get just the drive letter
            Dim driveFixed = System.IO.Path.GetPathRoot(drive)
            driveFixed = Replace(driveFixed, "\", String.Empty)

            'Perform Query
            Using querySearch As New ManagementObjectSearcher(String.Format("SELECT VolumeSerialNumber FROM Win32_LogicalDisk Where Name = '{0}'", driveFixed))
                Using queryCollection As ManagementObjectCollection = querySearch.Get()

                    For Each moItem In queryCollection.AsParallel()
                        driveSerial = CStr(moItem.Item("VolumeSerialNumber"))
                        Exit For
                    Next

                End Using
            End Using

            Return driveSerial

        End Function

    End Module
End Namespace