

using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.DBContext;
using ProductManagementAPI.Interface;
using ProductManagementAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
		new MySqlServerVersion(new Version(8, 0, 32))));

//builder.Services.AddDbContext<AppDbContext>(options =>
//	options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//		new MySqlServerVersion(new Version(8, 0, 31)),  // Replace with your MySQL version
//		b => b.MigrationsAssembly("Application")      // Specify your Class Library name
//	)
//);
//string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<AppDbContext>(options =>
//	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
//		b => b.MigrationsAssembly("Infrastucture")));  // Pointing to the library project

//string connectionString= builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AppDbContext>(options =>
//	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
//		b => b.MigrationsAssembly("Infrastucture")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<Utility>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
