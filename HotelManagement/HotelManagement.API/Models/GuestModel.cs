using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HotelManagement.API.Models
{
	public class GuestModel
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

		[JsonIgnore]
		public virtual ICollection<PaymentModel> Payments { get; set; }

		[JsonIgnore]
		public virtual ICollection<BookingModel> Bookings { get; set; }
	}
}