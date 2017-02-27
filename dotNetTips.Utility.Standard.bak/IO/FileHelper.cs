namespace dotNetTips.Utility.Standard.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Extensions;

    /// <summary>
    /// Class FileHelper.
    /// </summary>
    public static class FileHelper
    {
        #region Public Events

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Deletes the files.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns>IEnumerable&lt;KeyValuePair&lt;System.String, System.String&gt;&gt;.</returns>
        public static IEnumerable<KeyValuePair<string, string>> DeleteFiles(this IEnumerable<string> files)
        {
            var errors = new SortedDictionary<string, string>();

            System.Threading.Tasks.Parallel.ForEach(files, (information) =>
            {
                try
                {
                    File.Delete(information);
                }
                catch (IOException fileIOException)
                {
                    errors.AddIfNotExists(new KeyValuePair<string, string>(information, fileIOException.Message));
                }
                catch (UnauthorizedAccessException notAuthorizedException)
                {
                    errors.AddIfNotExists(new KeyValuePair<string, string>(information, notAuthorizedException.Message));
                }
            });

            return errors.AsEnumerable();
        }

        #endregion Public Methods
    }
}