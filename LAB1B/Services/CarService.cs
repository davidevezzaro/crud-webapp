using CarGrpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LAB1.Data;
using LAB1.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB1B.Services
{
	public class CarService : CarSer.CarSerBase
	{
		private readonly ApplicationDbContext _context;
		public CarService(ApplicationDbContext context) { 
			_context=context;
		}

		public override async Task<CreateCarResponse>CreateCar(CreateCarRequest request, ServerCallContext context)
		{
			if (request.SerialNumber == null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Provide a valid Serial Number"));
			}
			var carToAdd = new Car
			{
				SerialNumber = request.SerialNumber,
				Brand = request.Brand,
				Model = request.Model,
				Seats = request.Seats,
				EmailAddressOwner = request.EmailAddressOwner,
				Motor = request.Motor,
				PhoneNumberOwner = request.PhoneNumberOwner,
				Price = request.Price
			};
			await _context.AddAsync(carToAdd);
			await _context.SaveChangesAsync();

			return await Task.FromResult(new CreateCarResponse {
				SerialNumber = carToAdd.SerialNumber
			});
		}

		public override async Task<ReadCarResponse>GetOneCarBySerialNumber(ReadCarRequest request,ServerCallContext context)
		{
			if(request.SerialNumber <= 0)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "The Serial Number must be greater than zero"));
			}
			
			var retrievedCar = await _context.Cars.FirstOrDefaultAsync(t => t.SerialNumber == request.SerialNumber);

			if(retrievedCar != null)
			{
				return await Task.FromResult(new ReadCarResponse
				{
					SerialNumber=retrievedCar.SerialNumber,
					Brand = retrievedCar.Brand,
					Model = retrievedCar.Model,
					Seats = retrievedCar.Seats,
					EmailAddressOwner =retrievedCar.EmailAddressOwner,
					Motor = retrievedCar.Motor,
					PhoneNumberOwner = retrievedCar.PhoneNumberOwner,
					Price = retrievedCar.Price,
				}); 
			}
			throw new RpcException(new Status(StatusCode.NotFound,$"No existing car with Serial Number:{request.SerialNumber}"));
		}

		public override async Task<GetAllResponse>GetListOfAllCars(GetAllRequest request, ServerCallContext context)
		{
			var response = new GetAllResponse();
			var listCars= await _context.Cars.ToListAsync();

			foreach(var car in listCars)
			{
				response.Car.Add(new ReadCarResponse
				{
					SerialNumber = car.SerialNumber,
					Brand = car.Brand,
					Model = car.Model,
					Seats = car.Seats,
					EmailAddressOwner = car.EmailAddressOwner,
					Motor = car.Motor,
					PhoneNumberOwner = car.PhoneNumberOwner,
					Price = car.Price,
				});
			}
			return await Task.FromResult(response);
		}

		public override async Task<UpdateCarResponse> UpdatePriceOfACar(UpdateCarRequest request,ServerCallContext context)
		{
			if (request.SerialNumber <=0)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Please provide a valid Serial Number"));
			}

			var retrievedCar = await _context.Cars.FirstOrDefaultAsync(t => t.SerialNumber == request.SerialNumber);
			
			if(retrievedCar == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"Car with Serial Number:{request.SerialNumber} not found"));
			}

			//update the price
			retrievedCar.Price = request.Price;

			await _context.SaveChangesAsync();
			return await Task.FromResult(new UpdateCarResponse {
				SerialNumber= retrievedCar.SerialNumber
				});
		}

		public override async Task<DeleteCarResponse>DeleteCarBySerialNumber(DeleteCarRequest request, ServerCallContext context)
		{
			if(request.SerialNumber <=0)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Supply valid serial number"));
			}

			var retrievedCar = await _context.Cars.FirstOrDefaultAsync(t => t.SerialNumber == request.SerialNumber);

			if (retrievedCar == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"No task with id{request.SerialNumber}"));
			}

			_context.Remove(retrievedCar);
			await _context.SaveChangesAsync();

			return await Task.FromResult(new DeleteCarResponse
			{
				SerialNumber = retrievedCar.SerialNumber
			});
		}
	}
}
