// ***********************************************************************
// Assembly         : dotNetTips.Utility.Portable
// Author           : david
// Created          : 06-02-2017
//
// Last Modified By : david
// Last Modified On : 07-07-2017
// ***********************************************************************
// <copyright file="DataEntity.cs" company="dotNetTips.com">
//     David McCarter - dotNetTips.com © 2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace dotNetTips.Utility.Portable.Data
{
    /// <summary>
    /// Interface IDataEntity
    /// </summary>
    public interface IDataEntity: IValid
    {
        /// <summary>
        /// Gets or sets the created at date.
        /// </summary>
        /// <value>The created at.</value>
        DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataEntity" /> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        bool Deleted { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        int Id { get; set; }

        /// <summary>
        /// 	Gets or sets the public key.
        /// </summary>
        /// <value>
        /// 	The public key.
        /// </value>
        Guid PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        DateTimeOffset? UpdatedAt { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        byte[] Version { get; set; }
    }
}
