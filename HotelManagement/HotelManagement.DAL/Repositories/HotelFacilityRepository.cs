using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class HotelFacilityRepository : IRepository<HotelFacility>
	{
		private readonly HotelContext Database;

		public HotelFacilityRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<HotelFacility> GetAll()
		{
			return Database.HotelFacilities;
		}

		public HotelFacility Get(int id)
		{
			return Database.HotelFacilities.Find(id);
		}

		public IEnumerable<HotelFacility> Find(Func<HotelFacility, bool> predicate)
		{
			return Database.HotelFacilities.Where(predicate);
		}

		public void Create(HotelFacility item)
		{
			Database.HotelFacilities.Add(item);
		}

		public void Update(HotelFacility item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			HotelFacility hotelFacility = Get(id);
			if (hotelFacility != null)
				Database.HotelFacilities.Remove(hotelFacility);
		}
	}
}
