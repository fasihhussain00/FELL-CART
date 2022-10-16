using CartAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Application.IRepo
{
    public interface ICartRepo
    {
        Task<Cart> Create(Cart cart);
        Task<Cart> Delete(Guid? cartid);
        Task<IEnumerable<Cart>> Get(Guid? cartid = null, Guid? customerid = null);
        Task<Cart> Update(Cart cart);
    }
}
