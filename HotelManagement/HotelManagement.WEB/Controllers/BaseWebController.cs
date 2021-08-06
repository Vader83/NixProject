using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManagement.BLL.Interfaces;
using HotelManagement.BLL.Services;
using HotelManagement.WEB.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HotelManagement.WEB.Controllers
{
	public class BaseWebController : Controller
	{
		private ApplicationUserManager _appUserManager = null;
		private ApplicationRoleManager _appRoleManager = null;
		private IGuestService _guestService;
		private IHotelService _hotelService;
		private IRoomService _roomService;
		private IBookingService _bookingService;
		private IEmployeeService _employeeService;
		private ILoggerService _loggerService;

		protected ApplicationRoleManager AppRoleManager => _appRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
		protected ApplicationUserManager AppUserManager => _appUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
		protected IGuestService GuestService => _guestService;
		protected IHotelService HotelService => _hotelService;
		protected IRoomService RoomService => _roomService;
		protected IBookingService BookingService => _bookingService;
		protected IEmployeeService EmployeeService => _employeeService;
		protected ILoggerService LoggerService => _loggerService;

		public BaseWebController(IGuestService guestService, IHotelService hotelService, IRoomService roomService, 
			IBookingService bookingService, IEmployeeService employeeService, ILoggerService loggerService)
		{
			_guestService = guestService;
			_hotelService = hotelService;
			_roomService = roomService;
			_bookingService = bookingService;
			_employeeService = employeeService;
			_loggerService = loggerService;
		}

	}
}