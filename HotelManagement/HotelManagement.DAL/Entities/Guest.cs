using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DAL.Entities
{
	public class Guest
	{
		[Key]
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Patronymic { get; set; }

		public string Email { get; set; }

		public DateTime Birthday { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string Country { get; set; }

		public virtual ICollection<Payment> Payments { get; set; }
		public virtual ICollection<Booking> Bookings { get; set; }
	}
}
