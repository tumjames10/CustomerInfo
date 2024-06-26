﻿using CustomerInfo.BusinessLogic.Interface;
using CustomerInfo.Model;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInfo.Web.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
             _customerService = customerService; 
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customerService.GetCustomers();
        }

        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _customerService.GetCustomerByID(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer value)
        {
            if (value == null)
            {
                return BadRequest();
            }
                var dept = _customerService.CreateCustomer(value);

            return Ok(dept);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer value)
        {
            var customer = _customerService.GetCustomerByID(id);

            if (customer == null)
            {
                return NotFound();
            }

            customer.CustomerId = customer.CustomerId != 0 ? customer.CustomerId : id;

            return Ok(_customerService.UpdateCustomer(value));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _customerService.GetCustomerByID(id);

            if(customer == null)
            {
                return NotFound();
            }

            _customerService.DeleteCustomer(customer);

            return Ok();
        }
    }
}
