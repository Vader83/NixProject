using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HotelManagement.BLL.Infrastructure;
using HotelManagement.WEB.Infrastructure;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

namespace HotelManagement.WEB
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule moduleBundle = new ModuleBundle();
            NinjectModule dependencyModule = new DependencyModule("HotelModelAdmin");

            KernelBase kernel = new StandardKernel(moduleBundle, dependencyModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
