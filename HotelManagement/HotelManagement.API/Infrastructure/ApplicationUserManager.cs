using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace HotelManagement.API.Infrastructure
{
	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		public ApplicationUserManager(IUserStore<ApplicationUser> store)
			: base(store)
		{
		}

		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
		{
			var appDbContext = context.Get<ApplicationDbContext>();
			var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));          
			
			appUserManager.UserValidator = new UserValidator<ApplicationUser>(appUserManager)
			{
				AllowOnlyAlphanumericUserNames = true,
				RequireUniqueEmail = true
			};
			
			appUserManager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};
			
			return appUserManager;
		}
	}
}