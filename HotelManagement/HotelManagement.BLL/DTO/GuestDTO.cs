using System;
using System.Collections.Generic;

namespace HotelManagement.BLL.DTO
{
	public class GuestDTO
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Patronymic { get; set; }

		public string Email { get; set; }

		public DateTime Birthday { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string Country { get; set; }

		//public virtual ICollection<PaymentDTO> Payments { get; set; }
		//public virtual ICollection<BookingDTO> Bookings { get; set; }
	}
}
