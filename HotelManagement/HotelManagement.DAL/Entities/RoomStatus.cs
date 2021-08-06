using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DAL.Entities
{
	public class RoomStatus
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		
	}
}
