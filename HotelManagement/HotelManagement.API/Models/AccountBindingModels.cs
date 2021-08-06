using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagement.API.Models
{
	public class CreateUserBindingModel
	{
		[Required]
		[Display(Name = "First name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last name")]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Display(Name = "Role Name")]
		public string RoleName { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}

	public class ChangeUserPasswordBindingModel
	{
		[Required]
		[DisplayName("Current password")]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[Required]
		[DisplayName("New password")]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		public string NewPassword { get; set; }

		[Required]
		[DisplayName("Confirm new password")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmNewPassword { get; set; }
	}
}