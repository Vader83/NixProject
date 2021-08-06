using System;
using FluentValidation;
using HotelManagement.API.Models;

namespace HotelManagement.API.Utils.Validators
{
	public class GuestValidator : AbstractValidator<GuestModel>
	{
		public GuestValidator()
		{
			RuleFor(guest => guest.Address).NotEmpty().WithMessage("Address is required");
			RuleFor(guest => guest.City).NotEmpty().WithMessage("Guest city is required");
			RuleFor(guest => guest.Email).NotEmpty().WithMessage("Email is required");
			RuleFor(guest => guest.FirstName).NotEmpty().WithMessage("Guest firstname is required");
			RuleFor(guest => guest.LastName).NotEmpty().WithMessage("Guest lastname is required");
			RuleFor(guest => guest.Patronymic).NotEmpty().WithMessage("Guest patronymic is required");
			RuleFor(guest => DateTime.Today.Year - guest.Birthday.Year).GreaterThanOrEqualTo(18).WithMessage("Guest must be adult");
		}

	}
}