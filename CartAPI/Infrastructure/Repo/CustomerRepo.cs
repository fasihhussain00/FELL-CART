using CartAPI.Application.IRepo;
using CartAPI.Database;
using CartAPI.Domain.Model;
using CartAPI.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CartAPI.Infrastructure.Repo
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly DefaultDBManager _connection;

        public CustomerRepo(DefaultDBManager connection)
        {
            _connection = connection;
        }
        public async Task<Customer> Create(Customer customer)
        {
            var createdCustomer = await SaveCustomer(customer);
            return createdCustomer;
        }
        public async Task<Customer> Update(Customer customer)
        {
            var createdCustomer = await SaveCustomer(customer);
            return createdCustomer;
        }
        public async Task<IEnumerable<Customer>> Get(Guid id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", id == Guid.Empty ? null : id},
            };
            return await _connection.QueryListAsync<Customer>(StoredProcedures.GetCustomer, parameters);
        }
        public async Task<Customer> Delete(Guid id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", id},
            };
            var deletedCustomer = await _connection.QueryAsync<Customer>(StoredProcedures.DeleteCustomer, parameters);
            return deletedCustomer;

        }
        private Task<Customer> SaveCustomer(Customer customer)
        {
            var (id, name, address, email, phone, password) = customer;
            var parameters = new Dictionary<string, object>
            {
                { "id", id == Guid.Empty ? Guid.NewGuid() : id},
                { "name", name},
                { "address", address},
                { "phone", phone},
                { "email", email},
                { "password", GetHashString(password)},
            };
            var createdCustomer = _connection.QueryAsync<Customer>(StoredProcedures.SaveCustomer, parameters);
            return createdCustomer;
        }
        private static string GetHashString(string inputString)
        {
            var sb = new StringBuilder();
            foreach (var b in GetHash(inputString))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            }
        }
        public async Task<Customer> Authenticate(string email, string password)
        {
            var parameters = new Dictionary<string, object>
            {
                { "email",  email},
                { "password",  GetHashString(password)},
            };
            return await _connection.QueryAsync<Customer>(StoredProcedures.AuthCustomer, parameters);
        }
    }
}
