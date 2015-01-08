using System.Data.Entity.Infrastructure;

namespace WMPReview.DAL
{
    public class WMPFoodAppEntitiesFactory : IDbContextFactory<WMPFoodAppEntities>
    {
        public WMPFoodAppEntities Create()
        {
            return new WMPFoodAppEntities();
        }
    }
}