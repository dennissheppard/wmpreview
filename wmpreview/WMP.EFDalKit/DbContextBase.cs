using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using log4net;

namespace WMP.EFDalKit
{
    public abstract class DbContextBase : DbContext
    {
        private static ILog log = LogManager.GetLogger(typeof (DbContextBase));

        #region Constructor overloads

        //
        // Summary:
        //     Constructs a new context instance using conventions to create the name of
        //     the database to which a connection will be made. The by-convention name is
        //     the full name (namespace + class name) of the derived context class.  See
        //     the class remarks for how this is used to create a connection.
        protected DbContextBase()
                : base()
        {
        }

        //
        // Summary:
        //     Constructs a new context instance using conventions to create the name of
        //     the database to which a connection will be made, and initializes it from
        //     the given model.  The by-convention name is the full name (namespace + class
        //     name) of the derived context class.  See the class remarks for how this is
        //     used to create a connection.
        //
        // Parameters:
        //   model:
        //     The model that will back this context.
        protected DbContextBase(DbCompiledModel model)
                : base(model)
        {
        }

        //
        // Summary:
        //     Constructs a new context instance using the given string as the name or connection
        //     string for the database to which a connection will be made.  See the class
        //     remarks for how this is used to create a connection.
        //
        // Parameters:
        //   nameOrConnectionString:
        //     Either the database name or a connection string.
        public DbContextBase(string nameOrConnectionString)
                : base(nameOrConnectionString)
        {
        }

        //
        // Summary:
        //     Constructs a new context instance using the existing connection to connect
        //     to a database.  The connection will not be disposed when the context is disposed.
        //
        // Parameters:
        //   existingConnection:
        //     An existing connection to use for the new context.
        //
        //   contextOwnsConnection:
        //     If set to true the connection is disposed when the context is disposed, otherwise
        //     the caller must dispose the connection.
        public DbContextBase(DbConnection existingConnection, bool contextOwnsConnection)
                : base(existingConnection, contextOwnsConnection)
        {
        }

        //
        // Summary:
        //     Constructs a new context instance around an existing ObjectContext.  An existing
        //     ObjectContext to wrap with the new context.  If set to true the ObjectContext
        //     is disposed when the DbContext is disposed, otherwise the caller must dispose
        //     the connection.
        //
        // Parameters:
        //   objectContext:
        //     An existing ObjectContext to wrap with the new context.
        //
        //   dbContextOwnsObjectContext:
        //     If set to true the ObjectContext is disposed when the DbContext is disposed,
        //     otherwise the caller must dispose the connection.
        public DbContextBase(ObjectContext objectContext, bool dbContextOwnsObjectContext)
                : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        //
        // Summary:
        //     Constructs a new context instance using the given string as the name or connection
        //     string for the database to which a connection will be made, and initializes
        //     it from the given model.  See the class remarks for how this is used to create
        //     a connection.
        //
        // Parameters:
        //   nameOrConnectionString:
        //     Either the database name or a connection string.
        //
        //   model:
        //     The model that will back this context.
        public DbContextBase(string nameOrConnectionString, DbCompiledModel model)
                : base(nameOrConnectionString, model)
        {
        }

        //
        // Summary:
        //     Constructs a new context instance using the existing connection to connect
        //     to a database, and initializes it from the given model.  The connection will
        //     not be disposed when the context is disposed.  An existing connection to
        //     use for the new context.  The model that will back this context.  If set
        //     to true the connection is disposed when the context is disposed, otherwise
        //     the caller must dispose the connection.
        //
        // Parameters:
        //   existingConnection:
        //     An existing connection to use for the new context.
        //
        //   model:
        //     The model that will back this context.
        //
        //   contextOwnsConnection:
        //     If set to true the connection is disposed when the context is disposed, otherwise
        //     the caller must dispose the connection.
        public DbContextBase(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
                : base(existingConnection, model, contextOwnsConnection)
        {
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            log.DebugFormat("OnModelCreating for DbContextBase");
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                                      .SelectMany(x => x.ValidationErrors)
                                      .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}