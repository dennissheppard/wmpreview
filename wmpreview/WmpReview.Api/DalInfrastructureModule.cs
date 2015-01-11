using System.Data.Entity.Infrastructure;
using Autofac;
using WMP.EFDalKit;
using WMPReview.DAL;
using WMPReview.DAL.Repositories;

namespace WmpReview.Api
{
    public class DalInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>)).InstancePerLifetimeScope();

            builder.RegisterType<WMPFoodAppEntitiesFactory>().As<IDbContextFactory<WMPFoodAppEntities>>();
            builder.RegisterType<WMPFoodAppEntities>().AsSelf().InstancePerRequest();

            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            
            base.Load(builder);
        }
    }
}