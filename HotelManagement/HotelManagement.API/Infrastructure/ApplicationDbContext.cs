using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotelManagement.API.Infrastructure
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("HotelModelAdmin", throwIfV1Schema: false)
		{
			Database.SetInitializer(new ApplicationDbInitializer());
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}

	public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
	{
		protected override void Seed(ApplicationDbContext context)
		{
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			var userAdmin = new ApplicationUser()
			{
				Email = "admin@dream.com",
				UserName = "admin@dream.com",
				FirstName = "Admin",
				LastName = "Admin",
				EmailConfirmed = true
			};
			userManager.Create(userAdmin, "SuperP@assword4All");

			var userDoorman = new ApplicationUser()
			{
				Email = "doorman@dream.com",
				UserName = "doorman@dream.com",
				FirstName = "Doorman",
				LastName = "Doorman",
				EmailConfirmed = true
			};
			userManager.Create(userDoorman, "SuperP@assword4All");

			var userHousemaid1 = new ApplicationUser()
			{
				Email = "housemaid1@dream.com",
				UserName = "housemaid1@dream.com",
				FirstName = "Housemaid 1",
				LastName = "Housemaid 1",
				EmailConfirmed = true
			};
			userManager.Create(userHousemaid1, "SuperP@assword4All");

			var userHousemaid2 = new ApplicationUser()
			{
				Email = "housemaid2@dream.com",
				UserName = "housemaid2@dream.com",
				FirstName = "Housemaid 2",
				LastName = "Housemaid 2",
				EmailConfirmed = true
			};
			userManager.Create(userHousemaid2, "SuperP@assword4All");

			var userTechnician = new ApplicationUser()
			{
				Email = "technician@dream.com",
				UserName = "technician@dream.com",
				FirstName = "Technician",
				LastName = "Technician",
				EmailConfirmed = true
			};
			userManager.Create(userTechnician, "SuperP@assword4All");


			var userPresident = new ApplicationUser()
			{
				Email = "president@president.com",
				UserName = "president@president.com",
				FirstName = "Mr. President",
				LastName = "Mr. President",
				EmailConfirmed = true
			};
			userManager.Create(userPresident, "SuperP@assword4All");

			var userBusinessman = new ApplicationUser()
			{
				Email = "businessman@gmail.com",
				UserName = "businessman@gmail.com",
				FirstName = "Mr. Businessman",
				LastName = "Mr. Businessman",
				EmailConfirmed = true
			};
			userManager.Create(userBusinessman, "SuperP@assword4All");

			var userTraveler = new ApplicationUser()
			{
				Email = "smith58@gmail.com",
				UserName = "smith58@gmail.com",
				FirstName = "Bob",
				LastName = "Smith",
				EmailConfirmed = true
			};
			userManager.Create(userTraveler, "SuperP@assword4All");

			var userCouple = new ApplicationUser()
			{
				Email = "AliceScience@gmail.com",
				UserName = "AliceScience@gmail.com",
				FirstName = "Alice",
				LastName = "Zabolotskaya",
				EmailConfirmed = true
			};
			userManager.Create(userCouple, "SuperP@assword4All");

			var userFamily1 = new ApplicationUser()
			{
				Email = "KateJo@gmail.com",
				UserName = "KateJo@gmail.com",
				FirstName = "Kate",
				LastName = "Johnson",
				EmailConfirmed = true
			};
			userManager.Create(userFamily1, "SuperP@assword4All");

			var userFamily2 = new ApplicationUser()
			{
				Email = "SteveSteve@gmail.com",
				UserName = "SteveSteve@gmail.com",
				FirstName = "Steve",
				LastName = "Stevenson",
				EmailConfirmed = true
			};
			userManager.Create(userFamily2, "SuperP@assword4All");


			if (!roleManager.Roles.Any())
			{
				roleManager.Create(new IdentityRole { Name = "Admin" });
				roleManager.Create(new IdentityRole { Name = "Employee" });
				roleManager.Create(new IdentityRole { Name = "User" });
			}

			var admin = userManager.FindByName("admin@dream.com");
			userManager.AddToRole(admin.Id, "Admin");

			var doorman = userManager.FindByName("doorman@dream.com");
			userManager.AddToRole(doorman.Id, "Employee");

			var housemaid1= userManager.FindByName("housemaid1@dream.com");
			userManager.AddToRole(housemaid1.Id, "Employee");

			var housemaid2 = userManager.FindByName("housemaid2@dream.com");
			userManager.AddToRole(housemaid2.Id, "Employee");

			var technician = userManager.FindByName("technician@dream.com");
			userManager.AddToRole(technician.Id, "Employee");


			var president = userManager.FindByName("president@president.com");
			userManager.AddToRole(president.Id, "User");

			var businessman = userManager.FindByName("businessman@gmail.com");
			userManager.AddToRole(businessman.Id, "User");

			var traveler = userManager.FindByName("smith58@gmail.com");
			userManager.AddToRole(traveler.Id, "User");

			var couple = userManager.FindByName("AliceScience@gmail.com");
			userManager.AddToRole(couple.Id, "User");

			var family1 = userManager.FindByName("KateJo@gmail.com");
			userManager.AddToRole(family1.Id, "User");

			var family2 = userManager.FindByName("SteveSteve@gmail.com");
			userManager.AddToRole(family2.Id, "User");
		}
	}
}