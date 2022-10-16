using CartAPI.Application.IRepo;
using CartAPI.Application.IService;
using CartAPI.Database;
using CartAPI.Domain.Model;
using CartAPI.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPI.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IJwtRepo _jwtRepo;
        private readonly DefaultDBManager _connection;

        public CustomerService(ICustomerRepo customerRepo, IJwtRepo jwtRepo, DefaultDBManager connection)
        {
            _customerRepo = customerRepo;
            _jwtRepo = jwtRepo;
            _connection = connection;
        }

        public async Task<Customer> Create(Customer customer)
        {
            try
            {
                await _connection.BeginTransactionAsync();
                var createdCustomer = await _customerRepo.Create(customer);
                await _connection.CommitTransactionAsync();
                if (createdCustomer == null)
                    throw new CustomerNotCreatedException();
                return createdCustomer;
            }
            catch (Exception)
            {
                await _connection.RollBackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> Get(Guid id)
        {
            var customers = await _customerRepo.Get(id);
            if (customers == null || !customers.Any())
                throw new CustomerNotFoundException();
            return customers;
        }

        public async Task<Customer> Update(Customer customer)
        {
            try
            {
                await _connection.BeginTransactionAsync();
                var updatedCustomer = await _customerRepo.Update(customer);
                await _connection.CommitTransactionAsync();
                if (updatedCustomer == null)
                    throw new CustomerNotFoundException();
                return updatedCustomer;
            }
            catch (Exception)
            {
                await _connection.RollBackTransactionAsync();
                throw;
            }

        }

        public async Task<Customer> Delete(Guid id)
        {
            try
            {
                await _connection.BeginTransactionAsync();
                var deletedCustomer = await _customerRepo.Delete(id);
                await _connection.CommitTransactionAsync();
                if (deletedCustomer == null)
                    throw new CustomerNotFoundException();
                return deletedCustomer;
            }
            catch (Exception)
            {
                await _connection.RollBackTransactionAsync();
                throw;
            }

        }

        public async Task<Tokens> Authenticate(string username, string password)
        {
            var user = await _customerRepo.Authenticate(username, password);
            if (user == null)
                throw new CustomerNotFoundException();
            return await _jwtRepo.GenerateToken(user);
        }
    }
}
