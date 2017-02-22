// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-22-2017
//
// Last Modified By : David McCarter
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="DirectoryHelper.cs" company="dotNetTips.Utility.Standard">
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

/// <summary>
/// The IO namespace.
/// </summary>
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
                    lock (files) files.AddRange(foundFiles);
                }
            });

            return files.Distinct().AsEnumerable();
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
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            if (directory.Exists)
                await Task.Factory.StartNew(() =>
                {
                    directory.Delete(recursive: true);
                    return true;
                }).ConfigureAwait(true);
            return false;
        }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns>System.String.</returns>
        public static string GetFolderPath(SpecialFolder folder)
        {
            //TODO: FINISH - BLOG POST
            var folderPath = string.Empty;

            switch (folder)
            {
                case SpecialFolder.ApplicationData:
                    break;
                case SpecialFolder.Desktop:
                    break;
                case SpecialFolder.Programs:
                    break;
                case SpecialFolder.Personal:
                    folderPath = GetFolderPath(SpecialFolder.UserProfile);
                    break;
                case SpecialFolder.Favorites:
                    break;
                case SpecialFolder.Startup:
                    break;
                case SpecialFolder.Recent:
                    break;
                case SpecialFolder.SendTo:
                    break;
                case SpecialFolder.StartMenu:
                    break;
                case SpecialFolder.MyMusic:
                    break;
                case SpecialFolder.MyVideos:
                    break;
                case SpecialFolder.DesktopDirectory:
                    break;
                case SpecialFolder.MyComputer:
                    break;
                case SpecialFolder.NetworkShortcuts:
                    break;
                case SpecialFolder.Fonts:
                    break;
                case SpecialFolder.Templates:
                    break;
                case SpecialFolder.CommonStartMenu:
                    break;
                case SpecialFolder.CommonPrograms:
                    break;
                case SpecialFolder.CommonStartup:
                    break;
                case SpecialFolder.CommonDesktopDirectory:
                    break;
                case SpecialFolder.PrinterShortcuts:
                    break;
                case SpecialFolder.LocalApplicationData:
                    folderPath = Environment.GetEnvironmentVariable(
RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "LOCALAPPDATA" : "HOME");
                    break;
                case SpecialFolder.InternetCache:
                    break;
                case SpecialFolder.Cookies:
                    break;
                case SpecialFolder.History:
                    break;
                case SpecialFolder.CommonApplicationData:
                    break;
                case SpecialFolder.Windows:
                    break;
                case SpecialFolder.System:
                    break;
                case SpecialFolder.ProgramFiles:
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        folderPath = Environment.GetEnvironmentVariable("ProgramFiles");
                    else
                        folderPath = "/Applications";
                    break;
                case SpecialFolder.MyPictures:
                    break;
                case SpecialFolder.UserProfile:
                    folderPath = Environment.GetEnvironmentVariable(
RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "USERPROFILE" : "HOME");
                    break;
                case SpecialFolder.SystemX86:
                    break;
                case SpecialFolder.ProgramFilesX86:
                    break;
                case SpecialFolder.CommonProgramFiles:
                    break;
                case SpecialFolder.CommonProgramFilesX86:
                    break;
                case SpecialFolder.CommonTemplates:
                    break;
                case SpecialFolder.CommonDocuments:
                    break;
                case SpecialFolder.CommonAdminTools:
                    break;
                case SpecialFolder.AdminTools:
                    break;
                case SpecialFolder.CommonMusic:
                    break;
                case SpecialFolder.CommonPictures:
                    break;
                case SpecialFolder.CommonVideos:
                    break;
                case SpecialFolder.Resources:
                    break;
                case SpecialFolder.LocalizedResources:
                    break;
                case SpecialFolder.CommonOemLinks:
                    break;
                case SpecialFolder.CDBurning:
                    break;
                case SpecialFolder.UserDocuments:
                    folderPath = Path.Combine(GetFolderPath(SpecialFolder.UserProfile), "Documents");
                    break;
                case SpecialFolder.MicrosoftNet:
                    {
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        {
                            folderPath = Path.Combine(Environment.GetEnvironmentVariable("windir"), "Microsoft.NET");
                        }
                        break;
                    }
            }
            return folderPath;
        }
    }
}