namespace OnlineStore.AppServices.Common.Redis
{
    /// <summary>
    /// Интерфейс по работе с RedisCache.
    /// </summary>
    public interface IRedisCache
    {
        /// <summary>
        /// Получает данные из Redis по ключу.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task<T> GetAsync<T>(string key, CancellationToken cancellation);

        /// <summary>
        /// Записывает данные в Redis по указанному ключу.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="value">Данные.</param>
        /// <param name="lifeTime">Время жизни значения в кэше.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task SetAsync<T>(string key, T value, TimeSpan lifeTime, CancellationToken cancellation);

        /// <summary>
        /// Удаляет значение из кэша.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="cancellation">Токен отмены операции.</param>
        Task RemoveAsync(string key, CancellationToken cancellation);
    }
}
