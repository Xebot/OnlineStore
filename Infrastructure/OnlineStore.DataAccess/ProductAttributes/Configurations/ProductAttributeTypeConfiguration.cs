using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Contracts.Enums;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Extensions;

namespace OnlineStore.DataAccess.Attributes.Configurations
{
    public sealed class ProductAttributeTypeConfiguration : IEntityTypeConfiguration<ProductAttributeType>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeType> builder)
        {
            builder.ToTable("productattributetype");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired(true);

            builder.HasData(
                Enum.GetValues(typeof(ProductAttributeTypeEnum))
                .Cast<ProductAttributeTypeEnum>()
                .Select(e => new ProductAttributeType
                {
                    Id = (int)e,
                    Name = e.GetEnumDescription()
                }));
        }
    }
}
