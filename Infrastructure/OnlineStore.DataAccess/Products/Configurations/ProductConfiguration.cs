using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Products.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasMaxLength(1000)
                .IsRequired(true);

            builder.Property(t => t.Description)
                .IsRequired(true);

            builder.Property(t => t.Price)
                .HasPrecision(14,4)
                .IsRequired(true);

            builder.HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .IsRequired(false);

            builder.Property(t => t.ImageUrl)
                .IsRequired(false);

            builder.Property(t => t.StockQuantity)
                .IsRequired (true);

            builder.Property(t => t.CreatedAt)
                .IsRequired(true);

            builder.Property(t => t.UpdatedAt)
                .IsRequired(false);

            builder.HasMany(t => t.Images)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
