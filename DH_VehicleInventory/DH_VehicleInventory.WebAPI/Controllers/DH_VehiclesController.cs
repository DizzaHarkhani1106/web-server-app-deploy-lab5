using DH_VehicleInventory.Application.DTOs;
using DH_VehicleInventory.Application.Services;
using DH_VehicleInventory.Application.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DH_VehicleInventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DH_VehiclesController : ControllerBase
    {
        private readonly DH_VehicleService _vehicleService;
        private readonly DH_CreateVehicleValidator _validator;

        public DH_VehiclesController(
            DH_VehicleService vehicleService,
            DH_CreateVehicleValidator validator)
        {
            _vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] DH_CreateVehicleDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
                return BadRequest(new { errors = validationResult.Errors });

            try
            {
                var vehicleDto = _vehicleService.CreateVehicle(dto);
                return CreatedAtAction(nameof(GetVehicle), new { id = vehicleDto.Id }, vehicleDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while creating the vehicle", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleAsync(id);

                if (vehicle == null)
                    return NotFound(new { error = $"Vehicle with ID {id} not found" });

                return Ok(vehicle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving the vehicle", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            try
            {
                var vehicles = await _vehicleService.GetAllVehiclesAsync();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving vehicles", details = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] DH_UpdateVehicleStatusDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { error = "Status update DTO is required" });

                if (dto.StatusId < 1 || dto.StatusId > 4)
                    return BadRequest(new { error = "StatusId must be between 1 and 4" });

                var result = await _vehicleService.UpdateVehicleStatus(id, dto);

                if (!result)
                    return NotFound(new { error = $"Vehicle with ID {id} not found" });

                return Ok(new { success = true, message = "Vehicle status updated successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while updating vehicle status", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                var result = await _vehicleService.DeleteVehicleAsync(id);

                if (!result)
                    return NotFound(new { error = $"Vehicle with ID {id} not found" });

                return Ok(new { success = true, message = "Vehicle deleted successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while deleting the vehicle", details = ex.Message });
            }
        }
    }
}