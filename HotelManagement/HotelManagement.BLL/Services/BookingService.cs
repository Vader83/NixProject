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
	public class BookingService : IBookingService
	{
		private IUnitOfWork database;
		private IMapper toDtoMapper;
		private IMapper toEntityMapper;

		public BookingService(IUnitOfWork uow)
		{
			database = uow;
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()
					.ForMember(dto => dto.NewGuest, expression => expression.Ignore())
					.ForMember(dto => dto.BookedRoom, opt => opt.Ignore())
					.ForMember(dto => dto.Status, opt => opt.Ignore())
					.AfterMap((src, dest) =>
					{
						dest.NewGuest = new GuestDTO
						{
							Id = src.NewGuest.Id,
							FirstName = src.NewGuest.FirstName,
							LastName = src.NewGuest.LastName,
							Patronymic = src.NewGuest.Patronymic,
							Email = src.NewGuest.Email,
							Birthday = src.NewGuest.Birthday,
							Address = src.NewGuest.Address,
							City = src.NewGuest.City,
							Country = src.NewGuest.Country
						};
						dest.BookedRoom = new RoomDTO
						{
							Id = src.BookedRoom.Id,
							RoomNumber = src.BookedRoom.RoomNumber,
							RoomRate = src.BookedRoom.RoomRate,
							HotelId = src.BookedRoom.HotelId,
							StatusId = src.BookedRoom.StatusId,
							TypeId = src.BookedRoom.TypeId
						};
						dest.Status = new BookingStatusDTO
						{
							Id = src.Status.Id,
							Description = src.Status.Description,
							Name = src.Status.Name
						};
					})
				)
				.CreateMapper();
			toEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, Booking>()).CreateMapper();
		}

		public IEnumerable<BookingDTO> GetAll()
		{
			return toDtoMapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(database.Bookings.GetAll());
		}

		public BookingDTO Get(int id)
		{
			return toDtoMapper.Map<Booking, BookingDTO>(database.Bookings.Get(id));
		}

		public void Create(BookingDTO item)
		{
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingStatus, BookingStatusDTO>()).CreateMapper();
			var waitStatus = database.BookingStatuses.Find(status => status.Name == "wait".ToUpper()).Single();

			item.StatusId = waitStatus.Id;

			if (item.StatusId <= 0 || item.GuestId <= 0 || item.RoomId <= 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (item.DateFrom.CompareTo(item.DateTo) >= 0)
			{
				throw new ArgumentException("Start date must be less than end date");
			}

			if (item.AdultQuantity <= 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			if (item.ChildQuantity < 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			Booking newBooking = toEntityMapper.Map<BookingDTO, Booking>(item);
			
			database.Bookings.Create(newBooking);
			database.Save();
		}

		public void Update(BookingDTO item)
		{
			Booking updatingBooking = database.Bookings.Get(item.Id);

			if (updatingBooking == null)
			{
				throw new RecordNotFoundException(updatingBooking.Id, typeof(Booking));
			}

			updatingBooking = toEntityMapper.Map<BookingDTO, Booking>(item);

			database.Bookings.Update(updatingBooking);
			database.Save();
		}

		public void Delete(int id)
		{
			Booking deletingBooking = database.Bookings.Get(id);

			if (deletingBooking == null)
			{
				throw new RecordNotFoundException(deletingBooking.Id, typeof(Room));
			}

			database.Bookings.Delete(deletingBooking.Id);
			database.Save();
		}

		public IEnumerable<BookingDTO> GetAwaitingBookings()
		{
			BookingStatus waitStatus = database.BookingStatuses
				.GetAll()
				.SingleOrDefault(status => status.Name.Contains("WAIT".ToUpper()));
			
			if (waitStatus == null)
			{
				throw new RecordNotFoundException(typeof(BookingStatus));
			}

			var guestBookings = database.Bookings 
				.GetAll()
				.Where(booking => booking.StatusId == waitStatus.Id);

			return toDtoMapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(guestBookings);
		}

		public IEnumerable<BookingDTO> GetActiveBookings()
		{
			BookingStatus checkInStatus = database.BookingStatuses
				.GetAll()
				.SingleOrDefault(status => status.Name.Contains("CHECK_IN".ToUpper()));

			if (checkInStatus == null)
			{
				throw new RecordNotFoundException(typeof(BookingStatus));
			}

			var guestBookings = database.Bookings
				.GetAll()
				.Where(booking => booking.StatusId == checkInStatus.Id);

			return toDtoMapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(guestBookings);
		}

		public IEnumerable<BookingDTO> GetGuestBookings(string email)
		{
			Guest guest = database.Guests.Find(g => g.Email == email).Single();
			return toDtoMapper.Map<IEnumerable<Booking>, IEnumerable<BookingDTO>>(database.Bookings.Find(booking => booking.GuestId == guest.Id));
		}
	}
}
