using OnlineStore.AppServices.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Images.Repositories
{
    public interface IImageRepository : IRepository<ProductImage>
    {
        public Task<int> SaveAsync(ProductImage image, CancellationToken cancellation);
    }
}
