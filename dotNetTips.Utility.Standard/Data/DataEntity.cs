// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 06-26-2017
//
// Last Modified By : David McCarter
// Last Modified On : 09-16-2017
// ***********************************************************************
// <copyright file="DataEntity.cs" company="dotNetTips.com - David McCarter">
//     dotNetTips.com - David McCarter
// </copyright>
// <summary></summary>
using dotNetTips.Utility.Standard.Validation;
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNetTips.Utility.Standard.Data
{
    /// <summary>
    /// Base Class for Data Entities.
    /// </summary>
    /// <seealso cref="dotNetTips.Utility.Standard.Data.IDataEntity" />
    public abstract class DataEntity : IDataEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEntity" /> class.
        /// </summary>
        protected DataEntity()
        {
        }

        /// <summary>
        /// Determines whether [is entity valid].
        /// </summary>
        /// <returns><c>true</c> if [is entity valid]; otherwise, <c>false</c>.</returns>
        private IsValidResult IsEntityValid()
        {
            var returnValue = false;

            if(UpdatedAt.HasValue)
            {
                if(CreatedAt > UpdatedAt)
                {
                    returnValue = false;
                }
            }

            return new IsValidResult(returnValue);
        }

        /// <summary>
        /// Gets or sets the created at date.
        /// </summary>
        /// <value>The created at.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTimeOffset CreatedAt
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataEntity" /> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        public bool Deleted
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Checks to make sure entity is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public virtual IsValidResult IsValid => IsEntityValid();

        /// <summary>
        /// Gets or sets the public key.
        /// </summary>
        /// <value>The public key.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid PublicKey
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTimeOffset? UpdatedAt
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] Version
        {
            get; set;
        }
    }
}
