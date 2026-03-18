using DH_ApiGateway.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CarRental API Gateway",
        Version = "v1",
        Description = "Single entry point for all CarRental microservices with API Key authentication"
    });
});

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRental API Gateway v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseWhen(
    context => !context.Request.Path.StartsWithSegments("/api/healthcheck"),
    appBuilder => appBuilder.UseMiddleware<ApiKeyAuthenticationMiddleware>()
);

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.MapReverseProxy();

app.Run();