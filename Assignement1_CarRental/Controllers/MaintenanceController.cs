using Assignement1_CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignement1_CarRental.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MaintenanceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult History()
        {
            return View(new List<RepairHistoryViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> History(int vehicleId)
        {
            var client = _httpClientFactory.CreateClient("MaintenanceApi");
            var repairs = await client.GetFromJsonAsync<List<RepairHistoryViewModel>>(
                $"api/maintenance/vehicles/{vehicleId}/repairs");
            return View(repairs ?? new List<RepairHistoryViewModel>());
        }

        // PART 4 — Usage
        [HttpGet]
        public async Task<IActionResult> Usage()
        {
            var client = _httpClientFactory.CreateClient("MaintenanceApi");
            var result = await client.GetFromJsonAsync<UsageViewModel>("api/RepairHistory/usage");
            return View(result);
        }
    }
}