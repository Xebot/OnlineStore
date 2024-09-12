using Microsoft.EntityFrameworkCore;
using OnlineStore.AppServices.Products.Models;
using OnlineStore.AppServices.Products.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Products.Repositories
{
    public sealed class ProductRepository : EfRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(OnlineStoreDbContext dbContext) : base(dbContext)
        {
        }

        /// <inheritdoc/>
        public async override Task<List<Product>> GetAllAsync()
        {
            return await DbContext.Set<Product>()
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsAsync(GetProductsRequest request)
        {
            var query = DbContext
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
