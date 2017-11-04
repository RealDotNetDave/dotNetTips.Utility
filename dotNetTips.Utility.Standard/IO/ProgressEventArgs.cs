// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 08-07-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="ProgressEventArgs.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace dotNetTips.Utility.Standard.IO
{
    /// <summary>
    /// Class ProgressEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressEventArgs" /> class.
        /// </summary>
        public ProgressEventArgs() => Message = string.Empty;

        /// <summary>
        /// The message
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// The name
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// The progress state
        /// </summary>
        /// <value>The state of the progress.</value>
        public ProgressState ProgressState { get; set; }
        /// <summary>
        /// The size
        /// </summary>
        /// <value>The size.</value>
        public long Size { get; set; }
    }
}
