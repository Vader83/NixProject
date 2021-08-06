using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class PaymentStatusRepository : IRepository<PaymentStatus>
	{
		private readonly HotelContext Database;

		public PaymentStatusRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<PaymentStatus> GetAll()
		{
			return Database.PaymentStatuses;
		}

		public PaymentStatus Get(int id)
		{
			return Database.PaymentStatuses.Find(id);
		}

		public IEnumerable<PaymentStatus> Find(Func<PaymentStatus, bool> predicate)
		{
			return Database.PaymentStatuses.Where(predicate);
		}

		public void Create(PaymentStatus item)
		{
			Database.PaymentStatuses.Add(item);
		}

		public void Update(PaymentStatus item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			PaymentStatus paymentStatus = Get(id);
			if (paymentStatus != null)
				Database.PaymentStatuses.Remove(paymentStatus);
		}
	}
}
