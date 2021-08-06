using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class RoomFacilityRepository : IRepository<RoomFacility>
	{
		private readonly HotelContext Database;

		public RoomFacilityRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<RoomFacility> GetAll()
		{
			return Database.RoomFacilities;
		}

		public RoomFacility Get(int id)
		{
			return Database.RoomFacilities.Find(id);
		}

		public IEnumerable<RoomFacility> Find(Func<RoomFacility, bool> predicate)
		{
			return Database.RoomFacilities.Where(predicate);
		}

		public void Create(RoomFacility item)
		{
			Database.RoomFacilities.Add(item);
		}

		public void Update(RoomFacility item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			RoomFacility roomFacility = Get(id);
			if (roomFacility != null)
				Database.RoomFacilities.Remove(roomFacility);
		}
	}
}
