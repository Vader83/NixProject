using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace HotelManagement.WEB.Models
{
	public class RoomReturnModel
	{
		public string HotelName { get; set; }

		public string TypeName { get; set; }

		public string StatusName { get; set; }

		public string RoomNumber { get; set; }

		public decimal RoomRate { get; set; }

		public int DoubleBedCount { get; set; }

		public int SingleBedCount { get; set; }
	}

	public class FindEmptyRoomModel
	{
		public List<RoomReturnModel> RoomsInfo { get; set; }

		[Required]
		[DisplayName("Hotel")]
		public int HotelId { get; set; }

		[Required]
		[DisplayName("From")]
		[DataType(DataType.DateTime)]
		public DateTime DateFrom { get; set; }

		[Required]
		[DisplayName("To")]
		[DataType(DataType.DateTime)]
		public DateTime DateTo { get; set; }
	}

	public class RoomCreateModel
	{
		[Required]
		[Display(Name = "Hotel")]
		public int HotelId { get; set; }
		
		[Required]
		[Display(Name = "Type")]
		public int TypeId { get; set; }

		[Required]
		[Display(Name = "Room number")]
		public string RoomNumber { get; set; }

		[Required]
		[Display(Name = "Room rate")]
		[DataType(DataType.Currency)]
		public decimal RoomRate { get; set; }
	}

	public class RoomUpdateModel
	{
		public string HotelName { get; set; }

		[Required]
		public int Id { get; set; }
		
		public int TypeId { get; set; }
		
		public int StatusId { get; set; }
		
		public string RoomNumber { get; set; }
		
		[DataType(DataType.Currency)]
		public decimal RoomRate { get; set; }
		
		public int DoubleBedCount { get; set; }

		public int SingleBedCount { get; set; }
	}

	public class RoomDeleteModel
	{
		public RoomReturnModel RoomInfo { get; set; }
		
		[Required]
		[Display(Name = "Room")]
		public int Id { get; set; }
	}

	public class RoomListModel
	{
		public int Id { get; set; }

		public string TypeName { get; set; }

		public string RoomNumber { get; set; }

		public decimal RoomRate { get; set; }

		public int DoubleBedCount { get; set; }

		public int SingleBedCount { get; set; }

		public string TextToDisplay
		{
			get { return $"{RoomNumber}; Double: {DoubleBedCount}; single: {SingleBedCount}; Rate: {RoomRate}"; }
		}
	}
}