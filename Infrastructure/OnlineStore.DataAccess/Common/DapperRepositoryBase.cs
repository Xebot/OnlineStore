using OnlineStore.AppServices.Common;
using Dapper.Contrib.Extensions;
using Dapper;
using Npgsql;
using System.Data;

namespace OnlineStore.DataAccess.Common
{
    /// <summary>
    /// Базовый репозиторий по работе с Dapper.
    /// </summary>
    public abstract class DapperRepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DapperDbContext _context;

        /// <inheritdoc/>
        protected DapperRepositoryBase(DapperDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public Task AddAsync(T entity)
        {
            return _context.Connection.InsertAsync(entity);
        }

        /// <inheritdoc/>
        public Task<T> GetAsync(int id)
        {
            return _context.Connection.GetAsync<T>(id);
        }
    }
}