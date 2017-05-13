' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : david
' Created          : 05-05-2017
'
' Last Modified By : david
' Last Modified On : 05-05-2017
' ***********************************************************************
' <copyright file="Services.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections
Imports System.Text
Imports System.ServiceProcess
Imports System.Linq
Imports System.Collections.Generic
Imports System
Imports dotNetTips.Utility.Portable.OOP

''' <summary>
''' Class Services.
''' </summary>
Public Module Services
        'TODO: BLOG POST
        ''' <summary>
        ''' Services the status.
        ''' </summary>
        ''' <param name="serviceName">Name of the service.</param>
        ''' <returns>ServiceControllerStatus.</returns>
        ''' <exception cref="System.InvalidOperationException">Service not found.</exception>
        Public Function ServiceStatus(serviceName As String) As ServiceControllerStatus
            Dim service As Object = LoadService(serviceName)

            If service IsNot Nothing Then
                Return service.Status
            Else
                Throw New InvalidOperationException("Service not found.")
            End If
        End Function

        ''' <summary>
        ''' Services the exists.
        ''' </summary>
        ''' <param name="serviceName">Name of the service.</param>
        ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        Public Function ServiceExists(serviceName As String) As Boolean
            Dim service As Object = LoadService(serviceName)

            If service IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Stops the service.
        ''' </summary>
        ''' <param name="serviceName">Name of the service.</param>
        ''' <returns>ServiceActionResult.</returns>
        Public Function StopService(serviceName As String) As ServiceActionResult
            Dim statusResult As Object = ServiceActionResult.NotFound

            If ServiceExists(serviceName) = False Then
                Return statusResult
            End If

            Dim service As Object = LoadService(serviceName)

            If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Running) Then
                service.[Stop]()
                statusResult = ServiceActionResult.Stopped
            End If

            Return statusResult
        End Function

        ''' <summary>
        ''' Starts the service.
        ''' </summary>
        ''' <param name="serviceName">Name of the service.</param>
        ''' <returns>ServiceActionResult.</returns>
        Public Function StartService(serviceName As String) As ServiceActionResult
            Dim statusResult As Object = ServiceActionResult.NotFound

            If ServiceExists(serviceName) = False Then
                Return statusResult
            End If

            Dim service As Object = LoadService(serviceName)

            If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Stopped) Then
                service.Start()
                statusResult = ServiceActionResult.Running
            End If

            Return statusResult
        End Function

        ''' <summary>
        ''' Alls the services.
        ''' </summary>
        ''' <returns>IEnumerable(Of System.String).</returns>
        Public Function AllServices() As IEnumerable(Of String)
            Return ServiceController.GetServices().[Select](Function(p) p.ServiceName).AsEnumerable()
        End Function

        ''' <summary>
        ''' Loads the service.
        ''' </summary>
        ''' <param name="serviceName">Name of the service.</param>
        ''' <returns>ServiceController.</returns>
        Private Function LoadService(serviceName As String) As ServiceController
            Return ServiceController.GetServices().Where(Function(p) p.ServiceName = serviceName).FirstOrDefault()
        End Function

        ''' <summary>
        ''' Starts the stop services.
        ''' </summary>
        ''' <param name="requests">The requests.</param>
        Public Sub StartStopServices(requests As IEnumerable(Of ServiceAction))
            Encapsulation.TryValidateParam(requests, NameOf(requests))

            For Each request As Object In requests
                If request.ServiceActionRequest = ServiceActionRequest.Start Then
                    request.ServiceActionResult = StartService(request.ServiceName)
                ElseIf request.ServiceActionRequest = ServiceActionRequest.[Stop] Then
                    request.ServiceActionResult = StopService(request.ServiceName)
                End If
            Next
        End Sub
    End Module

    ''' <summary>
    ''' Enum ServiceActionResult
    ''' </summary>
    Public Enum ServiceActionResult
        ''' <summary>
        ''' The not found
        ''' </summary>
        NotFound
        ''' <summary>
        ''' The running
        ''' </summary>
        Running
        ''' <summary>
        ''' The stopped
        ''' </summary>
        Stopped
    End Enum

    ''' <summary>
    ''' Enum ServiceActionRequest
    ''' </summary>
    Public Enum ServiceActionRequest
        ''' <summary>
        ''' The unknown
        ''' </summary>
        Unknown
        ''' <summary>
        ''' The start
        ''' </summary>
        Start
        ''' <summary>
        ''' The stop
        ''' </summary>
        [Stop]
    End Enum

''' <summary>
''' Class ServiceAction.
''' </summary>
Public Class ServiceAction
    ''' <summary>
    ''' Gets or sets the name of the service.
    ''' </summary>
    ''' <value>The name of the service.</value>
    Public Property ServiceName() As String
    ''' <summary>
    ''' Gets or sets the service action request.
    ''' </summary>
    ''' <value>The service action request.</value>
    Public Property ServiceActionRequest() As ServiceActionRequest
    ''' <summary>
    ''' Gets or sets the service action result.
    ''' </summary>
    ''' <value>The service action result.</value>
    Public Property ServiceActionResult() As ServiceActionResult
End Class
