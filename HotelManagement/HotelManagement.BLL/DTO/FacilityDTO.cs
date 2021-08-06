namespace HotelManagement.BLL.DTO
{
	public class FacilityDTO
	{
		public int Id { get; set; }

		public int CategoryId { get; set; }
		
		public virtual FacilitiesCategoryDTO Category { get; set; }

		public string Name { get; set; }

	}
}
