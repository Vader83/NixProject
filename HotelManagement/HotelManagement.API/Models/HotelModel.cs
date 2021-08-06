using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.API.Models
{
	public class HotelModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string Country { get; set; }

		public string PostalCode { get; set; }

		public string TollFreeNumber { get; set; }

		public string WebSiteAddress { get; set; }

		public string EmailAddress { get; set; }
	}
}