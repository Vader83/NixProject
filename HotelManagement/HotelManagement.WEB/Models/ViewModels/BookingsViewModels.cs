using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using HotelManagement.BLL.DTO;

namespace HotelManagement.WEB.Models
{
	public class ActualBookingModel
	{
		public string FullName { get; set; }

		public int GuestId { get; set; }

		public string RoomNumber { get; set; }

		public DateTime DateFrom { get; set; }

		public DateTime DateTo { get; set; }

		public int AdultQuantity { get; set; }

		public int ChildQuantity { get; set; }

		public string TextToDisplay
		{
			get
			{
				return $"FN: {FullName}; Room: {RoomNumber}; Period: {DateFrom.ToShortDateString()} - {DateTo.ToShortDateString()}; A: {AdultQuantity}; Ch: {ChildQuantity}";
			}
		}
	}

	public class CheckBookingModel
	{
		public int GuestId { get; set; }
	}

	public class CreateBookingModel
	{
		public int GuestId { get; set; }

		public int RoomId { get; set; }

		public string Email { get; set; }

		[DisplayName("From")]
		public DateTime DateFrom { get; set; }

		[DisplayName("To")]
		public DateTime DateTo { get; set; }

		[DisplayName("Adult quantity")]
		public int AdultQuantity { get; set; }

		[DisplayName("Children quantity")]
		public int ChildQuantity { get; set; }
	}

	public class BookingReturnModel
	{
		public string FullName { get; set; }

		public string RoomNumber { get; set; }

		public DateTime DateFrom { get; set; }

		public DateTime DateTo { get; set; }

		public int AdultQuantity { get; set; }

		public int ChildQuantity { get; set; }

		public string Status { get; set; }
	}
}