// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 02-14-2018
//
// Last Modified By : David McCarter
// Last Modified On : 07-17-2018
// ***********************************************************************
// <copyright file="DirectoryHelper.cs" company="dotNetTips.com - David McCarter">
//     McCarter Consulting (David McCarter)
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
using dotNetTips.Utility.Standard.Extensions;
using dotNetTips.Utility.Standard.OOP;
using dotNetTips.Utility.Standard.Win32;

namespace dotNetTips.Utility.Standard.IO
{
    /// <summary>
    /// Class DirectoryHelper.
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// Loads the one drive folders.
        /// </summary>
        /// <returns>IEnumerable&lt;OneDriveFolder&gt;.</returns>
        public static IEnumerable<OneDriveFolder> LoadOneDriveFolders()
        {
            const string DisplayNameKey = "DisplayName";
            const string UserFolderKey = "UserFolder";
            const string AccountsKey = "Accounts";
            const string EmailKey = "UserEmail";

            var folders = new List<OneDriveFolder>();

            var oneDriveKey = RegistryHelper.GetCurrentUserRegistryKey(RegistryHelper.KeyCurrentUserOneDrive);

            if (oneDriveKey.IsNotNull())
            {
                //Get Accounts
                var accountKey = oneDriveKey.GetSubKey(AccountsKey);

                if (accountKey.IsNotNull() && accountKey.SubKeyCount > 0)
                {
                    foreach (var subKeyName in accountKey.GetSubKeyNames())
                    {
                        var key = accountKey.GetSubKey(subKeyName);

                        var folder = new OneDriveFolder();
                        var directoryValue = key.GetValue<string>(UserFolderKey);

                        if (directoryValue.HasValue())
                        {
                            folder.DirectoryInfo = new DirectoryInfo(directoryValue);

                            var emailValue = key.GetValue<string>(EmailKey);

                            if (emailValue.HasValue())
                            {
                                folder.UserEmail = emailValue;
                            }

                            //Figure out account type                           
                            var name = key.GetValue<string>(DisplayNameKey);

                            if (name.HasValue())
                            {
                                folder.AccountType = OneDriveAccountType.Business;
                                folder.AccountName = name;
                            }
                            else
                            {
                                folder.AccountName = key.GetValue<string>(string.Empty);
                            }

                            if (folder.AccountName.HasValue() && folder.DirectoryInfo.IsNotNull())
                            {
                                folders.Add(folder);
                            }
                        }
                    }
                }
            }

            return folders.AsEnumerable();
        }

        /// <summary>
        /// Applications the application data folder.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string AppDataFolder()
        {
            var userPath = Environment.GetEnvironmentVariable(
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "LOCALAPPDATA" : "Home");

            var companyName = Assembly.GetEntryAssembly()
                .GetCustomAttributes<AssemblyCompanyAttribute>()
                .FirstOrDefault()
                .Company.Trim();

            var path = Path.Combine(userPath, companyName);

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
                })
                    .ConfigureAwait(true);
            }

            return false;
        }

        /// <summary>
        /// Loads the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>IEnumerable&lt;FileInfo&gt;.</returns>
        public static IEnumerable<FileInfo> LoadFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return LoadFiles(new List<DirectoryInfo> { new DirectoryInfo(path) }, searchPattern, searchOption);
        }

        /// <summary>
        /// Loads the files.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>IEnumerable&lt;FileInfo&gt;.</returns>
        public static IEnumerable<FileInfo> LoadFiles(DirectoryInfo directory, string searchPattern, SearchOption searchOption)
        {
            return LoadFiles(new List<DirectoryInfo> { directory }, searchPattern, searchOption);
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

        /// <summary>
        /// Safes the directory search.
        /// </summary>
        /// <param name="rootDirectory">The root directory.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns>IEnumerable&lt;DirectoryInfo&gt;.</returns>
        public static IEnumerable<DirectoryInfo> SafeDirectorySearch(DirectoryInfo rootDirectory,
                                                                     string searchPattern = ControlChars.DoubleQuote)
        {
            Encapsulation.TryValidateParam(rootDirectory, nameof(rootDirectory));

            var folders = new List<DirectoryInfo>();
            folders.Add(rootDirectory);

            foreach (var topFolder in rootDirectory.GetDirectories(searchPattern, SearchOption.TopDirectoryOnly))
            {
                try
                {
                    foreach (var folder in SafeDirectorySearch(topFolder, searchPattern))
                    {
                        folders.Add(folder);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }

            return folders;
        }
    }
}