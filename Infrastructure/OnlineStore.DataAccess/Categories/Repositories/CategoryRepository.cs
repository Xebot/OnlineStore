using OnlineStore.AppServices.Categories.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Categories.Repositories
{
    public sealed class CategoryRepository : EfRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(
            MutableOnlineStoreDbContext mutableDbContext, 
            ReadonlyOnlineStoreDbContext readOnlyDbContext) 
            : base(mutableDbContext, readOnlyDbContext)
        {
        }
    }
}
