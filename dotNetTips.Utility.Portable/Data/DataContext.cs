using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace dotNetTips.Utility.Portable.Data
{
    /// <summary>
    /// Database context for GolfDbContext.
    /// </summary>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public abstract class DataContext<TContext> : DbContext where TContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GolfDbContext" /> class.
        /// </summary>
        protected DataContext(string connection)
           : base("name=GolfApiConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ValidateOnSaveEnabled = true;
            Database.SetInitializer<TContext>(null);

#if DEBUG
            this.Database.Log = s => Debug.WriteLine(s);
#endif
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


        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.</remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

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
                    entry.Entity.Added = DateTime.UtcNow;
                }
                else
                {
                    entry.Entity.Updated = DateTime.UtcNow;
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                }
            }
        }
    }

}
