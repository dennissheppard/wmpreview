using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using WMPReview.DAL;

namespace WmpReview.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ContainerConfiguration.SetupContainer();
            SetupMappings();
        }

        private void SetupMappings()
        {
            Mapper.CreateMap<User, Models.DTO.User>();

            //Business
            Mapper.CreateMap<Business, Models.DTO.User>();
            Mapper.CreateMap<Models.DTO.Business, User>();

            Mapper.CreateMap<Tag, Models.DTO.Tag>();
            Mapper.CreateMap<Models.DTO.Tag, Tag>();
        }
    }
}


