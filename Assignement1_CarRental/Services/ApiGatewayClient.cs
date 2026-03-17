using System.Text.Json;

namespace Assignement1_CarRental.Services
{
    public class ApiGatewayClient: IApiGatewayClient
    {
        
            private readonly HttpClient _httpClient;
            private readonly IConfiguration _configuration;
            private readonly ILogger<ApiGatewayClient> _logger;
            private readonly string _apiKey;
            private readonly string _gatewayBaseUrl;

            public ApiGatewayClient(
                HttpClient httpClient,
                IConfiguration configuration,
                ILogger<ApiGatewayClient> logger)
            {
                _httpClient = httpClient;
                _configuration = configuration;
                _logger = logger;

                _apiKey = configuration["ApiGateway:ApiKey"] ?? "default-key";
                _gatewayBaseUrl = configuration["ApiGateway:BaseUrl"] ?? "http://localhost:5100";
            }

            public async Task<T?> GetAsync<T>(string endpoint)
            {
                try
                {
                    var fullUrl = $"{_gatewayBaseUrl}{endpoint}";
                    _logger.LogInformation("GET request to {Endpoint}", fullUrl);

                    var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                    request.Headers.Add("X-API-Key", _apiKey);

                    var response = await _httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError("GET request failed with status code {StatusCode} for {Endpoint}",
                            response.StatusCode, fullUrl);
                        return default;
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in GET request to {Endpoint}", endpoint);
                    return default;
                }
            }

            public async Task<T?> PostAsync<T>(string endpoint, object data)
            {
                try
                {
                    var fullUrl = $"{_gatewayBaseUrl}{endpoint}";
                    _logger.LogInformation("POST request to {Endpoint}", fullUrl);

                    var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
                    request.Headers.Add("X-API-Key", _apiKey);

                    var jsonContent = new StringContent(
                        JsonSerializer.Serialize(data),
                        System.Text.Encoding.UTF8,
                        "application/json");

                    request.Content = jsonContent;

                    var response = await _httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError("POST request failed with status code {StatusCode} for {Endpoint}",
                            response.StatusCode, fullUrl);
                        return default;
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in POST request to {Endpoint}", endpoint);
                    return default;
                }
            }
            public async Task<T?> PutAsync<T>(string endpoint, object data)
            {
                try
                {
                    var fullUrl = $"{_gatewayBaseUrl}{endpoint}";
                    _logger.LogInformation("PUT request to {Endpoint}", fullUrl);

                    var request = new HttpRequestMessage(HttpMethod.Put, fullUrl);
                    request.Headers.Add("X-API-Key", _apiKey);

                    var jsonContent = new StringContent(
                        JsonSerializer.Serialize(data),
                        System.Text.Encoding.UTF8,
                        "application/json");

                    request.Content = jsonContent;

                    var response = await _httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError("PUT request failed with status code {StatusCode} for {Endpoint}",
                            response.StatusCode, fullUrl);
                        return default;
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in PUT request to {Endpoint}", endpoint);
                    return default;
                }
            }

            public async Task<bool> DeleteAsync(string endpoint)
            {
                try
                {
                    var fullUrl = $"{_gatewayBaseUrl}{endpoint}";
                    _logger.LogInformation("DELETE request to {Endpoint}", fullUrl);

                    var request = new HttpRequestMessage(HttpMethod.Delete, fullUrl);
                    request.Headers.Add("X-API-Key", _apiKey);

                    var response = await _httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError("DELETE request failed with status code {StatusCode} for {Endpoint}",
                            response.StatusCode, fullUrl);
                        return false;
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in DELETE request to {Endpoint}", endpoint);
                    return false;
                }
            }
        }
    }


