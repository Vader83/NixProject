using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.BLL.Exceptions
{
	public class RecordAlreadyExistsException : Exception
	{
		public RecordAlreadyExistsException(int id, Type entityType)
			: base($"This record already exists in {entityType.Name} table under id = {id}")
		{ }

	}
}
