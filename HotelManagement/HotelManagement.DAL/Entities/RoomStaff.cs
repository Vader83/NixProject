using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DAL.Entities
{
	public class RoomStaff
	{
		[Key]
		public int Id { get; set; }

		public int? RoomId { get; set; }
	
		[ForeignKey("RoomId")]
		public virtual Room ServicingRoom { get; set; }

		public int EmployeeId { get; set; }

		[ForeignKey("EmployeeId")]
		public virtual Employee ServicingStaff { get; set; }

		public string Description { get; set; }
	}
}
