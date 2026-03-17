namespace DH_ApiGateway.Middleware
{
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiKeyAuthenticationMiddleware> _logger;
        private readonly string _apiKey;
        private readonly string _headerName;

        public ApiKeyAuthenticationMiddleware(
            RequestDelegate next,
            ILogger<ApiKeyAuthenticationMiddleware> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;

            var apiKeySettings = configuration.GetSection("ApiKeySettings");
            _apiKey = apiKeySettings["ApiKey"] ?? "default-key";
            _headerName = apiKeySettings["HeaderName"] ?? "X-API-Key";
        }

        public async Task InvokeAsync(HttpContext context)
        {
         
            if (!context.Request.Headers.TryGetValue(_headerName, out var extractedApiKey))
            {
                _logger.LogWarning("Request received without API Key header from {RemoteIpAddress}", context.Connection.RemoteIpAddress);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Unauthorized",
                    message = $"Missing '{_headerName}' header. API Key is required."
                });
                return;
            }

        
            if (!_apiKey.Equals(extractedApiKey.ToString(), StringComparison.Ordinal))
            {
                _logger.LogWarning("Request received with invalid API Key from {RemoteIpAddress}", context.Connection.RemoteIpAddress);
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Forbidden",
                    message = "Invalid API Key"
                });
                return;
            }

            _logger.LogInformation("Request authenticated successfully from {RemoteIpAddress}", context.Connection.RemoteIpAddress);
            await _next(context);
        }
    }
}

