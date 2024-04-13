using CustomerInfo.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomerInfo.Entity
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(){ }
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        
    }
}
