using System.Collections.Generic;
using HotelManagement.BLL.DTO;

namespace HotelManagement.BLL.Interfaces
{
	public interface IBookingService : IDataService<BookingDTO>
	{
		IEnumerable<BookingDTO> GetAwaitingBookings();
		IEnumerable<BookingDTO> GetActiveBookings();
		IEnumerable<BookingDTO> GetGuestBookings(string email);
	}
}
