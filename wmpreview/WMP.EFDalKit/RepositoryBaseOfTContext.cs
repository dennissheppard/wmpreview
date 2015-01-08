using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using log4net;

namespace WMP.EFDalKit
{
    public abstract partial class RepositoryBase<TContext> where TContext : DbContextBase
    {
        #region Constructors

        /// <summary>
        /// The default constructor
        /// </summary>
        private RepositoryBase()
        {
            _UnitOfWork = null;
        }

        /// <summary>
        /// The default with unitOfWork dependency
        /// </summary>
        protected RepositoryBase(IUnitOfWork<TContext> unitOfWork)
                : this()
        {
            if (unitOfWork == null)
            {
                log.Warn("A null unit of work has been provided to the RepositoryBase, throwing exception");
                throw new ArgumentNullException("unitOfWork",
                                                "Cannot provide a null data context provider to the RepositoryBase");
            }

            UnitOfWork = unitOfWork;
            log.Debug("RepositoryBase constructed");
        }

        protected RepositoryBase(TContext dbContext)
                : this()
        {
            if (dbContext == null)
            {
                log.Warn("A null dbContext has been provided to RepositoryBase, throwing");
                throw new ArgumentNullException("dbContext", "Cannot provide a null context");
            }
            this.DbContext = dbContext;
        }

        protected RepositoryBase(IDbContextFactory<TContext> dbContextFactory)
                : this()
        {
            if (dbContextFactory == null)
            {
                log.Warn("A null context factory has been provided to the RepositoryBase, throwing exception");
                throw new ArgumentNullException("dbContextFactory",
                                                "Cannot provide a null data context factory to the RepositoryBase");
            }
            this.DbContextFactory = dbContextFactory;
            this.DbContext = dbContextFactory.Create();
        }

        #endregion

        #region UnitOfWork Details

        private IUnitOfWork<TContext> _UnitOfWork;

        protected IUnitOfWork<TContext> UnitOfWork
        {
            get { return _UnitOfWork; }
            set
            {
                _UnitOfWork = value;
                this.DbContext = UnitOfWork.DbContext;
            }
        }

        private void SetupUnitOfWork(IUnitOfWork<TContext> unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        #endregion

        private static ILog log = LogManager.GetLogger(typeof (RepositoryBase<TContext>));
        private TContext _dbContext = null;
        protected IDbContextFactory<TContext> DbContextFactory { get; set; }

        protected TContext DbContext
        {
            get { return _dbContext; }
            set { this._dbContext = value; }
        }

        #region Private fields

        #endregion

        #region Fetch helper methods

        protected virtual int FetchCount<T>(IQueryable<T> query) where T : class
        {
            try
            {
                log.Debug("Attempting to fetch a count for query");
                int result = 0;

                result = query.Count<T>();
                log.DebugFormat("Count fetched, returning {0}", result);

                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error caught while fethching count, throwing", ex);
                throw;
            }
        }


        /// <summary>
        /// Fetch the result of a query as a List using paging specifiers
        /// </summary>
        /// <typeparam name="T">The type of result object in the collection</typeparam>
        /// <param name="query">The query to execute</param>
        /// <param name="paging">The paging configurat</param>
        /// <returns>The results as a list of T</returns>
        protected virtual IList<T> FetchList<T>(IQueryable<T> query, DataPagingSpecifier paging) where T : class
        {
            try
            {
                log.Debug("Attempting to fetch a list");
                IList<T> result = null;

                result = FetchList(query.Page(paging));

                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error caught while fethching list, throwing", ex);
                throw;
            }
        }

        /// <summary>
        /// Fetchs the results of a query as a List        
        /// </summary>
        /// <typeparam name="T">The type of result object in the collection</typeparam>
        /// <param name="query">The query to execute</param>        
        /// <returns>The results as a list of T</returns>    
        protected virtual IList<T> FetchList<T>(IQueryable<T> query) where T : class
        {
            try
            {
                IList<T> result = null;

                result = query.ToList<T>();

                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error caught while fethching list, throwing", ex);
                throw;
            }
        }

        #endregion
    }
}