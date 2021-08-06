using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using HotelManagement.API.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelManagement.API.Models
{
	public class ModelFactory
	{
		private UrlHelper _UrlHelper;
		private ApplicationUserManager _AppUserManager;

		public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
		{
			_UrlHelper = new UrlHelper(request);
			_AppUserManager = appUserManager;
		}

		public RoleReturnModel Create(IdentityRole appRole)
		{
			return new RoleReturnModel
			{
				Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
				Id = appRole.Id,
				Name = appRole.Name
			};
		}

		public AccountReturnModel Create(ApplicationUser appUser)
		{
			return new AccountReturnModel
			{
				Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
				Id = appUser.Id,
				FirstName = appUser.FirstName,
				LastName = appUser.LastName,
				Email = appUser.Email,
				EmailConfirmed = appUser.EmailConfirmed,
				Roles = _AppUserManager.GetRolesAsync(appUser.Id).Result,
				Claims = _AppUserManager.GetClaimsAsync(appUser.Id).Result
			};
		}

    }
}