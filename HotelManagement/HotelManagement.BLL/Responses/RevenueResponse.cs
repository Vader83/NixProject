using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.BLL.Responses
{
	public class RevenueResponse
	{
		public decimal Total { get; set; }
		public DateTime Period { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is RevenueResponse response)
			{
				return this.Period.CompareTo(response.Period) == 0 &&
				       this.Total == response.Total;
			}
			else
			{
				return base.Equals(obj);
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
