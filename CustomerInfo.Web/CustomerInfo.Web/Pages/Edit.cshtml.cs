using CustomerInfo.Model;
using CustomerInfo.Web.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerInfo.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerServiceClient _customerService;
        private int customerID;

        public EditModel(ILogger<IndexModel> logger, ICustomerServiceClient customerService)
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

            Customer = _customerService.GetCustomerByID(id).Result;

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Error");
            }

            Customer.CustomerId = id;
            await _customerService.UpdateCustomer(Customer);

            return RedirectToPage("/Index");
        }

        public IActionResult Cancel()
        {
            return RedirectToPage("/Index");
        }
    }

}
