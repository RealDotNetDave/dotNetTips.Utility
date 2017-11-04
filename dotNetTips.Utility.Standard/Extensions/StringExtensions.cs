using System;
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
            if (fileSize == 0 && (fileSize >= long.MinValue && fileSize <= long.MaxValue))
            {
                throw new ArgumentNullException(nameof(fileSize), "File size is invalid.");
            }

            long size = 0;

            while (fileSize > 1024 && size < 4)
            {
                fileSize = Convert.ToInt64(fileSize / 1024);
                size += 1;
            }

            return fileSize + ControlChars.Space + (new string[] { Properties.Resources.Bytes, Properties.Resources.KB, Properties.Resources.MB, Properties.Resources.GB })[Convert.ToInt32(size)];
        }

        /// <summary>
        /// Indents the specified length.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="length">The length.</param>
        /// <param name="indentationCharacter">The indentation character.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">length - Length must be greater than 0.</exception>
        public static string Indent(this string str, int length, char indentationCharacter = ControlChars.Space)
        {
            if (length == 0)
            {
                throw new ArgumentNullException(nameof(length), "Length must be greater than 0.");
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