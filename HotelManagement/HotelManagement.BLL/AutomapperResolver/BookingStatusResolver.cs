using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelManagement.BLL.DTO;
using HotelManagement.DAL.Entities;

namespace HotelManagement.BLL.AutomapperResolver
{
	public class BookingStatusDTOResolver : IValueResolver<Booking, BookingDTO, BookingStatusDTO>
	{
		private IMapper toDtoMapper;

		public BookingStatusDTOResolver()
		{
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<BookingStatus, BookingStatusDTO>()).CreateMapper();
		}

		public BookingStatusDTO Resolve(Booking source, BookingDTO destination, BookingStatusDTO destMember, ResolutionContext context)
		{
			return toDtoMapper.Map<BookingStatus, BookingStatusDTO>(source.Status);
		}
	}
}
