using CustomerAPI.Models;

namespace CustomerAPI.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<Customer> Create(Customer customer);
        Task<Customer> Update(int id, Customer customer);
        Task<bool> Delete(int id);
    }
}
