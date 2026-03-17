using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace Assignment1_CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomerApi");
                var response = await client.GetAsync("/api/customers");
                var json = await response.Content.ReadAsStringAsync();
                var customers = JsonSerializer.Deserialize<List<Customer>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(customers ?? new List<Customer>());
            }
            catch
            {
                return View(new List<Customer>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(Customer customer)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomerApi");
                var json = JsonSerializer.Serialize(customer);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                await client.PostAsync("/api/customers", content);
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
                var client = _httpClientFactory.CreateClient("CustomerApi");
                var response = await client.GetAsync($"/api/customers/{id}");
                var json = await response.Content.ReadAsStringAsync();
                var customer = JsonSerializer.Deserialize<Customer>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(customer);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomerApi");
                var json = JsonSerializer.Serialize(customer);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                await client.PutAsync($"/api/customers/{id}", content);
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
                var client = _httpClientFactory.CreateClient("CustomerApi");
                await client.DeleteAsync($"/api/customers/{id}");
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}