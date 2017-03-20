// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : David McCarter
// Created          : 12-07-2016
//
// Last Modified By : David McCarter
// Last Modified On : 03-13-2017
// ***********************************************************************
// <copyright file="ISingleton.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace dotNetTips.Utility.Portable
{
    /// <summary>
    /// Interface ISingleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISingleton<T>
        where T : class
    {
    }
}