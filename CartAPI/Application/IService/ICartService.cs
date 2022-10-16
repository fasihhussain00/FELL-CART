using CartAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Application.IService
{
    public interface ICartService
    {
        Task<Cart> Create(Cart cart);
        Task<Cart> Delete(Guid id);
        Task<IEnumerable<Cart>> Get(Guid id, Guid customerid);
        Task<Cart> Update(Cart cart);
    }
}
