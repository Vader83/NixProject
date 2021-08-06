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
	class RoomDTOResolver : IValueResolver<Booking, BookingDTO, RoomDTO>
	{
		private IMapper toDtoMapper;

		public RoomDTOResolver()
		{
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()
				.ForMember(dto => dto.Bookings, opt => opt.MapFrom(new BookingDTOResolver()))
				.ForMember(dto => dto.RoomHotel, opt => opt.MapFrom(new HotelResolver()))
				.ForMember(dto => dto.StatusOfRoom, opt => opt.MapFrom(new RoomStatusResolver()))
				.ForMember(dto => dto.TypeOfRoom, opt => opt.MapFrom(new RoomTypeResolver()))
			).CreateMapper();
		}

		public RoomDTO Resolve(Booking source, BookingDTO destination, RoomDTO destMember, ResolutionContext context)
		{
			return toDtoMapper.Map<Room, RoomDTO>(source.BookedRoom);
		}
	}
}
