using System;
using System.Collections.Generic;
using HotelManagement.BLL.DTO;

namespace HotelManagement.BLL.Interfaces
{
	public interface IGuestService : IDataService<GuestDTO>
	{
		IEnumerable<GuestDTO> Find(Func<GuestDTO, bool> predicate);
		IEnumerable<GuestDTO> GetAllHotelGuests(int hotelId);
		void CheckIn(GuestDTO guestIn);
		void CheckOut(GuestDTO guestOut);
	}
}
