using System;
using System.Data.Entity;

namespace WMP.EFDalKit
{
    /// <summary>
    /// A unit of work interface for an EntityFramework DbContext class
    /// </summary>
    /// <typeparam name="TContext">The DbContext type</typeparam>
    public interface IUnitOfWork<out TContext> : IDisposable
            where TContext : DbContext
    {
        /// <summary>
        /// Provides access to the Underlying DbContext
        /// </summary>
        TContext DbContext { get; }

        /// <summary>
        /// Commits the data changes to the underlying data store.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}