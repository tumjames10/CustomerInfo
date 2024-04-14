using CustomerInfo.Model;
using CustomerInfo.Web.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerInfo.Web.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerServiceClient _customerService;
        private int customerID;

        public CreateModel(ILogger<IndexModel> logger, ICustomerServiceClient customerService)
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

            Customer = _customerService.CreateCustomer(Customer).Result;

            return RedirectToPage("/Index");
        }
    }

}
