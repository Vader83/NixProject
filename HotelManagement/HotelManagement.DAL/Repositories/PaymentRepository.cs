using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class PaymentRepository : IRepository<Payment>
	{
		private readonly HotelContext Database;

		public PaymentRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<Payment> GetAll()
		{
			return Database.Payments;
		}

		public Payment Get(int id)
		{
			return Database.Payments.Find(id);
		}

		public IEnumerable<Payment> Find(Func<Payment, bool> predicate)
		{
			return Database.Payments.Where(predicate);
		}

		public void Create(Payment item)
		{
			Database.Payments.Add(item);
		}

		public void Update(Payment item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Payment payment = Get(id);
			if (payment != null)
				Database.Payments.Remove(payment);
		}
	}
}
