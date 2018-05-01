' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : david
' Created          : 05-05-2017
'
' Last Modified By : david
' Last Modified On : 07-18-2017
' ***********************************************************************
' <copyright file="Services.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ServiceProcess
Imports dotNetTips.Utility.Portable.OOP

''' <summary>
''' Class that deals with Windows services.
''' </summary>
Public Module Services

    ''' <summary>
    ''' Loads the service.
    ''' </summary>
    ''' <param name="serviceName">Name of the service.</param>
    ''' <returns>ServiceController.</returns>
    Private Function LoadService(serviceName As String) As ServiceController
        Return ServiceController.GetServices().FirstOrDefault(Function(p) p.ServiceName = serviceName)
    End Function

    ''' <summary>
    ''' Alls the services.
    ''' </summary>
    ''' <returns>IEnumerable(Of System.String).</returns>
    Public Function AllServices() As IEnumerable(Of String)
        Return ServiceController.GetServices().[Select](Function(p) p.ServiceName).AsEnumerable()
    End Function

    ''' <summary>
    ''' Services the exists.
    ''' </summary>
    ''' <param name="serviceName">Name of the service.</param>
    ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    Public Function ServiceExists(serviceName As String) As Boolean
        Encapsulation.TryValidateParam(serviceName, NameOf(serviceName))

        Dim service = LoadService(serviceName)

        Return If(service IsNot Nothing, True, False)
    End Function

    ''' <summary>
    ''' Gets the status of a Windows service.
    ''' </summary>
    ''' <param name="serviceName">Name of the service.</param>
    ''' <returns>ServiceControllerStatus.</returns>
    ''' <exception cref="InvalidOperationException">Service not found.</exception>
    ''' <exception cref="System.InvalidOperationException">Service not found.</exception>
    Public Function ServiceStatus(serviceName As String) As ServiceControllerStatus
        Encapsulation.TryValidateParam(serviceName, NameOf(serviceName))

        If ServiceExists(serviceName) Then
            Dim service = LoadService(serviceName)

            If service IsNot Nothing Then
                Return service.Status
            End If
        End If
    End Function

    ''' <summary>
    ''' Starts the service.
    ''' </summary>
    ''' <param name="serviceName">Name of the service.</param>
    ''' <returns>ServiceActionResult.</returns>
    Public Function StartService(serviceName As String) As ServiceActionResult
        Encapsulation.TryValidateParam(serviceName, NameOf(serviceName))

        Dim statusResult = ServiceActionResult.NotFound

        If ServiceExists(serviceName) = False Then
            Return statusResult
        End If

        Try
            Dim service = LoadService(serviceName)

            If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Stopped) Then
                service.Start()
                statusResult = ServiceActionResult.Running
            End If

        Catch win32Ex As Win32Exception
            statusResult = ServiceActionResult.Error
        Catch invalidEx As InvalidOperationException
            statusResult = ServiceActionResult.Error
        End Try

        Return statusResult
    End Function

    ''' <summary>
    ''' Starts the stop services.
    ''' </summary>
    ''' <param name="services">The requests.</param>
    Public Sub StopServices(services As IEnumerable(Of ServiceAction))
        Encapsulation.TryValidateParam(Of ArgumentNullException)(services IsNot Nothing, NameOf(services))

        For Each service In services.AsParallel
            service.ServiceActionResult = StopService(service.ServiceName)
        Next

    End Sub


    ''' <summary>
    ''' Starts the services.	
    ''' </summary>
    ''' <param name="services">The services.</param>
    ''' <remarks></remarks>
    Public Sub StartServices(services As IEnumerable(Of ServiceAction))
        Encapsulation.TryValidateParam(Of ArgumentNullException)(services IsNot Nothing, NameOf(services))

        For Each service In services.AsParallel
            service.ServiceActionResult = StartService(service.ServiceName)
        Next

    End Sub

    ''' <summary>
    ''' Get the status of a Windows Service.	
    ''' </summary>
    ''' <param name="services">The services.</param>
    ''' <remarks></remarks>
    Public Sub ServicesStatus(services As IEnumerable(Of ServiceAction))
        Encapsulation.TryValidateParam(Of ArgumentNullException)(services IsNot Nothing, NameOf(services))

        ''Find status on services
        For Each service In services.AsParallel
            service.ServiceActionResult = ServiceStatus(service.ServiceName)
        Next
    End Sub

    ''' <summary>
    ''' Stops the service.
    ''' </summary>
    ''' <param name="serviceName">Name of the service.</param>
    ''' <returns>ServiceActionResult.</returns>
    Public Function StopService(serviceName As String) As ServiceActionResult
        Encapsulation.TryValidateParam(serviceName, NameOf(serviceName))

        Dim statusResult As Object = ServiceActionResult.NotFound

        If ServiceExists(serviceName) = False Then
            Return statusResult
        End If

        Try
            Dim service = LoadService(serviceName)

            If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Running) Then
                service.Stop()
                statusResult = ServiceActionResult.Stopped
            End If
        Catch win32Ex As Win32Exception
            statusResult = ServiceActionResult.Error
        Catch invalidEx As InvalidOperationException
            statusResult = ServiceActionResult.Error
        End Try

        Return statusResult
    End Function
End Module
