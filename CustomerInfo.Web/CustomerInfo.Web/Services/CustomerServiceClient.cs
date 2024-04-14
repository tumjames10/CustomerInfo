
using CustomerInfo.Model;
using CustomerInfo.Web.Config;
using CustomerInfo.Web.Services.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace CustomerInfo.Web.Services
{
    public class CustomerServiceClient : ICustomerServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ConfigSettings> _settings;
        public CustomerServiceClient(IOptions<ConfigSettings> settings, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            var serializeCustomer = JsonConvert.SerializeObject(customer);
            var requestContent = new StringContent(serializeCustomer, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_settings.Value.CustomerEndPoint, requestContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Customer>(content);
            }
            return null;
        }

        public async Task DeleteCustomer(Customer customer)
        {
            var response = await _httpClient.DeleteAsync($"{_settings.Value.CustomerEndPoint}/{customer.CustomerId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Customer> GetCustomerByID(int Id)
        {
            var response = await _httpClient.GetAsync($"{_settings.Value.CustomerEndPoint}/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Customer>(content);
            }
            return null;
        }

        public async Task<IList<Customer>> GetCustomers()
        {
            var response = await _httpClient.GetAsync(_settings.Value.CustomerEndPoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<Customer>>(content);
            }
            return null;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var serializeCustomer = JsonConvert.SerializeObject(customer);
            var requestContent = new StringContent(serializeCustomer, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_settings.Value.CustomerEndPoint}/{customer.CustomerId}", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Customer>(content);
            }

            return null;
        }
    }
}
