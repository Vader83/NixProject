using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DAL.Entities
{
	public class RoomFacility
	{
		[Key]
		public int Id { get; set; }

		public int? FacilityId { get; set; }

		[ForeignKey("FacilityId")]
		public virtual Facility FacilityOfRoom { get; set; }

		public int? RoomId { get; set; }

		[ForeignKey("RoomId")]
		public virtual Room RoomWithFacility { get; set; }
	}
}
