using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class RoomStaffRepository : IRepository<RoomStaff>
	{
		private readonly HotelContext Database;

		public RoomStaffRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<RoomStaff> GetAll()
		{
			return Database.RoomStaffs;
		}

		public RoomStaff Get(int id)
		{
			return Database.RoomStaffs.Find(id);
		}

		public IEnumerable<RoomStaff> Find(Func<RoomStaff, bool> predicate)
		{
			return Database.RoomStaffs.Where(predicate);
		}

		public void Create(RoomStaff item)
		{
			Database.RoomStaffs.Add(item);
		}

		public void Update(RoomStaff item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			RoomStaff roomStaff = Get(id);
			if (roomStaff != null)
				Database.RoomStaffs.Remove(roomStaff);
		}
	}
}
