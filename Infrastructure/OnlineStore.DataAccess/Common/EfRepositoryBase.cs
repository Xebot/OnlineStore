using Microsoft.EntityFrameworkCore;
using OnlineStore.AppServices.Common;

namespace OnlineStore.DataAccess.Common
{
    /// <summary>
    /// Базовый репозиторий для работы с БД через EF.
    /// </summary>
    public class EfRepositoryBase<T> : IRepository<T> where T : class
    {
        public readonly OnlineStoreDbContext DbContext;

        /// <inheritdoc/>
        public EfRepositoryBase(OnlineStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task AddAsync(T entity)
        {
            await DbContext.AddAsync(entity);
        }

        /// <inheritdoc/>
        public async virtual Task<List<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        /// <inheritdoc/>
        public async virtual Task<T> GetAsync(int id)
        {
            return await DbContext.FindAsync<T>(id);
        }
    }
}
