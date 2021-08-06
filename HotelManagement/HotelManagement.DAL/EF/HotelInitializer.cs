using System;
using System.Collections.Generic;
using System.Data.Entity;
using HotelManagement.DAL.Entities;

namespace HotelManagement.DAL.EF
{
	public class HotelDatabaseInitializer : DropCreateDatabaseAlways<HotelContext>
	{
		private Hotel InitialHotel { get; set; }

		private RoomType ApartmentType { get; set; }
		private RoomType PresidentType { get; set; }
		private RoomType FamilyType { get; set; }
		private RoomType LuxType { get; set; }

		private RoomStatus StatusEmpty { get; set; }
		private RoomStatus StatusOccupied { get; set; }

		private Room RoomPresident { get; set; }
		private Room RoomApartment1 { get; set; }
		private Room RoomApartment2 { get; set; }
		private Room RoomLux { get; set; }
		private Room RoomFamily1 { get; set; }
		private Room RoomFamily2 { get; set; }
		private Room RoomFamily3 { get; set; }

		private BookingStatus StatusLookingFor { get; set; }
		private BookingStatus StatusCheckIn { get; set; }
		private BookingStatus StatusCheckOut { get; set; }
		private BookingStatus StatusContinued { get; set; }
		private BookingStatus StatusNotArrived { get; set; }

		private Booking BookingPresident { get; set; }
		private Booking BookingTraveler { get; set; }
		private Booking BookingFamily1 { get; set; }
		private Booking BookingFamily2 { get; set; }
		private Booking BookingFamily3 { get; set; }
		private Booking BookingBusinessman1 { get; set; }
		private Booking BookingBusinessman2 { get; set; }
		private Booking BookingCouple { get; set; }

		private PaymentStatus StatusFailed { get; set; }
		private PaymentStatus StatusComplete { get; set; }
		private PaymentStatus StatusCancelled { get; set; }
		private PaymentStatus StatusPaymentExpected { get; set; }

		private Payment PaymentPresident { get; set; }
		private Payment PaymentTraveler { get; set; }
		private Payment PaymentFamily1 { get; set; }
		private Payment PaymentFamily2 { get; set; }
		private Payment PaymentFamily3 { get; set; }
		private Payment PaymentBusinessman1 { get; set; }
		private Payment PaymentBusinessman2 { get; set; }
		private Payment PaymentCouple { get; set; }

		private Guest GuestTraveler { get; set; }
		private Guest GuestPresident { get; set; }
		private Guest GuestFamily1 { get; set; }
		private Guest GuestFamily2 { get; set; }
		private Guest GuestBusinessman { get; set; }
		private Guest GuestCouple { get; set; }

		private Position PositionAdministrator { get; set; }
		private Position PositionDoorman { get; set; }
		private Position PositionHousemaid { get; set; }
		private Position PositionTechnician { get; set; }

		private Employee EmployeeAdministrator { get; set; }
		private Employee EmployeeDoorman { get; set; }
		private Employee EmployeeHousemaid1 { get; set; }
		private Employee EmployeeHousemaid2 { get; set; }
		private Employee EmployeeTechnician { get; set; }

		private FacilitiesCategory FacilitiesCategoryHotel { get; set; }
		private FacilitiesCategory FacilitiesCategoryRoom { get; set; }

		private Facility FacilityGym { get; set; }
		private Facility FacilityConferenceHall { get; set; }
		private Facility FacilityPool { get; set; }
		private Facility FacilityParking { get; set; }

		private Facility FacilityConditioner { get; set; }
		private Facility FacilitySafe { get; set; }
		private Facility FacilityInternet { get; set; }
		private Facility FacilityWorkroom { get; set; }

		private void HotelInitializer(HotelContext context)
		{
			InitialHotel = new Hotel()
			{
				Name = "Dream Hotel",
				Address = "Mystery St. 55",
				City = "Journey Town",
				Country = "Ukraine",
				PostalCode = "84522",
				TollFreeNumber = "8-800-555-35-35",
				WebSiteAddress = "drehotel.com",
				EmailAddress = "dreamhotel@dream.com"
			};

			context.Hotels.Add(InitialHotel);
			context.SaveChanges();
		}

		private void EmployeeInitializer(HotelContext context)
		{
			List<Position> positions = new List<Position>();

			PositionAdministrator = new Position()
			{
				Name = "PositionAdministrator"
			};
			positions.Add(PositionAdministrator);

			PositionDoorman = new Position()
			{
				Name = "PositionDoorman"
			};
			positions.Add(PositionDoorman);

			PositionHousemaid = new Position()
			{
				Name = "PositionHousemaid"
			};
			positions.Add(PositionHousemaid);

			PositionTechnician = new Position()
			{
				Name = "PositionTechnician"
			};
			positions.Add(PositionTechnician);

			context.Positions.AddRange(positions);
			context.SaveChanges();


			List<Employee> employees = new List<Employee>();

			EmployeeAdministrator = new Employee()
			{
				PositionId = PositionAdministrator.Id,
				HotelId = InitialHotel.Id,
				FirstName = "Admin",
				LastName = "Admin",
				Patronymic = "Admin",
				Birthday = new DateTime(1992, 9, 25),
				PhoneNumber = "+380998887766",
				EmailAddress = "admin@dream.com",
				Address = "Nauky ave. 999",
				City = "Kharkov",
				Country = "Ukraine",
				Gender = "Female"
			};
			employees.Add(EmployeeAdministrator);

			EmployeeDoorman = new Employee()
			{
				PositionId = PositionDoorman.Id,
				HotelId = InitialHotel.Id,
				FirstName = "Doorman",
				LastName = "Doorman",
				Patronymic = "Doorman",
				Birthday = new DateTime(1990, 5, 19),
				PhoneNumber = "+380998887755",
				EmailAddress = "doorman@dream.com",
				Address = "Nauky ave. 998",
				City = "Kharkov",
				Country = "Ukraine",
				Gender = "Male"
			};
			employees.Add(EmployeeDoorman);

			EmployeeHousemaid1 = new Employee()
			{
				PositionId = PositionHousemaid.Id,
				HotelId = InitialHotel.Id,
				FirstName = "Housemaid 1",
				LastName = "Housemaid 1",
				Patronymic = "Housemaid 1",
				Birthday = new DateTime(1996, 2, 10),
				PhoneNumber = "+380998887744",
				EmailAddress = "housemaid1@dream.com",
				Address = "Nauky ave. 997",
				City = "Kharkov",
				Country = "Ukraine",
				Gender = "Female"
			};
			employees.Add(EmployeeHousemaid1);

			EmployeeHousemaid2 = new Employee()
			{
				PositionId = PositionHousemaid.Id,
				HotelId = InitialHotel.Id,
				FirstName = "Housemaid 2",
				LastName = "Housemaid 2",
				Patronymic = "Housemaid 2",
				Birthday = new DateTime(1996, 1, 23),
				PhoneNumber = "+380998887733",
				EmailAddress = "housemaid2@dream.com",
				Address = "Nauky ave. 996",
				City = "Kharkov",
				Country = "Ukraine",
				Gender = "Male"
			};
			employees.Add(EmployeeHousemaid2);

			EmployeeTechnician = new Employee()
			{
				PositionId = PositionHousemaid.Id,
				HotelId = InitialHotel.Id,
				FirstName = "Technician",
				LastName = "Technician",
				Patronymic = "Technician",
				Birthday = new DateTime(1986, 4, 26),
				PhoneNumber = "+380998887722",
				EmailAddress = "technician@dream.com",
				Address = "Nauky ave. 995",
				City = "Kharkov",
				Country = "Ukraine",
				Gender = "Male"
			};
			employees.Add(EmployeeTechnician);

			context.Employees.AddRange(employees);
			context.SaveChanges();
		}

		private void FacilitiesInitializer(HotelContext context)
		{
			List<FacilitiesCategory> facilitiesCategories = new List<FacilitiesCategory>();

			FacilitiesCategoryHotel = new FacilitiesCategory()
			{
				Name = "Hotel"
			};
			facilitiesCategories.Add(FacilitiesCategoryHotel);

			FacilitiesCategoryRoom = new FacilitiesCategory()
			{
				Name = "Room"
			};
			facilitiesCategories.Add(FacilitiesCategoryRoom);

			context.FacilitiesCategories.AddRange(facilitiesCategories);
			context.SaveChanges();


			List<Facility> facilities = new List<Facility>(8);

			FacilityGym = new Facility()
			{
				Name = "Gym",
				CategoryId = FacilitiesCategoryHotel.Id
			};
			facilities.Add(FacilityGym);

			FacilityConferenceHall = new Facility()
			{
				Name = "Conference Hall",
				CategoryId = FacilitiesCategoryHotel.Id
			};
			facilities.Add(FacilityConferenceHall);

			FacilityPool = new Facility()
			{
				Name = "Water pool",
				CategoryId = FacilitiesCategoryHotel.Id
			};
			facilities.Add(FacilityPool);

			FacilityParking = new Facility()
			{
				Name = "Underground parking",
				CategoryId = FacilitiesCategoryHotel.Id
			};
			facilities.Add(FacilityParking);


			FacilityConditioner = new Facility()
			{
				Name = "Conditioner",
				CategoryId = FacilitiesCategoryRoom.Id
			};
			facilities.Add(FacilityConditioner);

			FacilitySafe = new Facility()
			{
				Name = "Safe",
				CategoryId = FacilitiesCategoryRoom.Id
			};
			facilities.Add(FacilitySafe);

			FacilityInternet = new Facility()
			{
				Name = "Internet",
				CategoryId = FacilitiesCategoryRoom.Id
			};
			facilities.Add(FacilityInternet);

			FacilityWorkroom = new Facility()
			{
				Name = "Workroom",
				CategoryId = FacilitiesCategoryRoom.Id
			};
			facilities.Add(FacilityWorkroom);

			context.Facilities.AddRange(facilities);
			context.SaveChanges();


			List<HotelFacility> hotelFacilities = new List<HotelFacility>()
			{
				new HotelFacility()
				{
					FacilityId = FacilityGym.Id,
					HotelId = InitialHotel.Id
				},
				new HotelFacility()
				{
					FacilityId = FacilityConferenceHall.Id,
					HotelId = InitialHotel.Id
				},
				new HotelFacility()
				{
					FacilityId = FacilityParking.Id,
					HotelId = InitialHotel.Id
				}
			};

			context.HotelFacilities.AddRange(hotelFacilities);
			context.SaveChanges();
		}

		private void RoomInitializer(HotelContext context)
		{
			#region room type initialize

			List<RoomType> roomTypes = new List<RoomType>();

			ApartmentType = new RoomType()
			{
				Name = "Apartment",
				Description = "Apartment room",
				DoubleBedCount = 2,
				SingleBedCount = 0
			};
			roomTypes.Add(ApartmentType);

			PresidentType = new RoomType()
			{
				Name = "President",
				Description = "President room",
				DoubleBedCount = 2,
				SingleBedCount = 2
			};
			roomTypes.Add(PresidentType);

			FamilyType = new RoomType()
			{
				Name = "Family",
				Description = "Family room",
				DoubleBedCount = 1,
				SingleBedCount = 2
			};
			roomTypes.Add(FamilyType);

			LuxType = new RoomType()
			{
				Name = "Lux",
				Description = "Lux room",
				DoubleBedCount = 2,
				SingleBedCount = 0
			};
			roomTypes.Add(LuxType);

			context.RoomTypes.AddRange(roomTypes);
			context.SaveChanges();

			#endregion

			#region room status initialize

			List<RoomStatus> roomStatuses = new List<RoomStatus>();

			StatusEmpty = new RoomStatus()
			{
				Name = "Empty".ToUpper(),
				Description = "Room is empty"
			};
			roomStatuses.Add(StatusEmpty);

			StatusOccupied = new RoomStatus()
			{
				Name = "Occupied".ToUpper(),
				Description = "Room is occupied"
			};
			roomStatuses.Add(StatusOccupied);

			context.RoomStatuses.AddRange(roomStatuses);
			context.SaveChanges();

			#endregion




			List<Room> rooms = new List<Room>(8);

			RoomPresident = new Room()
			{
				HotelId = InitialHotel.Id,
				TypeId = PresidentType.Id,
				StatusId = StatusEmpty.Id,
				RoomNumber = "7",
				RoomRate = 1000m
			};
			rooms.Add(RoomPresident);

			RoomApartment1 = new Room()
			{
				HotelId = InitialHotel.Id,
				TypeId = ApartmentType.Id,
				StatusId = StatusOccupied.Id,
				RoomNumber = "5",
				RoomRate = 400m
			};
			rooms.Add(RoomApartment1);

			RoomApartment2 = new Room()
			{
				HotelId = InitialHotel.Id,
				TypeId = ApartmentType.Id,
				StatusId = StatusEmpty.Id,
				RoomNumber = "4",
				RoomRate = 450m
			};
			rooms.Add(RoomApartment2);

			RoomLux = new Room()
			{
				HotelId = InitialHotel.Id,
				TypeId = LuxType.Id,
				StatusId = StatusEmpty.Id,
				RoomNumber = "6",
				RoomRate = 750m
			};
			rooms.Add(RoomLux);

			RoomFamily1 = new Room()
			{
				HotelId = InitialHotel.Id,
				TypeId = FamilyType.Id,
				StatusId = StatusOccupied.Id,
				RoomNumber = "3",
				RoomRate = 100m
			};
			rooms.Add(RoomFamily1);

			RoomFamily2 = new Room()
			{
				HotelId = InitialHotel.Id,
				TypeId = FamilyType.Id,
				StatusId = StatusEmpty.Id,
				RoomNumber = "2",
				RoomRate = 100m
			};
			rooms.Add(RoomFamily2);

			RoomFamily3 = new Room()
			{
				HotelId = InitialHotel.Id,
				TypeId = FamilyType.Id,
				StatusId = StatusOccupied.Id,
				RoomNumber = "1",
				RoomRate = 125m
			};
			rooms.Add(RoomFamily3);

			context.Rooms.AddRange(rooms);
			context.SaveChanges();


			List<RoomStaff> roomStaffs = new List<RoomStaff>(16)
			{
				new RoomStaff()
				{
					EmployeeId = EmployeeHousemaid1.Id,
					RoomId = RoomPresident.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeHousemaid1.Id,
					RoomId = RoomApartment1.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeHousemaid1.Id,
					RoomId = RoomFamily1.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeHousemaid2.Id,
					RoomId = RoomApartment2.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeHousemaid2.Id,
					RoomId = RoomLux.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeHousemaid2.Id,
					RoomId = RoomFamily2.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeHousemaid2.Id,
					RoomId = RoomFamily3.Id,
					Description = "Room service"
				},

				new RoomStaff()
				{
					EmployeeId = EmployeeTechnician.Id,
					RoomId = RoomPresident.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeTechnician.Id,
					RoomId = RoomApartment1.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeTechnician.Id,
					RoomId = RoomFamily1.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeTechnician.Id,
					RoomId = RoomApartment2.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeTechnician.Id,
					RoomId = RoomLux.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeTechnician.Id,
					RoomId = RoomFamily2.Id,
					Description = "Room service"
				},
				new RoomStaff()
				{
					EmployeeId = EmployeeTechnician.Id,
					RoomId = RoomFamily3.Id,
					Description = "Room service"
				},
			};

			context.RoomStaffs.AddRange(roomStaffs);
			context.SaveChanges();


			List<RoomFacility> roomFacilities = new List<RoomFacility>()
			{
				new RoomFacility()
				{
					FacilityId = FacilityInternet.Id,
					RoomId = RoomPresident.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityInternet.Id,
					RoomId = RoomLux.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityInternet.Id,
					RoomId = RoomApartment1.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityInternet.Id,
					RoomId = RoomApartment2.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityInternet.Id,
					RoomId = RoomFamily1.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityInternet.Id,
					RoomId = RoomFamily2.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityInternet.Id,
					RoomId = RoomFamily3.Id
				},

				new RoomFacility()
				{
					FacilityId = FacilityConditioner.Id,
					RoomId = RoomPresident.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityConditioner.Id,
					RoomId = RoomLux.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityConditioner.Id,
					RoomId = RoomApartment1.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityConditioner.Id,
					RoomId = RoomApartment2.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityConditioner.Id,
					RoomId = RoomFamily1.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityConditioner.Id,
					RoomId = RoomFamily2.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityConditioner.Id,
					RoomId = RoomFamily3.Id
				},

				new RoomFacility()
				{
					FacilityId = FacilityWorkroom.Id,
					RoomId = RoomPresident.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilityWorkroom.Id,
					RoomId = RoomApartment1.Id
				},

				new RoomFacility()
				{
					FacilityId = FacilitySafe.Id,
					RoomId = RoomPresident.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilitySafe.Id,
					RoomId = RoomLux.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilitySafe.Id,
					RoomId = RoomApartment1.Id
				},
				new RoomFacility()
				{
					FacilityId = FacilitySafe.Id,
					RoomId = RoomApartment2.Id
				},
			};

			context.RoomFacilities.AddRange(roomFacilities);
			context.SaveChanges();
		}

		private void GuestInitializer(HotelContext context)
		{
			List<Guest> guests = new List<Guest>();

			GuestPresident = new Guest()
			{
				FirstName = "Mr. President",
				LastName = "Mr. President",
				Patronymic = "Mr. President",
				Email = "president@president.com",
				Address = "White house",
				City = "Washington",
				Country = "USA",
				Birthday = new DateTime(1971, 6, 19)
			};
			guests.Add(GuestPresident);

			GuestBusinessman = new Guest()
			{
				FirstName = "Mr. Businessman",
				LastName = "Mr. Businessman",
				Patronymic = "Mr. Businessman",
				Email = "businessman@gmail.com",
				Address = "Derevjanko 65, 10",
				City = "Kiev",
				Country = "Ukraine",
				Birthday = new DateTime(1989, 5, 6)
			};
			guests.Add(GuestBusinessman);

			GuestTraveler = new Guest()
			{
				FirstName = "Bob",
				LastName = "Smith",
				Patronymic = "Jr.",
				Email = "smith58@gmail.com",
				Address = "De la Green St. 58, 5",
				City = "Capital",
				Country = "Fracne",
				Birthday = new DateTime(1991, 5, 9)
			};
			guests.Add(GuestTraveler);

			GuestCouple = new Guest()
			{
				FirstName = "Alice",
				LastName = "Zabolotskaya",
				Patronymic = "Andreevna",
				Email = "AliceScience@gmail.com",
				Address = "Science Ave. 99a, 13",
				City = "Kharkov",
				Country = "Ukraine",
				Birthday = new DateTime(1997, 3, 25)
			};
			guests.Add(GuestCouple);

			GuestFamily1 = new Guest()
			{
				FirstName = "Kate",
				LastName = "Johnson",
				Patronymic = "",
				Email = "KateJo@gmail.com",
				Address = "Wall St. 22, 364",
				City = "New York",
				Country = "USA",
				Birthday = new DateTime(1986, 5, 30)
			};
			guests.Add(GuestFamily1);

			GuestFamily2 = new Guest()
			{
				FirstName = "Steve",
				LastName = "Stevenson",
				Patronymic = "Sr.",
				Email = "SteveSteve@gmail.com",
				Address = "Poltavska 53",
				City = "Poltava",
				Country = "Ukraine",
				Birthday = new DateTime(1978, 9, 27)
			};
			guests.Add(GuestFamily2);

			context.Guests.AddRange(guests);
			context.SaveChanges();
		}

		private void BookingInitializer(HotelContext context)
		{
			List<BookingStatus> bookingStatuses = new List<BookingStatus>(8);

			StatusLookingFor = new BookingStatus()
			{
				Name = "WAIT".ToUpper(),
				Description = "Looking for guest".ToLower()
			};
			bookingStatuses.Add(StatusLookingFor);

			StatusContinued = new BookingStatus()
			{
				Name = "EXTENSION".ToUpper(),
				Description = "Guest has extended the booking".ToLower()
			};
			bookingStatuses.Add(StatusContinued);

			StatusCheckIn= new BookingStatus()
			{
				Name = "CHECK_IN".ToUpper(),
				Description = "Guest has checked in".ToLower()
			};
			bookingStatuses.Add(StatusCheckIn);

			StatusCheckOut = new BookingStatus()
			{
				Name = "CHECK_OUT".ToUpper(),
				Description = "Guest has checked out".ToLower()
			};
			bookingStatuses.Add(StatusCheckOut);

			StatusNotArrived = new BookingStatus()
			{
				Name = "ABSENCE".ToUpper(),
				Description = "Guest hasn't arrived".ToLower()
			};
			bookingStatuses.Add(StatusNotArrived);

			context.BookingStatuses.AddRange(bookingStatuses);
			context.SaveChanges();


			List<Booking> bookings = new List<Booking>(8);

			BookingPresident = new Booking()
			{
				GuestId = GuestPresident.Id,
				RoomId = RoomPresident.Id,
				StatusId = StatusCheckOut.Id,
				DateFrom = new DateTime(2021, 6, 5, 10, 0, 0),
				DateTo = new DateTime(2021, 6, 10, 10, 0, 0),
				AdultQuantity = 1,
				ChildQuantity = 0,
			};
			bookings.Add(BookingPresident);

			BookingBusinessman1 = new Booking()
			{
				GuestId = GuestBusinessman.Id,
				RoomId = RoomApartment1.Id,
				StatusId = StatusCheckOut.Id,
				DateFrom = new DateTime(2021, 2, 5, 10, 0, 0),
				DateTo = new DateTime(2021, 2, 12, 10, 0, 0),
				AdultQuantity = 2,
				ChildQuantity = 0,
			};
			bookings.Add(BookingBusinessman1);

			BookingBusinessman2 = new Booking()
			{
				GuestId = GuestBusinessman.Id,
				RoomId = RoomApartment1.Id,
				StatusId = StatusCheckIn.Id,
				DateFrom = DateTime.Today,
				DateTo = DateTime.Today.AddDays(14),
				AdultQuantity = 1,
				ChildQuantity = 0,
			};
			bookings.Add(BookingBusinessman2);

			BookingCouple = new Booking()
			{
				GuestId = GuestCouple.Id,
				RoomId = RoomLux.Id,
				StatusId = StatusLookingFor.Id,
				DateFrom = DateTime.Today.AddDays(1),
				DateTo = DateTime.Today.AddDays(2),
				AdultQuantity = 2,
				ChildQuantity = 0,
			};
			bookings.Add(BookingCouple);

			BookingTraveler = new Booking()
			{
				GuestId = GuestTraveler.Id,
				RoomId = RoomFamily1.Id,
				StatusId = StatusCheckIn.Id,
				DateFrom = DateTime.Today.AddDays(-1),
				DateTo = DateTime.Today.AddDays(14),
				AdultQuantity = 2,
				ChildQuantity = 1,
			};
			bookings.Add(BookingTraveler);

			BookingFamily1 = new Booking()
			{
				GuestId = GuestFamily1.Id,
				RoomId = RoomFamily1.Id,
				StatusId = StatusCheckOut.Id,
				DateFrom = new DateTime(2020, 12, 29, 10, 0, 0),
				DateTo = new DateTime(2021, 1, 10, 10, 0, 0),
				AdultQuantity = 2,
				ChildQuantity = 2,
			};
			bookings.Add(BookingFamily1);

			BookingFamily2 = new Booking()
			{
				GuestId = GuestFamily1.Id,
				RoomId = RoomFamily2.Id,
				StatusId = StatusLookingFor.Id,
				DateFrom = new DateTime(2021, 7, 23, 10, 0, 0),
				DateTo = new DateTime(2021, 8, 1, 10, 0, 0),
				AdultQuantity = 2,
				ChildQuantity = 2,
			};
			bookings.Add(BookingFamily2);

			BookingFamily3 = new Booking()
			{
				GuestId = GuestFamily2.Id,
				RoomId = RoomFamily3.Id,
				StatusId = StatusLookingFor.Id,
				DateFrom = new DateTime(2021, 7, 20, 10, 0, 0),
				DateTo = new DateTime(2021, 7, 29, 10, 0, 0),
				AdultQuantity = 2,
				ChildQuantity = 1,
			};
			bookings.Add(BookingFamily3);

			context.Bookings.AddRange(bookings);
			context.SaveChanges();
		}

		private void PaymentInitialize(HotelContext context)
		{
			List<PaymentStatus> paymentStatuses = new List<PaymentStatus>();

			StatusComplete = new PaymentStatus()
			{
				Name = "Complete".ToUpper(),
				Description = "Payment is complete".ToLower()
			};
			paymentStatuses.Add(StatusComplete);

			StatusFailed = new PaymentStatus()
			{
				Name = "fail".ToUpper(),
				Description = "Payment is failed".ToLower()
			};
			paymentStatuses.Add(StatusFailed);

			StatusCancelled = new PaymentStatus()
			{
				Name = "Cancelled".ToUpper(),
				Description = "Payment is cancelled".ToLower()
			};
			paymentStatuses.Add(StatusCancelled);

			StatusPaymentExpected = new PaymentStatus()
			{
				Name = "Expected".ToUpper(),
				Description = "Payment is expected".ToLower()
			};
			paymentStatuses.Add(StatusPaymentExpected);

			context.PaymentStatuses.AddRange(paymentStatuses);
			context.SaveChanges();


			List<Payment> payments = new List<Payment>();

			PaymentPresident = new Payment()
			{
				BookingId = BookingPresident.Id,
				GuestId = GuestPresident.Id,
				StatusId = StatusComplete.Id,
				CreditCardNumber = "1234 5678 9123 4567",
				PaymentDay = new DateTime(2021, 6, 5, 10, 0, 0),
				Total = RoomPresident.RoomRate * 5
			};
			payments.Add(PaymentPresident);

			PaymentBusinessman1 = new Payment()
			{
				BookingId = BookingBusinessman1.Id,
				GuestId = GuestBusinessman.Id,
				StatusId = StatusComplete.Id,
				CreditCardNumber = "1111 2222 3333 4444",
				PaymentDay = new DateTime(2021, 2, 1, 10, 0, 0),
				Total = RoomApartment1.RoomRate * 7
			};
			payments.Add(PaymentBusinessman1);

			PaymentBusinessman2 = new Payment()
			{
				BookingId = BookingBusinessman2.Id,
				GuestId = GuestBusinessman.Id,
				StatusId = StatusComplete.Id,
				CreditCardNumber = "1111 2222 3333 4444",
				PaymentDay = DateTime.Today.AddDays(-7),
				Total = RoomApartment1.RoomRate * 15
			};
			payments.Add(PaymentBusinessman2);

			PaymentCouple = new Payment()
			{
				BookingId = BookingCouple.Id,
				GuestId = GuestCouple.Id,
				StatusId = StatusComplete.Id,
				CreditCardNumber = "5555 6666 7777 8888",
				PaymentDay = DateTime.Today,
				Total = RoomLux.RoomRate * 1
			};
			payments.Add(PaymentCouple);

			PaymentTraveler = new Payment()
			{
				BookingId = BookingTraveler.Id,
				GuestId = GuestTraveler.Id,
				StatusId = StatusComplete.Id,
				CreditCardNumber = "1122 3344 5566 7788",
				PaymentDay = DateTime.Today.AddDays(-5),
				Total = RoomFamily1.RoomRate * 16
			};
			payments.Add(PaymentTraveler);

			PaymentFamily1 = new Payment()
			{
				BookingId = BookingFamily1.Id,
				GuestId = GuestFamily1.Id,
				StatusId = StatusComplete.Id,
				CreditCardNumber = "2233 4455 6677 8899",
				PaymentDay = DateTime.Today.AddDays(-5),
				Total = RoomFamily1.RoomRate * 16
			};
			payments.Add(PaymentFamily1);

			PaymentFamily2 = new Payment()
			{
				BookingId = BookingFamily2.Id,
				GuestId = GuestFamily1.Id,
				StatusId = StatusPaymentExpected.Id,
				CreditCardNumber = "2233 4455 6677 8899",
				PaymentDay = null,
				Total = RoomFamily2.RoomRate * 16
			};
			payments.Add(PaymentFamily2);

			PaymentFamily3 = new Payment()
			{
				BookingId = BookingFamily3.Id,
				GuestId = GuestFamily1.Id,
				StatusId = StatusComplete.Id,
				CreditCardNumber = "2233 4455 6677 0000",
				PaymentDay = new DateTime(2021, 7, 2, 10, 0, 0),
				Total = RoomFamily3.RoomRate * 9
			};
			payments.Add(PaymentFamily3);

			context.Payments.AddRange(payments);
			context.SaveChanges();
		}


		protected override void Seed(HotelContext context)
		{
			HotelInitializer(context);
			EmployeeInitializer(context);
			FacilitiesInitializer(context);
			RoomInitializer(context);
			GuestInitializer(context);
			BookingInitializer(context);
			PaymentInitialize(context);
		}
	}
}
