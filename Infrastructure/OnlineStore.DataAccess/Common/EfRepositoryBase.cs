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
        public async Task AddAsync(T entity, CancellationToken cancellation)
        {
            await MutableDbContext.AddAsync(entity, cancellation);
            await MutableDbContext.SaveChangesAsync(cancellation);
        }

        /// <inheritdoc/>
        public virtual Task<List<T>> GetAllAsync(CancellationToken cancellation)
        {
            return ReadOnlyDbContext.Set<T>().ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public virtual Task<T> GetAsync(int id)
        {
            return MutableDbContext.FindAsync<T>(id).AsTask();
        }

        /// <inheritdoc/>
        public Task UpdateAsync(T entity, CancellationToken cancellation)
        {
            MutableDbContext.Update(entity);
            return MutableDbContext.SaveChangesAsync(cancellation);
        }
    }
}
