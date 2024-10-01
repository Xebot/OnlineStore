using AutoMapper;
using OnlineStore.Contracts.Products;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ShortProductDto, Product>();
        }
    }
}
