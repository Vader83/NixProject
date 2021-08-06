using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.API.Models
{
	public class BookingModel
	{
		public int Id { get; set; }

		public int GuestId { get; set; }

		//public virtual GuestModel NewGuest { get; set; }

		public int RoomId { get; set; }

		//public virtual RoomModel BookedRoom { get; set; }

		public int StatusId { get; set; }

		//public virtual BookingStatusModel Status { get; set; }

		public DateTime DateFrom { get; set; }

		public DateTime DateTo { get; set; }

		public int AdultQuantity { get; set; }

		public int ChildQuantity { get; set; }
	}
}