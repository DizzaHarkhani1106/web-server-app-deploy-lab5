using Microsoft.EntityFrameworkCore;
using CustomerAPI.Data;
using CustomerAPI.Models;

namespace CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext _context;

        public CustomerService(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> Create(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(int id, Customer customer)
        {
            var item = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return null;

            item.Name = customer.Name;
            item.Email = customer.Email;
            item.Phone = customer.Phone;
            item.Address = customer.Address;

            _context.Customers.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return false;

            _context.Customers.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}