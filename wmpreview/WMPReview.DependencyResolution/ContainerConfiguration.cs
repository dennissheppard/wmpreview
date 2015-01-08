using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using WMPReview.DependencyResolution;

namespace DemoApp.DependencyResolution
{
    public static class ContainerConfiguration
    {
        public static void SetupContainer()
        {
            var container = BuildContainer();

            //Setup the Web API Dependency resolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static IContainer GetContainer()
        {
            var container = BuildContainer();

            //Setup the Web API Dependency resolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            //Common Registrations
           
            builder.RegisterModule<WMPReview.DependencyResolution.WebApiInfrastructureModule>();
            builder.RegisterModule<DalInfrastructureModule>();
            var container = builder.Build();
            return container;
        }
    }
}
