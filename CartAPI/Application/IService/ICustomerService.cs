using CartAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Application.IService
{
    public interface ICustomerService
    {
        Task<Tokens> Authenticate(string username, string password);
        Task<Customer> Create(Customer customer);
        Task<Customer> Delete(Guid id);
        Task<IEnumerable<Customer>> Get(Guid id);
        Task<Customer> Update(Customer customer);
    }
}
