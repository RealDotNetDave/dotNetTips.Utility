// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 07-21-2017
//
// Last Modified By : David McCarter
// Last Modified On : 11-02-2017
// ***********************************************************************
// <copyright file="DataContext.cs" company="dotNetTips.com - David McCarter">
//      McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
using Microsoft.EntityFrameworkCore;
// ***********************************************************************

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotNetTips.Utility.Standard.Data
{
    /// <summary>
    /// Database context.
    /// </summary>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public abstract class DataContext<TContext> : DbContext
        where TContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext{TContext}" /> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        protected DataContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Updates the entities.
        /// </summary>
        private void UpdateEntities()
        {
            if (this.ChangeTracker.HasChanges() == false)
            {
                return;
            }

            foreach (var entry in this.ChangeTracker.Entries<DataEntity>().Where(p => p.State != EntityState.Unchanged))
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

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).</returns>
        /// <remarks>This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.</remarks>
        public override int SaveChanges()
        {
            UpdateEntities();

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
        /// <remarks><para>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </para>
        /// <para>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </para></remarks>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            UpdateEntities();

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
