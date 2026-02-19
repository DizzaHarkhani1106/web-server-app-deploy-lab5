using Microsoft.AspNetCore.Mvc;
using Maintenance.WebAPI.Models;
using Maintenance.WebAPI.Services;
using System.Collections.Concurrent;

namespace Maintenance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepairHistoryController : ControllerBase
    {
        private readonly IRepairHistoryService _repairService;
        private readonly ConcurrentDictionary<string, int> _usageCounts;

        public RepairHistoryController(
            IRepairHistoryService repairService,
             ConcurrentDictionary<string, int> usageCounts)
        {
            _repairService = repairService;
            _usageCounts = usageCounts;
        }

        // ============================================
        // GET: api/RepairHistory/vehicles/{vehicleId}
        // ============================================
        [HttpGet("vehicles/{vehicleId}")]
        public IActionResult GetRepairHistory(int vehicleId)
        {
            var history = _repairService.GetByVehicleId(vehicleId);
            return Ok(history);
        }

        // ============================================
        // POST: api/RepairHistory
        // With Validation (400 errors)
        // ============================================
        [HttpPost]
        public IActionResult AddRepair([FromBody] RepairHistoryDto repair)
        {
            if (repair.VehicleId <= 0)
            {
                return BadRequest(new
                {
                    error = "InvalidParameter",
                    message = "VehicleId must be greater than zero."
                });
            }

            if (string.IsNullOrWhiteSpace(repair.Description))
            {
                return BadRequest(new
                {
                    error = "InvalidParameter",
                    message = "Description must not be empty."
                });
            }

            if (repair.Cost < 0)
            {
                return BadRequest(new
                {
                    error = "InvalidParameter",
                    message = "Cost cannot be negative."
                });
            }

            // Fake service does not store, so just return Ok
            return Ok(repair);
        }

        // ============================================
        // GET: api/RepairHistory/crash
        // Force 500 error
        // ============================================
        [HttpGet("crash")]
        public IActionResult Crash()
        {
            int x = 0;
            int y = 5 / x; // Divide by zero exception
            return Ok();
        }

        // ============================================
        // GET: api/RepairHistory/usage
        // Stateful behavior
        // ============================================
        [HttpGet("usage")]
        public IActionResult Usage()
        {
            var key = Request.Headers["X-Api-Key"].ToString();

            if (!_usageCounts.ContainsKey(key))
                _usageCounts[key] = 0;

            _usageCounts[key]++;

            return Ok(new
            {
                clientId = key,
                callCount = _usageCounts[key]
            });
        }
    }
}
