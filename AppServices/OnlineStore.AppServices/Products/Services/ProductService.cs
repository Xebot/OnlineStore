using OnlineStore.AppServices.Products.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Products.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(
            IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var result = await _repository.GetProductsAsync(new Models.GetProductsRequest
            {
                IncludeCategory = true,
            });

            return result;
        }
    }
}
