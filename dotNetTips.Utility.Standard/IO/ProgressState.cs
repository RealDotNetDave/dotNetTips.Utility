// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 08-07-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="ProgressState.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace dotNetTips.Utility.Standard.IO
{
    /// <summary>
    /// Enum ProgressState
    /// </summary>
    public enum ProgressState
    {
        /// <summary>
        /// The error
        /// </summary>
        Error = 0,

        /// <summary>
        /// The deleted
        /// </summary>
        Deleted = 1,

        /// <summary>
        /// The moved
        /// </summary>
        Moved = 2,

        /// <summary>
        /// The copied
        /// </summary>
        Copied = 3
    }
}
