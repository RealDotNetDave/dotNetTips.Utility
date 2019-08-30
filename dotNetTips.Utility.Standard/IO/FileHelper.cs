// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 02-11-2017
//
// Last Modified By : David McCarter
// Last Modified On : 06-03-2019
// ***********************************************************************
// <copyright file="FileHelper.cs" company="dotNetTips.com - David McCarter">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using dotNetTips.Utility.Standard.Extensions;
using dotNetTips.Utility.Standard.OOP;

namespace dotNetTips.Utility.Standard.IO
{
    /// <summary>
    /// Class FileHelper.
    /// </summary>
    /// TODO Edit XML Comment Template for FileHelper
    public static class FileHelper
    {
        /// <summary>
        /// The count for retries.
        /// </summary>
        private const int Retries = 10;

        /// <summary>
        /// Copies a file to a new directory as an asynchronous operation.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public static async Task<long> CopyFileAsync(FileInfo file, DirectoryInfo destinationFolder)
        {
            Encapsulation.TryValidateParam(file, nameof(file));
            Encapsulation.TryValidateParam(destinationFolder, nameof(destinationFolder));

            var newFileName = Path.Combine(destinationFolder.FullName, file.Name);

            using (var sourceStream = File.Open(file.FullName, FileMode.Open))
            {
                if (File.Exists(newFileName))
                {
                    File.Delete(newFileName);
                }

                using (var destinationStream = File.Create(newFileName))
                {
                    await sourceStream.CopyToAsync(destinationStream).ConfigureAwait(true);
                    await destinationStream.FlushAsync().ConfigureAwait(true);
                    destinationStream.Close();
                }
            }

            return file.Length;
        }


        /// <summary>
        /// Download file from web and unzips it as an asynchronous operation.
        /// </summary>
        /// <param name="remoteFileUrl">The remote file URL.</param>
        /// <param name="localExpandedDirPath">The local expanded dir path.</param>
        /// <returns>Task.</returns>
        public async static Task DownloadFileFromWebAndUnzipAsync(string remoteFileUrl, string localExpandedDirPath)
        {
            var tempFileNameBase = Guid.NewGuid().ToString();
            var tempDownloadPath = Path.Combine(Path.GetTempPath(), tempFileNameBase + Path.GetExtension(remoteFileUrl));

            DownloadFileFromWeb(remoteFileUrl, tempDownloadPath);

            await UnZipAsync(tempDownloadPath, localExpandedDirPath, true);
        }

        /// <summary>
        /// Deletes the files.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns>IEnumerable&lt;KeyValuePair&lt;System.String, System.String&gt;&gt;.</returns>
        /// TODO Edit XML Comment Template for DeleteFiles
        public static IEnumerable<KeyValuePair<string, string>> DeleteFiles(this IEnumerable<string> files)
        {
            var errors = new Dictionary<string, string>();

            Parallel.ForEach(files, (fileName) =>
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch (IOException fileIOException)
                    {
                        errors.AddIfNotExists(new KeyValuePair<string, string>(fileName, fileIOException.Message));
                    }
                    catch (UnauthorizedAccessException notAuthorizedException)
                    {
                        errors.AddIfNotExists(new KeyValuePair<string, string>(fileName, notAuthorizedException.Message));
                    }
                });

            return errors.AsEnumerable();
        }

        /// <summary>
        /// Downloads the file from the web.
        /// </summary>
        /// <param name="remoteFileUrl">The remote file URL.</param>
        /// <param name="localFilePath">The local file path.</param>
        public static void DownloadFileFromWeb(string remoteFileUrl, string localFilePath)
        {
            Encapsulation.TryValidateParam(remoteFileUrl, nameof(remoteFileUrl));
            Encapsulation.TryValidateParam(localFilePath, nameof(localFilePath));

            Directory.CreateDirectory(Path.GetDirectoryName(localFilePath));

            using (var client = new HttpClient())
            {
                using (var localStream = File.Create(localFilePath))
                {
                    using (var stream = client.GetStreamAsync(remoteFileUrl).Result)
                    {
                        stream.CopyTo(localStream);
                    }

                    localStream.Flush();
                }
            }
        }

        /// <summary>
        /// Moves the file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// TODO Edit XML Comment Template for MoveFile
        public static void MoveFile(string sourceFileName, string destinationFileName)
        {
            for (var retryCount = 0; retryCount < Retries; retryCount++)
            {
                if (!File.Exists(sourceFileName) && File.Exists(destinationFileName))
                {
                    return;
                }

                try
                {
                    File.Move(sourceFileName, destinationFileName);
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
        /// un g zip as an asynchronous operation.
        /// </summary>
        /// <param name="gzipPath">The gzip path.</param>
        /// <param name="expandedFilePath">The expanded file path.</param>
        /// <param name="deleteGZipFile">if set to <c>true</c> [delete g zip file].</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for UnGZipAsync
        public async static Task UnGZipAsync(string gzipPath, string expandedFilePath, bool deleteGZipFile)
        {
            Encapsulation.TryValidateParam(gzipPath, nameof(gzipPath));
            Encapsulation.TryValidateParam(expandedFilePath, nameof(expandedFilePath));
            Encapsulation.TryValidateParam<ArgumentInvalidException>(File.Exists(gzipPath), nameof(gzipPath), "GZip file not found.");

            await UnGZipAsync(gzipPath, expandedFilePath);

            if (deleteGZipFile)
            {
                File.Delete(gzipPath);
            }
        }

        /// <summary>
        /// un g zip as an asynchronous operation.
        /// </summary>
        /// <param name="gzipPath">The gzip path.</param>
        /// <param name="expandedFilePath">The expanded file path.</param>
        /// <returns>Task.</returns>
        /// TODO Edit XML Comment Template for UnGZipAsync
        public async static Task UnGZipAsync(string gzipPath, string expandedFilePath)
        {
            using (var gzipStream = File.OpenRead(gzipPath))
            {
                using (var expandedStream = new GZipStream(gzipStream, CompressionMode.Decompress))
                {
                    using (var targetFileStream = File.OpenWrite(expandedFilePath))
                    {
                        await expandedStream.CopyToAsync(targetFileStream);
                    }
                }
            }
        }

        /// <summary>
        /// Unzips a file as an asynchronous operation.
        /// </summary>
        /// <param name="zipPath">The path to the zip file.</param>
        /// <param name="expandToDirectory">The directory path where files will be unzipped.</param>
        /// <returns>Task.</returns>
        public static async Task UnZipAsync(string zipPath, string expandToDirectory)
        {
            Encapsulation.TryValidateParam(zipPath, nameof(zipPath));
            Encapsulation.TryValidateParam(expandToDirectory, nameof(expandToDirectory));
            Encapsulation.TryValidateParam<ArgumentInvalidException>(File.Exists(zipPath), nameof(zipPath), "Zip file not found.");

            await UnWinZipAsync(zipPath, expandToDirectory);
        }

        /// <summary>
        /// Unzips a file as an asynchronous operation.
        /// </summary>
        /// <param name="zipPath">The path to the zip file.</param>
        /// <param name="expandToDirectory">The directory path where files will be unzipped.</param>
        /// <param name="deleteZipFile">if set to <c>true</c> [deletes zip file].</param>
        /// <returns>Task.</returns>
        public static async Task UnZipAsync(string zipPath, string expandToDirectory, bool deleteZipFile)
        {
            await UnZipAsync(zipPath, expandToDirectory);

            if (deleteZipFile)
            {
                File.Delete(zipPath);
            }
        }

        /// <summary>
        /// Un-zips a file as an asynchronous operation.
        /// </summary>
        /// <param name="zipPath">The zip path.</param>
        /// <param name="expandedDirectoryPath">The expanded directory path.</param>
        /// <returns>Task.</returns>
        private static async Task UnWinZipAsync(string zipPath, string expandedDirectoryPath)
        {
            using (var zipFileStream = File.OpenRead(zipPath))
            {
                using (var zipArchiveStream = new ZipArchive(zipFileStream))
                {
                    for (var zipArchiveCount = 0; zipArchiveCount < zipArchiveStream.Entries.Count; zipArchiveCount++)
                    {
                        var zipArchiveEntry = zipArchiveStream.Entries[zipArchiveCount];

                        if (zipArchiveEntry.CompressedLength == 0)
                        {
                            continue;
                        }

                        var extractedFilePath = Path.Combine(expandedDirectoryPath, zipArchiveEntry.FullName);

                        Directory.CreateDirectory(Path.GetDirectoryName(extractedFilePath));

                        using (var zipStream = zipArchiveEntry.Open())
                        {
                            using (var extractedFileStream = File.OpenWrite(extractedFilePath))
                            {
                                await zipStream.CopyToAsync(extractedFileStream);
                            }
                        }
                    }
                }
            }
        }
    }
}