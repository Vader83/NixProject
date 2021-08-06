using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DAL.Entities
{
	public class Logger
	{
		[Key]
		public int Id { get; set; }

		public string UserId { get; set; }

		public string Username { get; set; }

		public string Action { get; set; }

		public DateTime ActionTime { get; set; }
		
	}
}
