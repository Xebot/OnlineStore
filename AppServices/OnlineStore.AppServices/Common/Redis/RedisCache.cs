using StackExchange.Redis;

namespace OnlineStore.AppServices.Common.Redis
{
    /// <summary>
    /// Сервис по работе с Redis.
    /// </summary>
    public sealed class RedisCache : IRedisCache
    {
        private readonly IDatabase _redisDb; 

        /// <inheritdoc/>
        public RedisCache(IDatabase redisDb)
        {
            _redisDb = redisDb;
        }

        /// <inheritdoc/>
        public async Task<string> GetAsync(string key)
        {
            var result = await _redisDb.StringGetAsync(key);
            return result;
        }

        /// <inheritdoc/>
        public Task SetStringAsync(string key, string value)
        {
            return _redisDb.StringSetAsync(key, value);
        }
    }
}
