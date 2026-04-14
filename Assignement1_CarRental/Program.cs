using Assignement1_CarRental.Services;

var builder = WebApplication.CreateBuilder(args);

var gatewayBaseUrl = builder.Configuration["ApiGateway:BaseUrl"] ?? "http://localhost:5100";
var gatewayApiKey = builder.Configuration["ApiGateway:ApiKey"] ?? string.Empty;

void ConfigureGatewayClient(HttpClient client)
{
    client.BaseAddress = new Uri(gatewayBaseUrl);
    if (!string.IsNullOrWhiteSpace(gatewayApiKey))
    {
        client.DefaultRequestHeaders.Remove("X-API-Key");
        client.DefaultRequestHeaders.Add("X-API-Key", gatewayApiKey);
    }
}

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IApiGatewayClient, ApiGatewayClient>();
builder.Services.AddScoped<IApiGatewayClient, ApiGatewayClient>();
builder.Services.AddHttpClient("CustomerApi", ConfigureGatewayClient);
builder.Services.AddHttpClient("MaintenanceApi", ConfigureGatewayClient);

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();