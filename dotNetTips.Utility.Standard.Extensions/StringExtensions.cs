// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Extensions
// Author           : David McCarter
// Created          : 09-15-2017
//
// Last Modified By : David McCarter
// Last Modified On : 11-28-2017
// ***********************************************************************
// <copyright file="StringExtensions.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace dotNetTips.Utility.Standard.Extensions
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Determines whether the specified the string contains any.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <param name="characters">The characters.</param>
        /// <returns><c>true</c> if the specified characters contains any; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">input - List cannot be null.
        /// or
        /// characters - Characters cannot be null or 0 length.
        /// or
        /// Null character.</exception>
        /// <exception cref="System.ArgumentNullException">Null character.</exception>
        public static bool ContainsAny(this string input, params string[] characters)
        {
            return characters.Any(character => input.Contains(character));
        }

        /// <summary>
        /// Computes the sha256 hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
        public static string ComputeSha256Hash(this string data)
        {
            // Create a SHA256   
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                // Convert byte array to a string   
                var builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }


        /// <summary>
        /// Defaults if null.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        public static string DefaultIfNull(this string s) => s ?? string.Empty;

        /// <summary>
        /// Defaults if null.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string DefaultIfNull(this string s, string defaultValue) => s ?? (defaultValue ?? string.Empty);

        /// <summary>
        /// Defaults if null or empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string DefaultIfNullOrEmpty(this string value, string defaultValue) => string.IsNullOrEmpty(value) ? value : defaultValue;

        /// <summary>
        /// Formats the size of the file.
        /// </summary>
        /// <param name="fileSize">Size of the file.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">fileSize - File size is invalid.</exception>
        public static string FormatFileSize(this long fileSize)
        {
            long size = 0;

            while (fileSize > 1024 && size < 4)
            {
                fileSize = Convert.ToInt64(fileSize / 1024);
                size += 1;
            }

            return $"{fileSize} {((new string[] { Properties.Resources.Bytes, Properties.Resources.KB, Properties.Resources.MB, Properties.Resources.GB })[Convert.ToInt32(size)])}";
        }

        /// <summary>
        /// Concatenates the specified first message with passed in string[].
        /// </summary>
        /// <param name="firstMessage">The first message.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="addLineFeed">The add line feed. If set to true, delimiter will not be used.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>System.String.</returns>
        public static string Concat(this string firstMessage, string delimiter, bool addLineFeed, params string[] args)
        {
            if (string.IsNullOrEmpty(delimiter))
            {
                delimiter = string.Empty;
            }

            var sb = new StringBuilder(firstMessage);

            if (args != null && args.Any())
            {
                foreach (var value in args)
                {
                    if (addLineFeed)
                    {
                        sb.AppendLine(value);
                    }
                    else
                    {
                        sb.Append(string.Concat(value, delimiter));
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Indents the specified length.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="length">The length.</param>
        /// <param name="indentationCharacter">The indentation character.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">length - Length must be greater than 0.</exception>
        public static string Indent(this string str, int length, char indentationCharacter)
        {
            if (length == 0)
            {
                throw new ArgumentNullException(nameof(length), Properties.Resources.LengthMustBeGreaterThan0);
            }

            var sb = new StringBuilder();

            if (length < 0)
            {
                sb.Append(str);
            }

            int i;
            for (i = 1; i <= Math.Abs(length); i++)
            {
                sb.Append(indentationCharacter);
            }

            if (length > 0)
            {
                sb.Append(str);
            }


            return sb.ToString();
        }
        #endregion Public Methods
    }
}