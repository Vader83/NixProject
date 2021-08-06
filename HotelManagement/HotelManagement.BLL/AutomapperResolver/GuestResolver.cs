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
	public class GuestDTOResolver : IValueResolver<Booking, BookingDTO, GuestDTO>
	{
		private IMapper toDtoMapper;

		public GuestDTOResolver()
		{
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Guest, GuestDTO>()).CreateMapper();
		}

		public GuestDTO Resolve(Booking source, BookingDTO destination, GuestDTO destMember, ResolutionContext context)
		{
			return toDtoMapper.Map<Guest, GuestDTO>(source.NewGuest);
		}
	}
}
