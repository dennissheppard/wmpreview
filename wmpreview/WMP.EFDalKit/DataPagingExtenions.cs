using System;
using System.Linq;

namespace WMP.EFDalKit
{
    public static class DataPagingExtensions
    {
        /// <summary>
        /// Applies paging specification to a query
        /// </summary>
        /// <typeparam name="TEntity">The entity type that is being paged</typeparam>
        /// <param name="query">An instance of a query to paging</param>
        /// <param name="paging">The paging information</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Page<TEntity>(this IQueryable<TEntity> query, DataPagingSpecifier paging)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query", "Cannot page a null query");
            }
            if (paging == null)
            {
                throw new ArgumentNullException("paging", "Paging information must not be null");
            }

            var result = query;

            //If we're not of the start page
            if (!paging.IsStartPage())
            {
                //Skip to the appropriate page
                result = result.Skip(paging.GetSkip());
            }
            result = result.Take(paging.GetTake());

            return result;
        }

        public static IOrderedEnumerable<TEntity> Page<TEntity>(
                this IOrderedEnumerable<TEntity> query,
                DataPagingSpecifier paging)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query", "Cannot page a null query");
            }
            if (paging == null)
            {
                throw new ArgumentNullException("paging", "Paging information must not be null");
            }

            var result = query;

            //If we're not of the start page
            if (!paging.IsStartPage())
            {
                //Skip to the appropriate page
                result = (IOrderedEnumerable<TEntity>) result.Skip(paging.GetSkip());
            }
            result = (IOrderedEnumerable<TEntity>) result.Take(paging.GetTake());

            return result;
        }
    }
}