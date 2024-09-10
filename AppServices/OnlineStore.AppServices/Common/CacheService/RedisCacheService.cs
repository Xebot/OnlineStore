using OnlineStore.AppServices.Common.Redis;

namespace OnlineStore.AppServices.Common.CacheService
{
    /// <summary>
    /// Сервис кэширования в рэдис.
    /// </summary>
    public sealed class RedisCacheService : ICacheService
    {
        private readonly IRedisCache _rediCacheService;

        /// <inheritdoc/>
        public RedisCacheService(IRedisCache rediCacheService)
        {
            _rediCacheService = rediCacheService;
        }

        /// <inheritdoc/>
        public async Task<T> GetOrSetAsync<T>(string key, TimeSpan lifeTime, Func<Task<T>> func, CancellationToken cancellation)
        {
            var cachedItem = await _rediCacheService.GetAsync<T>(key, cancellation);

            if (cachedItem != null)
            {
                return cachedItem;
            }

            var item = await func();

            if (item != null)
            {
                await _rediCacheService.SetAsync(key, item, lifeTime, cancellation);
            }

            return item;
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(string key, CancellationToken cancelattion)
        {
            await _rediCacheService.RemoveAsync(key, cancelattion);
        }
    }
}
