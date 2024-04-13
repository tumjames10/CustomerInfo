using CustomerInfo.BusinessLogic.Interface;
using CustomerInfo.Model;
using CustomerInfo.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerInfo.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerService _customerService;


        public IndexModel(ILogger<IndexModel> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        public void OnGet()
        {
            Customers = _customerService.GetCustomers().ToList();
        }

      

        [BindProperty]
        public IList<Customer> Customers { get; set; } = new List<Customer>();

        public void OnPost() {

         
        }

    }
}