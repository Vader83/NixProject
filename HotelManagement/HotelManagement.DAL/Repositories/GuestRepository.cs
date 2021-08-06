using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class GuestRepository : IRepository<Guest>
	{
		private readonly HotelContext Database;

		public GuestRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<Guest> GetAll()
		{
			return Database.Guests;
		}

		public Guest Get(int id)
		{
			return Database.Guests.Find(id);
		}

		public IEnumerable<Guest> Find(Func<Guest, bool> predicate)
		{
			return Database.Guests.Where(predicate);
		}

		public void Create(Guest item)
		{
			Database.Guests.Add(item);
		}

		public void Update(Guest item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Guest guest = Get(id);
			if (guest != null)
				Database.Guests.Remove(guest);
		}
	}
}
