namespace HotelManagement.BLL.DTO
{
	public class RoomStaffDTO
	{
		public int Id { get; set; }

		public int? RoomId { get; set; }
	
		public virtual RoomDTO ServicingRoom { get; set; }

		public int EmployeeId { get; set; }

		public virtual EmployeeDTO ServicingStaff { get; set; }

		public string Description { get; set; }
	}
}
