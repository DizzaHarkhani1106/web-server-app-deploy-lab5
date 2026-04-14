using DH_VehicleInventory.Infrastructure.Data;
using DH_VehicleInventory.Domain.VehicleAggregate;
using DH_VehicleInventory.Infrastructure.Repositories;
using DH_VehicleInventory.Application.Services;
using DH_VehicleInventory.Application.Validators;
using Microsoft.EntityFrameworkCore;
using DH_GlobalExceptionHandler.Extensions;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DH_InventoryDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)));

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

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("VehicleInventory.DbInit");
    var dbContext = scope.ServiceProvider.GetRequiredService<DH_InventoryDbContext>();

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException("DefaultConnection is not configured for VehicleInventory API.");
    }

    var sqlBuilder = new SqlConnectionStringBuilder(connectionString)
    {
        InitialCatalog = "master"
    };

    const int maxRetries = 12;
    var initialized = false;

    for (var attempt = 1; attempt <= maxRetries && !initialized; attempt++)
    {
        try
        {
            using (var connection = new SqlConnection(sqlBuilder.ConnectionString))
            {
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = "IF DB_ID('VehicleInventoryDb') IS NULL CREATE DATABASE [VehicleInventoryDb];";
                command.ExecuteNonQuery();
            }

            dbContext.Database.Migrate();
            initialized = true;
            logger.LogInformation("VehicleInventory database is ready.");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "VehicleInventory DB init attempt {Attempt}/{MaxRetries} failed.", attempt, maxRetries);
            if (attempt == maxRetries)
            {
                throw;
            }

            Thread.Sleep(3000);
        }
    }
}

app.UseGlobalExceptionMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowApiGateway");
app.UseAuthorization();
app.MapControllers();

app.Run();