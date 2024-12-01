using LAB1.Data;
using LAB1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Runtime.CompilerServices;

namespace LAB1B.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public CarsController(ApplicationDbContext context) {
			_context = context;
		}

		#region GET
		[HttpGet("~/api/Cars")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> GetCars()
		{
			var recipes= await _context.Cars.ToListAsync();
			if (!recipes.Any())
			{
				//404
				return NotFound();
			}
			//200
			return Ok(recipes);
		}

		[HttpGet("~/api/Cars/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> GetById(int id)
		{
			var car = await _context.Cars.FindAsync(id);
			if (car == null)
			{
				//404
				return NotFound();
			}
			//200
			return Ok(car);
		}
		#endregion

		#region POST
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Create(Car car)
		{
			var findExistingCar = await _context.Cars.FindAsync(car.SerialNumber);
			if (findExistingCar!=null)
			{
				return BadRequest();
			}
			else
			{
				await _context.Cars.AddAsync(car);
				await _context.SaveChangesAsync();
			}

			return CreatedAtAction(nameof(GetById), new { id = car.SerialNumber }, car);
		}
		#endregion

		#region PUT
		[HttpPut("~/api/Cars/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Update(int id, Car car)
		{
			if(id!= car.SerialNumber)
			{
				return NotFound();
			}
			else
			{
				_context.Entry(car).State = EntityState.Modified;
				await _context.SaveChangesAsync();

			}
			return NoContent();
		}
		#endregion

		#region DELETE
		[HttpDelete("~/api/Cars/{id}")]//api/Cars/12345678
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Delete (int id)
		{
			var carToDelete = await _context.Cars.FindAsync(id);
			if(carToDelete == null)
			{
				return BadRequest();
			}
			else
			{
				_context.Cars.Remove(carToDelete);
				await _context.SaveChangesAsync();
			}

			return NoContent();
		}
		#endregion

		#region STREAMING
		//STREAMING API
		[HttpGet("~/api/StreamingCars")]
		public async IAsyncEnumerable<Car> AutoGet([EnumeratorCancellation] CancellationToken cancellationToken)
		{
			List<Car> dummyCars = new List<Car>();
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
				PhoneNumberOwner = "+370 812555678",
				Price = 2700
			};
			Car c3 = new Car()
			{
				SerialNumber = 747372638,
				Brand = "Toyota",
				Model = "Corolla",
				Seats = 2,
				RegistrationDate = DateTime.Now.AddDays(-2),
				Motor = "Diesel",
				EmailAddressOwner = "giammerda@gino.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 15000
			};
			Car c4 = new Car()
			{
				SerialNumber = 127283625,
				Brand = "Mercedes",
				Model = "A180d",
				Seats = 8,
				RegistrationDate = DateTime.Now.AddDays(-2),
				Motor = "Diesel",
				EmailAddressOwner = "mauro@gmail.lt",
				PhoneNumberOwner = "+370 812345678",
				Price = 4289
			};
			Car c5 = new Car()
			{
				SerialNumber = 44444444,
				Brand = "Fiat",
				Model = "500",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-354),
				Motor = "Diesel",
				EmailAddressOwner = "antonio.larosa@yahoo.com",
				PhoneNumberOwner = "+370 831336750",
				Price = 4289
			};
			Car c6 = new Car()
			{
				SerialNumber = 42141892,
				Brand = "Fiat",
				Model = "Punto",
				Seats = 5,
				RegistrationDate = DateTime.Now.AddDays(-354),
				Motor = "Diesel",
				EmailAddressOwner = "thomas.turbato@outlook.it",
				PhoneNumberOwner = "+370 846578071",
				Price = 15000
			};

			dummyCars.Add(c1);
			dummyCars.Add(c2);
			dummyCars.Add(c3);
			dummyCars.Add(c4);
			dummyCars.Add(c5);
			dummyCars.Add(c6);

			while (!cancellationToken.IsCancellationRequested)
			{
				var r = Random.Shared.Next(0, dummyCars.Count - 1);
				await Task.Delay(2000);
				yield return dummyCars.ElementAt(r);
			}
		}
		#endregion

	}

}
