using CartAPI.Application.IRepo;
using CartAPI.Application.IService;
using CartAPI.Database;
using CartAPI.Domain.Model;
using CartAPI.Infrastructure.Exceptions;
using CartAPI.Infrastructure.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPI.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly DefaultDBManager _connection;

        public ProductService(IProductRepo productRepo, DefaultDBManager connection)
        {
            _productRepo = productRepo;
            _connection = connection;
        }

        public async Task<Product> Create(Product product)
        {
            try
            {
                await _connection.BeginTransactionAsync();
                var createdProduct = await _productRepo.Create(product);
                await _connection.CommitTransactionAsync();
                if (createdProduct == null)
                    throw new ProductNotCreatedException();
                return createdProduct;
            }
            catch (Exception)
            {
                await _connection.RollBackTransactionAsync();
                throw;
            }

        }

        public async Task<Product> Update(Product product)
        {
            try
            {
                await _connection.BeginTransactionAsync();
                var updatedProduct = await _productRepo.Update(product);
                await _connection.CommitTransactionAsync();
                if (updatedProduct == null)
                    throw new ProductNotFoundException();
                return updatedProduct;
            }
            catch (Exception)
            {
                await _connection.RollBackTransactionAsync();
                throw;
            }

        }

        public async Task<IEnumerable<Product>> Get(Guid id, string category)
        {
            var products = await _productRepo.Get(id, category);
            if (products == null || !products.Any())
                throw new ProductNotFoundException();
            return products;
        }
        public async Task<Product> Delete(Guid id)
        {
            try
            {
                await _connection.BeginTransactionAsync();
                var deletedProduct = await _productRepo.Delete(id);
                await _connection.CommitTransactionAsync();
                if (deletedProduct == null)
                    throw new ProductNotFoundException();
                return deletedProduct;
            }
            catch (Exception)
            {
                await _connection.RollBackTransactionAsync();
                throw;
            }
            
        }

    }
}
