using System.Data.Entity.Infrastructure;
using Autofac;
using WMP.EFDalKit;
using WMPReview.DAL;

namespace WMPReview.DependencyResolution
{
    public class DalInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(UnitOfWork<>))                   
                   .InstancePerRequest()
                   .InstancePerLifetimeScope();

            builder.RegisterType<WMPFoodAppEntitiesFactory>().As<IDbContextFactory<WMPFoodAppEntities>>();
            builder.RegisterType<WMPFoodAppEntities>().AsSelf().InstancePerRequest();
            base.Load(builder);
        }
    }
}