using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class FacilityRepository : IRepository<Facility>
	{
		private readonly HotelContext Database;

		public FacilityRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<Facility> GetAll()
		{
			return Database.Facilities;
		}

		public Facility Get(int id)
		{
			return Database.Facilities.Find(id);
		}

		public IEnumerable<Facility> Find(Func<Facility, bool> predicate)
		{
			return Database.Facilities.Where(predicate);
		}

		public void Create(Facility item)
		{
			Database.Facilities.Add(item);
		}

		public void Update(Facility item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Facility facility = Get(id);
			if (facility != null)
				Database.Facilities.Remove(facility);
		}
	}
}
