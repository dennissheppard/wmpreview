using System.ComponentModel;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DemoApp.DependencyResolution;
using IContainer = Autofac.IContainer;

namespace WmpReview.Api
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
           
            builder.RegisterModule<WebApiInfrastructureModule>();
            builder.RegisterModule<DalInfrastructureModule>();
            var container = builder.Build();
            return container;
        }
    }
}
