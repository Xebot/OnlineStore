using System;
using System.Collections.Generic;
using System.Linq;
namespace OnlineStore.AppServices.Common
{
    /// <summary>
    /// Интерфейс общего репозитория.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получает сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Добавляет сущность в БД.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        Task AddAsync(T entity);
    }
}
