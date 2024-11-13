using StackExchange.Redis.Extensions.Core.Abstractions;

namespace OnlineStore.AppServices.Common.Redis
{
    /// <summary>
    /// Сервис по работе с Redis.
    /// </summary>
    public sealed class RedisCache : IRedisCache
    {
        private readonly IRedisDatabase _redisDb; 

        /// <inheritdoc/>
        public RedisCache(IRedisDatabase redisDb)
        {
            _redisDb = redisDb;
        }

        /// <inheritdoc/>
        public Task<T> GetAsync<T>(string key, CancellationToken cancellation)
        {
            return _redisDb.GetAsync<T>(key);
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(string key, CancellationToken cancellation)
        {
            await _redisDb.RemoveAsync(key);
        }

        /// <inheritdoc/>
        public Task SetAsync<T>(string key, T value, TimeSpan lifeTime, CancellationToken cancellation)
        {
            return _redisDb.AddAsync<T>(key, value, lifeTime);
        }
    }
}
