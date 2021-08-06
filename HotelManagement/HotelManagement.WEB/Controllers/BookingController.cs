using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Interfaces;
using HotelManagement.WEB.Models;

namespace HotelManagement.WEB.Controllers
{
    public class BookingController : BaseWebController
    {
	    private IMapper toViewModelMapper;
	    private IMapper toDtoMapper;

		public BookingController(IGuestService guestService, IHotelService hotelService, IRoomService roomService,
			IBookingService bookingService, IEmployeeService employeeService, ILoggerService loggerService)
			: base(guestService, hotelService, roomService, bookingService, employeeService, loggerService)
		{
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public ActionResult GetAllBookings()
		{
			toViewModelMapper = GetReturnBookingModelMapper();

			List<BookingReturnModel> bookings =
				toViewModelMapper.Map<IEnumerable<BookingDTO>, List<BookingReturnModel>>(BookingService.GetAll());

			return View(bookings);
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public ActionResult GetUserBookings(string email)
		{
			toViewModelMapper = GetReturnBookingModelMapper();

			List<BookingReturnModel> bookings =
				toViewModelMapper.Map<IEnumerable<BookingDTO>, List<BookingReturnModel>>(BookingService.GetGuestBookings(email));

			return View(bookings);
		}

		[Authorize(Roles = "User")]
		[HttpGet]
		public ActionResult GetCurrentUserBookings()
		{
			toViewModelMapper = GetReturnBookingModelMapper();

			List<BookingReturnModel> bookings =
				toViewModelMapper.Map<IEnumerable<BookingDTO>, List<BookingReturnModel>>(BookingService.GetGuestBookings(User.Identity.Name));

			return View(bookings);
		}

		[Authorize(Roles = "Admin, User")]
		[HttpGet]
		public ActionResult CreateBooking(int hotelId = 1)
		{
			ViewBag.roomList = PrepareRoomModel(hotelId);

			return View();
		}

		[Authorize(Roles = "Admin, User")]
		[HttpPost]
		public ActionResult CreateBooking(CreateBookingModel model)
		{
			toDtoMapper = GetCreateDtoMapper();
			GuestDTO guest = GuestService.Find(dto => dto.Email == model.Email).Single();
			BookingDTO newBooking = toDtoMapper.Map<CreateBookingModel, BookingDTO>(model);
			newBooking.GuestId = guest.Id;

			BookingService.Create(newBooking);
			return RedirectToAction("Index", "Home");
		}

		#region Helper methods

		private SelectList PrepareRoomModel(int hotelId)
		{
			toViewModelMapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomListModel>()
				.ForMember(bindings => bindings.SingleBedCount, expression => expression.MapFrom(dto => dto.TypeOfRoom.SingleBedCount))
				.ForMember(bindings => bindings.DoubleBedCount, expression => expression.MapFrom(dto => dto.TypeOfRoom.DoubleBedCount))
				.ForMember(bindings => bindings.TypeName, expression => expression.MapFrom(dto => dto.TypeOfRoom.Name))
			).CreateMapper();
			List<RoomListModel> bindingList =
				toViewModelMapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomListModel>>(RoomService.GetRoomsInHotel(hotelId)).ToList();
			SelectList roomList = new SelectList(bindingList, "Id", "TextToDisplay");

			return roomList;
		}


		private IMapper GetReturnBookingModelMapper()
		{
			return new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, BookingReturnModel>()
				.ForMember(model => model.FullName, expression => expression
					.MapFrom(dto => dto.NewGuest.FirstName + " " + dto.NewGuest.LastName + " " + dto.NewGuest.Patronymic))
				.ForMember(model => model.Status, expression => expression.MapFrom(dto => dto.Status.Name))
				.ForMember(model => model.RoomNumber, expression => expression.MapFrom(dto => dto.BookedRoom.RoomNumber))
			).CreateMapper();
		}

		private IMapper GetCreateDtoMapper()
		{
			return new MapperConfiguration(cfg => cfg.CreateMap<CreateBookingModel, BookingDTO>()).CreateMapper();
		}

		#endregion
	}
}