using Maintenance.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Maintenance.WebAPI.Data;
using System.Collections.Concurrent;
using DH_GlobalExceptionHandler.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MaintenanceWebAPIContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MaintenanceWebAPIContext")
        ?? throw new InvalidOperationException("Connection string not found.")
    ));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "X-Api-Key",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Enter your API key"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IRepairHistoryService, FakeRepairHistoryService>();
builder.Services.AddHttpClient("MaintenanceApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7113/");
    client.DefaultRequestHeaders.Add("X-Api-Key", "car-rental-secret-key-12345");  // ← Updated to match gateway
});

var usageCounts = new ConcurrentDictionary<string, int>(StringComparer.Ordinal);
builder.Services.AddSingleton(usageCounts);

var app = builder.Build();
app.UseGlobalExceptionMiddleware();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();


app.MapControllers();
app.Run();