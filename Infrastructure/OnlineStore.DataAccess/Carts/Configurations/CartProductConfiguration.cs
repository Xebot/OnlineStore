using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DataAccess.Carts.Configurations
{
    public sealed class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.ToTable("cartproduct");

            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Cart)
                .WithMany()
                .HasForeignKey(t => t.CartId)
                .IsRequired(true);

            builder.HasOne(t => t.Product)
                .WithMany()
                .HasForeignKey(t => t.ProductId)
                .IsRequired(true);
        }
    }
}
