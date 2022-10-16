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
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly DefaultDBManager _connection;

        public CartService(ICartRepo cartRepo, DefaultDBManager connection)
        {
            _cartRepo = cartRepo;
            _connection = connection;
        }
        public async Task<Cart> Create(Cart cart)
        {
            try
            {
                await _connection.BeginTransactionAsync();
                var createdCart = await _cartRepo.Create(cart);
                await _connection.CommitTransactionAsync();
                if (createdCart == null)
                    throw new CartNotCreatedException();
                return createdCart;
            }
            catch (Exception)
            {
                await _connection.RollBackTransactionAsync();
                throw;
            }

        }
        public async Task<Cart> Update(Cart cart)
        {
            try
            {
                await _connection.BeginTransactionAsync();
                var updatedCart = await _cartRepo.Update(cart);
                await _connection.CommitTransactionAsync();
                if (updatedCart == null)
                    throw new CartNotFoundException();
                return updatedCart;
            }
            catch (Exception)
            {
                await _connection.RollBackTransactionAsync();
                throw;
            }

        }
        public async Task<IEnumerable<Cart>> Get(Guid id, Guid customerid)
        {
            var carts = await _cartRepo.Get(id, customerid);
            if (carts == null || !carts.Any())
                throw new CartNotFoundException();
            return carts;
        }
        public async Task<Cart> Delete(Guid id)
        {
            try
            {
                await _connection.BeginTransactionAsync();
                var deletedCart = await _cartRepo.Delete(id);
                await _connection.CommitTransactionAsync();
                if (deletedCart == null)
                    throw new CartNotFoundException();
                return deletedCart;
            }
            catch (Exception)
            {
                await _connection.RollBackTransactionAsync();
                throw;
            }
        }
    }
}
