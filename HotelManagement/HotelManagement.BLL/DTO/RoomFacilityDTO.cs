namespace HotelManagement.BLL.DTO
{
	public class RoomFacilityDTO
	{
		public int Id { get; set; }

		public int? FacilityId { get; set; }

		public virtual FacilityDTO FacilityOfRoom { get; set; }

		public int? RoomId { get; set; }

		public virtual RoomDTO RoomWithFacility { get; set; }
	}
}
