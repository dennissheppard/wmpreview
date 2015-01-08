namespace WMP.EFDalKit
{
    public class SimpleRepository<TContext, TEntity> : RepositoryBase<TContext, TEntity>, IRepository<TEntity>
            where TContext : DbContextBase
            where TEntity : class
    {
        protected SimpleRepository(IUnitOfWork<TContext> unitOfWork)
                : base(unitOfWork)
        {
        }
    }
}