using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class RoomStatusRepository : IRepository<RoomStatus>
	{
		private readonly HotelContext Database;

		public RoomStatusRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<RoomStatus> GetAll()
		{
			return Database.RoomStatuses;
		}

		public RoomStatus Get(int id)
		{
			return Database.RoomStatuses.Find(id);
		}

		public IEnumerable<RoomStatus> Find(Func<RoomStatus, bool> predicate)
		{
			return Database.RoomStatuses.Where(predicate);
		}

		public void Create(RoomStatus item)
		{
			Database.RoomStatuses.Add(item);
		}

		public void Update(RoomStatus item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			RoomStatus roomStaff = Get(id);
			if (roomStaff != null)
				Database.RoomStatuses.Remove(roomStaff);
		}
	}
}
