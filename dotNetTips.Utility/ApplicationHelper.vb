' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : David McCarter
' Created          : 08-16-2016
'
' Last Modified By : David McCarter
' Last Modified On : 07-20-2017
' ***********************************************************************
' <copyright file="ApplicationHelper.vb" company="McCarter Consulting - David McCarter">
'     David McCarter - dotNetTips.com © 2017
' </copyright>
' <summary></summary>
' ***********************************************************************

Imports dotNetTips.Utility.My
Imports dotNetTips.Utility.Portable.OOP
Imports Microsoft.Win32
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Security.Principal
Imports System.Threading

''' <summary>
''' Class ApplicationHelper.
''' </summary>
Public Module ApplicationHelper

    ''' <summary>
    ''' The temporary ASP files
    ''' </summary>
    Private Const TempAspFiles As String = "\Temporary ASP.NET Files\"


    ''' <summary>
    ''' Creates the key if not exists.
    ''' </summary>
    ''' <param name="keyName">Name of the key.</param>
    ''' <returns>RegistryKey.</returns>
    Private Function CreateKeyIfNotExists(keyName As String) As RegistryKey
        Return Registry.Users.CreateSubKey(keyName, True)
    End Function

    ''' <summary>
    ''' Available cultures.
    ''' </summary>
    ''' <param name="binDirectory">The bin directory.</param>
    ''' <returns>IEnumerable(Of CultureInfo).</returns>
    Public Function AvailableCultures(binDirectory As String) As IEnumerable(Of CultureInfo)

        Dim cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures).Where(Function(c) Directory.Exists(Path.Combine(binDirectory, c.TwoLetterISOLanguageName)))

        Return cultures
    End Function

    ''' <summary>
    ''' Gets the calling assembly name.
    ''' </summary>
    ''' <returns>Assembly name.</returns>
    Public Function CurrentAssemblyName() As String
        Return Assembly.GetEntryAssembly.GetName.Name
    End Function

    ''' <summary>
    ''' Loads a list of the running assembly referenced assemblies.
    ''' </summary>
    ''' <returns>IEnumerable(Of System.String).</returns>
    Public Function CurrentAssemblyReferencedAssemblies() As IEnumerable(Of String)
        Dim referencedAssemblies As New List(Of String)

        For Each assembly As AssemblyName In Reflection.Assembly.GetEntryAssembly.GetReferencedAssemblies
            referencedAssemblies.Add(assembly.ToString)
        Next

        Return referencedAssemblies.AsReadOnly().AsEnumerable()

    End Function

    ''' <summary>
    ''' Verifies the current user on the current thread is an administrator.
    ''' </summary>
    ''' <returns><see cref="Boolean">True</see> if current user is administrator.</returns>
    Public Function CurrentUserIsAdministrator() As Boolean
        Dim currentDomain = Thread.GetDomain()

        currentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal)
        Dim currentPrincipal = CType(Thread.CurrentPrincipal, WindowsPrincipal)

        Return currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator)

    End Function

    ''' <summary>
    ''' Gets the user reg key.
    ''' </summary>
    ''' <param name="keyName">Name of the key.</param>
    ''' <param name="valueName">Name of the value.</param>
    ''' <returns>System.String.</returns>
    Public Function GetUserRegKey(keyName As String, valueName As String) As String

        Using regKey = CreateKeyIfNotExists(keyName)

            Return regKey.GetValue(valueName, String.Empty)

        End Using

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
        Encapsulation.TryValidateParam(Of ArgumentNullException)(Not String.IsNullOrEmpty(processName), "processName is nothing or empty.")

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
    Public Sub KillCurrentProcess()
        KillProcess(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location))
    End Sub

    ''' <summary>
    ''' Kills the process.
    ''' </summary>
    ''' <param name="processName">Name of the process.</param>
    Public Sub KillProcess(processName As String)
        Encapsulation.TryValidateParam(Of ArgumentNullException)(Not String.IsNullOrEmpty(processName), "processName is nothing or empty.")
        Dim app = Process.GetProcessesByName(processName).FirstOrDefault

        If app IsNot Nothing Then
            app.Kill()
            app.WaitForExit()
        End If
    End Sub

    ''' <summary>
    ''' Plays the sound.
    ''' </summary>
    ''' <param name="location">The location.</param>
    Public Sub PlaySound(location As String)
        Computer.Audio.Play(location)
    End Sub

    ''' <summary>
    ''' Plays the sound.
    ''' </summary>
    ''' <param name="data">The data.</param>
    ''' <param name="playMode">The play mode.</param>
    Public Sub PlaySound(data As Byte(), playMode As AudioPlayMode)
        Computer.Audio.Play(data, playMode)
    End Sub

    ''' <summary>
    ''' Plays the sound.
    ''' </summary>
    ''' <param name="stream">The stream.</param>
    ''' <param name="playMode">The play mode.</param>
    Public Sub PlaySound(stream As Stream, playMode As AudioPlayMode)
        Computer.Audio.Play(stream, playMode)
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

    ''' <summary>
    ''' Updates the user reg key.
    ''' </summary>
    ''' <param name="keyName">Name of the key.</param>
    ''' <param name="valueName">Name of the value.</param>
    ''' <param name="value">The value.</param>
    Public Sub UpdateUserRegKey(keyName As String, valueName As String, value As String)

        Using regKey = CreateKeyIfNotExists(keyName)

            regKey.SetValue(valueName, value, RegistryValueKind.String)

            regKey.Flush()

        End Using

    End Sub
End Module