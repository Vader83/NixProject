using System;
using System.Collections.Generic;
using HotelManagement.BLL.DTO;

namespace HotelManagement.BLL.Interfaces
{
	public interface IRoomService : IDataService<RoomDTO>
	{
		IEnumerable<RoomDTO> EmptyRoomsForDate(DateTime dateFrom, DateTime dateTo, int hotelId);
		IEnumerable<RoomDTO> GetRoomsInHotel(int hotelId);
		IEnumerable<RoomTypeDTO> GetTypes();
		IEnumerable<RoomStatusDTO> GetStatuses();
	}
}
