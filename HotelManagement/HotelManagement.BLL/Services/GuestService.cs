using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Exceptions;
using HotelManagement.BLL.Interfaces;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.BLL.Services
{
	public class GuestService : IGuestService
	{
		private IUnitOfWork database;
		private IMapper toDtoMapper;
		private IMapper toEntityMapper;

		public GuestService(IUnitOfWork uow)
		{
			database = uow;
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Guest, GuestDTO>()).CreateMapper();
			toEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<GuestDTO, Guest>()).CreateMapper();
		}

		public IEnumerable<GuestDTO> GetAll()
		{
			return toDtoMapper.Map<IEnumerable<Guest>, IEnumerable<GuestDTO>>(database.Guests.GetAll());
		}

		public GuestDTO Get(int id)
		{
			Guest guest = database.Guests.Get(id);

			if (guest == null)
			{
				throw new RecordNotFoundException(id, typeof(Guest));
			}

			GuestDTO guestDto = toDtoMapper.Map<Guest, GuestDTO>(guest);

			return guestDto;
		}

		public void Create(GuestDTO item)
		{
			Guest existingGuest = database.Guests.Find(guest => guest.Email.Equals(item.Email)).SingleOrDefault();

			if (existingGuest != null)
			{
				throw new RecordAlreadyExistsException(existingGuest.Id, typeof(Guest));
			}

			Guest newGuest = toEntityMapper.Map<GuestDTO, Guest>(item);

			database.Guests.Create(newGuest);
			database.Save();
		}

		public void Update(GuestDTO item)
		{
			Guest updatingGuest = database.Guests.Get(item.Id);

			if (updatingGuest == null)
			{
				throw new RecordNotFoundException(item.Id, typeof(Guest));
			}

			updatingGuest = toEntityMapper.Map<GuestDTO, Guest>(item);

			database.Guests.Update(updatingGuest);
			database.Save();
		}

		public void Delete(int id)
		{
			Guest deletingGuest = database.Guests.Get(id);

			if (deletingGuest == null)
			{
				throw new RecordNotFoundException(id, typeof(Guest));
			}

			database.Guests.Delete(id);
			database.Save();
		}

		public IEnumerable<GuestDTO> Find(Func<GuestDTO, bool> predicate)
		{
			var guests = toDtoMapper.Map<IEnumerable<Guest>, IEnumerable<GuestDTO>>(database.Guests.GetAll());
			return guests.Where(predicate);
		}

		public IEnumerable<GuestDTO> GetAllHotelGuests(int hotelId)
		{
			BookingStatus CheckIn = database.BookingStatuses
				.GetAll()
				.SingleOrDefault(status => status.Name.Contains("CHECK_IN".ToUpper()));

			if (CheckIn == null)
			{
				throw new RecordNotFoundException(typeof(BookingStatus));
			}

			var bookingCheckIn = database.Bookings.Find(booking => booking.StatusId == CheckIn.Id && booking.BookedRoom.HotelId == hotelId);

			var guestsInHotel = new List<GuestDTO>();

			foreach (var booking in bookingCheckIn)
			{
				guestsInHotel.Add(toDtoMapper.Map<Guest, GuestDTO>(booking.NewGuest));
			}

			return guestsInHotel;
		}

		public void CheckIn(GuestDTO guestIn)
		{
			BookingStatus waitStatus = database.BookingStatuses
				.GetAll()
				.SingleOrDefault(status => status.Name.Contains("WAIT".ToUpper()));

			BookingStatus checkInStatus = database.BookingStatuses
				.GetAll()
				.SingleOrDefault(status => status.Name.Contains("CHECK_IN".ToUpper()));

			if (waitStatus == null || checkInStatus == null)
			{
				throw new RecordNotFoundException(typeof(BookingStatus));
			}

			Booking guestBooking = database.Bookings	//check with several booking per guest
				.GetAll()
				.Where(booking => booking.StatusId == waitStatus.Id && booking.GuestId == guestIn.Id)
				.GroupBy(booking => booking.DateFrom)
				.First()
				.Single();

			if (guestBooking == null)
			{
				throw new RecordNotFoundException(typeof(Booking));
			}

			PaymentStatus paymentCompleteStatus = database.PaymentStatuses
				.GetAll()
				.SingleOrDefault(status => status.Name.Contains("COMPLETE".ToUpper()));

			if (paymentCompleteStatus == null)
			{
				throw new RecordNotFoundException(typeof(PaymentStatus));
			}

			Payment bookingPayment = database.Payments
				.GetAll()
				.SingleOrDefault(payment => payment.BookingId == guestBooking.Id);

			//if (bookingPayment == null)
			//{
			//	throw new NotPayedBookingException(guestIn.Id);
			//}

			//if (bookingPayment.StatusId != paymentCompleteStatus.Id)
			//{
			//	throw new NotPayedBookingException(guestIn.Id);
			//}

			guestBooking.StatusId = checkInStatus.Id;

			database.Bookings.Update(guestBooking);
			database.Save();
		}

		public void CheckOut(GuestDTO guestOut)
		{
			BookingStatus checkOutStatus = database.BookingStatuses
				.GetAll()
				.SingleOrDefault(status => status.Name.Contains("CHECK_OUT".ToUpper()));

			BookingStatus checkInStatus = database.BookingStatuses
				.GetAll()
				.SingleOrDefault(status => status.Name.Contains("CHECK_IN".ToUpper()));

			if (checkOutStatus == null || checkInStatus == null)
			{
				throw new RecordNotFoundException(typeof(BookingStatus));
			}

			Booking guestBooking = database.Bookings
				.GetAll()
				.SingleOrDefault(booking => booking.StatusId == checkInStatus.Id && booking.GuestId == guestOut.Id);

			if (guestBooking == null)
			{
				throw new RecordNotFoundException(typeof(Booking));
			}

			guestBooking.StatusId = checkOutStatus.Id;

			database.Bookings.Update(guestBooking);
			database.Save();
		}
	}
}
