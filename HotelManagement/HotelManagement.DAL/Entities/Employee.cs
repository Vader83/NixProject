using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DAL.Entities
{
	public class Employee
	{
		[Key]
		public int Id { get; set; }

		public int PositionId { get; set; }

		[ForeignKey("PositionId")]
		public virtual Position EmployeePosition { get; set; }

		public int HotelId { get; set; }

		[ForeignKey("HotelId")]
		public virtual Hotel WorkPlace { get; set; }

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
