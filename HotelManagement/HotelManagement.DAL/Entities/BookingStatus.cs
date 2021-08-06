using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DAL.Entities
{
	public class BookingStatus
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
