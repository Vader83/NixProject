using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelManagement.BLL.Responses;

namespace HotelManagement.WEB.Models
{
	public class HotelRevenueModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Month { get; set; }
		public List<RevenueResponse> HotelRevenue { get; set; }
	}
}