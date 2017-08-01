using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using dotNetTips.Utility.Standard.Data;
using Microsoft.EntityFrameworkCore;

namespace dotNetTips.Utility.Portable.Data
{
    /// <summary>
    /// Database context for GolfDbContext.
    /// </summary>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public abstract class DataContext<TContext> : DbContext where TContext : DbContext
    {
        public DataContext( DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).</returns>
        public override int SaveChanges()
        {
            this.UpdateEntities();

            return base.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation.
        /// The task result contains the number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).</returns>
        /// <remarks>Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.</remarks>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this.UpdateEntities();

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Updates the entities.
        /// </summary>
        private void UpdateEntities()
        {
            foreach (var entry in ChangeTracker.Entries<DataEntity>().AsParallel())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;

                    if (entry.Entity.PublicKey == null || entry.Entity.PublicKey == Guid.Empty)
                    {
                        entry.Entity.PublicKey = Guid.NewGuid();
                    }
                }
                else
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }

}
