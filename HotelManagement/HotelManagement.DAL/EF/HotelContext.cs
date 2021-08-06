using System.Data.Entity;
using HotelManagement.DAL.Entities;

namespace HotelManagement.DAL.EF
{
	public class HotelContext : DbContext
	{
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<BookingStatus> BookingStatuses { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<FacilitiesCategory> FacilitiesCategories { get; set; }
		public DbSet<Facility> Facilities { get; set; }
		public DbSet<Guest> Guests { get; set; }
		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<HotelFacility> HotelFacilities { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<PaymentStatus> PaymentStatuses { get; set; }
		public DbSet<Position> Positions { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<RoomFacility> RoomFacilities { get; set; }
		public DbSet<RoomStaff> RoomStaffs { get; set; }
		public DbSet<RoomStatus> RoomStatuses { get; set; }
		public DbSet<RoomType> RoomTypes { get; set; }

		public DbSet<Logger> Logger { get; set; }

		public HotelContext(string connectionString)
			:base(connectionString)
		{
			Database.SetInitializer<HotelContext>(new HotelDatabaseInitializer());
			Configuration.AutoDetectChangesEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Payment>()
				.HasRequired(payment => payment.Payer)
				.WithMany(guest => guest.Payments)
				.WillCascadeOnDelete(false);
			
			base.OnModelCreating(modelBuilder);
		}
	}
}
