using Autofac;
using Autofac.Integration.WebApi;
using WmpReview.Api.Controllers;

namespace DemoApp.DependencyResolution
{
    public class WebApiInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Web api controllers
            builder.RegisterApiControllers(typeof(ReviewController).Assembly);

            base.Load(builder);
        }
    }
}