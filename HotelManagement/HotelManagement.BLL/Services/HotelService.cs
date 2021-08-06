using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HotelManagement.BLL.DTO;
using HotelManagement.BLL.Exceptions;
using HotelManagement.BLL.Interfaces;
using HotelManagement.BLL.Responses;
using HotelManagement.DAL.Entities;
using HotelManagement.DAL.Interfaces;

namespace HotelManagement.BLL.Services
{
	public class HotelService : IHotelService
	{
		private IUnitOfWork database;
		private IMapper toDtoMapper;
		private IMapper toEntityMapper;

		public HotelService(IUnitOfWork uow)
		{
			database = uow;
			toDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, HotelDTO>()).CreateMapper();
			toEntityMapper = new MapperConfiguration(cfg => cfg.CreateMap<HotelDTO, Hotel>()).CreateMapper();
		}

		public IEnumerable<HotelDTO> GetAll()
		{
			return toDtoMapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(database.Hotels.GetAll());
		}

		public HotelDTO Get(int id)
		{
			return toDtoMapper.Map<Hotel, HotelDTO>(database.Hotels.Get(id));
		}

		public void Create(HotelDTO item)
		{
			Hotel newHotel = toEntityMapper.Map<HotelDTO, Hotel>(item);

			database.Hotels.Create(newHotel);
			database.Save();
		}

		public void Update(HotelDTO item)
		{
			Hotel updatingHotel = database.Hotels.Get(item.Id);

			if (updatingHotel == null)
			{
				throw new RecordNotFoundException(item.Id, typeof(Hotel));
			}

			updatingHotel = toEntityMapper.Map<HotelDTO, Hotel>(item);

			database.Hotels.Update(updatingHotel);
			database.Save();
		}

		public void Delete(int id)
		{
			Hotel deletingHotel = database.Hotels.Get(id);

			if (deletingHotel == null)
			{
				throw new RecordNotFoundException(id, typeof(Hotel));
			}
			
			database.Hotels.Delete(id);
			database.Save();
		}

		public IEnumerable<RevenueResponse> GetRevenueByMonth(int hotelId, int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException();
			}

			Hotel hotelRevenue = database.Hotels.Get(hotelId);

			if (hotelRevenue == null)
			{
				throw new RecordNotFoundException(hotelId, typeof(Hotel));
			}

			List<RevenueResponse> revenueByMonth = new List<RevenueResponse>();

			IEnumerable<Payment> hotelPayments = database.Payments
				.Find(payment => payment.BookingPayment.BookedRoom.HotelId == hotelId)
				.Where(payment => payment.PaymentDay != null && 
				                  payment.PaymentDay.Value.Month == month);

			IEnumerable<int> yearList = hotelPayments
				.Select(payment => payment.PaymentDay.Value.Year)
				.Distinct();
			
			foreach (var year in yearList)
			{
				var monthYearPayments = hotelPayments
					.Where(payment => payment.PaymentDay.Value.Year == year);

				revenueByMonth.Add(new RevenueResponse()
				{
					Period = new DateTime(year, month, 1),
					Total = monthYearPayments.Sum(payment => payment.Total)
				});
			}

			return revenueByMonth;
		}
	}
}
