using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.BLL.Exceptions
{
	public class RecordNotFoundException : Exception
	{
		public RecordNotFoundException(int id, Type entityType)
			:base($"Record was not found in {entityType.Name} table by id = {id}")
		{ }

		public RecordNotFoundException(Type entityType)
			: base($"Record was not found in {entityType.Name} table by filter")
		{ }
	}
}
