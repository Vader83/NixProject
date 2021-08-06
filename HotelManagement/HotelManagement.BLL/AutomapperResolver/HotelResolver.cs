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
	public class HotelResolver : IValueResolver<Room, RoomDTO, HotelDTO>
	{
		private IMapper toDtoMapper;

		public HotelResolver()
		{
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()).CreateMapper();
		}

		public HotelDTO Resolve(Room source, RoomDTO destination, HotelDTO destMember, ResolutionContext context)
		{
			return toDtoMapper.Map<Hotel, HotelDTO>(source.RoomHotel);
		}
	}
}
