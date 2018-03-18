// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 12-08-2017
// ***********************************************************************
// <copyright file="App.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using dotNetTips.Utility.Standard.OOP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Class ApplicationHelper.
    /// </summary>
    public static class App
    {
        private const int ProcessorCountRefreshInterval = 30000;

        private static volatile int _processorCount;
        private static volatile int _lastProcessorCountRefreshTicks;

        /// <summary>
        /// Gets the processor count.
        /// </summary>
        /// <value>The processor count.</value>
        public static int ProcessorCount
        {
            get
            {
                var now = Environment.TickCount;

                if (_processorCount == 0 || now - _lastProcessorCountRefreshTicks >= ProcessorCountRefreshInterval)
                {
                    _processorCount = Environment.ProcessorCount;
                    _lastProcessorCountRefreshTicks = now;
                }

                return _processorCount;
            }
        }

        #region Public Methods

        /// <summary>
        /// Gets the assembly information.
        /// </summary>
        /// <value>The assembly information.</value>
        public static AppInfo AssemblyInfo => Info();

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

            return Process.GetProcessesByName(processName).Any() ? true : false;
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

        private static AppInfo appInfo;

        /// <summary>
        /// App information.
        /// </summary>
        /// <returns>Info.</returns>
        private static AppInfo Info()
        {
            if (appInfo == null)
            {
                appInfo = new AppInfo();

                var assembly = Assembly.GetEntryAssembly();

                appInfo.Company = assembly.GetCustomAttributes<AssemblyCompanyAttribute>().FirstOrDefault().Company;
                appInfo.Configuration = assembly.GetCustomAttributes<AssemblyConfigurationAttribute>().FirstOrDefault().Configuration;
                appInfo.Copyright = assembly.GetCustomAttributes<AssemblyCopyrightAttribute>().FirstOrDefault().Copyright;
                appInfo.Description = assembly.GetCustomAttributes<AssemblyDescriptionAttribute>().FirstOrDefault().Description;
                appInfo.FileVersion = assembly.GetCustomAttributes<AssemblyFileVersionAttribute>().FirstOrDefault().Version;
                //  appInfo.Version = assembly.GetCustomAttributes<AssemblyInformationalVersionAttribute>()?.FirstOrDefault().InformationalVersion;
                appInfo.Product = assembly.GetCustomAttributes<AssemblyProductAttribute>().FirstOrDefault().Product;
                appInfo.Title = assembly.GetCustomAttributes<AssemblyTitleAttribute>().FirstOrDefault().Title;
            }

            return appInfo;
        }

        #endregion Public Methods
    }
}
