using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Contracts.Enums;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Configurations
{
    public sealed class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("attributes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.Name)
                .HasColumnName("name")
                .IsRequired(true);

            builder.HasData(new ProductAttribute
            {
                Id = 1,
                Name = "Color2"
            },
            new ProductAttribute
            {
                Id = 2,
                Name = "NumberOfStrings"
            });
        }
    }
}
