using CustomerInfo.BusinessLogic.Interface;
using CustomerInfo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerInfo.Web.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerService _customerService;

        public DeleteModel(ILogger<IndexModel> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new Customer();


        public void OnGet(int id)
        {
            if(id <= 0)
            {
                RedirectToPage("/Error");
            }

            Customer = _customerService.GetCustomerByID(id);
        }

        public IActionResult OnPost(int id)
        {
           
            Customer.CustomerId = id;
            _customerService.DeleteCustomer(Customer);

            return RedirectToPage("/Index");
        }
    }

}
