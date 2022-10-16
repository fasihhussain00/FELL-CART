﻿using CartAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartAPI.Application.IRepo
{
    public interface IProductRepo
    {
        Task<Product> Create(Product product);
        Task<Product> Delete(Guid id);
        Task<IEnumerable<Product>> Get(Guid id, string category);
        Task<Product> Update(Product product);
    }
}
