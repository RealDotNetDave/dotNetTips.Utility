' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : dotnetdave
' Created          : 04-23-2016
'
' Last Modified By : dotnetdave
' Last Modified On : 04-23-2016
' ***********************************************************************
' <copyright file="ServicesHelper.vb" company="">
'     . All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.ServiceProcess

''' <summary>
'''
''' </summary>
Public Module ServicesHelper
    ''' <summary>
    ''' Stops the SQL server.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopSQLServer()
        If ApplicationHelper.IsProcessRunning("MSSQLSERVER") Then
            Dim service = ServiceController.GetServices().Where(Function(p) p.ServiceName = "MSSQLSERVER").FirstOrDefault

            If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Running) Then
                service.Stop()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Starts the SQL server.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartSqlServer()

        Dim service = ServiceController.GetServices().Where(Function(p) p.ServiceName = "MSSQLSERVER").FirstOrDefault

        If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Stopped) Then
            service.Start()
        End If

    End Sub
End Module