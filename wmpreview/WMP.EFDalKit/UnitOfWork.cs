using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using log4net;

namespace WMP.EFDalKit
{
    /// <summary>
    /// A unit of work implementation that can be used with any DAL
    /// This implementation depends on either a DbContextProvider or a DbContext
    /// Once this UoW is disposed, you cannot call commit again or access the context anymore
    /// This UoW also used log4net to log activity
    /// </summary>
    /// <typeparam name="TContext">The DbContext type</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        #region Constructor Overloads

        private UnitOfWork()
        {
            try
            {
                log.InfoFormat("Attempting to initialize a new unit of work", this.UniqeIdentifier);
                Initialize();
            }
            finally
            {
                log.InfoFormat("A new unit of work has been created, with unique Id: {0}", this.UniqeIdentifier);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextFactory"></param>
        public UnitOfWork(IDbContextFactory<TContext> contextFactory)
                : this()
        {
            try
            {
                log.Debug("A new unit of work is being constructed using a dbcontext provider");
                if (contextFactory == null)
                {
                    log.WarnFormat("A null context factory was passed into the UnitOfWork, throwing exception");
                    throw new ArgumentNullException("contextFactory",
                                                    "A non-null IDbContextFactory<TContext> must be provided");
                }
                this.DbContextFactory = contextFactory;
            }
            catch (Exception ex)
            {
                log.Error("An error was caught while creating a UnitOfWork", ex);
                throw;
            }
        }

        #endregion

        #region IUnitOfWork<TContext>, IDisposable Implementations

        /// <summary>
        /// Calls SaveChanges on the DbContext
        /// </summary>
        /// <returns>the Result of the SaveChanges call</returns>
        public virtual int SaveChanges()
        {
            try
            {
                int result = default(int);

                log.InfoFormat("Attempting to Commit UnitOfWork to DbContext: {0}", UniqeIdentifier);

                result = DbContext.SaveChanges();

                log.DebugFormat("Save changes complete with result: {0}, for UnitOfWork UniqueId: {1}",
                                result,
                                UniqeIdentifier);

                return result;
            }
            catch (Exception ex)
            {
                log.Error("An error was caught while Committing to DbContext", ex);
                throw;
            }
        }

        #endregion

        #region Cleanup methods

        /// <summary>
        /// IDisposable dispose implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.IsContextInitialized())
                {
                    this.DbContext.Dispose();
                }

                _IsDisposed = true;
            }
        }

        #endregion

        #region Class Initialization helpers

        private void Initialize()
        {
            InitializeUniqueId();
        }

        private void InitializeUniqueId()
        {
            this.UniqeIdentifier = Guid.NewGuid();
        }

        #endregion

        #region Protected class members

        /// <summary>
        /// Uniquely id's this unit of work, may be useful to help with logging        
        /// </summary>
        protected Guid UniqeIdentifier
        {
            get
            {
                if (_UniqeIdentifier == null)
                {
                    InitializeUniqueId();
                }
                return _UniqeIdentifier;
            }
            set { _UniqeIdentifier = value; }
        }

        protected TContext InitializeDbContext()
        {
            ThrowExceptionIfDisposed();
            return this.GetOrCreate();
        }

        #endregion

        #region Private fields

        private static ILog log = LogManager.GetLogger(typeof (UnitOfWork<TContext>));
        private TContext _DbContext;
        private IDbContextFactory<TContext> _DbContextFactory;
        private bool _IsDisposed = false;
        private Guid _UniqeIdentifier;

        #endregion

        protected IDbContextFactory<TContext> DbContextFactory
        {
            get { return _DbContextFactory; }
            set { _DbContextFactory = value; }
        }

        #region IUnitOfWork<TContext> Members

        public TContext DbContext
        {
            get
            {
                ThrowExceptionIfDisposed();
                return GetOrCreate();
            }
            protected set { _DbContext = value; }
        }

        #endregion

        protected TContext GetOrCreate()
        {
            if (!IsContextInitialized())
            {
                CreateContext();
            }
            return _DbContext;
        }

        private void CreateContext()
        {
            this._DbContext = DbContextFactory.Create();
        }

        private bool IsContextInitialized()
        {
            return _DbContext != null;
        }

        #region Private Methods

        private void ThrowExceptionIfDisposed()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("DbContext",
                                                  "Attempting to access the DbContext after the Unit of Work has been disposed");
        }

        #endregion
    }
}