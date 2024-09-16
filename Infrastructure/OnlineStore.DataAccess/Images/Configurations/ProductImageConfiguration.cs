using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Images.Configurations
{
    public sealed class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasMaxLength(1000)
                .IsRequired(true);

            builder.Property(t => t.Url)
                .HasMaxLength(2048)
                .IsRequired(true);

            builder.HasOne(t => t.Product)
                .WithMany(t => t.Images)
                .HasForeignKey(t => t.ProductId)
                .IsRequired();
        }
    }
}
