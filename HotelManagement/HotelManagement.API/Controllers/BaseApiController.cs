using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HotelManagement.API.Infrastructure;
using HotelManagement.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HotelManagement.API.Controllers
{
	public class BaseApiController : ApiController
	{
		private ModelFactory _modelFactory;
		private ApplicationUserManager _appUserManager = null;
		private ApplicationRoleManager _appRoleManager = null;

		protected ApplicationRoleManager AppRoleManager => _appRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
		protected ApplicationUserManager AppUserManager => _appUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
		protected ModelFactory TheModelFactory => _modelFactory ?? (_modelFactory = new ModelFactory(this.Request, this.AppUserManager));
		
		public BaseApiController()
		{
		}

		protected IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
	}
}