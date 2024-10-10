using OnlineStore.AppServices.Common;
using OnlineStore.AppServices.Products.Models;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Products.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsAsync(GetProductsRequest request, CancellationToken cancellation);

        Task<int> GetProductsTotalCountAsync(CancellationToken cancellation);
    }
}
