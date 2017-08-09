// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 08-06-2017
//
// Last Modified By : David McCarter
// Last Modified On : 08-07-2017
// ***********************************************************************
// <copyright file="Processor.cs" company="dotNetTips.com - David McCarter">
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
        /// The name
        /// </summary>
        public string Name;

        /// <summary>
        /// The size
        /// </summary>
        public long Size;

        /// <summary>
        /// The progress state
        /// </summary>
        public ProgressState ProgressState;

        /// <summary>
        /// The message
        /// </summary>
        public string Message;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressEventArgs"/> class.
        /// </summary>
        public ProgressEventArgs()
        {
            this.Message = string.Empty;
        }
    }
}
