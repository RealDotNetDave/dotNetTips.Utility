' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 12-31-2014
'
' Last Modified By : David McCarter
' Last Modified On : 08-02-2016
' ***********************************************************************
' <copyright file="ApplicationHelper.vb" company="dotNetTips.com">
'     '     '     dotNetTips.com. All rights reserved.
'
'
' </copyright>
' <summary></summary>
' *************************************************************************
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Diagnostics.Contracts
Imports System.Security.Principal
''' <summary>
''' Class ApplicationHelper.
''' </summary>
Public Module ApplicationHelper

    ''' <summary>
    ''' The temporary ASP files
    ''' </summary>
    Private Const TempAspFiles As String = "\Temporary ASP.NET Files\"

    ''' <summary>
    ''' Available cultures.
    ''' </summary>
    ''' <param name="binDirectory">The bin directory.</param>
    ''' <returns>IEnumerable(Of CultureInfo).</returns>
    Public Function AvailableCultures(binDirectory As String) As IEnumerable(Of CultureInfo)

        Dim cultures As IEnumerable(Of CultureInfo) = CultureInfo.GetCultures(CultureTypes.NeutralCultures).Where(Function(c) System.IO.Directory.Exists(System.IO.Path.Combine(binDirectory, c.TwoLetterISOLanguageName)))

        Return cultures
    End Function

    ''' <summary>
    ''' Gets the calling assembly name.
    ''' </summary>
    ''' <returns>Assembly name.</returns>
    Public Function CurrentAssemblyName() As String
        Return System.Reflection.Assembly.GetEntryAssembly.GetName.Name
    End Function

    ''' <summary>
    ''' Loads a list of the running assembly referenced assemblies.
    ''' </summary>
    ''' <returns>IEnumerable(Of System.String).</returns>
    Public Function CurrentAssemblyReferencedAssemblies() As IEnumerable(Of String)
        Dim referencedAssemblies As New List(Of String)

        For Each assembly As System.Reflection.AssemblyName In System.Reflection.Assembly.GetEntryAssembly.GetReferencedAssemblies
            referencedAssemblies.Add(assembly.ToString)
        Next

        Return referencedAssemblies.AsReadOnly().AsEnumerable()

    End Function

    ''' <summary>
    ''' Verifies the current user on the current thread is an administrator.
    ''' </summary>
    ''' <returns><see cref="Boolean">True</see> if current user is administrator.</returns>
    Public Function CurrentUserIsAdministrator() As Boolean
        Dim currentDomain As AppDomain = System.Threading.Thread.GetDomain()

        currentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal)
        Dim currentPrincipal As System.Security.Principal.WindowsPrincipal = CType(System.Threading.Thread.CurrentPrincipal, System.Security.Principal.WindowsPrincipal)

        Return currentPrincipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)

    End Function

    ''' <summary>
    ''' Checks to see if the current application is ASP.NET
    ''' </summary>
    ''' <returns>True if running ASP.NET</returns>
    Public Function IsAspNet() As Boolean
        Return If(Not String.IsNullOrEmpty(AppDomain.CurrentDomain.DynamicDirectory), AppDomain.CurrentDomain.DynamicDirectory.Contains(TempAspFiles), False)
    End Function

    ''' <summary>
    ''' Check to see if the current app is already running.
    ''' </summary>
    ''' <returns><c>true</c> if app is not running, <c>false</c> otherwise.</returns>
    Public Function IsProcessRunning() As Boolean

        Return If(Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1, True, False)

    End Function
    ''' <summary>
    ''' Determines whether [is application already running] [the specified process name].
    ''' </summary>
    ''' <param name="processName">Name of the process.</param>
    ''' <returns><c>true</c> if [is application already running] [the specified process name]; otherwise, <c>false</c>.</returns>
    Public Function IsProcessRunning(processName As String) As Boolean
        Contract.Requires(Of ArgumentNullException)(Not String.IsNullOrEmpty(processName), "processName is nothing or empty.")
        Return If(Process.GetProcessesByName(processName).Count() > 0, True, False)
    End Function

    ''' <summary>
    ''' Determines whether [is run as administrator].
    ''' </summary>
    ''' <returns><c>true</c> if [is run as administrator]; otherwise, <c>false</c>.</returns>
    Public Function IsRunAsAdministrator() As Boolean
        Dim wi = WindowsIdentity.GetCurrent()
        Dim wp = New WindowsPrincipal(wi)

        Return wp.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    ''' <summary>
    ''' Kills the current process.
    ''' </summary>
    Public Sub KillProcess()
        KillProcess(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location))
    End Sub

    ''' <summary>
    ''' Kills the process.
    ''' </summary>
    ''' <param name="processName">Name of the process.</param>
    Public Sub KillProcess(processName As String)
        Contract.Requires(Of ArgumentNullException)(Not String.IsNullOrEmpty(processName), "processName is nothing or empty.")
        Dim app = System.Diagnostics.Process.GetProcessesByName(processName).FirstOrDefault

        If app IsNot Nothing Then
            app.Kill()
            app.WaitForExit()
        End If
    End Sub

    ''' <summary>
    ''' Restarts an app as administrator.
    ''' </summary>
    Public Sub RunAsAdministrator()
        If Not IsRunAsAdministrator() Then

            Dim processInfo = New ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase) With {.UseShellExecute = True, .Verb = "runas"}

            Process.Start(processInfo)

            Process.GetCurrentProcess.Kill()
        End If
    End Sub
End Module