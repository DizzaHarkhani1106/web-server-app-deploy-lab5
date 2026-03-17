var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("MaintenanceApi", (sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(config["MaintenanceApi:LocalUrl"]!);
    client.DefaultRequestHeaders.Add("X-Api-Key", "MY_SECRET_KEY_123");
});

builder.Services.AddHttpClient("InventoryApi", (sp, client) =>
{
    client.BaseAddress = new Uri("https://localhost:7267");
});

builder.Services.AddHttpClient("CustomerApi", (sp, client) =>
{
    client.BaseAddress = new Uri("https://localhost:7247");
});

var app = builder.Build();

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