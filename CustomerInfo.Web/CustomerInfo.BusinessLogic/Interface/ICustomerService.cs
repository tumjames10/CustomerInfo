using CustomerInfo.Model;

namespace CustomerInfo.BusinessLogic.Interface
{
    public interface ICustomerService
    {

        Customer CreateCustomer(Customer customer);

        Customer GetCustomerByID(int Id);

        Customer UpdateCustomer(Customer customer);

        void DeleteCustomer (Customer customer);

        IList<Customer> GetCustomers();
    }
}