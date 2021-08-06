using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DAL.Entities
{
	public class RoomType
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int DoubleBedCount { get; set; }

		public int SingleBedCount { get; set; }
		
		
	}
}
