using System.Collections.Generic;
using AutoMapper;
using HotelManagement.BLL.DTO;
using HotelManagement.DAL.Entities;

namespace HotelManagement.BLL.AutomapperResolver
{
	public class BookingDTOResolver : IValueResolver<Room, RoomDTO, ICollection<BookingDTO>>
	{
		private IMapper toDtoMapper;

		public BookingDTOResolver()
		{
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Booking,BookingDTO>()
				.ForMember(dto => dto.NewGuest, opt => opt.MapFrom(new GuestDTOResolver()))
				.ForMember(dto => dto.BookedRoom, opt => opt.MapFrom(new RoomDTOResolver()))
				.ForMember(dto => dto.Status, opt => opt.MapFrom(new BookingStatusDTOResolver()))
			).CreateMapper();
		}

		public ICollection<BookingDTO> Resolve(Room source, RoomDTO destination, ICollection<BookingDTO> destMember, ResolutionContext context)
		{
			return toDtoMapper.Map<ICollection<Booking>, ICollection<BookingDTO>>(source.Bookings);
		}
	}
}
