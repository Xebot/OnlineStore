using Microsoft.EntityFrameworkCore;
using OnlineStore.AppServices.Images.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DataAccess.Images.Repositories
{
    public sealed class ImageRepository : EfRepositoryBase<ProductImage>, IImageRepository
    {
        public ImageRepository(MutableOnlineStoreDbContext mutableDbContext, 
            ReadonlyOnlineStoreDbContext readOnlyDbContext) 
            : base(mutableDbContext, readOnlyDbContext)
        {
        }

        public Task<ProductImage?> GetByUrlAsync(string url, CancellationToken cancellation)
        {
            return MutableDbContext.Set<ProductImage>().FirstOrDefaultAsync(i => i.Url == url, cancellation);
        }

        public async Task<int> SaveAsync(ProductImage image, CancellationToken cancellation)
        {
            await MutableDbContext.AddAsync(image, cancellation);
            await MutableDbContext.SaveChangesAsync(cancellation);

            return image.Id;
        }
    }
}
