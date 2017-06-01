using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetTips.Utility.Standard.Data
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
        [Column(Order = 97)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataEntity"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        [Column(Order = 96)]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(Order = 98)]
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        [Timestamp, Column(Order = 99)]
        public byte[] Version { get; set; }
    }
}
