using CartAPI.Application.IService;
using CartAPI.Presentation.Controllers;
using CartAPI.Presentation.ViewModel.Auth.Input;
using CartAPI.Presentation.ViewModel.Customer.Input;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CartAPIUnitTest
{
    public class CustomerControllerTest
    {
        private CustomerController customerController;
        public CustomerControllerTest()
        {
            var customerService = A.Fake<ICustomerService>();
            customerController = new CustomerController(customerService);
        }
        
        [Fact]
        public void CreateUserTest_Returns_CreatedUser()
        {
            var customer = new Customer
            {
                Name = "Fasih",
                Email = "fallenx@x.com",
                Address = "PK",
                Password = "1234",
                Phone = "03171067141"
            };
            var objectResult = customerController.AddCustomer(customer).Result as ObjectResult;
            
            Assert.NotNull(objectResult);
            Assert.Equal(201, objectResult.StatusCode);
        }
        [Fact]
        public void UpdatedUserTest_Returns_UpdatedUser()
        {
            var customer = new Customer
            {
                ID = System.Guid.Empty,
                Name = "Fasih",
                Email = "fallenx@x.com",
                Address = "PK",
                Password = "1234",
                Phone = "03171067141"
            };
            var objectResult = customerController.AddCustomer(customer).Result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(201, objectResult.StatusCode);
        }
        [Fact]
        public void LoginUserTest_Returns_TokenAndRefreshToken()
        {
            var auth = new Auth
            {
                Email = "fallenx@x.com",
                Password = "1234",
            };
            var objectResult = customerController.Authenticate(auth.Email, auth.Password).Result as ObjectResult;

            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void GetCustomerByID_Returns_Customer()
        {
            
        }
        [Fact]
        public void RemoveCustomer_Returns_RemovedCustomer()
        {

        }        
    }
}
