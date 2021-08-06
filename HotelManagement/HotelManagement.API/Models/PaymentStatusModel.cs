using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.API.Models
{
	public class PaymentStatusModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}