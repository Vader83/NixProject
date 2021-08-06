using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.BLL.Exceptions
{
	public class NotPayedBookingException : Exception
	{
		public NotPayedBookingException(int id)
			: base($"Guest with id = {id} hasn't payed booking")
		{ }
	}
}
