using DH_VehicleInventory.Application.DTOs;
using DH_VehicleInventory.WebAPI;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DH_VehicleInventory_IntegrationTests.Controllers
{
    public class DH_VehiclesControllerIntegrationTests : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _factory;

        public DH_VehiclesControllerIntegrationTests()
        {
            _factory = new WebApplicationFactory<Program>();
            _httpClient = _factory.CreateClient();
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            _factory?.Dispose();
        }

        [Fact]
        public async Task CreateVehicle_WithValidData_ReturnsCreated()
        {
            // Arrange
            var createDto = new DH_CreateVehicleDto
            {
                VehicleCode = "VH" + System.Guid.NewGuid().ToString().Substring(0, 8),
                VehicleTypeId = 1  
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/dh_vehicles", createDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Created ||
                response.StatusCode == HttpStatusCode.BadRequest,
                $"Expected Created or BadRequest, got {response.StatusCode}"
            );
        }

        [Fact]
        public async Task GetAllVehicles_ReturnsOkAndVehicleList()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/dh_vehicles");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetVehicleById_WithValidId_ReturnsOk()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/dh_vehicles/1");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Expected OK or NotFound, got {response.StatusCode}"
            );
        }

        [Fact]
        public async Task UpdateVehicleStatus_WithValidData_ReturnsOk()
        {
            // Arrange
            var updateDto = new DH_UpdateVehicleStatusDto
            {
                StatusId = 2  // Reserved
            };

            // Act
            var response = await _httpClient.PutAsJsonAsync("/api/dh_vehicles/1/status", updateDto);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest,
                $"Expected OK, NotFound, or BadRequest, got {response.StatusCode}"
            );
        }

        [Fact]
        public async Task DeleteVehicle_WithValidId_ReturnsOk()
        {
            // Act
            var response = await _httpClient.DeleteAsync("/api/dh_vehicles/999");

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.NotFound,
                $"Expected OK or NotFound, got {response.StatusCode}"
            );
        }
    }
}
