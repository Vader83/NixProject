using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DAL.Entities
{
	public class Facility
	{
		[Key]
		public int Id { get; set; }

		public int CategoryId { get; set; }
		
		[ForeignKey("CategoryId")]
		public virtual FacilitiesCategory Category { get; set; }

		public string Name { get; set; }

	}
}
