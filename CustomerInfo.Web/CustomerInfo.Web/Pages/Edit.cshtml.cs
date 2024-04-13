using CustomerInfo.BusinessLogic.Interface;
using CustomerInfo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerInfo.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerService _customerService;
        private int customerID;

        public EditModel(ILogger<IndexModel> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new Customer();


        public IActionResult OnGet(int id)
        {
            if(id <= 0)
            {
                return RedirectToPage("/Error");
            }

            Customer = _customerService.GetCustomerByID(id);

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Error");
            }

            Customer.CustomerId = id;
            _customerService.UpdateCustomer(Customer);

            return RedirectToPage("/Index");
        }
    }

}
