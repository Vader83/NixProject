using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DAL.Entities
{
	public class FacilitiesCategory
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }
	}
}
