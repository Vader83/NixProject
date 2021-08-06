using System.Collections.Generic;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Responses;

namespace HotelManagement.BLL.Interfaces
{
	public interface IHotelService : IDataService<HotelDTO>
	{
		IEnumerable<RevenueResponse> GetRevenueByMonth(int hotelId, int month);
	}
}
