' ***********************************************************************
' Assembly         : dotNetTips.Utility
' Author           : davidmccarter
' Created          : 12-31-2014
'
' Last Modified By : davidmccarter
' Last Modified On : 04-27-2015
' ***********************************************************************
' <copyright file="ApplicationHelper.vb" company="dotNetTips.com">
'     dotNetTips.com. All rights reserved.
' </copyright>
' <summary></summary>
' ***********************************************************************
Imports System.Collections.Generic
Imports System.Reflection
''' <summary>
''' Application Helper Class
''' </summary>
''' <remarks></remarks>
Public Module ApplicationHelper

    Private Const TempAspFiles As String = "\Temporary ASP.NET Files\"

    ''' <summary>
    ''' Checks to see if the current application is ASP.NET
    ''' </summary>
    ''' <returns>True if running ASP.NET</returns>
    ''' <remarks></remarks>
    Public Function IsAspNet() As Boolean
        Return If(Not String.IsNullOrEmpty(System.AppDomain.CurrentDomain.DynamicDirectory), System.AppDomain.CurrentDomain.DynamicDirectory.Contains(TempAspFiles), False)
    End Function

    ''' <summary>
    ''' Gets the calling assembly name.
    ''' </summary>
    ''' <returns>Assembly name.</returns>
    ''' <remarks></remarks>
    Public Function CurrentAssemblyName() As String
        Return System.Reflection.Assembly.GetEntryAssembly.GetName.Name
    End Function

    ''' <summary>
    ''' Loads a list of the running assembly referenced assemblies.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' <remarks></remarks>
    Public Function CurrentUserIsAdministrator() As Boolean
        Dim currentDomain As AppDomain = System.Threading.Thread.GetDomain()

        currentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal)
        Dim currentPrincipal As System.Security.Principal.WindowsPrincipal = CType(System.Threading.Thread.CurrentPrincipal, System.Security.Principal.WindowsPrincipal)

        Return currentPrincipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)

    End Function

    ''' <summary>
    ''' Availables the cultures.
    ''' </summary>
    ''' <param name="binDirectory">The bin directory.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AvailableCultures(binDirectory As String) As IEnumerable(Of CultureInfo)
        Dim neutralCulture = Assembly.GetExecutingAssembly().GetName.CultureInfo

        Dim cultures As IEnumerable(Of CultureInfo) = CultureInfo.GetCultures(CultureTypes.NeutralCultures).Where(Function(c) System.IO.Directory.Exists(System.IO.Path.Combine(binDirectory, c.TwoLetterISOLanguageName)))

        Return cultures
    End Function

End Module