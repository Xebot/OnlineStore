using Microsoft.EntityFrameworkCore;
using OnlineStore.AppServices.Asparagus;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Asparagus.Repositories
{
    public sealed class AsparagusLoverRepository : EfRepositoryBase<AsparagusLover>, IAsparagusLoverRepository
    {
        public AsparagusLoverRepository(MutableOnlineStoreDbContext mutableDbContext, 
            ReadonlyOnlineStoreDbContext readOnlyDbContext) : base(mutableDbContext, readOnlyDbContext)
        {
        }

        public async Task AddOrUpdateAsync(AsparagusLover entity)
        {
            if (entity.Id <= 0)
            {
                await MutableDbContext.AddAsync(entity);
            }
            else
            {
                MutableDbContext.Update(entity);
            }

            MutableDbContext.SaveChanges();
        }

        public Task<AsparagusLover> GetByEmailAsync(string email)
        {
            return ReadOnlyDbContext.Set<AsparagusLover>().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
