using Maintenance.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Maintenance.WebAPI.Data;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// Database
// =======================================
builder.Services.AddDbContext<MaintenanceWebAPIContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MaintenanceWebAPIContext")
        ?? throw new InvalidOperationException("Connection string not found.")
    ));

// =======================================
// Services
// =======================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepairHistoryService, FakeRepairHistoryService>();

// ✅ Needed for MaintenanceController's IHttpClientFactory
builder.Services.AddHttpClient("MaintenanceApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7113/"); 
    client.DefaultRequestHeaders.Add("X-Api-Key", "MY_SECRET_KEY_123");
});

// =======================================
// PART 3 – Stateful (ConcurrentDictionary per reference doc)
// =======================================
var usageCounts = new ConcurrentDictionary<string, int>(StringComparer.Ordinal);
builder.Services.AddSingleton(usageCounts);

var app = builder.Build();

// =======================================
// Swagger
// =======================================
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            error = "ServerError",
            message = "An unexpected error occurred."
        });
    }
});

const string API_KEY = "MY_SECRET_KEY_123";
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.TryGetValue("X-Api-Key", out var key) || key != API_KEY)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsJsonAsync(new
        {
            error = "Unauthorized",
            message = "Missing or invalid API key."
        });
        return;
    }
    await next();
});


app.UseAuthorization();
app.MapControllers();
app.Run();