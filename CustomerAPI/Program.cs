using Microsoft.EntityFrameworkCore;
using CustomerAPI.Data;
using CustomerAPI.Services;
using DH_GlobalExceptionHandler.Extensions;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Initialize the database with retry logic
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var masterConnectionString = connectionString.Replace("CustomerDb", "master");
    
    int maxRetries = 10;
    int retryCount = 0;
    bool success = false;
    
    while (retryCount < maxRetries && !success)
    {
        try
        {
            Console.WriteLine($"Attempt {retryCount + 1}/{maxRetries}: Connecting to SQL Server...");
            
            // Create database if it doesn't exist
            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'CustomerDb') CREATE DATABASE [CustomerDb]";
                    command.ExecuteNonQuery();
                }
            }
            
            // Apply migrations
            dbContext.Database.Migrate();
            Console.WriteLine("Database initialized successfully.");
            success = true;
        }
        catch (Exception ex)
        {
            retryCount++;
            if (retryCount < maxRetries)
            {
                Console.WriteLine($"Connection attempt failed: {ex.Message}. Retrying in 3 seconds...");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                Console.WriteLine($"Failed to initialize database after {maxRetries} attempts: {ex.Message}");
            }
        }
    }
}

app.UseGlobalExceptionMiddleware();
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();