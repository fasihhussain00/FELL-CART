using CartAPI.Application.IRepo;
using CartAPI.Database;
using CartAPI.Domain.Model;
using CartAPI.Domain.Queries;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPI.Infrastructure.Repo
{
    public class CartRepo : ICartRepo
    {
        private readonly DefaultDBManager _connection;

        public CartRepo(DefaultDBManager connection)
        {
            _connection = connection;
        }
        public async Task<Cart> Create(Cart cart)
        {

            var createdCart = await SaveCartWithDetails(cart);
            createdCart.LineItems = (await GetCartDetail(createdCart.ID)).ToList();
            return createdCart;

        }
        public async Task<Cart> Update(Cart cart)
        {
            var updatedCart = await SaveCartWithDetails(cart);
            updatedCart.LineItems = (await GetCartDetail(updatedCart.ID)).ToList();
            return updatedCart;
        }
        public async Task<IEnumerable<Cart>> Get(Guid? cartid = null, Guid? customerid = null)
        {
            var parameters = new Dictionary<string, object>
            {
                { "cartid", cartid == Guid.Empty ? null : cartid},
                { "customerid", customerid == Guid.Empty ? null : customerid},
            };

            var carts = await _connection.QueryListAsync<Cart, Guid, Cart>(sql: StoredProcedures.GetCart, parameters: parameters, SplitOn: "customerid", func: (cart, customerid) =>
            {
                cart.Customer ??= new Customer { ID = customerid };
                return cart;
            });

            foreach (var cart in carts)
            {
                cart.LineItems = (await GetCartDetail(cartid)).ToList();
            }
            return carts;
        }
        public async Task<Cart> Delete(Guid? cartid)
        {
            var parameters = new Dictionary<string, object>
            {
                { "cartid", cartid == Guid.Empty ? null : cartid},
            };
            var cart = await _connection.QueryAsync<Cart>(StoredProcedures.DeleteCart, parameters);
            return cart;

        }
        private Task<IEnumerable<CartDetail>> GetCartDetail(Guid? cartid)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", null},
                { "cartid", cartid == Guid.Empty ? null : cartid},
            };
            return _connection.QueryListAsync<CartDetail, Guid, CartDetail>(sql: StoredProcedures.GetCartDetail, parameters: parameters, SplitOn: "productid", func: (cartdetail, productid) =>
            {
                cartdetail.Product ??= new Product { ID = productid };
                return cartdetail;
            });
        }
        private Task<Cart> SaveCartWithDetails(Cart cart)
        {
            cart.ID = cart.ID == Guid.Empty ? Guid.NewGuid() : cart.ID;

            var cartdetailTable = new DataTable();
            cartdetailTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("id", Type.GetType(typeof(Guid).ToString())),
                new DataColumn("cartid", Type.GetType(typeof(Guid).ToString())),
                new DataColumn("productid", Type.GetType(typeof(Guid).ToString())),
                new DataColumn("quantity", Type.GetType(typeof(double).ToString())),
            });

            foreach (var lineItems in cart.LineItems)
            {
                cartdetailTable.Rows.Add(new object[] { lineItems.ID == Guid.Empty ? Guid.NewGuid() : lineItems.ID, cart.ID, lineItems.Product.ID, lineItems.Quantity });
            }

            var parameters = new Dictionary<string, object>
            {
                { "cartid", cart.ID },
                { "customerid", cart.Customer.ID},
                { "cartdetails", cartdetailTable.AsTableValuedParameter()},
            };
            return _connection.QueryAsync<Cart>(StoredProcedures.SaveCart, parameters);
        }
    }
}
