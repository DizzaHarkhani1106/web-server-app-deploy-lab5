using DH_VehicleInventory.Infrastructure.Data;
using DH_VehicleInventory.Domain.VehicleAggregate;
using DH_VehicleInventory.Infrastructure.Repositories;
using DH_VehicleInventory.Application.Services;
using DH_VehicleInventory.Application.Validators;
using Microsoft.EntityFrameworkCore;
using DH_GlobalExceptionHandler.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DH_InventoryDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IVehicleRepository, DH_VehicleRepository>();
builder.Services.AddScoped<DH_VehicleService>();
builder.Services.AddScoped<DH_CreateVehicleValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowApiGateway", policy =>
    {
        policy.WithOrigins("http://localhost:5001")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseGlobalExceptionMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowApiGateway");
app.UseAuthorization();
app.MapControllers();

app.Run();