using CartAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Application.IRepo
{
    public interface ICustomerRepo
    {
        Task<Customer> Create(Customer customer);
        Task<Customer> Delete(Guid id);
        Task<IEnumerable<Customer>> Get(Guid id);
        Task<Customer> Update(Customer customer);
        Task<Customer> Authenticate(string username, string password);
    }
}
