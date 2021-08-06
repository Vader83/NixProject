using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DAL.Entities
{
	public class Booking
	{
		[Key]
		public int Id { get; set; }

		public int GuestId { get; set; }

		[ForeignKey("GuestId")]
		public virtual Guest NewGuest { get; set; }

		public int RoomId { get; set; }

		[ForeignKey("RoomId")]
		public virtual Room BookedRoom { get; set; }

		public int StatusId { get; set; }

		[ForeignKey("StatusId")]
		public virtual BookingStatus Status { get; set; }

		public DateTime DateFrom { get; set; }

		public DateTime DateTo { get; set; }

		public int AdultQuantity { get; set; }

		public int ChildQuantity { get; set; }
	}
}
