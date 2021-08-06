using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using HotelManagement.API.Controllers;
using HotelManagement.API.Utils;
using HotelManagement.BLL.Infrastructure;
using HotelManagement.BLL.Services;
using Ninject;
using Ninject.Modules;
using Ninject.Web.WebApi.Filter;

namespace HotelManagement.API
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			//NinjectModule hotelModule = new HotelModule();
			//NinjectModule guestModule = new GuestModule();
			//NinjectModule roomModule = new RoomModule();
			//NinjectModule bookingModule = new BookingModule();
			//NinjectModule dependencyModule = new DependencyModule("HotelModelAdmin");

			//var kernel = new StandardKernel(hotelModule, guestModule, roomModule, bookingModule, dependencyModule);
			//kernel.Bind<DefaultFilterProviders>().ToSelf().WithConstructorArgument(GlobalConfiguration.Configuration.Services.GetFilterProviders());
			//kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetModelValidatorProviders()));
			//GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
		}
	}
}