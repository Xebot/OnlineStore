using OnlineStore.AppServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<T> GetAsync(int id)
        {
            return await DbContext.FindAsync<T>(id);
        }
    }
}
