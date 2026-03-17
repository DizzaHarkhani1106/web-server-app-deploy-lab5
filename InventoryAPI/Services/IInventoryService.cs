using InventoryAPI.Models;

namespace InventoryAPI.Services
{
    public interface IInventoryService
    {
        Task<List<Inventory>> GetAll();
        Task<Inventory> GetById(int id);
        Task<Inventory> Create(Inventory inventory);
        Task<Inventory> Update(int id, Inventory inventory);
        Task<bool> Delete(int id);
    }
}
