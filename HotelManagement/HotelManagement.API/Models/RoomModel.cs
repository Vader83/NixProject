using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.API.Models
{
	public class RoomModel
	{
		public int Id { get; set; }

		public int HotelId { get; set; }

		//public virtual HotelModel RoomHotel { get; set; }

		public int TypeId { get; set; }

		//public virtual RoomTypeModel TypeOfRoom { get; set; }

		public int StatusId { get; set; }

		//public virtual RoomStatusModel StatusOfRoom { get; set; }

		public string RoomNumber { get; set; }

		public decimal RoomRate { get; set; }

		//public virtual ICollection<BookingModel> Bookings { get; set; }
	}
}