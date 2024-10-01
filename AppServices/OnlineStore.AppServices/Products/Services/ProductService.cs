using AutoMapper;
using OnlineStore.AppServices.Products.Repositories;
using OnlineStore.Contracts.Products;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Products.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellationToken)
        {
            var domainProduct = _mapper.Map<Product>(productDto);

            return _repository.AddAsync(domainProduct);
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _repository.GetProductsAsync(new Models.GetProductsRequest
            {
                IncludeCategory = true,
            });
        }
    }
}
