using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Models;
using InventoryAPI.Services;

namespace InventoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoriesController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> GetAll()
        {
            var items = await _service.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetById(int id)
        {
            var item = await _service.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Inventory>> Create(Inventory inventory)
        {
            var item = await _service.Create(inventory);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Inventory>> Update(int id, Inventory inventory)
        {
            var item = await _service.Update(id, inventory);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}