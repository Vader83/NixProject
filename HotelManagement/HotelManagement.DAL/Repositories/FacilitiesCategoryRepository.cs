using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class FacilitiesCategoryRepository : IRepository<FacilitiesCategory>
	{
		private readonly HotelContext Database;

		public FacilitiesCategoryRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<FacilitiesCategory> GetAll()
		{
			return Database.FacilitiesCategories;
		}

		public FacilitiesCategory Get(int id)
		{
			return Database.FacilitiesCategories.Find(id);
		}

		public IEnumerable<FacilitiesCategory> Find(Func<FacilitiesCategory, bool> predicate)
		{
			return Database.FacilitiesCategories.Where(predicate);
		}

		public void Create(FacilitiesCategory item)
		{
			Database.FacilitiesCategories.Add(item);
		}

		public void Update(FacilitiesCategory item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			FacilitiesCategory facilitiesCategory = Get(id);
			if (facilitiesCategory != null)
				Database.FacilitiesCategories.Remove(facilitiesCategory);
		}
	}
}
