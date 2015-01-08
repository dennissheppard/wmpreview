using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using log4net;

namespace WMP.EFDalKit
{
    /// <summary>
    /// A base class Repository Implementation for a specific Entity class and Context class,    
    /// </summary>
    /// <typeparam name="TContext">The DbContext type, must inherit from DbContextBase</typeparam>
    /// <typeparam name="TEntity">The Entity Type, could be any class</typeparam>
    public abstract class RepositoryBase<TContext, TEntity> : RepositoryBase<TContext>, IRepository<TEntity>
            where TContext : DbContextBase
            where TEntity : class
    {
        #region Constructors

        /// <summary>
        /// The default public constructor
        /// </summary>
        protected RepositoryBase(IUnitOfWork<TContext> unitOfWork)
                : base(unitOfWork)
        {
        }

        protected RepositoryBase(IDbContextFactory<TContext> dbContextFactory)
                : base(dbContextFactory)
        {
        }

        protected RepositoryBase(TContext dbContext)
                : base(dbContext)
        {
        }

        #endregion

        private static readonly ILog Log = LogManager.GetLogger(typeof (RepositoryBase<TContext, TEntity>));

        #region IRepository

        /// <summary>
        /// Exposes an entity set for querying  should not be public because we don't want to 
        /// expose an IQueryable to the upper tiers
        /// </summary>
        protected virtual DbSet<TEntity> EntitySet
        {
            get
            {
                if (_entitySet == null)
                {
                    InitializeDbSet();
                }
                return _entitySet;
            }
        }

        /// <summary>
        /// Adds an item to the repository        
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The Entity that was added</returns>
        public virtual TEntity Add(TEntity entity)
        {
            Log.DebugFormat("Attempting to Add entity: {0}", entity);
            return EntitySet.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        /// <returns></returns>
        public virtual TEntity Delete(TEntity entity)
        {
            Log.DebugFormat("Attempting to remove entity: {0}", entity);
            return EntitySet.Remove(entity);
        }

        /// <summary>
        /// Attaches an entity to the context
        /// </summary>
        /// <param name="entity">The entity to attach</param>
        public virtual void Attach(TEntity entity)
        {
            Log.DebugFormat("Attempting to attach entity: {0}", entity);
            EntitySet.Attach(entity);
        }

        /// <summary>
        /// Detects changes and then refreshes the given entity
        /// Default refresh mode of store wins
        /// </summary>
        /// <param name="entity">The entity to refresh</param>
        public virtual void Refresh(TEntity entity)
        {
            Log.DebugFormat("Attempting to refresh entity: {0}", entity);
            var entry = GetEntryForEntity(entity);
            entry.Reload();
        }

        /// <summary>
        /// Sets the original values on the entity from the database, preserving any changes
        /// on the current values
        /// </summary>
        /// <param name="entity"></param>
        public virtual void RefreshOriginalValues(TEntity entity)
        {
            Log.DebugFormat("Attempting to refresh original values for entity: {0}", entity);
            var entry = GetEntryForEntity(entity);
            entry.OriginalValues.SetValues(entry.GetDatabaseValues());
        }

        /// <summary>
        /// Creates a new instance of the entity and attaches it to the context
        /// </summary>
        /// <returns>The Attached Entity instance</returns>
        public virtual TEntity Create()
        {
            return Create(true);
        }

        /// <summary>
        /// Creates an instance of an entity, optionally attaching to the context
        /// </summary>
        /// <param name="shouldAttach">Specifies if the the entity should be attached</param>
        /// <returns>The Attached Entity instance</returns>
        public virtual TEntity Create(bool shouldAttach)
        {
            Log.DebugFormat("Attempting to create an entity of type: {0}", typeof (TEntity));
            TEntity result;

            try
            {
                result = EntitySet.Create();
                if (shouldAttach)
                {
                    Log.Debug("Attaching new entity");
                    Add(result);
                }
            }
            catch (DataException ex)
            {
                ProcessFatalDbException(ex);
                throw;
            }


            if (result == null)
            {
                throw new ApplicationException("Error, Create was not able to sucessully create a new entity instance");
            }

            return result;
        }

        private void ProcessFatalDbException(Exception ex)
        {
            Log.Fatal(
                    "Cannot continue Fatal DbException occurred while attempting to access database, check your database and connection settings",
                    ex);
        }

        #endregion

        #region Private Methods

        private void InitializeDbSet()
        {
            _entitySet = DbContext.Set<TEntity>();
        }

        private DbEntityEntry<TEntity> GetEntryForEntity(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            return entry;
        }

        #endregion

        #region Private fields

        private DbSet<TEntity> _entitySet;

        #endregion
    }
}