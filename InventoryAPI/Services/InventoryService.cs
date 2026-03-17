using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryDbContext _context;

        public InventoryService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> GetAll()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory> GetById(int id)
        {
            return await _context.Inventories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Inventory> Create(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<Inventory> Update(int id, Inventory inventory)
        {
            var item = await _context.Inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return null;

            item.Name = inventory.Name;
            item.Quantity = inventory.Quantity;
            item.Price = inventory.Price;

            _context.Inventories.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.Inventories.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return false;

            _context.Inventories.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}