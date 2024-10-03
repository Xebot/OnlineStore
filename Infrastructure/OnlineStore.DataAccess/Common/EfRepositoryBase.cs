using Microsoft.EntityFrameworkCore;
using OnlineStore.AppServices.Common;

namespace OnlineStore.DataAccess.Common
{
    /// <summary>
    /// Базовый репозиторий для работы с БД через EF.
    /// </summary>
    public class EfRepositoryBase<T> : IRepository<T> where T : class
    {
        public readonly MutableOnlineStoreDbContext MutableDbContext;
        public readonly ReadonlyOnlineStoreDbContext ReadOnlyDbContext;

        /// <inheritdoc/>
        public EfRepositoryBase(
            MutableOnlineStoreDbContext mutableDbContext, 
            ReadonlyOnlineStoreDbContext readOnlyDbContext)
        {
            MutableDbContext = mutableDbContext;
            ReadOnlyDbContext = readOnlyDbContext;
        }

        /// <inheritdoc/>
        public async Task AddAsync(T entity)
        {
            await MutableDbContext.AddAsync(entity);
            await MutableDbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public virtual Task<List<T>> GetAllAsync()
        {
            return ReadOnlyDbContext.Set<T>().ToListAsync();
        }

        /// <inheritdoc/>
        public virtual Task<T> GetAsync(int id)
        {
            return MutableDbContext.FindAsync<T>(id).AsTask();
        }
    }
}
