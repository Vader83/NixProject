using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagement.WEB.Models
{
	public class GuestReturnModel
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Patronymic { get; set; }

		public string Email { get; set; }

		public DateTime Birthday { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string Country { get; set; }

		public string FullName => LastName + " " + FirstName + " " + Patronymic;

	}

	public class GuestCreateModel
	{
		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[Display(Name = "Patronymic")]
		public string Patronymic { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[Display(Name = "Birthday")]
		public DateTime Birthday { get; set; }

		[Required]
		[Display(Name = "Address")]
		public string Address { get; set; }

		[Required]
		[Display(Name = "City")]
		public string City { get; set; }

		[Required]
		[Display(Name = "Country")]
		public string Country { get; set; }

	}

	public class GuestUpdateModel
	{
		public int Id { get; set; }

		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Display(Name = "Patronymic")]
		public string Patronymic { get; set; }

		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "Birthday")]
		public DateTime Birthday { get; set; }

		[Display(Name = "Address")]
		public string Address { get; set; }

		[Display(Name = "City")]
		public string City { get; set; }

		[Display(Name = "Country")]
		public string Country { get; set; }

	}
}
