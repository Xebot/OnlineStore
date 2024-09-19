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
        }

        /// <inheritdoc/>
        public async virtual Task<List<T>> GetAllAsync()
        {
            return await ReadOnlyDbContext.Set<T>().ToListAsync();
        }

        /// <inheritdoc/>
        public async virtual Task<T> GetAsync(int id)
        {
            return await MutableDbContext.FindAsync<T>(id);
        }
    }
}
