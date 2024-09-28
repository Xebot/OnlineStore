using AutoMapper;
using OnlineStore.AppServices.Common.CacheService;
using OnlineStore.Contracts.ProductAttributes;

namespace OnlineStore.AppServices.Attributes.Services
{
    /// <summary>
    /// Кэширующий декоратор для сервиса атрибутов продуктов.
    /// </summary>
    public sealed class CachedProductAttributeService : IProductAttributeService
    {
        private readonly IProductAttributeService _productAtributeService;
        private readonly ICacheService _cacheService;

        /// <inheritdoc/>
        public CachedProductAttributeService(
            IProductAttributeService productAtributeService,
            ICacheService cacheService)
        {
            _productAtributeService = productAtributeService;
            _cacheService = cacheService;
        }

        /// <inheritdoc/>
        public Task<ProductAttributeDto> GetAsync(int id)
        {
            return _cacheService.GetOrSetAsync(
                key: $"ProductAttributes_{id}",
                lifeTime: TimeSpan.FromMinutes(10),
                func: async () => (await _productAtributeService.GetAsync(id)),
                cancellation: CancellationToken.None);
        }
    }
}
