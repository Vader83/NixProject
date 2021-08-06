using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using HotelManagement.API.Models;

namespace HotelManagement.API.Utils.Validators
{
	public class BookingValidator : AbstractValidator<BookingModel>
	{
		public BookingValidator()
		{
			RuleFor(book => book.GuestId).GreaterThanOrEqualTo(1).WithMessage("Guest Id is required");
			RuleFor(book => book.RoomId).GreaterThanOrEqualTo(1).WithMessage("Room Id is required");
			RuleFor(book => book.StatusId).GreaterThanOrEqualTo(1).WithMessage("Status Id is required");
			RuleFor(book => book.AdultQuantity).GreaterThanOrEqualTo(1).WithMessage("At least one adult is required");
			RuleFor(book => book.ChildQuantity).GreaterThanOrEqualTo(0).WithMessage("Child quantity can't be negative");
			RuleFor(book => book).Must(model => IsValidDate(model.DateFrom,model.DateTo)).WithMessage("Start date must be less than end date");
		}

		private bool IsValidDate(DateTime dateFrom, DateTime dateTo) => dateFrom.CompareTo(dateTo) < 0 ? true : false;
	}
}