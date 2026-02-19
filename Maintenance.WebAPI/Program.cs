using Maintenance.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Maintenance.WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MaintenanceWebAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MaintenanceWebAPIContext") ?? throw new InvalidOperationException("Connection string 'MaintenanceWebAPIContext' not found.")));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepairHistoryService, FakeRepairHistoryService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();