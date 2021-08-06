using System;

namespace HotelManagement.BLL.DTO
{
	public class BookingDTO
	{
		public int Id { get; set; }

		public int GuestId { get; set; }

		public virtual GuestDTO NewGuest { get; set; }

		public int RoomId { get; set; }

		public virtual RoomDTO BookedRoom { get; set; }

		public int StatusId { get; set; }

		public virtual BookingStatusDTO Status { get; set; }

		public DateTime DateFrom { get; set; }

		public DateTime DateTo { get; set; }

		public int AdultQuantity { get; set; }

		public int ChildQuantity { get; set; }
	}
}
