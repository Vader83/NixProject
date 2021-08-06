using System;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class EFUnitOfWork : IUnitOfWork
	{
		private HotelContext Database;

		private BookingRepository _bookingsRepository;
		private BookingStatusRepository _bookingStatusesRepository;
		private EmployeeRepository _employeesRepository;
		private FacilitiesCategoryRepository _facilitiesCategoriesRepository;
		private FacilityRepository _facilitiesRepository;
		private GuestRepository _guestsRepository;
		private HotelRepository _hotelsRepository;
		private HotelFacilityRepository _hotelFacilitiesRepository;
		private PaymentRepository _paymentsRepository;
		private PaymentStatusRepository _paymentStatusesRepository;
		private PositionRepository _positionsRepository;
		private RoomRepository _roomsRepository;
		private RoomFacilityRepository _roomFacilitiesRepository;
		private RoomStaffRepository _roomStaffsRepository;
		private RoomStatusRepository _roomStatusesRepository;
		private RoomTypeRepository _roomTypesRepository;
		private LoggerRepository _loggerRepository;

		public IRepository<Booking> Bookings => _bookingsRepository ?? (_bookingsRepository = new BookingRepository(Database));
		public IRepository<BookingStatus> BookingStatuses => _bookingStatusesRepository ?? (_bookingStatusesRepository = new BookingStatusRepository(Database));
		public IRepository<Employee> Employees => _employeesRepository ?? (_employeesRepository = new EmployeeRepository(Database));
		public IRepository<FacilitiesCategory> FacilitiesCategories => _facilitiesCategoriesRepository ?? (_facilitiesCategoriesRepository = new FacilitiesCategoryRepository(Database));
		public IRepository<Facility> Facilities => _facilitiesRepository ?? (_facilitiesRepository = new FacilityRepository(Database));
		public IRepository<Guest> Guests => _guestsRepository ?? (_guestsRepository = new GuestRepository(Database));
		public IRepository<Hotel> Hotels => _hotelsRepository ?? (_hotelsRepository = new HotelRepository(Database));
		public IRepository<HotelFacility> HotelFacilities => _hotelFacilitiesRepository ?? (_hotelFacilitiesRepository = new HotelFacilityRepository(Database));
		public IRepository<Payment> Payments => _paymentsRepository ?? (_paymentsRepository = new PaymentRepository(Database));
		public IRepository<PaymentStatus> PaymentStatuses => _paymentStatusesRepository ?? (_paymentStatusesRepository = new PaymentStatusRepository(Database));
		public IRepository<Position> Positions => _positionsRepository ?? (_positionsRepository = new PositionRepository(Database));
		public IRepository<Room> Rooms => _roomsRepository ?? (_roomsRepository = new RoomRepository(Database));
		public IRepository<RoomFacility> RoomFacilities => _roomFacilitiesRepository ?? (_roomFacilitiesRepository = new RoomFacilityRepository(Database));
		public IRepository<RoomStaff> RoomStaffs => _roomStaffsRepository ?? (_roomStaffsRepository = new RoomStaffRepository(Database));
		public IRepository<RoomStatus> RoomStatuses => _roomStatusesRepository ?? (_roomStatusesRepository = new RoomStatusRepository(Database));
		public IRepository<RoomType> RoomTypes => _roomTypesRepository ?? (_roomTypesRepository = new RoomTypeRepository(Database));
		public IRepository<Logger> Loggers => _loggerRepository ?? (_loggerRepository = new LoggerRepository(Database));

		public EFUnitOfWork(string connectionString)
		{
			Database = new HotelContext(connectionString);
		}
		
		public void Save()
		{
			Database.SaveChanges();
		}

		public void Dispose()
		{
			Database.Dispose();
		}
	}
}
