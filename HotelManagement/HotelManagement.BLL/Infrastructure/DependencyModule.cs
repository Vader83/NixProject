using HotelManagement.BLL.Interfaces;
using HotelManagement.BLL.Services;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;
using HotelManagement.DAL.Repositories;
using Ninject.Modules;

namespace HotelManagement.BLL.Infrastructure
{
	public class DependencyModule : NinjectModule
	{
		private string connectionString;

		public DependencyModule(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public override void Load()
		{
			Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
		}
    }

	public class ModuleBundle : NinjectModule
	{
		public override void Load()
		{
			Bind<IBookingService>().To<BookingService>();
			Bind<IGuestService>().To<GuestService>();
			Bind<IHotelService>().To<HotelService>();
			Bind<IRoomService>().To<RoomService>();
			Bind<IEmployeeService>().To<EmployeeService>();
			Bind<ILoggerService>().To<LoggerService>();
		}
	}

	public class BookingModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IBookingService>().To<BookingService>();
		}
	}

	public class GuestModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IGuestService>().To<GuestService>();
		}
	}

	public class HotelModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IHotelService>().To<HotelService>();
		}
	}

	public class RoomModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IRoomService>().To<RoomService>();
		}
	}
	
	public class EmployeeModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IEmployeeService>().To<EmployeeService>();
		}
	}

	public class LoggerModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ILoggerService>().To<LoggerService>();
		}
	}
}
