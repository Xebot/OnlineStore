using Microsoft.AspNetCore.Http;
using OnlineStore.AppServices.Products.Models;
using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Products;
using OnlineStore.Domain.Entities;

namespace OnlineStore.AppServices.Products.Services
{
    public interface IProductService
    {
        Task<ProductsListDto> GetProductsAsync(PagedRequest request, CancellationToken cancellationToken);

        Task AddProductAsync(ShortProductDto productDto, IFormFile imageFile, CancellationToken cancellationToken);
    }
}
