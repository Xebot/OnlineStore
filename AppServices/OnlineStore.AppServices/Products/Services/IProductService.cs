﻿using OnlineStore.AppServices.Products.Models;
using OnlineStore.Contracts.Products;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Products.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(GetProductsRequest request);

        Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellationToken);

        bool IsBussinessDay();
    }
}
