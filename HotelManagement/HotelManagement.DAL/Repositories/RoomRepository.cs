using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class RoomRepository : IRepository<Room>
	{
		private readonly HotelContext Database;

		public RoomRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<Room> GetAll()
		{
			return Database.Rooms;
		}

		public Room Get(int id)
		{
			return Database.Rooms.Find(id);
		}

		public IEnumerable<Room> Find(Func<Room, bool> predicate)
		{
			return Database.Rooms.Where(predicate);
		}

		public void Create(Room item)
		{
			Database.Rooms.Add(item);
		}

		public void Update(Room item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Room room = Get(id);
			if (room != null)
				Database.Rooms.Remove(room);
		}
	}
}
