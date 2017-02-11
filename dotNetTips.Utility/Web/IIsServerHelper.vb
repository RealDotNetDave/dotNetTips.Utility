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
Imports System.ServiceProcess

Namespace Web
    ''' <summary>
    ''' Performs IIS Server functions
    ''' </summary>
    Public Module IIsServerHelper
        Const W3Svc As String = "W3SVC"

        ''' <summary>
        ''' Allows actions to be taken on a web server.
        ''' </summary>
        ''' <param name="server">Server name.</param>
        ''' <param name="userName">User name. Required for remote server.</param>
        ''' <param name="password">User password. Required for remote server.</param>
        ''' <param name="domain">Optional domain name.</param>
        ''' <param name="instance">Web server instance. Optional, W3SVC will be used if not given.</param>
        ''' <param name="action">Action to take on the web server.</param>
        ''' <returns>Return value from call to the web server.</returns>
        ''' <remarks></remarks>
        Public Function ManageIISWebService(ByVal server As String, ByVal userName As String, ByVal password As System.Security.SecureString, ByVal domain As String, ByVal instance As String, ByVal action As WebServiceAction) As Int32
            Dim result As Int32
            Contracts.Contract.Requires(Of ArgumentNullException)(password IsNot Nothing)
            Contracts.Contract.Requires(Of ArgumentOutOfRangeException)(WebServiceAction.IsDefined(GetType(WebServiceAction), action))

            'Default to the local system
            Dim path As String = String.Format(CultureInfo.CurrentCulture, "\\{0}\root\MicrosoftIISv2", server)
            Dim connection As New ConnectionOptions

            If Not String.IsNullOrEmpty(userName) Then
                connection.Username = userName
            End If

            If (Not IsNothing(password)) AndAlso (Not String.IsNullOrEmpty(password.ToString)) Then
                connection.Password = password.ToString
            End If

            If Not String.IsNullOrEmpty(domain) Then
                connection.Authority = String.Format(CultureInfo.CurrentCulture, "ntlmdomain:{0}", domain)
            End If

            connection.Impersonation = ImpersonationLevel.Impersonate
            connection.Authentication = AuthenticationLevel.Call

            'Create the connection and connect
            Dim scope As New ManagementScope(path, connection)
            scope.Connect()

            'Set instance
            If String.IsNullOrEmpty(instance) Then
                instance = W3Svc
            End If

            'Create the ManagementObject
            Using classInstance As New ManagementObject(scope, New ManagementPath(String.Format(CultureInfo.CurrentCulture, "IIsWebService.Name='{0}'", instance)), Nothing)

                'Run the command
                Using parameters As ManagementBaseObject = classInstance.InvokeMethod(action.ToString, Nothing, Nothing)

                    If Not Int32.TryParse(parameters(NameOf(result)).ToString, result) Then
                        result = -1
                    End If

                End Using

            End Using

            Return result

        End Function

        ''' <summary>
        ''' Stops the IIS.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub StopIIS()
            If ApplicationHelper.IsProcessRunning("W3SVC") Then
                Dim service = ServiceController.GetServices().Where(Function(p) p.ServiceName = "W3SVC").FirstOrDefault

                If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Running) Then
                    service.Stop()
                End If
            End If
        End Sub

        ''' <summary>
        ''' Starts the IIS.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub StartIIS()

            Dim service = ServiceController.GetServices().Where(Function(p) p.ServiceName = "W3SVC").FirstOrDefault

            If (service IsNot Nothing AndAlso service.Status = ServiceControllerStatus.Stopped) Then
                service.Start()
            End If

        End Sub

    End Module
End Namespace