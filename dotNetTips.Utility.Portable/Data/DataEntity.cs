using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;

namespace dotNetTips.Utility.Portable.Data
{
    /// <summary>
    /// Base Class for Data Entities.
    /// </summary>
    public abstract class DataEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEntity"/> class.
        /// </summary>
        protected DataEntity()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the created at date.
        /// </summary>
        /// <value>The created at.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataEntity"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(IsPrimaryKey =true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [Column(IsDbGenerated =true)]
        public byte[] Version { get; set; }
    }
}
