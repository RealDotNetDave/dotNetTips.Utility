// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 08-06-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="Processor.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotNetTips.Utility.Standard.OOP;
using dotNetTips.Utility.Standard.Properties;

namespace dotNetTips.Utility.Standard.IO
{
    /// <summary>
    /// Class Processor.
    /// </summary>
    public class Processor
    {
        /// <summary>
        /// Occurs when processor processes a file or folder.
        /// </summary>
        public event EventHandler<ProgressEventArgs> Processed;

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

            var backUpFolderRoot = destinationFolder.FullName.Split(ControlChars.BackSlash).Last().Trim();

            foreach (var tempFile in files.AsParallel())
            {
                if (tempFile.Exists)
                {
                    try
                    {
                        var newFileName = tempFile.FullName.Replace(destinationFolder.FullName, backUpFolderRoot);

                        tempFile.CopyTo(newFileName, true);

                        successCount += 1;

                        OnProcessed(new ProgressEventArgs
                        {
                            Name = tempFile.FullName,
                            ProgressState = ProgressState.Copied,
                            Size = tempFile.Length
                        });
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        OnProcessed(new ProgressEventArgs
                        {
                            Name = tempFile.FullName,
                            ProgressState = ProgressState.Error,
                            Size = tempFile.Length,
                            Message = ex.Message
                        });
                    }

                }
                else
                {
                    OnProcessed(new ProgressEventArgs
                    {
                        Name = tempFile.FullName,
                        ProgressState = ProgressState.Error,
                        Size = tempFile.Length,
                        Message = Properties.Resources.FileNotFound
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

            foreach (var tempFile in files.AsParallel())
            {
                if (tempFile.Exists)
                {
                    try
                    {
                        tempFile.Delete();

                        successCount += 1;

                        OnProcessed(new ProgressEventArgs
                        {
                            Name = tempFile.FullName,
                            ProgressState = ProgressState.Deleted,
                            Size = tempFile.Length
                        });

                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        OnProcessed(new ProgressEventArgs
                        {
                            Name = tempFile.FullName,
                            ProgressState = ProgressState.Error,
                            Size = tempFile.Length,
                            Message = ex.Message
                        });
                    }

                }
                else
                {
                    OnProcessed(new ProgressEventArgs
                    {
                        Name = tempFile.FullName,
                        ProgressState = ProgressState.Error,
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
        public int DeleteFolders(IEnumerable<DirectoryInfo> folders)
        {
            Encapsulation.TryValidateParam(folders, nameof(folders));

            var successCount = 0;

            foreach (var tempFolder in folders.AsParallel())
            {
                if (tempFolder.Exists)
                {
                    try
                    {
                        tempFolder.Delete(true);

                        successCount += 1;

                        OnProcessed(new ProgressEventArgs
                        {
                            Name = tempFolder.FullName,
                            ProgressState = ProgressState.Deleted
                        });

                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        OnProcessed(new ProgressEventArgs
                        {
                            Name = tempFolder.FullName,
                            ProgressState = ProgressState.Error,
                            Message = ex.Message
                        });
                    }
                }
                else
                {
                    OnProcessed(new ProgressEventArgs
                    {
                        Name = tempFolder.FullName,
                        ProgressState = ProgressState.Error,
                        Message = Resources.FolderNotFound
                    });
                }
            }

            return successCount;

        }
        /// <summary>
        /// Handles the <see cref="E:Processed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ProgressEventArgs" /> instance containing the event data.</param>
        protected virtual void OnProcessed(ProgressEventArgs e)
        {
            Processed?.Invoke(this, e);
        }
    }
}
