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
            try
            {
                var client = _httpClientFactory.CreateClient("MaintenanceApi");
                var repairs = await client.GetFromJsonAsync<List<RepairHistoryViewModel>>(
                    $"api/repairhistory/vehicles/{vehicleId}");
                return View(repairs ?? new List<RepairHistoryViewModel>());
            }
            catch
            {
                return View(new List<RepairHistoryViewModel>());
            }
        }


        [HttpGet]
        public async Task<IActionResult> Usage()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("MaintenanceApi");
                var result = await client.GetFromJsonAsync<UsageViewModel>("api/repairhistory/usage");
                return View(result ?? new UsageViewModel());
            }
            catch
            {
                return View(new UsageViewModel());
            }
        }
    }
}