using System;
using System.Collections.Generic;
using AutoMapper;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Exceptions;
using HotelManagement.BLL.Interfaces;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.BLL.Services
{
	class LoggerService : ILoggerService
	{
		private IUnitOfWork database;
		private IMapper toDtoMapper;
		private IMapper toEntityMapper;

		public LoggerService(IUnitOfWork uow)
		{
			database = uow;
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Logger, LoggerDTO>()).CreateMapper();
			toEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<LoggerDTO, Logger>()).CreateMapper();
		}

		public IEnumerable<LoggerDTO> GetAll()
		{
			return toDtoMapper.Map<IEnumerable<Logger>, IEnumerable<LoggerDTO>>(database.Loggers.GetAll());
		}

		public LoggerDTO Get(int id)
		{
			return toDtoMapper.Map<Logger, LoggerDTO>(database.Loggers.Get(id));
		}

		public void Create(LoggerDTO item)
		{
			Logger newLogger = toEntityMapper.Map<LoggerDTO, Logger>(item);
			database.Loggers.Create(newLogger);
			database.Save();
		}

		public void Update(LoggerDTO item)
		{
			Logger updatingLogger = database.Loggers.Get(item.Id);

			if (updatingLogger== null)
			{
				throw new RecordNotFoundException(item.Id, typeof(Logger));
			}

			updatingLogger = toEntityMapper.Map<LoggerDTO, Logger>(item);

			database.Loggers.Update(updatingLogger);
			database.Save();
		}

		public void Delete(int id)
		{
			Logger deletingLogger = database.Loggers.Get(id);

			if (deletingLogger == null)
			{
				throw new RecordNotFoundException(id, typeof(Logger));
			}

			database.Loggers.Delete(id);
			database.Save();
		}

		public void WriteLog(string userId, string username, string action)
		{
			Logger newLogger = new Logger
			{
				UserId = userId,
				Username = username,
				Action = action,
				ActionTime = DateTime.Now
			};

			database.Loggers.Create(newLogger);
			database.Save();
		}
	}
}
