using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DAL.Entities
{
	public class Payment
	{
		[Key]
		public int Id { get; set; }

		public int BookingId { get; set; }

		[ForeignKey("BookingId")]
		public virtual Booking BookingPayment { get; set; }

		public int GuestId { get; set; }

		[ForeignKey("GuestId")]
		public virtual Guest Payer { get; set; }

		public int StatusId { get; set; }

		[ForeignKey("StatusId")]
		public virtual PaymentStatus Status { get; set; }

		public string CreditCardNumber { get; set; }

		public DateTime? PaymentDay { get; set; }

		public decimal Total { get; set; }
	}
}
