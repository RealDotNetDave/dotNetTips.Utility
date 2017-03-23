' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-07-2016
'
' Last Modified By : David McCarter
' Last Modified On : 03-13-2017
' ***********************************************************************
' <copyright file="ServicesHelper.vb" company="NicheWare - David McCarter">
'     '     NicheWare - David McCarter
'
' </copyright>
' <summary></summary>
' *************************************************************************

Imports System.ServiceProcess

''' <summary>
''' Class ServicesHelper.
''' </summary>
Public Module ServicesHelper
    ''' <summary>
    ''' Stops the SQL server.
    ''' </summary>
    Public Sub StopSQLServer()

        Dim service = ServiceController.GetServices().Where(Function(p) p.ServiceName = "MSSQLSERVER").FirstOrDefault

        If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Running) Then
            service.Stop()
        End If

    End Sub

    ''' <summary>
    ''' Starts the SQL server.
    ''' </summary>
    Public Sub StartSqlServer()

        Dim service = ServiceController.GetServices().Where(Function(p) p.ServiceName = "MSSQLSERVER").FirstOrDefault

        If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Stopped) Then
            service.Start()
        End If

    End Sub
End Module