using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DAL.Entities
{
	public class Room
	{
		[Key] 
		public int Id { get; set; }

		public int HotelId { get; set; }
		
		[ForeignKey("HotelId")] 
		public virtual Hotel RoomHotel { get; set; }

		public int TypeId { get; set; }

		[ForeignKey("TypeId")]
		public virtual RoomType TypeOfRoom { get; set; }

		public int StatusId { get; set; }

		[ForeignKey("StatusId")]
		public virtual RoomStatus StatusOfRoom { get; set; }

		public string RoomNumber { get; set; }

		public decimal RoomRate { get; set; }

		public virtual ICollection<Booking> Bookings { get; set; }
	}
}
