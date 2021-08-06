using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class BookingRepository : IRepository<Booking>
	{
		private readonly HotelContext Database;

		public BookingRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<Booking> GetAll()
		{
			return Database.Bookings;
		}

		public Booking Get(int id)
		{
			return Database.Bookings.Find(id);
		}

		public IEnumerable<Booking> Find(Func<Booking, bool> predicate)
		{
			return Database.Bookings.Where(predicate);
		}
		
		public void Create(Booking item)
		{
			Database.Bookings.Add(item);
		}

		public void Update(Booking item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Booking booking = Get(id);
			if (booking != null)
				Database.Bookings.Remove(booking);
		}
	}
}
