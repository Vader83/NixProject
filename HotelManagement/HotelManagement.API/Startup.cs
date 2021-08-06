using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common.Owin;
using HotelManagement.API.Infrastructure;
using HotelManagement.API.Providers;
using HotelManagement.API.Utils;
using HotelManagement.BLL.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Ninject.Modules;
using Ninject.Web.WebApi.Filter;
using Owin;

namespace HotelManagement.API
{
	public class Startup
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration httpConfig = new HttpConfiguration();

			ConfigureOAuthTokenGeneration(appBuilder);

			ConfigureOAuthTokenConsumption(appBuilder);

			ConfigureWebApi(httpConfig);

			appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

			appBuilder.UseNinject(CreateKernel);

			appBuilder.UseWebApi(GlobalConfiguration.Configuration);
		}
		
		private void ConfigureWebApi(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}

		private void ConfigureOAuthTokenGeneration(IAppBuilder app)
		{
			// Configure the db context and user manager to use a single instance per request
			app.CreatePerOwinContext(ApplicationDbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
			{
				//For Dev environment only (on production should be AllowInsecureHttp = false)
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/oauth/token"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new ApplicationOAuthProvider(),
				AccessTokenFormat = new CustomJwtFormat("https://localhost:44310")
			};

			// OAuth 2.0 Bearer Access Token Generation
			app.UseOAuthAuthorizationServer(OAuthServerOptions);
		}

		private void ConfigureOAuthTokenConsumption(IAppBuilder app)
		{
			var issuer = "https://localhost:44310";
			string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
			byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

			// Api controllers with an [Authorize] attribute will be validated with JWT
			app.UseJwtBearerAuthentication(
				new JwtBearerAuthenticationOptions
				{
					AuthenticationMode = AuthenticationMode.Active,
					AllowedAudiences = new[] { audienceId },
					IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
					{
						new SymmetricKeyIssuerSecurityKeyProvider(issuer, audienceSecret) 
					}
					//IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
					//{
					//	new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
					//}
				});
		}

		private static StandardKernel CreateKernel()
		{
			NinjectModule hotelModule = new HotelModule();
			NinjectModule guestModule = new GuestModule();
			NinjectModule roomModule = new RoomModule();
			NinjectModule bookingModule = new BookingModule();
			NinjectModule dependencyModule = new DependencyModule("HotelModelAdmin");

			var kernel = new StandardKernel(hotelModule, guestModule, roomModule, bookingModule, dependencyModule);
			//kernel.Bind<NinjectFilterProvider>().ToSelf().WithConstructorArgument(GlobalConfiguration.Configuration.Services.GetFilterProviders());
			//kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetModelValidatorProviders()));
			GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);

			return kernel;
		}
	}
}