// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="App.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using dotNetTips.Utility.Standard.OOP;

namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Class ApplicationHelper.
    /// </summary>
    public static class App
    {
        #region Public Methods

        /// <summary>
        /// Gets the assembly information.
        /// </summary>
        /// <value>The assembly information.</value>
        public static Info AssemblyInfo => Info();

        /// <summary>
        /// Loads a list of the running assembly referenced assemblies.
        /// </summary>
        /// <returns>IEnumerable(Of System.String).</returns>
        public static IEnumerable<string> ReferencedAssemblies()
        {
            var referencedAssemblies = new List<string>();

            foreach (var assembly in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                referencedAssemblies.Add(assembly.ToString());
            }

            return referencedAssemblies.AsReadOnly().AsEnumerable();
        }

        /// <summary>
        /// Checks to see if the current application is ASP.NET
        /// </summary>
        /// <returns>True if running ASP.NET</returns>
        public static bool IsRunningFromAspNet()
        {
            return !string.IsNullOrEmpty(AppDomain.CurrentDomain.DynamicDirectory) ? AppDomain.CurrentDomain.DynamicDirectory.Contains(TempAspFiles) : false;
        }

        /// <summary>
        /// The temporary ASP files location
        /// </summary>
        private const string TempAspFiles = "\\Temporary ASP.NET Files\\";

        /// <summary>
        /// Check to see if the current app is already running.
        /// </summary>
        /// <returns><c>true</c> if app is not running, <c>false</c> otherwise.</returns>
        public static bool IsRunning() => Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1 ? true : false;

        /// <summary>
        /// Determines whether [is application already running] [the specified process name].
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns><c>true</c> if [is application already running] [the specified process name]; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">processName - Process name is required.</exception>
        public static bool IsProcessRunning(string processName)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(string.IsNullOrEmpty(processName) == false, "Process name is required.");

            return Process.GetProcessesByName(processName).Count() > 0 ? true : false;
        }

        /// <summary>
        /// Kills the current process.
        /// </summary>
        public static void Kill() => KillProcess(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));

        /// <summary>
        /// Kills the process.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <exception cref="ArgumentNullException">processName - Process name is nothing or empty.</exception>
        public static void KillProcess(string processName)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(string.IsNullOrEmpty(processName), "Process name is required.");

            var app = System.Diagnostics.Process.GetProcessesByName(processName).FirstOrDefault();

            if (app != null)
            {
                app.Kill();
                app.WaitForExit();
            }
        }

        /// <summary>
        /// Restarts an app as administrator.
        /// </summary>
        public static void RunAsAdministrator()
        {
            var processInfo = new ProcessStartInfo(Assembly.GetEntryAssembly().CodeBase)
            {
                UseShellExecute = true,
            };

            Process.Start(processInfo);

            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// Informations this instance.
        /// </summary>
        /// <returns>Info.</returns>
        public static Info Info()
        {
            var info = new Info();

            var assembly = System.Reflection.Assembly.GetEntryAssembly();

            info.Company = assembly.GetCustomAttributes<AssemblyCompanyAttribute>()
    .FirstOrDefault().Company;
            info.Configuration = assembly.GetCustomAttributes<AssemblyConfigurationAttribute>()
     .FirstOrDefault().Configuration;
            info.Copyright = assembly.GetCustomAttributes<AssemblyCopyrightAttribute>()
      .FirstOrDefault().Copyright;
            info.Description = assembly.GetCustomAttributes<AssemblyDescriptionAttribute>()
           .FirstOrDefault().Description;
            info.FileVersion = assembly.GetCustomAttributes<AssemblyFileVersionAttribute>()
      .FirstOrDefault().Version;
            info.AssemblyVersion = assembly.GetCustomAttributes<AssemblyInformationalVersionAttribute>()
      .FirstOrDefault().InformationalVersion;
            info.AssemblyProduct = assembly.GetCustomAttributes<AssemblyProductAttribute>()
       .FirstOrDefault().Product;
            info.AssemblyTitle = assembly.GetCustomAttributes<AssemblyTitleAttribute>()
    .FirstOrDefault().Title;

            return info;
        }

        #endregion Public Methods
    }
}
