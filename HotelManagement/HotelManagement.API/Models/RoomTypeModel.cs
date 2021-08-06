using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.API.Models
{
	public class RoomTypeModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int DoubleBedCount { get; set; }

		public int SingleBedCount { get; set; }
	}
}