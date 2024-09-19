﻿using Microsoft.EntityFrameworkCore;
using OnlineStore.AppServices.Products.Models;
using OnlineStore.AppServices.Products.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Products.Repositories
{
    /// <summary>
    /// Репозиторий по работе с товарами.
    /// </summary>
    public sealed class ProductRepository : EfRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(MutableOnlineStoreDbContext mutableDbContext, 
            ReadonlyOnlineStoreDbContext readOnlyDbContext) 
            : base(mutableDbContext, readOnlyDbContext)
        {
        }

        /// <inheritdoc/>
        public async override Task<List<Product>> GetAllAsync()
        {
            return await ReadOnlyDbContext.Set<Product>()
                .Include(x => x.Category)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Product>> GetProductsAsync(GetProductsRequest request)
        {
            var query = ReadOnlyDbContext
                .Set<Product>()
                .AsQueryable();

            if (request.IncludeCategory)
            {
                query = query
                    .Include(x => x.Category);
            }

            //if (request.IncludeImages)
            //{
            //    query = query
            //        .Include(x => x.Images);
            //}

            query = query.Where(x => x.Id > 10);

            var products = await query.ToListAsync();

            foreach (var product in products)
            {
                product.Price = 10;
            }


            return products;
        }

        //public async override Task<Product> GetAsync(int id)
        //{
        //    var entity = await DbContext.FindAsync<Product>(id);

        //    await DbContext.Entry(entity)
        //        .Reference(x => x.Category)
        //        .LoadAsync();

        //    return entity;
        //}
    }
}
