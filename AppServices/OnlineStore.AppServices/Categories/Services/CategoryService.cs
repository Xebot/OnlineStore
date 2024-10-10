using AutoMapper;
using OnlineStore.AppServices.Categories.Repositories;
using OnlineStore.Contracts.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Categories.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<CategoryDto>> GetCategoriesAsync(CancellationToken cancellation)
        {
            var result = await _repository.GetAllAsync();

            return _mapper.Map<IReadOnlyCollection<CategoryDto>>(result);
        }
    }
}
