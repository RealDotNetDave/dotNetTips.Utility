// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 11-01-2017
// ***********************************************************************
// <copyright file="DirectoryHelper.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using dotNetTips.Utility.Standard.OOP;

namespace dotNetTips.Utility.Standard.IO
{
    /// <summary>
    /// Class DirectoryHelper.
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Applications the application data folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string AppDataFolder()
        {
            var userPath = Environment.GetEnvironmentVariable(
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "LOCALAPPDATA" : "Home");

            var companyName = Assembly.GetEntryAssembly().GetCustomAttributes<AssemblyCompanyAttribute>().FirstOrDefault().Company.Trim();

            var path = System.IO.Path.Combine(userPath, companyName);

            return path;
        }
        /// <summary>
        /// Delete directory as an asynchronous operation.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">directory</exception>
        /// <exception cref="ArgumentNullException">directory</exception>
        public static async Task<bool> DeleteDirectoryAsync(DirectoryInfo directory)
        {
            Encapsulation.TryValidateParam<ArgumentNullException>(directory != null);

            if (directory.Exists)
            {
                await Task.Factory.StartNew(() =>
                {
                    directory.Delete(recursive: true);
                    return true;
                }).ConfigureAwait(true);
            }

            return false;
        }
        /// <summary>
        /// Loads the files.
        /// </summary>
        /// <param name="directories">The directories.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>IEnumerable(Of FileInfo).</returns>
        public static IEnumerable<FileInfo> LoadFiles(IEnumerable<DirectoryInfo> directories, string searchPattern, SearchOption searchOption)
        {
            var files = new List<FileInfo>();

            Parallel.ForEach(directories, (directory) =>
            {
                if ((directory.Exists))
                {
                    var foundFiles = directory.EnumerateFiles(searchPattern, searchOption);
                    lock (files)
                    {
                        files.AddRange(foundFiles);
                    }
                }
            });

            return files.Distinct().AsEnumerable();
        }
    }
}