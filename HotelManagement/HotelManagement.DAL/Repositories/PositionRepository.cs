using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class PositionRepository : IRepository<Position>
	{
		private readonly HotelContext Database;

		public PositionRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<Position> GetAll()
		{
			return Database.Positions;
		}

		public Position Get(int id)
		{
			return Database.Positions.Find(id);
		}

		public IEnumerable<Position> Find(Func<Position, bool> predicate)
		{
			return Database.Positions.Where(predicate);
		}

		public void Create(Position item)
		{
			Database.Positions.Add(item);
		}

		public void Update(Position item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Position position = Get(id);
			if (position != null)
				Database.Positions.Remove(position);
		}
	}
}
