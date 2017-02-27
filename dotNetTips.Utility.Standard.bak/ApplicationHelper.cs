using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Diagnostics.Contracts;
using System.Security.Principal;

namespace dotNetTips.Utility.Standard
{
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

            foreach (System.Reflection.AssemblyName assembly in System.Reflection.Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                referencedAssemblies.Add(assembly.ToString());
            }

            return referencedAssemblies.AsReadOnly().AsEnumerable();

        }

        /// <summary>
        /// Verifies the current user on the current thread is an administrator.
        /// </summary>
        /// <returns><see cref="Boolean">True</see> if current user is administrator.</returns>
        public static bool CurrentUserIsAdministrator()
        {
            var currentDomain =  Thread.GetDomain();

            currentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);
            System.Security.Principal.WindowsPrincipal currentPrincipal = (System.Security.Principal.WindowsPrincipal)System.Threading.Thread.CurrentPrincipal;

            return currentPrincipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);

        }

        /// <summary>
        /// Checks to see if the current application is ASP.NET
        /// </summary>
        /// <returns>True if running ASP.NET</returns>
        public bool IsAspNet()
        {
            return !string.IsNullOrEmpty(AppDomain.CurrentDomain.DynamicDirectory) ? AppDomain.CurrentDomain.DynamicDirectory.Contains(TempAspFiles) : false;
        }

        /// <summary>
        /// Check to see if the current app is already running.
        /// </summary>
        /// <returns><c>true</c> if app is not running, <c>false</c> otherwise.</returns>
        public bool IsProcessRunning()
        {

            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1 ? true : false;

        }
        /// <summary>
        /// Determines whether [is application already running] [the specified process name].
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <returns><c>true</c> if [is application already running] [the specified process name]; otherwise, <c>false</c>.</returns>
        public bool IsProcessRunning(string processName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(processName), "processName is nothing or empty.");
            return Process.GetProcessesByName(processName).Count() > 0 ? true : false;
        }

        /// <summary>
        /// Determines whether [is run as administrator].
        /// </summary>
        /// <returns><c>true</c> if [is run as administrator]; otherwise, <c>false</c>.</returns>
        public bool IsRunAsAdministrator()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);

            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Kills the current process.
        /// </summary>
        public void KillProcess()
        {
            KillProcess(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));
        }

        /// <summary>
        /// Kills the process.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        public void KillProcess(string processName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(processName), "processName is nothing or empty.");
            object app = System.Diagnostics.Process.GetProcessesByName(processName).FirstOrDefault;

            if (app != null)
            {
                app.Kill();
                app.WaitForExit();
            }
        }

        /// <summary>
        /// Restarts an app as administrator.
        /// </summary>
        public void RunAsAdministrator()
        {

            if (!IsRunAsAdministrator())
            {
                var processInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase)
                {
                    UseShellExecute = true,
                    Verb = "runas"
                };

                Process.Start(processInfo);

                Process.GetCurrentProcess.Kill();
            }
        }
    }
}
