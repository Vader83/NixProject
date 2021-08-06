using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using FluentValidation.Results;
using HotelManagement.API.Models;
using HotelManagement.API.Utils.Validators;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Interfaces;

namespace HotelManagement.API.Controllers
{
	[RoutePrefix("api/Booking")]
	public class BookingController : ApiController
	{
		private IBookingService bookingService;
		private IMapper toModelMapper;
		private IMapper toDtoMapper;
		private BookingValidator bookingValidator;

		public BookingController(IBookingService bookingService)
		{
			this.bookingService = bookingService;
			this.toModelMapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, BookingModel>()).CreateMapper();

			this.toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingModel, BookingDTO>()).CreateMapper();

			this.bookingValidator = new BookingValidator();
		}

		[Authorize(Roles = "Admin, Employee")]
		[HttpGet]
		public IEnumerable<BookingModel> GetAllBookings()
		{
			return toModelMapper.Map<IEnumerable<BookingDTO>, IEnumerable<BookingModel>>(bookingService.GetAll());
		}

		[AllowAnonymous]
		[HttpPost]
		public HttpResponseMessage CreateBooking(HttpRequestMessage request, [FromBody] BookingModel value)
		{
			ValidationResult bookingValidation = bookingValidator.Validate(value);

			if (bookingValidation.IsValid)
			{
				BookingDTO newBooking = toDtoMapper.Map<BookingModel, BookingDTO>(value);

				bookingService.Create(newBooking);
				return request.CreateResponse(HttpStatusCode.NoContent);
			}

			return request.CreateResponse(HttpStatusCode.BadRequest, bookingValidation.GetErrorMessage());
		}

	}
}