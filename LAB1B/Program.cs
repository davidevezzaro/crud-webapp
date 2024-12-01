using LAB1.Data;
using LAB1B.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);//building web application

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// This is swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Entity Framework Core service configuration
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
	//connection string inside appsettings.json
	builder.Configuration.GetConnectionString("DefaultConnection")
	));

builder.Services.AddGrpc();

var app = builder.Build();


app.MapGrpcService<CarService>();
app.MapGet("/", () => "Communication with GRPC endpoints must be made trhough a GRPC client!");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())//swagger allowed only on development purpose
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
