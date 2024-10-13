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
        /// <param name="cancellation">Токен отмены операции.</param>
        Task AddAsync(T entity, CancellationToken cancellation);

        /// <summary>
        /// Получает все записи.
        /// </summary>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task<List<T>> GetAllAsync(CancellationToken cancellation);

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task UpdateAsync(T entity, CancellationToken cancellation);
    }
}
