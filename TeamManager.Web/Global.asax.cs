﻿using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TeamManager.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterAutofac();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MappingConfig.RegisterMaps();
        }

        private void RegisterAutofac()
        {
            var resolver = CompositionRoot.DependencyInjector.GetAutofacResolver(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(resolver);
        }
    }
}
