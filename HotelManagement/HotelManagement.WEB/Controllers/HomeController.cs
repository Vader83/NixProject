using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using HotelManagement.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelManagement.WEB.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
		[Authorize]
		public ActionResult About()
		{
			string message = $"Your application description page. \n User name: {User.Identity.Name}\n";

			var roles = ((ClaimsIdentity) User.Identity).Claims
				.Where(c => c.Type == ClaimTypes.Role)
				.Select(c => c.Value);

			foreach (var role in roles)
			{
				message += role + " ";
			}

			ViewBag.Message = message;
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}


		
	}
}