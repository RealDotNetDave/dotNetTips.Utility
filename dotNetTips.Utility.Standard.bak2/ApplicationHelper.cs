// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-22-2017
//
// Last Modified By : David McCarter
// Last Modified On : 01-21-2017
// ***********************************************************************
// <copyright file="ApplicationHelper.cs" company="dotNetTips.Utility.Standard">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

/// <summary>
/// The Standard namespace.
/// </summary>
namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Class ApplicationHelper.
    /// </summary>
    public static class ApplicationHelper
    {
        /// <summary>
        /// The temporary ASP files
        /// </summary>
        private const string TempAspFiles = "\\Temporary ASP.NET Files\\";

        /// <summary>
        /// Gets the calling assembly name.
        /// </summary>
        /// <returns>Assembly name.</returns>
        public static string CurrentAssemblyName()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        }

        /// <summary>
        /// Loads a list of the running assembly referenced assemblies.
        /// </summary>
        /// <returns>IEnumerable(Of System.String).</returns>
        public static IEnumerable<string> CurrentAssemblyReferencedAssemblies()
        {
            List<string> referencedAssemblies = new List<string>();

            foreach (System.Reflection.AssemblyName assembly in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                referencedAssemblies.Add(assembly.ToString());
            }

            return referencedAssemblies.AsReadOnly().AsEnumerable();
        }

        /// <summary>
        /// Checks to see if the current application is ASP.NET
        /// </summary>
        /// <returns>True if running ASP.NET</returns>
        public static bool IsAspNet()
        {
            return false;
            //TODO: FIX
            //return !string.IsNullOrEmpty(AppDomain.CurrentDomain.DynamicDirectory) ? AppDomain.CurrentDomain.DynamicDirectory.Contains(TempAspFiles) : false;
        }

        /// <summary>
        /// Check to see if the current app is already running.
        /// </summary>
        /// <returns><c>true</c> if app is not running, <c>false</c> otherwise.</returns>
        public static bool IsProcessRunning()
        {
            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1 ? true : false;
        }
        /// <summary>
        /// Determines whether [is application already running] [the specified process name].
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns><c>true</c> if [is application already running] [the specified process name]; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">processName - Process name is requried.</exception>
        public static bool IsProcessRunning(string processName)
        {
            if(String.IsNullOrEmpty(processName))
            {
                throw new ArgumentNullException(nameof(processName), "Process name is requried.");
            }

            return Process.GetProcessesByName(processName).Count() > 0 ? true : false;
        }

        /// <summary>
        /// Determines whether [is run as administrator].
        /// </summary>
        /// <returns><c>true</c> if [is run as administrator]; otherwise, <c>false</c>.</returns>
        public static bool IsRunAsAdministrator()
        {
            //TODO: FIX
            //var wi = WindowsIdentity.GetCurrent();
            //var wp = new WindowsPrincipal(wi);

            //return wp.IsInRole(WindowsBuiltInRole.Administrator);

            return true;
        }

        /// <summary>
        /// Kills the current process.
        /// </summary>
        public static void KillProcess()
        {
            KillProcess(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));
        }

        /// <summary>
        /// Kills the process.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <exception cref="ArgumentNullException">processName - Process name is nothing or empty.</exception>
        public static void KillProcess(string processName)
        {
            if (string.IsNullOrEmpty(processName))
            { 
              throw new ArgumentNullException(nameof(processName), "Process name is nothing or empty.");
            }
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

            if (!IsRunAsAdministrator())
            {
                var processInfo = new ProcessStartInfo(Assembly.GetEntryAssembly().CodeBase)
                {
                    UseShellExecute = true,
                };

                Process.Start(processInfo);

                Process.GetCurrentProcess().Kill();
            }
        }
    }
}
