using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagement.API.Models
{
	public class PaymentModel
	{
		public int Id { get; set; }

		public int BookingId { get; set; }

		public virtual BookingModel BookingPayment { get; set; }

		public int GuestId { get; set; }

		public virtual GuestModel Payer { get; set; }

		public int StatusId { get; set; }

		public virtual PaymentStatusModel Status { get; set; }

		public string CreditCardNumber { get; set; }

		public DateTime? PaymentDay { get; set; }

		public decimal Total { get; set; }
	}
}