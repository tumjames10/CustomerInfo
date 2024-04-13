using CustomerInfo.BusinessLogic.Interface;
using CustomerInfo.Model;
using CustomerInfo.Repository;
using CustomerInfo.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CustomerInfo.BusinessLogic
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;

        public CustomerService(ICustomerRepository customerRepository, IAddressRepository addressRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
        }

        public Customer CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentException("Customer is null");
            }

            Entity.Model.Customer cst = new Entity.Model.Customer();

            cst.FirstName = customer.FirstName;
            cst.LastName = customer.LastName;
            cst.MiddleName = customer.MiddleName;
            cst.DateOfBirth = customer.DateOfBirth;

            var resultCst = _customerRepository.Insert(cst);
            _customerRepository.SaveChanges();

            // add address
            Entity.Model.Address address = new Entity.Model.Address();
            address.Street = customer.Street;
            address.City = customer.City;
            address.Province = customer.Province;
            address.Country = customer.Country;
            address.CustomerId = resultCst.CustomerId;

            var resultAddress = _addressRepository.Insert(address);
            _addressRepository.SaveChanges();

            customer.CustomerId = resultCst.CustomerId;
            customer.AddressId = resultAddress.AddressId;

            return customer;
        }

        public void DeleteCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentException("Customer is null");
            }

            var address = _addressRepository.GetAll(a => a.CustomerId == customer.CustomerId).First();

            _addressRepository.Delete(address.AddressId);
            _addressRepository.SaveChanges();

            _customerRepository.Delete(customer.CustomerId);
            _customerRepository.SaveChanges();
        }

        public Customer GetCustomerByID(int Id)
        {
            if(Id <= 0)
            {
                throw new ArgumentException("Id have no value");
            }

           return _customerRepository.GetAll(a=> a.CustomerId == Id).Include(a => a.Address)
                 .Select(a => new Customer()
                 {
                     AddressId = a.Address.AddressId,
                     FirstName = a.FirstName,
                     LastName = a.LastName,
                     MiddleName = a.MiddleName,
                     DateOfBirth = a.DateOfBirth,
                     City = a.Address.City,
                     Street = a.Address.Street,
                     Country = a.Address.Country,
                     CustomerId = a.CustomerId,
                     Province = a.Address.Province
                 }).FirstOrDefault();
        }

        public IList<Customer> GetCustomers()
        {
            var listCustomers = _customerRepository.GetAll().Include(a => a.Address).ToList()
                .Select(a => new Customer()
                {
                    AddressId = a.Address.AddressId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    MiddleName = a.MiddleName,
                    DateOfBirth = a.DateOfBirth,
                    City = a.Address.City,
                    Street = a.Address.Street,
                    Country = a.Address.Country,
                    CustomerId = a.CustomerId,
                    Province= a.Address.Province
                });

            return listCustomers.ToList();
        }

        public Customer UpdateCustomer(Customer customer)
        {

            if (customer == null)
            {
                throw new ArgumentException("Customer is null");
            }

            Entity.Model.Customer cst = new Entity.Model.Customer();
            cst.CustomerId = customer.CustomerId;
            cst.FirstName = customer.FirstName;
            cst.LastName = customer.LastName;
            cst.MiddleName = customer.MiddleName;
            cst.DateOfBirth = customer.DateOfBirth;

            var resultCst = _customerRepository.Update(cst.CustomerId, cst);
            _customerRepository.SaveChanges();

            Entity.Model.Address address = new Entity.Model.Address();

            var foundAddress = _addressRepository.GetAll(a => a.CustomerId == customer.CustomerId).First();
            address.AddressId = foundAddress.AddressId;
            address.Street = customer.Street;
            address.City = customer.City;
            address.Province = customer.Province;
            address.Country = customer.Country;
            address.CustomerId = resultCst.CustomerId;

            var resultAddress = _addressRepository.Update(address.AddressId, address);
            _addressRepository.SaveChanges();

            customer.CustomerId = resultCst.CustomerId;
            customer.AddressId = resultAddress.AddressId;

            return customer;
        }
    }
}
