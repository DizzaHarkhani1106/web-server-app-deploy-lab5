using Microsoft.AspNetCore.Mvc;

namespace WebApiGateway.Controllers
{
    public class ProxyController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _maintenanceApiBaseUrl;

        public ProxyController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _maintenanceApiBaseUrl = configuration["MaintenanceApi:LocalUrl"];
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpPatch]
        public async Task ProxyAll(string path)
        {
            var client = _httpClientFactory.CreateClient();

            // Build the backend URL
            var backendUrl = $"{_maintenanceApiBaseUrl}api/maintenance/{path}{Request.QueryString}";

            // Create the backend request
            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(Request.Method),
                RequestUri = new Uri(backendUrl)
            };

            // Copy the request body if present
            if (Request.ContentLength > 0)
            {
                requestMessage.Content = new StreamContent(Request.Body);
                foreach (var header in Request.Headers)
                {
                    if (!requestMessage.Content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
                    {
                        requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                    }
                }
            }

            // Send the request
            var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);

            // Copy the response status code and headers
            Response.StatusCode = (int)response.StatusCode;
            foreach (var header in response.Headers)
            {
                Response.Headers[header.Key] = header.Value.ToArray();
            }
            foreach (var header in response.Content.Headers)
            {
                Response.Headers[header.Key] = header.Value.ToArray();
            }

            // Write the response body
            await response.Content.CopyToAsync(Response.Body);
        }

    }

