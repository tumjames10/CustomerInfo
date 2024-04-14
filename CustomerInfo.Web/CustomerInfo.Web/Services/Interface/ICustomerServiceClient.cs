using CustomerInfo.Model;

namespace CustomerInfo.Web.Services.Interface
{
    public interface ICustomerServiceClient
    {
        Task<Customer> CreateCustomer(Customer customer);

        Task<Customer> GetCustomerByID(int Id);

        Task<Customer> UpdateCustomer(Customer customer);

        Task DeleteCustomer(Customer customer);

        Task<IList<Customer>> GetCustomers();
    }
}
