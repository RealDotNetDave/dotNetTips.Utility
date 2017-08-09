// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : david
// Created          : 02-12-2017
//
// Last Modified By : david
// Last Modified On : 06-24-2017
// ***********************************************************************
// <copyright file="ISingleton.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace dotNetTips.Utility.Standard
{
    /// <summary>
    /// Interface ISingleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingleton<T> where T : class
    {
        /// <summary>
        /// Returns instance for the object.
        /// </summary>
        /// <returns>T.</returns>
        T Instance();
    }
}