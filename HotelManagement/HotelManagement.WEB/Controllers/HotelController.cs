using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Interfaces;
using HotelManagement.BLL.Responses;
using HotelManagement.WEB.Models;

namespace HotelManagement.WEB.Controllers
{
	[Authorize(Roles = "Admin")]
    public class HotelController : BaseWebController
    {
	    private IMapper toWebMapper;

		public HotelController(IGuestService guestService, IHotelService hotelService, IRoomService roomService, 
			IBookingService bookingService, IEmployeeService employeeService, ILoggerService loggerService) 
			: base(guestService, hotelService, roomService, bookingService, employeeService, loggerService)
		{
		}

		[Route("Revenue")]
	    [HttpGet]
	    public ActionResult GetRevenue()
	    {
		    ViewBag.hotels = PrepareHotelModel();

			return View();
	    }

	    [Route("Revenue")]
		[HttpPost]
	    public ActionResult GetRevenue(HotelRevenueModel model)
	    {
		    ViewBag.hotels = PrepareHotelModel();
			if (model.Month >= 1 && model.Month <= 12)
		    {
			    model.HotelRevenue = HotelService.GetRevenueByMonth(model.Id, model.Month).ToList();
			    return View(model);
			}

			ModelState.AddModelError("","Month value must be between 1 and 12");
			

			return View(model);
	    }

	    #region Helper methods

	    private SelectList PrepareHotelModel()
	    {
		    this.toWebMapper = new MapperConfiguration(cfg => cfg.CreateMap<HotelDTO, HotelRevenueBindings>()
			    .ForMember(bindings => bindings.Id, expression => expression.MapFrom(dto => dto.Id))
			    .ForMember(bindings => bindings.Name, expression => expression.MapFrom(dto => dto.Name))
		    ).CreateMapper();
		    List<HotelRevenueBindings> bindingList =
			    toWebMapper.Map<IEnumerable<HotelDTO>, IEnumerable<HotelRevenueBindings>>(HotelService.GetAll()).ToList();
		    SelectList hoteList = new SelectList(bindingList, "Id", "Name");

		    return hoteList;
	    }

	    #endregion


	    
    }
}