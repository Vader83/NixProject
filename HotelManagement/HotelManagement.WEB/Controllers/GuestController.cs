using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AutoMapper;
using System.Web.Mvc;
using System.Web.Routing;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Exceptions;
using HotelManagement.BLL.Interfaces;
using HotelManagement.WEB.Models;

namespace HotelManagement.WEB.Controllers
{
    
    public class GuestController : BaseWebController
    {
	    private IMapper toViewModelMapper;
        private IMapper toDtoMapper;

        public GuestController(IGuestService guestService, IHotelService hotelService, IRoomService roomService, 
	        IBookingService bookingService, IEmployeeService employeeService, ILoggerService loggerService) 
	        : base(guestService, hotelService, roomService, bookingService, employeeService, loggerService)
        {
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetAllGuests()
        {
            toViewModelMapper = this.GetReturnModelMapper();
            var guests = toViewModelMapper.Map<IEnumerable<GuestDTO>, List<GuestReturnModel>>(GuestService.GetAll());
            
            return View(guests);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetGuest(int id)
        {
	        toViewModelMapper = this.GetReturnModelMapper();
	        var guest = toViewModelMapper.Map<GuestDTO, GuestReturnModel>(GuestService.Get(id));

	        return View(guest);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetGuestsInHotelById(int hotelId)
        {
	        toViewModelMapper = this.GetReturnModelMapper();
	        var guests = toViewModelMapper.Map<IEnumerable<GuestDTO>, List<GuestReturnModel>>(GuestService.GetAllHotelGuests(hotelId));

	        return View(guests);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetGuestsInHotel()
        {
	        var currentUser = EmployeeService.GetAll().Single(dto => dto.EmailAddress == User.Identity.Name);
	        toViewModelMapper = this.GetReturnModelMapper();
	        var guests = toViewModelMapper.Map<IEnumerable<GuestDTO>, List<GuestReturnModel>>(GuestService.GetAllHotelGuests(currentUser.HotelId));

	        return View(guests);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateGuest()
        {
	        return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateGuest(GuestCreateModel model)
        {
	        if (!ModelState.IsValid)
	        {

		        return View(model);
            }

	        toDtoMapper = GetCreateDtoMapper();
            GuestDTO newGuest = toDtoMapper.Map<GuestCreateModel, GuestDTO>(model);
	        GuestService.Create(newGuest);

	        newGuest = GuestService.Find(dto => dto.Email == model.Email).Single();
            var routeParams = new RouteValueDictionary();
            routeParams.Add("id", newGuest.Id);

	        return RedirectToAction("GetGuest","Guest", routeParams);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CheckIn()
        {
	        toViewModelMapper = GetActualBookingModelMapper();

            var bookings = toViewModelMapper.Map<List<BookingDTO>, List<ActualBookingModel>>(BookingService.GetAwaitingBookings().ToList());
	        SelectList actualBookings = new SelectList(bookings, "GuestId", "TextToDisplay");
	        ViewBag.actualBookings = actualBookings;

	        return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CheckIn(int guestId)
        {
			GuestDTO guest = GuestService.Get(guestId);

			if (guest == null)
			{
				toViewModelMapper = GetActualBookingModelMapper();

				var bookings = toViewModelMapper.Map<List<BookingDTO>, List<ActualBookingModel>>(BookingService.GetAwaitingBookings().ToList());
				SelectList actualBookings = new SelectList(bookings, "GuestId", "TextToDisplay");
				ViewBag.actualBookings = actualBookings;

				return View();
			}

			try
			{
				GuestService.CheckIn(guest);
			}
			catch (Exception e)
			{
				if (e is NotPayedBookingException)
				{
                    ModelState.AddModelError("","Guest haven`t paid for the booking");
                    return View();
				}
				if (e is RecordNotFoundException)
				{
					ModelState.AddModelError("", "Record not Found");
					return View();
                }
			}

			return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CheckOut()
        {
	        toViewModelMapper = GetActualBookingModelMapper();

	        var bookings = toViewModelMapper.Map<List<BookingDTO>, List<ActualBookingModel>>(BookingService.GetActiveBookings().ToList());
	        SelectList actualBookings = new SelectList(bookings, "GuestId", "TextToDisplay");
	        ViewBag.currentBookings = actualBookings;

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CheckOut(int guestId)
        {
	        GuestDTO guest = GuestService.Get(guestId);

	        if (guest == null)
	        {
				toViewModelMapper = GetActualBookingModelMapper();

				var bookings = toViewModelMapper.Map<List<BookingDTO>, List<ActualBookingModel>>(BookingService.GetActiveBookings().ToList());
				SelectList actualBookings = new SelectList(bookings, "GuestId", "TextToDisplay");
				ViewBag.currentBookings = actualBookings;

				return View();
			}
            
		    GuestService.CheckOut(guest);
	        
	        return RedirectToAction("Index", "Home");
        }
        
		[Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult UpdateGuest()
        {
	        toViewModelMapper = GetUpdateModelMapper();

	        var guest = toViewModelMapper.
		        Map<GuestDTO, GuestUpdateModel>(GuestService.Find(dto => dto.Email == User.Identity.Name).Single());

	        return View(guest);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult UpdateGuest(GuestUpdateModel model)
        {
	        if (!ModelState.IsValid)
	        {
				return View(model);
			}

	        GuestDTO updatingGuest = GuestService.Find(dto => dto.Email == User.Identity.Name).Single();
	        model.Id = updatingGuest.Id;
			model.Email = User.Identity.Name;

	        toDtoMapper = GetUpdateDtoMapper();
			updatingGuest = toDtoMapper.Map<GuestUpdateModel, GuestDTO>(model);
		    GuestService.Update(updatingGuest);

		    return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteGuest()
        {
	        return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteGuest(int id)
        {
	        try
	        {
		        GuestDTO deletingGuest = GuestService.Get(id);
            }
	        catch (RecordNotFoundException)
	        {
		        ModelState.AddModelError("", "Guest was not found.");
		        return View();
            }

	        GuestService.Delete(id);
	       
            return View();
        }

        #region Helper methods

        private IMapper GetReturnModelMapper()
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<GuestDTO, GuestReturnModel>()).CreateMapper();
        }

        private IMapper GetCreateDtoMapper()
        {
	        return new MapperConfiguration(cfg => cfg.CreateMap<GuestCreateModel, GuestDTO>()).CreateMapper();
        }

        private IMapper GetUpdateDtoMapper()
        {
	        return new MapperConfiguration(cfg => cfg.CreateMap<GuestUpdateModel, GuestDTO>()).CreateMapper();
        }

        private IMapper GetUpdateModelMapper()
        {
	        return new MapperConfiguration(cfg => cfg.CreateMap<GuestDTO, GuestUpdateModel>()).CreateMapper();
        }

		
		private IMapper GetActualBookingModelMapper()
        {
	        return new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, ActualBookingModel>()
		        .ForMember(model => model.FullName, expression => expression
			        .MapFrom(dto => dto.NewGuest.FirstName + " " + dto.NewGuest.LastName + " " + dto.NewGuest.Patronymic))
		        .ForMember(model => model.RoomNumber,expression => expression.MapFrom(dto => dto.BookedRoom.RoomNumber))
	        ).CreateMapper();
        }

        #endregion

    }
}