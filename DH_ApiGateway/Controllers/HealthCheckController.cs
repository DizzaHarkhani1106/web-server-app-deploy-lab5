using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DH_ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet("health")]
        [AllowAnonymous]
        public IActionResult GetHealth()
        {
            _logger.LogInformation("Health check requested");

            return Ok(new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow,
                service = "CarRental API Gateway",
                version = "1.0.0"
            });
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            _logger.LogInformation("Gateway info requested");

            return Ok(new
            {
                serviceName = "CarRental API Gateway",
                version = "1.0.0",
                description = "Single entry point for all CarRental microservices",
                timestamp = DateTime.UtcNow,
                availableServices = new[]
                {
                    new
                    {
                        name = "CustomerAPI",
                        path = "/api/customers",
                        internalPort = 5101,
                        externalPath = "http://localhost:5100/api/customers"
                    },
                    new
                    {
                        name = "InventoryAPI",
                        path = "/api/inventory",
                        internalPort = 5102,
                        externalPath = "http://localhost:5100/api/inventory"
                    },
                    new
                    {
                        name = "MaintenanceAPI",
                        path = "/api/maintenance",
                        internalPort = 5103,
                        externalPath = "http://localhost:5100/api/maintenance"
                    }
                }
            });
        }

        [HttpGet("ready")]
        [AllowAnonymous]
        public IActionResult IsReady()
        {
            _logger.LogInformation("Readiness check requested");

            return Ok(new
            {
                ready = true,
                timestamp = DateTime.UtcNow
            });
        }
    }
}
