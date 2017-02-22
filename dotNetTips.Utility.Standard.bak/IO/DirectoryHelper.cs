// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-16-2017
//
// Last Modified By : David McCarter
// Last Modified On : 01-20-2017
// ***********************************************************************
// <copyright file="DirectoryHelper.cs" company="dotNetTips.com">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
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
        public static string AppApplicationDataFolder()
        {
            //TODO: BLOG POST
            var userPath = Environment.GetEnvironmentVariable(
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "LOCALAPPDATA" : "Home");

            var assy = System.Reflection.Assembly.GetEntryAssembly();
            var companyName = assy.GetCustomAttributes<AssemblyCompanyAttribute>().FirstOrDefault();
            var path = System.IO.Path.Combine(userPath,companyName.Company);
            
            return path;
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

            //TODO: BLOG POST
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

        /// <summary>
        /// delete directory as an asynchronous operation.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">directory</exception>
        public static async Task<bool> DeleteDirectoryAsync(DirectoryInfo directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

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
    }
}