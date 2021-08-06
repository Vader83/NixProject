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
	public class RoomTypeResolver : IValueResolver<Room, RoomDTO, RoomTypeDTO>
	{
		private IMapper toDtoMapper;

		public RoomTypeResolver()
		{
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomType, RoomTypeDTO>()).CreateMapper();
		}

		public RoomTypeDTO Resolve(Room source, RoomDTO destination, RoomTypeDTO destMember, ResolutionContext context)
		{
			return toDtoMapper.Map<RoomType, RoomTypeDTO>(source.TypeOfRoom);
		}
	}
}
