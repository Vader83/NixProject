namespace HotelManagement.BLL.DTO
{
	public class HotelFacilityDTO
	{
		public int Id { get; set; }

		public int? FacilityId { get; set; }

		public virtual FacilityDTO FacilityOfHotel { get; set; }

		public int? HotelId { get; set; }

		public virtual HotelDTO HotelWithFacility { get; set; }

	}
}
