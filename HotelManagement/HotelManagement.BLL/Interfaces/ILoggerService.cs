using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.BLL.DTO;

namespace HotelManagement.BLL.Interfaces
{
	public interface ILoggerService : IDataService<LoggerDTO>
	{
		void WriteLog(string userId, string username, string action);
	}
}
