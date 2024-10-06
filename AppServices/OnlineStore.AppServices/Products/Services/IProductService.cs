using OnlineStore.Contracts.Products;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Products.Services
{
    public interface IProductService
    {
        Task<ShortProductList> GetProductsAsync(CancellationToken cancellationToken);

        Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellationToken);
    }
}
