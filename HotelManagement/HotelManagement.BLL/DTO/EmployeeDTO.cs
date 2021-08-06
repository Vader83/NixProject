using System;

namespace HotelManagement.BLL.DTO
{
	public class EmployeeDTO
	{
		public int Id { get; set; }

		public int PositionId { get; set; }

		public virtual PositionDTO EmployeePosition { get; set; }

		public int HotelId { get; set; }

		public virtual HotelDTO WorkPlace { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Patronymic { get; set; }

		public DateTime Birthday { get; set; }

		public string PhoneNumber { get; set; }

		public string EmailAddress { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string Country { get; set; }

		public string Gender { get; set; }

	}
}
