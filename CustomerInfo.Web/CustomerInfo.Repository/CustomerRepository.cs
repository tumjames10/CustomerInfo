using CustomerInfo.Entity;
using CustomerInfo.Entity.Model;
using CustomerInfo.Repository.Interface;
using Microsoft.Identity.Client;

namespace CustomerInfo.Repository
{
    public class CustomerRepository : BaseRepository<Customer> , ICustomerRepository
    {
        public CustomerRepository(CustomerContext ctx) : base(ctx) { }
        
    }
}
