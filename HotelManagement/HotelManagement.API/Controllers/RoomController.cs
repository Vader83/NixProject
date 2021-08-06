using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FluentValidation.Results;
using HotelManagement.API.Models;
using HotelManagement.API.Utils.Validators;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Exceptions;
using HotelManagement.BLL.Interfaces;

namespace HotelManagement.API.Controllers
{
	[Authorize(Roles = "Admin")]
	[RoutePrefix("api/Room")]
	public class RoomController : ApiController
	{
		private IRoomService roomService;
		private IMapper toModelMapper;
		private IMapper toDtoMapper;
		private RoomValidator roomValidator;

		public RoomController(IRoomService roomService)
		{
			this.roomService = roomService;
			this.toModelMapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomModel>()).CreateMapper();
			this.toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomModel, RoomDTO>()).CreateMapper();
			this.roomValidator = new RoomValidator();
		}

		[Route("")]
		[HttpGet]
		public IEnumerable<RoomModel> GetAllRooms()
		{
			return toModelMapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(roomService.GetAll());
		}

		[Route("inHotel/{hotelId:int}")]
		[HttpGet]
		public IEnumerable<RoomModel> GetRoomsInHotel(int hotelId)
		{
			return toModelMapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(roomService.GetRoomsInHotel(hotelId));
		}

		[Route("{id:int}")]
		[HttpGet]
		public HttpResponseMessage GetRoom(int id)
		{
			var roomDto = roomService.Get(id);

			if (roomDto == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, toModelMapper.Map<RoomDTO, RoomModel>(roomDto));
		}

		// GET: api/room/empty?dateFrom=2021-07-01&dateTo=2021-07-20&hotelId=1
		[Route("Empty")]
		[AllowAnonymous]
		[HttpGet]
		public IEnumerable<RoomModel> GetEmptyRoomsForDate(DateTime dateFrom, DateTime dateTo, int hotelId)
		{
			return toModelMapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(roomService.EmptyRoomsForDate(dateFrom, dateTo, hotelId));
		}

		[Route("")]
		[HttpPost]
		public HttpResponseMessage CreateRoom([FromBody] RoomModel value)
		{
			ValidationResult roomValidation = roomValidator.Validate(value);

			if (roomValidation.IsValid)
			{
				try
				{
					RoomDTO newRoom = toDtoMapper.Map<RoomModel, RoomDTO>(value);
					roomService.Create(newRoom);
				}
				catch (RecordAlreadyExistsException e)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
				}

				return Request.CreateResponse(HttpStatusCode.NoContent);
			}

			return Request.CreateResponse(HttpStatusCode.BadRequest, roomValidation.GetErrorMessage());
		}

		[Route("")]
		[HttpPut]
		public HttpResponseMessage UpdateRoom(int id, [FromBody] RoomModel value)
		{
			ValidationResult roomValidation = roomValidator.Validate(value);

			if (roomValidation.IsValid)
			{
				RoomDTO updatingRoom = roomService.Get(id);

				if (updatingRoom != null)
				{
					try
					{
						updatingRoom = toDtoMapper.Map<RoomModel, RoomDTO>(value);
						updatingRoom.Id = id;
						roomService.Update(updatingRoom);
					}
					catch (RecordNotFoundException)
					{
						return Request.CreateResponse(HttpStatusCode.NotFound);
					}

					return Request.CreateResponse(HttpStatusCode.NoContent);
				}

				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.BadRequest, roomValidation.GetErrorMessage());
		}

		[Route("")]
		[HttpDelete]
		public HttpResponseMessage DeleteRoom(int id)
		{
			RoomDTO deletingRoom = roomService.Get(id);

			if (deletingRoom != null)
			{
				roomService.Delete(id);
				return Request.CreateResponse(HttpStatusCode.NoContent);
			}

			return Request.CreateResponse(HttpStatusCode.NotFound);
		}
	}

}
