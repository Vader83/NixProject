using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using HotelManagement.API.Models;

namespace HotelManagement.API.Utils.Validators
{
	public class RoomValidator : AbstractValidator<RoomModel>
	{
		public RoomValidator()
		{
			RuleFor(room => room.HotelId).GreaterThanOrEqualTo(1).WithMessage("Hotel Id is required");
			RuleFor(room => room.StatusId).GreaterThanOrEqualTo(1).WithMessage("Status Id is required");
			RuleFor(room => room.TypeId).GreaterThanOrEqualTo(1).WithMessage("Type Id is required");
			RuleFor(room => room.RoomNumber).NotEmpty().WithMessage("Room number is required");
			RuleFor(room => room.RoomRate).GreaterThanOrEqualTo(1m).WithMessage("Room rate can't be negative or zero");
		}
	}
}