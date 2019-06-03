// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 02-14-2018
//
// Last Modified By : David McCarter
// Last Modified On : 05-23-2019
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
using System.Threading;
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
        /// The retries
        /// </summary>
        private const int Retries = 10;

        /// <summary>
        /// Applications the application data folder for Windows and Mac.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string AppDataFolder()
        {
            var userPath = Environment.GetEnvironmentVariable(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "LOCALAPPDATA" : "Home");

            var companyName = Assembly.GetEntryAssembly()
                .GetCustomAttributes<AssemblyCompanyAttribute>()
                .FirstOrDefault()
                .Company.Trim();

            var path = Path.Combine(userPath, companyName);

            return path;
        }

        /// <summary>
        /// Copies directory to a new location.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="destinationDirectory">The destination directory.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public static void CopyDirectory(string sourceDirectory, string destinationDirectory, bool overwrite = true)
        {
            Encapsulation.TryValidateParam(sourceDirectory, nameof(sourceDirectory));
            Encapsulation.TryValidateParam(destinationDirectory, nameof(destinationDirectory));

            var directory = new DirectoryInfo(sourceDirectory);

            var directiories = directory.GetDirectories();

            if (Directory.Exists(destinationDirectory) == false)
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            var files = directory.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];

                file.CopyTo(Path.Combine(destinationDirectory, file.Name), overwrite);
            }

            for (int i = 0; i < directiories.Length; i++)
            {
                var subDirectory = directiories[i];

                CopyDirectory(subDirectory.FullName, Path.Combine(destinationDirectory, subDirectory.Name), overwrite);
            }
        }

        /// <summary>
        /// Deletes the directory with retry.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void DeleteDirectory(string path)
        {
            for (var retryCount = 0; retryCount < Retries; retryCount++)
            {
                if (!Directory.Exists(path))
                {
                    return;
                }

                try
                {
                    // On some systems, directories/files created programmatically are created with attributes
                    // that prevent them from being deleted. Set those attributes to be normal
                    SetAttributesNormal(path);
                    Directory.Delete(path, true);

                    return;
                }
                catch (IOException) when (retryCount < Retries - 1)
                {
                }
                catch (UnauthorizedAccessException) when (retryCount < Retries - 1)
                {
                }

                // If something has a transient lock on the file waiting may resolve the issue
                Thread.Sleep((retryCount + 1) * 10);
            }
        }

        /// <summary>
        /// Delete directory, with retries, as an asynchronous operation.
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
                    DeleteDirectory(directory.FullName);
                    return true;
                }).ConfigureAwait(true);
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

            foreach (var directory in directories)
            {
                if ((directory.Exists))
                {
                    var foundFiles = directory.EnumerateFiles(searchPattern, searchOption);

                    files.AddRange(foundFiles);
                }
            }

            return files.Distinct().AsEnumerable();
        }
        /// <summary>
        /// Loads the one drive folders.
        /// </summary>
        /// <returns>IEnumerable&lt;OneDriveFolder&gt;.</returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        /// <exception cref="System.PlatformNotSupportedException"></exception>
        public static IEnumerable<OneDriveFolder> LoadOneDriveFolders()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == false)
            {
                throw new PlatformNotSupportedException();
            }

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
        /// Moves the directory.
        /// </summary>
        /// <param name="sourceDirectoryName">Name of the source dir.</param>
        /// <param name="destinationDirectoryName">Name of the dest dir.</param>
        public static void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
        {
            for (var retryCount = 0; retryCount < Retries; retryCount++)
            {
                if (!Directory.Exists(sourceDirectoryName) && Directory.Exists(destinationDirectoryName))
                {
                    return;
                }

                try
                {
                    Directory.Move(sourceDirectoryName, destinationDirectoryName);
                    return;
                }
                catch (IOException) when (retryCount < Retries - 1)
                {
                }
                catch (UnauthorizedAccessException) when (retryCount < Retries - 1)
                {
                }

                // If something has a transient lock on the file waiting may resolve the issue
                Thread.Sleep((retryCount + 1) * 10);
            }
        }

        /// <summary>Safe directory search.</summary>
        /// <param name="rootDirectory">The root directory.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">All or Top Directory Only</param>
        /// <returns>IEnumerable&lt;DirectoryInfo&gt;.</returns>
        public static IEnumerable<DirectoryInfo> SafeDirectorySearch(DirectoryInfo rootDirectory, string searchPattern = ControlChars.DoubleQuote, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Encapsulation.TryValidateParam(rootDirectory, nameof(rootDirectory));

            var folders = new List<DirectoryInfo>
            {
                rootDirectory
            };

            foreach (var topFolder in rootDirectory.GetDirectories(searchPattern, searchOption))
            {
                try
                {
                    folders.AddRange(SafeDirectorySearch(topFolder, searchPattern));
                }
                catch (UnauthorizedAccessException ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }

            return folders;
        }

        /// <summary>
        /// Sets the attributes normal for all files and directories in the path.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void SetAttributesNormal(string path)
        {
            Encapsulation.TryValidateParam(path, nameof(path));

            for (var directoryCount = 0; directoryCount < Directory.GetDirectories(path).Length; directoryCount++)
            {
                SetAttributesNormal(Directory.GetDirectories(path)[directoryCount]);
            }

            for (var fileCount = 0; fileCount < Directory.GetFiles(path).Length; fileCount++)
            {
                File.SetAttributes(Directory.GetFiles(path)[fileCount], System.IO.FileAttributes.Normal);
            }
        }
    }
}