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
	[Authorize(Roles = "Admin")]
	public class RoomController : BaseWebController
    {
	    private IMapper toViewModelMapper;
	    private IMapper toDtoMapper;

	    public RoomController(IGuestService guestService, IHotelService hotelService, IRoomService roomService, IBookingService 
	        bookingService, IEmployeeService employeeService, ILoggerService loggerService) 
	        : base(guestService, hotelService, roomService, bookingService, employeeService, loggerService)
        {
        }

		[HttpGet]
		public ActionResult GetAllRooms()
		{
			toViewModelMapper = this.GetReturnModelMapper();
			var rooms = toViewModelMapper.Map<IEnumerable<RoomDTO>, List<RoomReturnModel>>(RoomService.GetAll());

			return View(rooms);
		}

		[HttpGet]
		public ActionResult GetRoomsInHotelById(int hotelId)
		{
			toViewModelMapper = this.GetReturnModelMapper();
			var rooms = toViewModelMapper.Map<IEnumerable<RoomDTO>, List<RoomReturnModel>>(RoomService.GetRoomsInHotel(hotelId));

			return View(rooms);
		}

		[HttpGet]
		public ActionResult GetRoomsInHotel()
		{
			var currentUser = EmployeeService.GetAll().Single(dto => dto.EmailAddress == User.Identity.Name);
			toViewModelMapper = this.GetReturnModelMapper();
			var rooms = toViewModelMapper.Map<IEnumerable<RoomDTO>, List<RoomReturnModel>>(RoomService.GetRoomsInHotel(currentUser.HotelId));

			return View(rooms);
		}

		[HttpGet]
		public ActionResult GetRoom(int id)
		{
			toViewModelMapper = this.GetReturnModelMapper();
			var rooms = toViewModelMapper.Map<RoomDTO, RoomReturnModel>(RoomService.Get(id));

			return View(rooms);
		}

		[AllowAnonymous]
		[HttpGet]
		public ActionResult GetEmptyRoomsForDate()
		{
			ViewBag.hotelList = PrepareHotelModel();

			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult GetEmptyRoomsForDate(FindEmptyRoomModel model)
		{
			ViewBag.hotelList = PrepareHotelModel();

			if (model.DateFrom.CompareTo(DateTime.Today) < 0)
			{
				ModelState.AddModelError("", "Start date must be greater or equal than today");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			toViewModelMapper = GetReturnModelMapper();
			model.RoomsInfo = toViewModelMapper.Map<IEnumerable<RoomDTO>, List<RoomReturnModel>>(RoomService.EmptyRoomsForDate(model.DateFrom, model.DateTo, model.HotelId));
			return View(model);
		}

		[HttpGet]
		public ActionResult CreateRoom()
		{
			ViewBag.hotelList = PrepareHotelModel();
			ViewBag.typeList = PrepareRoomTypeModel();

			return View();
		}

		[HttpPost]
		public ActionResult CreateRoom(RoomCreateModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.hotelList = PrepareHotelModel();
				ViewBag.typeList = PrepareRoomTypeModel();
				return View(model);
			}

			toDtoMapper = GetCreateDtoMapper();

			RoomService.Create(toDtoMapper.Map<RoomCreateModel, RoomDTO>(model));

			return RedirectToAction("GetAllRooms");
		}

		[HttpGet]
		public ActionResult UpdateRoom()
		{
			

			return View();
		}

		[HttpPost]
		public ActionResult UpdateRoom(RoomUpdateModel model, string action)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.typeList = new SelectList(null);
				ViewBag.statusList = new SelectList(null);

				return View(model);
			}

			if (action == "get")
			{
				toViewModelMapper = GetUpdateModelMapper();
				model = toViewModelMapper.Map<RoomDTO, RoomUpdateModel>(RoomService.Get(model.Id));

				ViewBag.typeList = PrepareRoomTypeModel(model.TypeId);
				ViewBag.statusList = PrepareRoomStatusModel(model.StatusId);

				return View(model);
			}
			if (action == "update")
			{
				toDtoMapper = GetUpdateDtoMapper();

				RoomService.Update(toDtoMapper.Map<RoomUpdateModel, RoomDTO>(model));
			}

			return RedirectToAction("GetAllRooms");
		}

		[HttpGet]
		public ActionResult DeleteRoom()
		{
			return View();
		}

		[HttpPost]
		public ActionResult DeleteRoom(RoomDeleteModel model, string action)
		{
			if (action == "get")
			{
				toViewModelMapper = GetReturnModelMapper();
				model.RoomInfo = toViewModelMapper.Map<RoomDTO, RoomReturnModel>(RoomService.Get(model.Id));
				return View(model);
			}
			if (action == "delete")
			{
				RoomService.Delete(model.Id);
			}

			return RedirectToAction("GetAllRooms", "Room");
		}


		#region Helper methods

		private SelectList PrepareHotelModel()
		{
			this.toViewModelMapper = new MapperConfiguration(cfg => cfg.CreateMap<HotelDTO, HotelRevenueBindings>()
				.ForMember(bindings => bindings.Id, expression => expression.MapFrom(dto => dto.Id))
				.ForMember(bindings => bindings.Name, expression => expression.MapFrom(dto => dto.Name))
			).CreateMapper();
			List<HotelRevenueBindings> bindingList =
				toViewModelMapper.Map<IEnumerable<HotelDTO>, IEnumerable<HotelRevenueBindings>>(HotelService.GetAll()).ToList();
			SelectList hoteList = new SelectList(bindingList, "Id", "Name");

			return hoteList;
		}

		private SelectList PrepareRoomTypeModel(int selectedValue = 1)
		{
			this.toViewModelMapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomTypeDTO, RoomTypeReturnModel>()
				.ForMember(bindings => bindings.Id, expression => expression.MapFrom(dto => dto.Id))
				.ForMember(bindings => bindings.Name, expression => expression.MapFrom(dto => dto.Name))
			).CreateMapper();
			List<RoomTypeReturnModel> bindingList =
				toViewModelMapper.Map<IEnumerable<RoomTypeDTO>, IEnumerable<RoomTypeReturnModel>>(RoomService.GetTypes()).ToList();
			SelectList typeList = new SelectList(bindingList, "Id", "Name", selectedValue);

			return typeList;
		}

		private SelectList PrepareRoomStatusModel(int selectedValue = 1)
		{
			this.toViewModelMapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomStatusDTO, RoomStatusReturnModel>()
				.ForMember(bindings => bindings.Id, expression => expression.MapFrom(dto => dto.Id))
				.ForMember(bindings => bindings.Name, expression => expression.MapFrom(dto => dto.Name))
			).CreateMapper();
			List<RoomStatusReturnModel> bindingList =
				toViewModelMapper.Map<IEnumerable<RoomStatusDTO>, IEnumerable<RoomStatusReturnModel>>(RoomService.GetStatuses()).ToList();
			SelectList statusList = new SelectList(bindingList, "Id", "Name", selectedValue);

			return statusList;
		}

		private IMapper GetReturnModelMapper()
		{
			return new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomReturnModel>()
				.ForMember(model => model.TypeName, expression => expression.MapFrom(dto => dto.TypeOfRoom.Name))
				.ForMember(model => model.DoubleBedCount, expression => expression.MapFrom(dto => dto.TypeOfRoom.DoubleBedCount))
				.ForMember(model => model.SingleBedCount, expression => expression.MapFrom(dto => dto.TypeOfRoom.SingleBedCount))
				.ForMember(model => model.HotelName, expression => expression.MapFrom(dto => dto.RoomHotel.Name))
				.ForMember(model => model.StatusName, expression => expression.MapFrom(dto => dto.StatusOfRoom.Name))
			).CreateMapper();
		}

		private IMapper GetUpdateModelMapper()
		{
			return new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomUpdateModel>()
				.ForMember(model => model.DoubleBedCount, expression => expression.MapFrom(dto => dto.TypeOfRoom.DoubleBedCount))
				.ForMember(model => model.SingleBedCount, expression => expression.MapFrom(dto => dto.TypeOfRoom.SingleBedCount))
				.ForMember(model => model.HotelName, expression => expression.MapFrom(dto => dto.RoomHotel.Name))
			).CreateMapper();
		}

		private IMapper GetDeleteModelMapper()
		{
			return new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomDeleteModel>()
				.ForMember(model => model.RoomInfo, expression => expression.Ignore())
				.ForMember(model => model.Id, expression => expression.MapFrom(dto => dto.Id))
				.AfterMap((src, dest) =>
				{
					dest.RoomInfo = new RoomReturnModel
					{
						HotelName = src.RoomHotel.Name,
						RoomNumber = src.RoomNumber,
						TypeName = src.TypeOfRoom.Name,
						StatusName = src.StatusOfRoom.Name,
						RoomRate = src.RoomRate,
						DoubleBedCount = src.TypeOfRoom.DoubleBedCount,
						SingleBedCount = src.TypeOfRoom.SingleBedCount
					};
				})
			).CreateMapper();
		}

		private IMapper GetCreateDtoMapper()
		{
			return new MapperConfiguration(cfg=> cfg.CreateMap<RoomCreateModel, RoomDTO>()).CreateMapper();
		}

		private IMapper GetUpdateDtoMapper()
		{
			return new MapperConfiguration(cfg => cfg.CreateMap<RoomUpdateModel, RoomDTO>()).CreateMapper();
		}

		#endregion
	}
}
