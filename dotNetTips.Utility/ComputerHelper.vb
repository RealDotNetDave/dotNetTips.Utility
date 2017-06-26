' ***********************************************************************
' Assembly         : dotNetTips
' Author           : David McCarter
' Created          : 12-10-2008
'
' Last Modified By : David McCarter
' Last Modified On : 05-11-2017
' ***********************************************************************
' <copyright file="ComputerHelper.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Management

''' <summary>
''' System information helper.
''' </summary>
Public Module ComputerHelper

    ''' <summary>
    ''' Product install state.
    ''' </summary>
    Private Enum ProductInstallState
        ''' <summary>
        ''' Bad Configuration
        ''' </summary>
        BadConfiguration = -6

        ''' <summary>
        ''' Invalid Argument
        ''' </summary>
        InvalidArgument = -2

        ''' <summary>
        ''' Unknown Package
        ''' </summary>
        UnknownPackage = -1

        ''' <summary>
        ''' Advertised
        ''' </summary>
        Advertised = 1

        ''' <summary>
        ''' Absent
        ''' </summary>
        Absent = 2

        ''' <summary>
        ''' Installed
        ''' </summary>
        Installed = 5
    End Enum

#Region "Public  Methods"
    ''' <summary>
    ''' Computer information for the current computer.
    ''' </summary>
    ''' <returns><seealso cref="ComputerInfo" /></returns>
    Public Function ComputerInformation() As ComputerInfo

        Return New ComputerInfo() With {.InstalledUICulture = My.Computer.Info.InstalledUICulture.DisplayName, .OSFullName = My.Computer.Info.OSFullName, .OSPlatform = My.Computer.Info.OSPlatform, .OSVersion = My.Computer.Info.OSVersion}

    End Function

    ''' <summary>
    ''' Computer IP addresses.
    ''' </summary>
    ''' <param name="hostName">Name of the host.</param>
    ''' <returns>ObjectModel.ReadOnlyCollection(Of System.Net.IPAddress).</returns>
    Public Function ComputerIPAddresses(ByVal hostName As String) As ObjectModel.ReadOnlyCollection(Of System.Net.IPAddress)
        Return New ObjectModel.ReadOnlyCollection(Of System.Net.IPAddress)(System.Net.Dns.GetHostEntry(hostName).AddressList.ToList)
    End Function

    ''' <summary>
    ''' Computers mac addresses.
    ''' </summary>
    ''' <returns>ReadOnlyCollection(Of System.String).</returns>
    Public Function ComputerMacAddresses() As ReadOnlyCollection(Of String)

        Using mc As New ManagementClass("Win32_NetworkAdapterConfiguration")
            Using mos As ManagementObjectCollection = mc.GetInstances()

                Dim addressList = New List(Of String)

                For Each mo As ManagementObject In mos
                    If CBool(mo("IPEnabled")) = True Then
                        addressList.Add(mo("MACAddress").ToString())
                    End If
                Next

                Return New ReadOnlyCollection(Of String)(addressList)
            End Using
        End Using

    End Function

    ''' <summary>
    ''' Computer IP addresses.
    ''' </summary>
    ''' <returns>ObjectModel.ReadOnlyCollection(Of System.Net.IPAddress).</returns>
    Public Function ComputerIPAddresses() As ObjectModel.ReadOnlyCollection(Of System.Net.IPAddress)
        Return ComputerIPAddresses(System.Net.Dns.GetHostName())
    End Function

    ''' <summary>
    ''' Current computer memory information.
    ''' </summary>
    ''' <returns><seealso cref="ComputerMemory" /></returns>
    Public Function ComputerMemory() As ComputerMemory
        Dim result As New ComputerMemory

        With result
            If (Not Long.TryParse(My.Computer.Info.AvailablePhysicalMemory.ToString(CultureInfo.CurrentCulture), .AvailablePhysicalMemory)) Then
                .AvailablePhysicalMemory = 0
            End If

            If Not Long.TryParse(My.Computer.Info.AvailableVirtualMemory.ToString(CultureInfo.CurrentCulture), .AvailableVirtualMemory) Then
                .AvailableVirtualMemory = 0
            End If

            If Not Long.TryParse(My.Computer.Info.TotalPhysicalMemory.ToString(CultureInfo.CurrentCulture), .TotalPhysicalMemory) Then
                .TotalPhysicalMemory = 0
            End If

            If Not Long.TryParse(My.Computer.Info.TotalVirtualMemory.ToString(CultureInfo.CurrentCulture), .TotalVirtualMemory) Then
                .TotalVirtualMemory = 0
            End If
        End With

        Return result

    End Function

    ''' <summary>
    ''' Return drive info for the current drives on the system.
    ''' </summary>
    ''' <returns><seealso cref="System.IO.DriveInfo" /></returns>
    Public Function ComputerReadyDrives() As ReadOnlyCollection(Of System.IO.DriveInfo)
        Dim drives As New List(Of System.IO.DriveInfo)

        For Each drive As System.IO.DriveInfo In My.Computer.FileSystem.Drives
            If drive.IsReady Then
                drives.Add(drive)
            End If
        Next

        Return New ReadOnlyCollection(Of System.IO.DriveInfo)(drives)

    End Function

    ''' <summary>
    ''' Current computer time.
    ''' </summary>
    ''' <returns><seealso cref="DateTime" /></returns>
    Public Function CurrentTime() As DateTime
        Return My.Computer.Clock.LocalTime
    End Function

    ''' <summary>
    ''' Renews DHCP for the current computer.
    ''' </summary>
    ''' <returns>Int32.</returns>
    Public Function RenewDhcpLease() As Int32
        Dim result As Int32

        Using classInstance As New ManagementClass("root\CIMV2", "Win32_NetworkAdapterConfiguration", Nothing)

            ' Execute the method and obtain the return values.
            Using outParams As ManagementBaseObject = classInstance.InvokeMethod("RenewDHCPLeaseAll", Nothing, Nothing)

                If Not Int32.TryParse(outParams(NameOf(result)).ToString, result) Then
                    result = -1
                End If

            End Using
        End Using

        Return result

    End Function
    ''' <summary>
    ''' Finds devices attached to the computer.
    ''' </summary>
    ''' <param name="machineNameOrAddress">Machine name or address.</param>
    ''' <returns>Collection of <seealso cref="DeviceInformation" /></returns>
    ''' <remarks>This call could take some time. Recommend multi-threading.</remarks>
    Public Function ServerDevices(ByVal machineNameOrAddress As String) As System.Collections.ObjectModel.ReadOnlyCollection(Of DeviceInformation)
        Dim devices As New System.Collections.Generic.List(Of DeviceInformation)

        'default to the local system
        'query the management scope for the instance(s) of the operating system
        'management object

        Using searcher As New ManagementObjectSearcher(New ManagementScope(String.Format(CultureInfo.CurrentCulture, "\\{0}\root\CIMV2", machineNameOrAddress), New ConnectionOptions()), New ObjectQuery("SELECT Caption,Description FROM CIM_LogicalDevice"))

            Using searchResults As ManagementObjectCollection = searcher.Get()

                For Each management As ManagementObject In searchResults
                    'get the time for the last successful boot up of the machine, and calculate the time difference
                    Dim device As New DeviceInformation() With {.Caption = CStr(management.GetPropertyValue("Caption")), .Description = CStr(management.GetPropertyValue("Description"))}
                    devices.Add(device)
                Next

            End Using

        End Using

        Return New ReadOnlyCollection(Of DeviceInformation)(devices)

    End Function

    ''' <summary>
    ''' Finds software installed on a computer.
    ''' </summary>
    ''' <param name="machineNameOrAddress">Machine name or address.</param>
    ''' <returns>Collection of <seealso cref="InstalledProduct" /></returns>
    ''' <remarks>This call could take some time. Recommend multi-threading.</remarks>
    Public Function ServerInstalledSoftware(ByVal machineNameOrAddress As String) As ReadOnlyCollection(Of InstalledProduct)

        Dim result As New List(Of InstalledProduct)

        Using searcher As New ManagementObjectSearcher(String.Format(CultureInfo.CurrentCulture, "\\{0}\root\CIMV2", machineNameOrAddress), "SELECT Caption, Description, IdentifyingNumber, InstallDate, InstallLocation, InstallState, Name, PackageCache, SKUNumber, Vendor, Version FROM Win32_Product")

            For Each queryObj As ManagementObject In searcher.Get

                If Not IsNothing(queryObj) Then
                    Dim product As New InstalledProduct

                    With product
                        If Not IsNothing(queryObj("Caption")) Then
                            .Caption = queryObj("Caption").ToString
                        End If
                        If Not IsNothing(queryObj("Description")) Then
                            .Description = queryObj("Description").ToString
                        End If
                        If Not IsNothing(queryObj("IdentifyingNumber")) Then
                            .IdentifyingNumber = queryObj("IdentifyingNumber").ToString
                        End If

                        .InstallDate = ConvertWmiDateTime(queryObj("InstallDate").ToString)

                        If Not IsNothing(queryObj("InstallLocation")) Then
                            .InstallLocation = queryObj("InstallLocation").ToString
                        End If

                        If Not Int32.TryParse(queryObj("InstallState").ToString, .InstallState) Then
                            'Default to Unknown
                            .InstallState = ProductInstallState.UnknownPackage
                        End If

                        If System.Enum.IsDefined(GetType(ProductInstallState), .InstallState) Then
                            .InstallStateDescription = System.Enum.GetName(GetType(ProductInstallState), .InstallState).ToString
                        End If

                        If Not IsNothing(queryObj("Name")) Then
                            .Name = queryObj("Name").ToString
                        End If

                        If Not IsNothing(queryObj("PackageCache")) Then
                            .PackageCache = queryObj("PackageCache").ToString
                        End If

                        If Not IsNothing(queryObj("SKUNumber")) Then
                            .SkuNumber = queryObj("SKUNumber").ToString
                        End If

                        If Not IsNothing(queryObj("Vendor")) Then
                            .Vendor = queryObj("Vendor").ToString
                        End If

                        If Not IsNothing(queryObj(NameOf(Version))) Then
                            .Version = queryObj(NameOf(Version)).ToString
                        End If

                    End With

                    result.Add(product)

                End If
            Next

        End Using

        Return New ReadOnlyCollection(Of InstalledProduct)(result)

    End Function

    ''' <summary>
    ''' Releases DHCP for the current computer.
    ''' </summary>
    ''' <returns>Int32.</returns>
    Public Function ReleaseDhcpLease() As Int32
        Dim result As Int32

        Using classInstance As New ManagementClass("root\CIMV2", "Win32_NetworkAdapterConfiguration", Nothing)

            ' Execute the method and obtain the return values.
            Using outParams As ManagementBaseObject = classInstance.InvokeMethod("ReleaseDHCPLeaseAll", Nothing, Nothing)
#Disable Warning CC0021
                ' You should use nameof instead of the parameter element name string
                If Not Int32.TryParse(outParams("result").ToString, result) Then
#Enable Warning CC0021
                    result = -1
                End If
            End Using
        End Using

        Return result

    End Function

    ''' <summary>
    ''' Finds the server up time.
    ''' </summary>
    ''' <param name="machineNameOrAddress">Machine name or address.</param>
    ''' <returns><seealso cref="ServerUpTimeInformation" /></returns>
    Public Function ServerUpTime(ByVal machineNameOrAddress As String) As ServerUptimeInformation
        Dim result As New ServerUptimeInformation

        'query the management scope for the instance(s) of the operating system
        'management object

        Using searcher As New ManagementObjectSearcher(New ManagementScope(String.Format(CultureInfo.CurrentCulture, "\\{0}\root\CIMV2", machineNameOrAddress), New ConnectionOptions()), New ObjectQuery("SELECT LocalDateTime, LastBootUpTime FROM CIM_OperatingSystem"))

            Using searchResults As ManagementObjectCollection = searcher.Get()

                For Each management As ManagementObject In searchResults
                    'get the time for the last successful boot up of the machine, and calculate the time diff
                    result.CurrentTime = ConvertWmiDateTime(CStr(management.GetPropertyValue("LocalDateTime")))
                    result.BootTime = ConvertWmiDateTime(CStr(management.GetPropertyValue("LastBootUpTime")))

                    Dim span As TimeSpan = result.CurrentTime.Subtract(result.BootTime)

                    result.Uptime = span

                Next
            End Using

        End Using

        Return result

    End Function

    ''' <summary>
    ''' Fines user domain and name.
    ''' </summary>
    ''' <returns>User domain and name.</returns>
    Public Function LookupUserNameDomain() As String
        Return String.Format(CultureInfo.CurrentCulture, "{0}\{1}", System.Environment.UserDomainName, System.Environment.UserName)
    End Function
#End Region

#Region "Private  Methods"
    ''' <summary>
    ''' Converts the WMI date time.
    ''' </summary>
    ''' <param name="value">a datetime string in the WMI format</param>
    ''' <returns>a DateTime object converted from the WMI datetime string</returns>
    Private Function ConvertWmiDateTime(ByVal value As String) As System.DateTime
        Dim year As Int32
        Dim month As Int32
        Dim day As Int32
        Dim hour As Int32 = 0
        Dim minute As Int32 = 0
        Dim second As Int32 = 0
        Dim millisecond As Int32 = 0
        Dim tempString As String = System.String.Empty

        If System.String.IsNullOrEmpty(value) Then
            Return System.DateTime.MinValue
        End If

        '8 character date
        tempString = value.Substring(0, 4)
        year = If("****" <> tempString, System.Int32.Parse(tempString, CultureInfo.CurrentCulture), System.DateTime.Now.Year)

        tempString = value.Substring(4, 2)
        month = If("**" <> tempString, System.Int32.Parse(tempString, CultureInfo.CurrentCulture), 1)

        tempString = value.Substring(6, 2)
        day = If("**" <> tempString, System.Int32.Parse(tempString, CultureInfo.CurrentCulture), 1)

        '25 character date/time
        If value.Length <= 8 Then
            Return New System.DateTime(year, month, day, hour, minute, second, millisecond)
        End If

        tempString = value.Substring(8, 2)
        If "**" <> tempString Then
            hour = System.Int32.Parse(tempString, CultureInfo.CurrentCulture)
        End If
        tempString = value.Substring(10, 2)
        If "**" <> tempString Then
            minute = System.Int32.Parse(tempString, CultureInfo.CurrentCulture)
        End If
        tempString = value.Substring(12, 2)
        If "**" <> tempString Then
            second = System.Int32.Parse(tempString, CultureInfo.CurrentCulture)
        End If
        tempString = value.Substring(15, 3)
        If "***" <> tempString Then
            millisecond = System.Int32.Parse(tempString, CultureInfo.CurrentCulture)
        End If

        Return New System.DateTime(year, month, day, hour, minute, second, millisecond)

    End Function
#End Region

End Module