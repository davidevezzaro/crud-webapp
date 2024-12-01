using Azure;
using EntityFrameworkCoreMock;
using FakeItEasy;
using LAB1.Data;
using LAB1.Models;
using LAB1B.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.InteropServices;
using Xunit.Sdk;

namespace LAB1B.Test
{
	public class CarsControllerTest
	{
		#region Tests on GET Methods
		[Fact]
		public void GetAllTheCars()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);
			Car c1 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};
			Car c2 = new Car()
			{
				SerialNumber = 758493847,
				Brand = "Alfa Romeo",
				Model = "Giulia",
				Seats = 4,
				RegistrationDate = DateTime.Now.AddDays(-2),
				Motor = "Petrol",
				EmailAddressOwner = "flavio.distefano@gmail.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};
			Car c3 = new Car()
			{
				SerialNumber = 747372638,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 2,
				RegistrationDate = DateTime.Now.AddDays(-2),
				Motor = "Diesel",
				EmailAddressOwner = "giammerda@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};
			Car c4 = new Car()
			{
				SerialNumber = 127283625,
				Brand = "",
				Model = "Fabia",
				Seats = 2,
				RegistrationDate = DateTime.Now.AddDays(-2),
				Motor = "Diesel",
				EmailAddressOwner = "giammerda@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};
			context.Add(c1);
			context.Add(c2);
			context.Add(c3);
			context.Add(c4);
			context.SaveChanges();

			//act
			var actionResult = repository.GetCars().Result;

			//assert
			var result = actionResult as OkObjectResult;
			var returnCars = result.Value as IEnumerable<Car>;
			Assert.IsType<List<Car>>(returnCars);
			Assert.Distinct(returnCars);
			Assert.NotEmpty(returnCars);
			Assert.Collection(returnCars,
				item1 => Assert.Equal(12345678, item1.SerialNumber),
				item2 => Assert.Equal(758493847, item2.SerialNumber),
				item3 => Assert.Equal(747372638, item3.SerialNumber),
				item4 => Assert.Equal(127283625, item4.SerialNumber)
				);
			Assert.True(actionResult is OkObjectResult);
		}

		[Fact]
		public void GetNoCar()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);

			//act
			var actionResult = repository.GetCars().Result;

			//assert
			Assert.True(actionResult is NotFoundResult);
			Assert.False(actionResult is OkResult);
		}

		[Fact]
		public void CarById()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);
			Car c1 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};
			Car c2 = new Car()
			{
				SerialNumber = 758493847,
				Brand = "Alfa Romeo",
				Model = "Giulia",
				Seats = 4,
				RegistrationDate = DateTime.Now.AddDays(-2),
				Motor = "Petrol",
				EmailAddressOwner = "flavio.distefano@gmail.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};
			context.Add(c1);
			context.Add(c2);
			context.SaveChanges();

			//act
			var actionResult = repository.GetById(c1.SerialNumber).Result;

			//assert
			var result = actionResult as OkObjectResult;
			var returnCar = result.Value as Car;
			Assert.IsType<Car>(returnCar);
			Assert.NotNull(returnCar);
			Assert.Equal(12345678, returnCar.SerialNumber);
			Assert.False(actionResult is NotFoundResult);
			Assert.True(actionResult is OkObjectResult);
		}

		[Fact]
		public void GetNoCarById()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);
			int id = 11111111;

			//act
			var actionResult = repository.GetById(id).Result;

			//assert
			Assert.True(actionResult is NotFoundResult);
			Assert.False(actionResult is OkResult);
		}
		#endregion

		#region Tests on POST Methods
		[Fact]
		public void CreateNewCars()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);

			Car c1 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};

			Car c2 = new Car()
			{
				SerialNumber = 758493847,
				Brand = "Alfa Romeo",
				Model = "Giulia",
				Seats = 4,
				RegistrationDate = DateTime.Now.AddDays(-2),
				Motor = "Petrol",
				EmailAddressOwner = "flavio.distefano@gmail.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};

			//act
			var actionResult1 = repository.Create(c1).Result;
			var actionResult2 = repository.Create(c2).Result;

			//assert
			Assert.True(actionResult1 is CreatedAtActionResult);
			Assert.True(actionResult2 is CreatedAtActionResult);
			Assert.True(context.Cars.Count() == 2);
		}
		[Fact]
		public void CreateDuplicatedCar()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);
			Car c1 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};

			//CAR with the same serial number
			Car c2 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "BMW",
				Model = "Series 1",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "checca.pocchia@gmail.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};

			context.Add(c1);
			context.SaveChanges();

			//act
			var actionResult = repository.Create(c2).Result;

			//assert
			Assert.True(context.Cars.Count() == 1);
			Assert.True(actionResult is BadRequestResult);
		}
		#endregion
		
		#region Tests on PUT Methods
		[Fact]
		public void UpdateOneCar()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);

			Car c1 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};

			context.Add(c1);
			context.SaveChanges();

			//now change price to the previous car
			c1.Price = 7000;

			//act
			var actionResult = repository.Update(c1.SerialNumber,c1).Result;

			//assert
			Assert.True(actionResult is NoContentResult);
			Assert.False(actionResult is NotFoundResult);
			Assert.True(context.Cars.Count() == 1);
			Assert.True(context.Cars.Find(c1.SerialNumber).Price == 7000);
		}
		[Fact]
		public void UpdateCarWithInvalidSerialNumber()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);
			Car c1 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};

			context.Add(c1);
			context.SaveChanges();

			//corrupt the serial number
			int mismatchingSerialNumber = 11111678;
			//change serial number and price to the previous car
			Car c1Updated = new Car()
			{
				SerialNumber = 11111111,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 7000
			};

			//act
			var actionResult = repository.Update(mismatchingSerialNumber, c1Updated).Result;

			//assert
			Assert.True(actionResult is NotFoundResult);
			Assert.True(context.Cars.Count() == 1);
			Assert.True(context.Cars.Find(c1.SerialNumber).Price == 2700);
			Assert.True(context.Cars.Find(c1.SerialNumber).SerialNumber == 12345678);
		}

		#endregion

		#region Tests on DELETE Methods
		[Fact]
		public void DeleteOneCar()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);

			Car c1 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};

			context.Add(c1);
			context.SaveChanges();

			//act
			var actionResult = repository.Delete(c1.SerialNumber).Result;

			//assert
			Assert.True(actionResult is NoContentResult);
			Assert.False(actionResult is BadRequestResult);
			Assert.True(context.Cars.Count() == 0);
			Assert.True(context.Cars.Find(c1.SerialNumber) == null);
		}
		[Fact]
		public void DeleteOneCarWithInvalidSerialNumber()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);
			Car c1 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};

			context.Add(c1);
			context.SaveChanges();

			//corrupt the serial number
			int mismatchingSerialNumber = 11111678;
			//change serial number and price to the previous car
			Car c1Updated = new Car()
			{
				SerialNumber = 11111111,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 7000
			};

			//act
			var actionResult = repository.Delete(mismatchingSerialNumber).Result;

			//assert
			Assert.True(actionResult is BadRequestResult);
			Assert.False(actionResult is NoContentResult);
			Assert.True(context.Cars.Count() == 1);
			Assert.True(context.Cars.Find(c1.SerialNumber) == c1);
		}

		#endregion

		#region Tests on STREAMING 
		[Fact]
		public void StreamingCars()
		{
			//arrange
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var context = new ApplicationDbContext(optionsBuilder.Options);
			var repository = new CarsController(context);

			Car c1 = new Car()
			{
				SerialNumber = 12345678,
				Brand = "Skoda",
				Model = "Fabia",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-50),
				Motor = "Diesel",
				EmailAddressOwner = "giam.minchia@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 2700
			};

			//act
			var actionResult = repository.AutoGet(new CancellationToken());

			//assert
			Assert.True(actionResult is IAsyncEnumerator<Car>);
		}

		#endregion
	}
}