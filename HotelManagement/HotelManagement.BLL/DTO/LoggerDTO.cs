using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.BLL.DTO
{
	public class LoggerDTO
	{
		public int Id { get; set; }

		public string UserId { get; set; }

		public string Username { get; set; }

		public string Action { get; set; }

		public DateTime ActionTime { get; set; }
	}
}
