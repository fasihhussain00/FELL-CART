using CartAPI.Tests.Models;
using CartAPI.Tests.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CartAPI.Tests.Brokers
{
    public partial class CartAPIBroker
    {
        private const string customerRelativeUrl = "api/Customer";
        public async Task<Response<Customer>> PostCustomerAsync(Customer customer)
            => await apiFactoryClient.PostContentAsync<Customer, Response<Customer>>(customerRelativeUrl, customer);

        public async Task<Response<IEnumerable<Customer>>> GetCustomersAsync(Guid id)
        {
            HttpResponseMessage responseMessage = await baseClient.GetAsync($"{customerRelativeUrl}?customerid={id}");
            string body = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<IEnumerable<Customer>>>(body);

        }

        public async Task<Response<Customer>> DeleteCustomersAsync(Guid id)
            => await apiFactoryClient.DeleteContentAsync<Response<Customer>>($"{customerRelativeUrl}?customerid={id}");        
    }
}
