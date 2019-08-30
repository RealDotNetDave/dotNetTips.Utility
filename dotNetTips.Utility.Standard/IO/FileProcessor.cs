﻿// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 08-06-2017
//
// Last Modified By : David McCarter
// Last Modified On : 07-30-2019
// ***********************************************************************
// <copyright file="FileProcessor.cs" company="dotNetTips.com - David McCarter">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using dotNetTips.Utility.Standard.Diagnostics;
using dotNetTips.Utility.Standard.OOP;
using dotNetTips.Utility.Standard.Properties;

namespace dotNetTips.Utility.Standard.IO
{
    /// <summary>
    /// Class Processor.
    /// </summary>
    public class FileProcessor
    {
        /// <summary>
        /// Occurs when processor processes a file or folder.
        /// </summary>
        public event EventHandler<FileProgressEventArgs> Processed;

        /// <summary>
        /// Handles the <see cref="E:Processed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="FileProgressEventArgs" /> instance containing the event data.</param>
        /// TODO Edit XML Comment Template for OnProcessed
        protected virtual void OnProcessed(FileProgressEventArgs e) => Processed?.Invoke(this, e);

        /// <summary>
        /// Copies files to new location. Will not throw exceptions.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <returns>System.Object.</returns>
        /// <remarks>Use the Processed event to find out if file copied succeeded or failed.</remarks>
        public int CopyFiles(IEnumerable<FileInfo> files, DirectoryInfo destinationFolder)
        {
            Encapsulation.TryValidateParam(files, nameof(files));
            Encapsulation.TryValidateParam<ArgumentNullException>(destinationFolder != null, nameof(destinationFolder));

            var successCount = 0;

            Thread.CurrentThread.Priority = ThreadPriority.Lowest;

            if (destinationFolder.Exists == false)
            {
                destinationFolder.Create();
            }

            foreach (var tempFile in files)
            {
                if (tempFile.Exists)
                {
                    try
                    {
                        var newFileName = new FileInfo(tempFile.FullName.Replace(tempFile.Directory.Root.FullName, destinationFolder.FullName));

                        if (newFileName.Directory.Exists == false)
                        {
                            newFileName.Directory.Create();
                        }

                        var psw = PerformanceStopwatch.StartNew();

                        tempFile.CopyTo(newFileName.FullName, true);

                        var perf = psw.StopReset();

                        successCount += 1;

                        this.OnProcessed(new FileProgressEventArgs
                        {
                            Name = tempFile.FullName,
                            Message = tempFile.Name,
                            ProgressState = FileProgressState.Copied,
                            Size = tempFile.Length,
                            SpeedInMilliseconds = perf.TotalMilliseconds
                        });
                    }
                    catch (Exception ex)
                    {
                        //Send error if an Exception happens.
                        this.OnProcessed(new FileProgressEventArgs
                        {
                            Name = tempFile.FullName,
                            ProgressState = FileProgressState.Error,
                            Size = tempFile.Length,
                            Message = ex.Message
                        });
                    }
                }
                else
                {
                    this.OnProcessed(new FileProgressEventArgs
                    {
                        Name = tempFile.FullName,
                        ProgressState = FileProgressState.Error,
                        Size = tempFile.Length,
                        Message = Resources.FileNotFound
                    });
                }
            }

            return successCount;
        }

        /// <summary>
        /// Deletes file list.
        /// </summary>
        /// <param name="files">The file list to delete.</param>
        /// <returns>System.Int32 with the number of files that were successfully deleted.</returns>
        /// <remarks>Use the <seealso cref="Processed">Processed</seealso> event to find out if file deletion succeeded or failed.</remarks>
        public int DeleteFiles(IEnumerable<FileInfo> files)
        {
            Encapsulation.TryValidateParam(files, nameof(files));

            var successCount = 0;

            foreach (var tempFile in files)
            {
                if (tempFile.Exists)
                {
                    try
                    {

                        var psw = PerformanceStopwatch.StartNew();

                        tempFile.Delete();

                        var perf = psw.StopReset();

                        successCount += 1;

                        this.OnProcessed(new FileProgressEventArgs
                        {
                            Name = tempFile.FullName,
                            Message = tempFile.Name,
                            ProgressState = FileProgressState.Deleted,
                            Size = tempFile.Length,
                            SpeedInMilliseconds = perf.TotalMilliseconds
                        });
                    }
                    catch (Exception ex) when (ex is IOException || ex is SecurityException || ex is UnauthorizedAccessException)
                    {
                        this.OnProcessed(new FileProgressEventArgs
                        {
                            Name = tempFile.FullName,
                            ProgressState = FileProgressState.Error,
                            Size = tempFile.Length,
                            Message = ex.Message
                        });
                    }
                }
                else
                {
                    this.OnProcessed(new FileProgressEventArgs
                    {
                        Name = tempFile.FullName,
                        ProgressState = FileProgressState.Error,
                        Size = tempFile.Length,
                        Message = Resources.FileNotFound
                    });
                }
            }

            return successCount;
        }

        /// <summary>
        /// Deletes the folders.
        /// </summary>
        /// <param name="folders">The folders.</param>
        /// <returns>System.Int32.</returns>
        /// TODO Edit XML Comment Template for DeleteFolders
        public int DeleteFolders(IEnumerable<DirectoryInfo> folders)
        {
            Encapsulation.TryValidateParam(folders, nameof(folders));

            var successCount = 0;

            foreach (var tempFolder in folders)
            {
                if (tempFolder.Exists)
                {
                    try
                    {
                        tempFolder.Delete(true);

                        successCount += 1;

                        this.OnProcessed(new FileProgressEventArgs
                        {
                            Name = tempFolder.FullName,
                            ProgressState = FileProgressState.Deleted
                        });
                    }
                    catch (Exception ex) when (ex is IOException || ex is SecurityException || ex is UnauthorizedAccessException || ex is DirectoryNotFoundException)
                    {
                        this.OnProcessed(new FileProgressEventArgs
                        {
                            Name = tempFolder.FullName,
                            ProgressState = FileProgressState.Error,
                            Message = ex.Message
                        });
                    }
                }
                else
                {
                    this.OnProcessed(new FileProgressEventArgs
                    {
                        Name = tempFolder.FullName,
                        ProgressState = FileProgressState.Error,
                        Message = Resources.FolderNotFound
                    });
                }
            }

            return successCount;
        }
    }
}
