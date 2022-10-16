using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;
using System.Net.Http;

namespace CartAPI.Tests.Brokers
{
    public partial class CartAPIBroker
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient baseClient;
        private readonly IRESTFulApiFactoryClient apiFactoryClient;

        public CartAPIBroker()
        {
            webApplicationFactory = new WebApplicationFactory<Startup>();
            baseClient = webApplicationFactory.CreateClient();
            apiFactoryClient = new RESTFulApiFactoryClient(baseClient);
        }
    }
}
