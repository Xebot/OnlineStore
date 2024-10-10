using AutoMapper;
using OnlineStore.AppServices.Attributes.Repositories;
using OnlineStore.AppServices.Common.CacheService;
using OnlineStore.Contracts.ProductAttributes;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Attributes.Services
{
    /// <summary>
    /// Сервис по работе с атрибутами продукта.
    /// </summary>
    public sealed class ProductAttributeService : IProductAttributeService
    {
        private readonly IAttributesRepository _attributesRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        /// <inheritdoc/>
        public ProductAttributeService(
            IAttributesRepository attributesRepository,
            IMapper mapper,
            ICacheService cacheService)
        {
            _attributesRepository = attributesRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        /// <inheritdoc/>
        public async Task<ProductAttributeDto> GetAsync(int id)
        {
            var attribute = await _attributesRepository.GetAsync(id);

            var dto = _mapper.Map<ProductAttributeDto>(attribute);

            return dto;
        }
    }
}
