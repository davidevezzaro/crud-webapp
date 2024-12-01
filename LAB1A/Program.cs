using LAB1.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// here you can add services to the IoC container.

//DEPENDENCY INJECTION HERE!
//Entity Framework Core service configuration
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    //connection string inside appsettings.json
    builder.Configuration.GetConnectionString("DefaultConnection")
    )) ;

builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//PIPELINE REDIRECTION

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//DEFAULT CONTROLLER/ACTION IF ANYTHING IS PROVIDED
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
