using CartAPI.Tests.Brokers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace CartAPI.Tests.APIs.Customer
{
    [Collection(nameof(ApiTestCollection))]
    public class CustomerApiTests
    {
        private readonly CartAPIBroker _cartAPIBroker;
        public CustomerApiTests(CartAPIBroker cartAPIBroker)
        {
            _cartAPIBroker = cartAPIBroker;
        }
        private Models.Customer CreateRandomCustomer() =>
                new Filler<Models.Customer>().Create();

        [Fact]
        public async Task CreateCustomer_ShouldReturnCustomer()
        {
            //given
            var customer = CreateRandomCustomer();
            var inputCustomer = customer;
            var expectedCustomer = inputCustomer;

            //when
            var createdCustomer = await _cartAPIBroker.PostCustomerAsync(inputCustomer);
            var deletedCustomer = await _cartAPIBroker.DeleteCustomersAsync(createdCustomer.Data.ID);

            //actualCustomer.Should().BeEquivalentTo(expectedCustomer);
        }
       
    }
}
