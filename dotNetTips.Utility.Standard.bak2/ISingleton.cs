// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 01-22-2017
//
// Last Modified By : David McCarter
// Last Modified On : 01-16-2017
// ***********************************************************************
// <copyright file="ISingleton.cs" company="dotNetTips.Utility.Standard">
//     Copyright (c) dotNetTips.com - McCarter Consulting. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

/// <summary>
/// The Standard namespace.
/// </summary>
namespace dotNetTips.Utility.Standard
{
    //TODO: Abstract class too?
    /// <summary>
    /// Interface ISingleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISingleton<T> where T : class
    {
        //static T Instance { get; }
    }
}