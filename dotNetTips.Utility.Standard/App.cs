// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 06-26-2017
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
        #region Private Fields

        /// <summary>
        /// The temporary ASP files
        /// </summary>
        private const string TempAspFiles = "\\Temporary ASP.NET Files\\";

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Gets the calling assembly name.
        /// </summary>
        /// <returns>Assembly name.</returns>
        public static string AssemblyName()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        }

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
            return false;
            //TODO: FIX
            //return !string.IsNullOrEmpty(AppDomain.CurrentDomain.DynamicDirectory) ? AppDomain.CurrentDomain.DynamicDirectory.Contains(TempAspFiles) : false;
        }

        /// <summary>
        /// Check to see if the current app is already running.
        /// </summary>
        /// <returns><c>true</c> if app is not running, <c>false</c> otherwise.</returns>
        public static bool IsRunning()
        {
            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1 ? true : false;
        }
        /// <summary>
        /// Determines whether [is application already running] [the specified process name].
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns><c>true</c> if [is application already running] [the specified process name]; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">processName - Process name is required.</exception>
        public static bool IsProcessRunning(string processName)
        {

            Encapsulation.TryValidateParam<ArgumentNullException>(String.IsNullOrEmpty(processName)==false, "Process name is required.");

            return Process.GetProcessesByName(processName).Count() > 0 ? true : false;
        }

        /// <summary>
        /// Determines whether [is run as administrator].
        /// </summary>
        /// <returns><c>true</c> if [is run as administrator]; otherwise, <c>false</c>.</returns>
        public static bool IsRunningAsAdmin()
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
        public static void Kill()
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
            Encapsulation.TryValidateParam<ArgumentNullException>(String.IsNullOrEmpty(processName), "Process name is required.");

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

            if (!IsRunningAsAdmin())
            {
                var processInfo = new ProcessStartInfo(Assembly.GetEntryAssembly().CodeBase)
                {
                    UseShellExecute = true,
                };

                Process.Start(processInfo);

                Process.GetCurrentProcess().Kill();
            }
        }

        /// <summary>
        /// Informations this instance.
        /// </summary>
        /// <returns>Info.</returns>
        public static Info Info()
        {
            var info = new Info();

            var assy = System.Reflection.Assembly.GetEntryAssembly();

            info.Company = assy.GetCustomAttributes<AssemblyCompanyAttribute>()
    .FirstOrDefault().Company;
            info.Configuration = assy.GetCustomAttributes<AssemblyConfigurationAttribute>()
     .FirstOrDefault().Configuration;
            info.Copyright = assy.GetCustomAttributes<AssemblyCopyrightAttribute>()
      .FirstOrDefault().Copyright;
       //     info.Culture = assy.GetCustomAttributes<AssemblyCultureAttribute>()
       //.FirstOrDefault().Culture;
            info.Description = assy.GetCustomAttributes<AssemblyDescriptionAttribute>()
      .FirstOrDefault().Description;
            info.FileVersion = assy.GetCustomAttributes<AssemblyFileVersionAttribute>()
      .FirstOrDefault().Version;
            info.AssemblyVersion = assy.GetCustomAttributes<AssemblyInformationalVersionAttribute>()
      .FirstOrDefault().InformationalVersion;
            info.AssemblyProduct = assy.GetCustomAttributes<AssemblyProductAttribute>()
       .FirstOrDefault().Product;
            info.AssemblyTitle = assy.GetCustomAttributes<AssemblyTitleAttribute>()
    .FirstOrDefault().Title;

            return info;
        }

        #endregion Public Methods
    }

    /// <summary>
    /// Class Info.
    /// </summary>
    public class Info
    {
        /// <summary>
        /// Gets the company.
        /// </summary>
        /// <value>The company.</value>
        public string Company { get; internal set; }
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public string Configuration { get; internal set; }
        /// <summary>
        /// Gets the copyright.
        /// </summary>
        /// <value>The copyright.</value>
        public string Copyright { get; internal set; }
        /// <summary>
        /// Gets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public string Culture { get; internal set; }
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; internal set; }
        /// <summary>
        /// Gets the file version.
        /// </summary>
        /// <value>The file version.</value>
        public string FileVersion { get; internal set; }
        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        /// <value>The assembly version.</value>
        public string AssemblyVersion { get; internal set; }
        /// <summary>
        /// Gets the assembly product.
        /// </summary>
        /// <value>The assembly product.</value>
        public string AssemblyProduct { get; internal set; }
        /// <summary>
        /// Gets the assembly title.
        /// </summary>
        /// <value>The assembly title.</value>
        public string AssemblyTitle { get; internal set; }
    }
}
