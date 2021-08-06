using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class HotelRepository : IRepository<Hotel>
	{
		private readonly HotelContext Database;

		public HotelRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<Hotel> GetAll()
		{
			return Database.Hotels;
		}

		public Hotel Get(int id)
		{
			return Database.Hotels.Find(id);
		}

		public IEnumerable<Hotel> Find(Func<Hotel, bool> predicate)
		{
			return Database.Hotels.Where(predicate);
		}

		public void Create(Hotel item)
		{
			Database.Hotels.Add(item);
		}

		public void Update(Hotel item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Hotel hotel = Get(id);
			if (hotel != null)
				Database.Hotels.Remove(hotel);
		}
	}
}
