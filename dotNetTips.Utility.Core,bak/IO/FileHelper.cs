// *********************************************************************** Assembly :
// dotNetTips.Utility.Core Author : David McCarter Created : 05-31-2016
//
// Last Modified By : David McCarter Last Modified On : 07-30-2016 ***********************************************************************
// <copyright file="FileHelper.cs" company="McCarter Consulting">
//     Copyright (c) McCarter Consulting. All rights reserved.
// </copyright>
// <summary>
// </summary>
// ***********************************************************************
namespace dotNetTips.Utility.Core.IO
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

            foreach (var information in files.AsParallel())
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
            }
            return errors.AsEnumerable();
        }

        #endregion Public Methods
    }
}