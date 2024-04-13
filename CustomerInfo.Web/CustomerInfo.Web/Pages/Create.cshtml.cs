using CustomerInfo.BusinessLogic.Interface;
using CustomerInfo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerInfo.Web.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerService _customerService;
        private int customerID;

        public CreateModel(ILogger<IndexModel> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new Customer();

        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Customer = _customerService.CreateCustomer(Customer);

            return RedirectToPage("/Index");
        }
    }

}
