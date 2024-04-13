using CustomerInfo.Entity;
using CustomerInfo.Entity.Model;
using CustomerInfo.Repository.Interface;
using Microsoft.Identity.Client;

namespace CustomerInfo.Repository
{
    public class AddressRepository : BaseRepository<Address> , IAddressRepository
    {
        public AddressRepository(CustomerContext ctx) : base(ctx) { }
        
    }
}
