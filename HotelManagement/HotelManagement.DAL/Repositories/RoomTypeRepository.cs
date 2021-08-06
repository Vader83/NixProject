using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class RoomTypeRepository : IRepository<RoomType>
	{
		private readonly HotelContext Database;

		public RoomTypeRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<RoomType> GetAll()
		{
			return Database.RoomTypes;
		}

		public RoomType Get(int id)
		{
			return Database.RoomTypes.Find(id);
		}

		public IEnumerable<RoomType> Find(Func<RoomType, bool> predicate)
		{
			return Database.RoomTypes.Where(predicate);
		}

		public void Create(RoomType item)
		{
			Database.RoomTypes.Add(item);
		}

		public void Update(RoomType item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			RoomType roomType = Get(id);
			if (roomType != null)
				Database.RoomTypes.Remove(roomType);
		}
	}
}
