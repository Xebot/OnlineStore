using AutoMapper;
using OnlineStore.Contracts.ProductAttributes;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Mappings
{
    public class ProductAttributeMappingProfile : Profile
    {
        public ProductAttributeMappingProfile()
        {
            CreateMap<ProductAttribute, ProductAttributeDto>()
                .ForMember(dest => dest.FullAttributeName, o => o.MapFrom(src => src.Name));
        }
    }
}
