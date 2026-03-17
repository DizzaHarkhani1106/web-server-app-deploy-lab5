using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace Assignment1_CarRental.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InventoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("InventoryApi");
                var response = await client.GetAsync("/api/inventories");
                var json = await response.Content.ReadAsStringAsync();
                var inventories = JsonSerializer.Deserialize<List<Inventory>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(inventories ?? new List<Inventory>());
            }
            catch
            {
                return View(new List<Inventory>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(Inventory inventory)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("InventoryApi");
                var json = JsonSerializer.Serialize(inventory);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                await client.PostAsync("/api/inventories", content);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("InventoryApi");
                var response = await client.GetAsync($"/api/inventories/{id}");
                var json = await response.Content.ReadAsStringAsync();
                var inventory = JsonSerializer.Deserialize<Inventory>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(inventory);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Inventory inventory)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("InventoryApi");
                var json = JsonSerializer.Serialize(inventory);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                await client.PutAsync($"/api/inventories/{id}", content);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("InventoryApi");
                await client.DeleteAsync($"/api/inventories/{id}");
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }

    public class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}