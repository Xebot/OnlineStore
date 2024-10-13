using Microsoft.EntityFrameworkCore;
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
        public async override Task<List<Product>> GetAllAsync(CancellationToken cancellation)
        {
            return await ReadOnlyDbContext.Set<Product>()
                .Include(x => x.Category)
                .ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<List<Product>> GetProductsAsync(GetProductsRequest request, CancellationToken cancellation)
        {
            var query = ReadOnlyDbContext
                .Set<Product>()
                .AsQueryable()
                .Where(p => !p.IsDeleted);

            if (request.IncludeCategory)
            {
                query = query
                    .Include(x => x.Category);
            }

            if (request.IncludeImages)
            {
                query = query
                    .Include(x => x.Images);
            }

            query = query
                .OrderBy(x => x.Id)
                .Skip(request.Skip)
                .Take(request.Take);

            return query.ToListAsync(cancellation);
        }

        public Task<int> GetProductsTotalCountAsync(CancellationToken cancellation)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Where(p => !p.IsDeleted)
                .CountAsync(cancellation);
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
