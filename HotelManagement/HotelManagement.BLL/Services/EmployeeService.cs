using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Exceptions;
using HotelManagement.BLL.Interfaces;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.BLL.Services
{
	class EmployeeService : IEmployeeService
	{
		private IUnitOfWork database;
		private IMapper toDtoMapper;
		private IMapper toEntityMapper;

		public EmployeeService(IUnitOfWork uow)
		{
			database = uow;
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()
					.ForMember(dto => dto.WorkPlace, opt => opt.Ignore())
					.ForMember(dto => dto.EmployeePosition, opt => opt.Ignore())
				)
				.CreateMapper();
			toEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, Employee>()).CreateMapper();
		}

		public IEnumerable<EmployeeDTO> GetAll()
		{
			return toDtoMapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(database.Employees.GetAll());
		}

		public EmployeeDTO Get(int id)
		{
			return toDtoMapper.Map<Employee, EmployeeDTO>(database.Employees.Get(id));
		}

		public void Create(EmployeeDTO item)
		{
			Employee newEmployee = toEntityMapper.Map<EmployeeDTO, Employee>(item);

			database.Employees.Create(newEmployee);
			database.Save();
		}

		public void Update(EmployeeDTO item)
		{
			Employee updatingEmployee = database.Employees.Get(item.Id);

			if (updatingEmployee == null)
			{
				throw new RecordNotFoundException(updatingEmployee.Id, typeof(Employee));
			}

			updatingEmployee = toEntityMapper.Map<EmployeeDTO, Employee>(item);

			database.Employees.Update(updatingEmployee);
			database.Save();
		}

		public void Delete(int id)
		{
			Employee deletingEmployee = database.Employees.Get(id);

			if (deletingEmployee == null)
			{
				throw new RecordNotFoundException(deletingEmployee.Id, typeof(Employee));
			}

			database.Employees.Delete(deletingEmployee.Id);
			database.Save();
		}

		public EmployeeDTO FindByEmail(string email)
		{
			return toDtoMapper.Map<Employee, EmployeeDTO>(database.Employees.Find(employee => employee.EmailAddress == email).SingleOrDefault());
		}
	}
}
