using CartAPI.Application.IRepo;
using CartAPI.Database;
using CartAPI.Domain.Model;
using CartAPI.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Infrastructure.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly DefaultDBManager _connection;

        public ProductRepo(DefaultDBManager connection)
        {
            _connection = connection;
        }
        public async Task<Product> Create(Product product)
        {

            var createdProduct = await SaveProduct(product);
            return createdProduct;

        }
        public async Task<IEnumerable<Product>> Get(Guid id, string category)
        {
            var parameters = new Dictionary<string, object>
                {
                    { "id", id == Guid.Empty ? null : id },
                    { "category", category == null || category == "" ? null : category }
                };
            return await _connection.QueryListAsync<Product>(StoredProcedures.GetProduct, parameters);
        }
        public async Task<Product> Update(Product product)
        {

            var createdProduct = await SaveProduct(product);
            return createdProduct;

        }
        public async Task<Product> Delete(Guid id)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", id}
            };
            var deletedProduct = await _connection.QueryAsync<Product>(StoredProcedures.DeleteProduct, parameters);
            return deletedProduct;

        }
        private Task<Product> SaveProduct(Product product)
        {

            var parameters = new Dictionary<string, object>
            {
                { "id", product.ID == Guid.Empty ? Guid.NewGuid() : product.ID},
                { "title", product.Title},
                { "category", product.Category},
                { "price", product.Price},
                { "quantity", product.Quantity},
                { "metadata", product.Metadata},
            };
            return _connection.QueryAsync<Product>(StoredProcedures.SaveProduct, parameters);
        }
    }
}
