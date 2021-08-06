using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class BookingStatusRepository : IRepository<BookingStatus>
	{
		private readonly HotelContext Database;

		public BookingStatusRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<BookingStatus> GetAll()
		{
			return Database.BookingStatuses;
		}

		public BookingStatus Get(int id)
		{
			return Database.BookingStatuses.Find(id);
		}

		public IEnumerable<BookingStatus> Find(Func<BookingStatus, bool> predicate)
		{
			return Database.BookingStatuses.Where(predicate);
		}

		public void Create(BookingStatus item)
		{
			Database.BookingStatuses.Add(item);
		}

		public void Update(BookingStatus item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			BookingStatus bookingStatus = Get(id);
			if (bookingStatus != null)
				Database.BookingStatuses.Remove(bookingStatus);
		}
	}
}
