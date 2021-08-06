using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using HotelManagement.BLL.Interfaces;
using HotelManagement.BLL.Responses;

namespace HotelManagement.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[RoutePrefix("api/Hotel")]
	public class HotelController : ApiController
	{
		private IHotelService hotelService;

		public HotelController(IHotelService hotelService)
		{
			this.hotelService = hotelService;
		}

		[Route("Revenue/{hotelId:int}/{month:int}")]
		[HttpGet]
		public IEnumerable<RevenueResponse> GetRevenue(int hotelId, int month)
		{
			return hotelService.GetRevenueByMonth(hotelId, month);
		}

	}
}