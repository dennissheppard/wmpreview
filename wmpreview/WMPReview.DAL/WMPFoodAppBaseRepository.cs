using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMP.EFDalKit;

namespace WMPReview.DAL
{
    public class WMPFoodAppBaseRepository<TEntity> : RepositoryBase<WMPFoodAppEntities, TEntity> where TEntity : class
    {
        public WMPFoodAppBaseRepository(IUnitOfWork<WMPFoodAppEntities> unitOfWork) : base(unitOfWork)
        {
        }

        private WMPFoodAppBaseRepository(IDbContextFactory<WMPFoodAppEntities> dbContextFactory) : base(dbContextFactory)
        {
        }

        private WMPFoodAppBaseRepository(WMPFoodAppEntities dbContext) : base(dbContext)
        {
        }
    }
}
