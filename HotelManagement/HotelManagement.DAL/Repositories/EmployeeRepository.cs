using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HotelManagement.DAL.EF;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.DAL.Repositories
{
	public class EmployeeRepository : IRepository<Employee>
	{
		private readonly HotelContext Database;

		public EmployeeRepository(HotelContext context)
		{
			Database = context;
		}

		public IEnumerable<Employee> GetAll()
		{
			return Database.Employees;
		}

		public Employee Get(int id)
		{
			return Database.Employees.Find(id);
		}

		public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
		{
			return Database.Employees.Where(predicate);
		}

		public void Create(Employee item)
		{
			Database.Employees.Add(item);
		}

		public void Update(Employee item)
		{
			Database.Entry(item).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			Employee employee = Get(id);
			if (employee != null)
				Database.Employees.Remove(employee);
		}
	}
}
