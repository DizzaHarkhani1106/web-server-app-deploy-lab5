using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Data
{      
        public class CustomerDbContext : DbContext
        {
            public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

            public DbSet<Customer> Customers { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Customer>().HasData(
                    new Customer { Id = 1, Name = "John Doe", Email = "john@email.com", Phone = "555-0001", Address = "123 Main St" },
                    new Customer { Id = 2, Name = "Jane Smith", Email = "jane@email.com", Phone = "555-0002", Address = "456 Oak Ave" },
                    new Customer { Id = 3, Name = "Bob Johnson", Email = "bob@email.com", Phone = "555-0003", Address = "789 Pine St" }
                );
            }
        }
    }

