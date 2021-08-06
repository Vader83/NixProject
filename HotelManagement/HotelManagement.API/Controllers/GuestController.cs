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
using HotelManagement.BLL.Exceptions;
using HotelManagement.BLL.Interfaces;

namespace HotelManagement.API.Controllers
{
    [RoutePrefix("api/Guest")]
    [Authorize(Roles = "Admin")]
    public class GuestController : ApiController
    {
        private IGuestService guestService;
        private IMapper toModelMapper;
        private IMapper toDtoMapper;
        private GuestValidator guestValidator;

        public GuestController(IGuestService guestService)
        {
            this.guestService = guestService;
            this.toModelMapper = new MapperConfiguration(cfg => cfg.CreateMap<GuestDTO, GuestModel>()).CreateMapper();
            this.toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<GuestModel, GuestDTO>()).CreateMapper();
            guestValidator = new GuestValidator();
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetAllGuests(HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.OK, toModelMapper.Map<IEnumerable<GuestDTO>, IEnumerable<GuestModel>>(guestService.GetAll()));
        }

        [Route("{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetGuest(HttpRequestMessage request, int id)
        {
	        GuestDTO guest = guestService.Get(id);

	        if (guest != null)
	        {
		        var returnGuest = toModelMapper.Map<GuestDTO, GuestModel>(guestService.Get(id));
		        return request.CreateResponse(HttpStatusCode.OK, returnGuest);
	        }

	        return request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("inHotel/{hotelId:int}")]
        [HttpGet]
        public IEnumerable<GuestModel> GetGuestsInHotel(HttpRequestMessage request, int hotelId)
        {
            return toModelMapper.Map<IEnumerable<GuestDTO>, IEnumerable<GuestModel>>(guestService.GetAllHotelGuests(hotelId));
        }
        
        [Route("")]
        [HttpPost]
        public HttpResponseMessage CreateGuest(HttpRequestMessage request, [FromBody] GuestModel value)
        {
            ValidationResult guestValidation = guestValidator.Validate(value);

            if (guestValidation.IsValid)
            {
                GuestDTO newGuest = toDtoMapper.Map<GuestModel, GuestDTO>(value);
                guestService.Create(newGuest);
                return request.CreateResponse(HttpStatusCode.NoContent);
            }

            return request.CreateResponse(HttpStatusCode.BadRequest, guestValidation.Errors);
        }

        [Route("CheckIn/{guestId:int}")]
        [HttpPut]
        public HttpResponseMessage CheckIn(HttpRequestMessage request, int guestId)
        {
            GuestDTO guest = guestService.Get(guestId);

            if (guest == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                guestService.CheckIn(guest);
            }
            catch (Exception e)
            {
                if (e is NotPayedBookingException)
                {
                    return request.CreateResponse(HttpStatusCode.PaymentRequired);
                }
                if (e is RecordNotFoundException)
                {
                    return request.CreateResponse(HttpStatusCode.NotFound);
                }
            }

            return request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("CheckOut/{guestId:int}")]
        [HttpPut]
        public HttpResponseMessage CheckOut(HttpRequestMessage request, int guestId)
        {
            GuestDTO guest = guestService.Get(guestId);

            if (guest == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            guestService.CheckOut(guest);
            return request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Route("{id:int}")]
        [HttpPut]
        public HttpResponseMessage UpdateGuest(HttpRequestMessage request, int id, [FromBody] GuestModel value)
        {
            ValidationResult guestValidation = guestValidator.Validate(value);

            if (guestValidation.IsValid)
            {
                GuestDTO updatingGuest = guestService.Get(id);

                if (updatingGuest != null)
                {
                    try
                    {
                        updatingGuest = toDtoMapper.Map<GuestModel, GuestDTO>(value);
                        updatingGuest.Id = id;
                        guestService.Update(updatingGuest);
                    }
                    catch (RecordNotFoundException)
                    {
                        return request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    return request.CreateResponse(HttpStatusCode.OK);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            return request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public HttpResponseMessage DeleteGuest(HttpRequestMessage request, int id)
        {
            GuestDTO deletingGuest = guestService.Get(id);

            if (deletingGuest == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

            guestService.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}