using OnlineStore.Contracts.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Categories.Services
{
    public interface ICategoryService
    {
        Task<IReadOnlyCollection<CategoryDto>> GetCategoriesAsync(CancellationToken cancellation);
    }
}
