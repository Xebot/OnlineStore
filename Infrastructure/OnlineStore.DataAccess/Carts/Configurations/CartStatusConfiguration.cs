using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Contracts.Enums;
using OnlineStore.Domain.Entities;
using OnlineStore.Infrastructure.Extensions;

namespace OnlineStore.DataAccess.Carts.Configurations
{
    public sealed class CartStatusConfiguration : IEntityTypeConfiguration<CartStatus>
    {
        public void Configure(EntityTypeBuilder<CartStatus> builder)
        {
            builder.ToTable("CartStatus");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasData(
                Enum.GetValues(typeof(CartStatusEnum))
                .Cast<CartStatusEnum>()
                .Select(e => new CartStatus
                {
                    Id = (int)e,
                    Name = e.GetEnumDescription()
                }));
        }
    }
}
