using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	class LoggerRepository : IRepository<Logger>
	{
		private readonly HotelContext Database;

		public LoggerRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<Logger> GetAll()
		{
			return Database.Logger;
		}

		public Logger Get(int id)
		{
			return Database.Logger.Find(id);
		}

		public IEnumerable<Logger> Find(Func<Logger, bool> predicate)
		{
			return Database.Logger.Where(predicate);
		}

		public void Create(Logger item)
		{
			Database.Logger.Add(item);
		}

		public void Update(Logger item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Logger logger = Get(id);
			if (logger != null)
				Database.Logger.Remove(logger);
		}
	}
}
