using System.Collections.Generic;

namespace HotelManagement.BLL.DTO
{
	public class RoomDTO
	{
		public int Id { get; set; }

		public int HotelId { get; set; }
		
		public virtual HotelDTO RoomHotel { get; set; }

		public int TypeId { get; set; }

		public virtual RoomTypeDTO TypeOfRoom { get; set; }

		public int StatusId { get; set; }

		public virtual RoomStatusDTO StatusOfRoom { get; set; }

		public string RoomNumber { get; set; }

		public decimal RoomRate { get; set; }

		public virtual ICollection<BookingDTO> Bookings { get; set; }
	}
}
