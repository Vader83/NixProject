using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DAL.Entities
{
	public class HotelFacility
	{
		[Key]
		public int Id { get; set; }

		public int? FacilityId { get; set; }

		[ForeignKey("FacilityId")]
		public virtual Facility FacilityOfHotel { get; set; }

		public int? HotelId { get; set; }

		[ForeignKey("HotelId")]
		public virtual Hotel HotelWithFacility { get; set; }

	}
}
