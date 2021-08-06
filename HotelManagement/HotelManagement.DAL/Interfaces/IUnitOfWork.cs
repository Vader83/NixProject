using HotelManagement.DAL.Entities;

namespace HotelManagement.DAL.Interfaces
{
	public interface IUnitOfWork
	{
		IRepository<Booking> Bookings { get; }
		IRepository<BookingStatus> BookingStatuses { get; }
		IRepository<Employee> Employees { get; }
		IRepository<FacilitiesCategory> FacilitiesCategories { get; }
		IRepository<Facility> Facilities { get; }
		IRepository<Guest> Guests { get; }
		IRepository<Hotel> Hotels { get; }
		IRepository<HotelFacility> HotelFacilities { get; }
		IRepository<Payment> Payments { get; }
		IRepository<PaymentStatus> PaymentStatuses { get; }
		IRepository<Position> Positions { get; }
		IRepository<Room> Rooms { get; }
		IRepository<RoomFacility> RoomFacilities { get; }
		IRepository<RoomStaff> RoomStaffs { get; }
		IRepository<RoomStatus> RoomStatuses { get; }
		IRepository<RoomType> RoomTypes { get; }
		IRepository<Logger> Loggers { get; }
		void Save();
		void Dispose();
	}
}
