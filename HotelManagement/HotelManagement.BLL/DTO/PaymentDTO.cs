using System;

namespace HotelManagement.BLL.DTO
{
	public class PaymentDTO
	{
		public int Id { get; set; }

		public int BookingId { get; set; }

		public virtual BookingDTO BookingPayment { get; set; }

		public int GuestId { get; set; }

		public virtual GuestDTO Payer { get; set; }

		public int StatusId { get; set; }

		public virtual PaymentStatusDTO Status { get; set; }

		public string CreditCardNumber { get; set; }

		public DateTime? PaymentDay { get; set; }

		public decimal Total { get; set; }
	}
}
