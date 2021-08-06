using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HotelManagement.BLL.AutomapperResolver;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Exceptions;
using HotelManagement.BLL.Interfaces;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.BLL.Services
{
	public class RoomService : IRoomService
	{
		private IUnitOfWork database;
		private IMapper toDtoMapper;
		private IMapper toEntityMapper;
		private IMapper bookingMapper;

		public RoomService(IUnitOfWork uow)
		{
			database = uow;
			bookingMapper = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()
				.ForMember(dto => dto.BookedRoom, opt => opt.Ignore())
				.ForMember(dto => dto.NewGuest, opt => opt.Ignore())
				.ForMember(dto => dto.Status, opt => opt.Ignore())
				).CreateMapper();

			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()
					.ForMember(dto => dto.Bookings, opt => opt.Ignore())
					.ForMember(dto => dto.RoomHotel, opt => opt.Ignore())
					.ForMember(dto => dto.StatusOfRoom, opt => opt.Ignore())
					.ForMember(dto => dto.TypeOfRoom, opt => opt.Ignore())
					.AfterMap((src, dest) =>
					{
						dest.RoomHotel = new HotelDTO
						{
							Id = src.RoomHotel.Id,
							Name = src.RoomHotel.Name,
							Address = src.RoomHotel.Address,
							Country = src.RoomHotel.Country,
							EmailAddress = src.RoomHotel.EmailAddress,
							City = src.RoomHotel.City,
							PostalCode = src.RoomHotel.PostalCode,
							TollFreeNumber = src.RoomHotel.TollFreeNumber,
							WebSiteAddress = src.RoomHotel.WebSiteAddress
						};

						dest.StatusOfRoom = new RoomStatusDTO
						{
							Name = src.StatusOfRoom.Name,
							Description = src.StatusOfRoom.Description,
							Id = src.StatusOfRoom.Id
						};

						dest.TypeOfRoom = new RoomTypeDTO
						{
							Id = src.TypeOfRoom.Id,
							Name = src.TypeOfRoom.Name,
							SingleBedCount = src.TypeOfRoom.SingleBedCount,
							Description = src.TypeOfRoom.Description,
							DoubleBedCount = src.TypeOfRoom.DoubleBedCount
						};
					})
				)
				.CreateMapper();

			toEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, Room>()
				.ForMember(room => room.Bookings, opt => opt.MapFrom(dto => dto.Bookings)))
				.CreateMapper();
		}

		public IEnumerable<RoomDTO> GetRoomsInHotel(int hotelId)
		{
			Hotel hotel = database.Hotels.Get(hotelId);

			if (hotel == null)
			{
				throw new RecordNotFoundException(hotelId, typeof(Hotel));
			}

			var rooms = database.Rooms.Find(room => room.HotelId == hotelId).ToList();
			var roomsDTo = toDtoMapper.Map<IEnumerable<Room>, IEnumerable<RoomDTO>>(rooms).ToList();

			for (int i = 0; i < roomsDTo.Count; i++)
			{
				roomsDTo[i].Bookings = bookingMapper.Map<ICollection<Booking>, ICollection<BookingDTO>>(rooms[i].Bookings);
			}

			
			return roomsDTo;
		}

		public IEnumerable<RoomDTO> GetAll()
		{
			var rooms = database.Rooms.GetAll().ToList();
			var roomsDTo = toDtoMapper.Map<IEnumerable<Room>, IEnumerable<RoomDTO>>(rooms).ToList();

			for (int i = 0; i < roomsDTo.Count; i++)
			{
				roomsDTo[i].Bookings = bookingMapper.Map<ICollection<Booking>, ICollection<BookingDTO>>(rooms[i].Bookings);
			}

			return roomsDTo;
		}

		public RoomDTO Get(int id)
		{
			var room = toDtoMapper.Map<Room, RoomDTO>(database.Rooms.Get(id));
			
			return room;
		}

		public void Create(RoomDTO item)
		{
			RoomDTO existingRoom = GetRoomsInHotel(item.HotelId)
				.SingleOrDefault(room => room.RoomNumber == item.RoomNumber);

			RoomStatus emptyStatus = database.RoomStatuses.Find(status => status.Name == "empty".ToUpper()).Single();

			if (existingRoom != null)
			{
				throw new RecordAlreadyExistsException(existingRoom.Id, typeof(Room));
			}

			Room newRoom = toEntityMapper.Map<RoomDTO, Room>(item);
			newRoom.StatusId = emptyStatus.Id;

			database.Rooms.Create(newRoom);
			database.Save();
		}

		public void Update(RoomDTO item)
		{
			Room updatingRoom = database.Rooms.Get(item.Id);

			if (updatingRoom == null)
			{
				throw new RecordNotFoundException(updatingRoom.Id, typeof(Room));
			}

			updatingRoom = toEntityMapper.Map<RoomDTO, Room>(item);

			database.Rooms.Update(updatingRoom);
			database.Save();
		}

		public void Delete(int id)
		{
			Room deletingRoom = database.Rooms.Get(id);

			if (deletingRoom == null)
			{
				throw new RecordNotFoundException(deletingRoom.Id, typeof(Room));
			}

			List<RoomFacility> roomFacilities =
				database.RoomFacilities.Find(facility => facility.RoomId == deletingRoom.Id).ToList();

			foreach (var facility in roomFacilities)
			{
				database.RoomFacilities.Delete(facility.Id);
			}

			List<RoomStaff> roomStaffs =
				database.RoomStaffs.Find(staff => staff.RoomId == deletingRoom.Id).ToList();

			foreach (var staff in roomStaffs)
			{
				database.RoomStaffs.Delete(staff.Id);
			}

			database.Save();

			database.Rooms.Delete(deletingRoom.Id);
			database.Save();
		}

		public IEnumerable<RoomDTO> EmptyRoomsForDate(DateTime dateFrom, DateTime dateTo, int hotelId)
		{
			Hotel hotelRevenue = database.Hotels.Get(hotelId);

			if (hotelRevenue == null)
			{
				throw new RecordNotFoundException(hotelId, typeof(Hotel));
			}

			if (dateFrom.CompareTo(DateTime.Today) < 0)
			{
				throw new ArgumentException("Start date must be greater or equal than today");
			}

			if (dateFrom.CompareTo(dateTo) >= 0)
			{
				throw new ArgumentException("Start date must be less than the end date");
			}

			BookingStatus CheckOut = database.BookingStatuses
				.GetAll()
				.SingleOrDefault(status => status.Name.Contains("CHECK_OUT".ToUpper()));

			if (CheckOut == null)
			{
				throw new RecordNotFoundException(typeof(BookingStatus));
			}

			IEnumerable<RoomDTO> rooms = GetRoomsInHotel(hotelId);
			List<RoomDTO> emptyRooms = new List<RoomDTO>();

			foreach (var room in rooms)
			{
				bool isEmptyForDate = true;

				foreach (var roomBooking in room.Bookings.Where(booking => booking.StatusId != CheckOut.Id))
				{
					if (roomBooking.DateFrom > dateTo)
					{
						isEmptyForDate = true;
					}
					else if (roomBooking.DateTo < dateFrom)
					{
						isEmptyForDate = true;
					}
					else
					{
						isEmptyForDate = false;
					}

					if (!isEmptyForDate)
					{
						break;
					}
				}

				if (isEmptyForDate)
				{
					emptyRooms.Add(room);
				}

			}

			return emptyRooms;
		}

		public IEnumerable<RoomTypeDTO> GetTypes()
		{
			var mapper = new MapperConfiguration(cfg=> cfg.CreateMap<RoomType, RoomTypeDTO>()).CreateMapper();
			var types = database.RoomTypes.GetAll().ToList();
			var typesDto = mapper.Map<IEnumerable<RoomType>, IEnumerable<RoomTypeDTO>>(types).ToList();
			
			return typesDto;
		}

		public IEnumerable<RoomStatusDTO> GetStatuses()

		{
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomStatus, RoomStatusDTO>()).CreateMapper();
			var statuses = database.RoomStatuses.GetAll().ToList();
			var statusesDto = mapper.Map<IEnumerable<RoomStatus>, IEnumerable<RoomStatusDTO>>(statuses).ToList();
			
			return statusesDto;
		}
	}
}
