namespace OnlineStore.AppServices.Common.CacheService
{
    /// <summary>
    /// Интерфейс сервиса кэширования.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Получает данные из кэша или добавляет их туда в случае отсутствия.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="lifeTime">Время хранения.</param>
        /// <param name="func">Делегат получения результата.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task<T> GetOrSetAsync<T>(string key, TimeSpan lifeTime, Func<Task<T>> func, CancellationToken cancellation);

        /// <summary>
        /// Удаляет данные из кэша.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="cancelattion">Токен отмены операции.</param>
        Task RemoveAsync(string key, CancellationToken cancelattion);
    }
}
