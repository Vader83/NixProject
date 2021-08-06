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
	public class RoomStatusResolver : IValueResolver<Room, RoomDTO, RoomStatusDTO>
	{
		private IMapper toDtoMapper;

		public RoomStatusResolver()
		{
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<RoomStatus, RoomStatusDTO>()).CreateMapper();
		}

		public RoomStatusDTO Resolve(Room source, RoomDTO destination, RoomStatusDTO destMember, ResolutionContext context)
		{
			return toDtoMapper.Map<RoomStatus, RoomStatusDTO>(source.StatusOfRoom);
		}
	}
}
